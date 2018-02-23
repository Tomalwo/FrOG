using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GalapagosComponents;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;

namespace FrOG
{
    public class GrasshopperInOut
    {
        private readonly GH_Document _doc;

        public List<GH_NumberSlider> Sliders { get; private set; }
        private List<GalapagosGeneListObject> _genepools;

        private readonly List<Guid> _inputGuids;
        private IGH_Param _output;

        public List<Variable> Variables { get; private set; }
        public int VariableN { get { return Variables.Count; } }

        public OptimizationComponent OptimizationComponent { get; private set; }
       

        //Variables as String
        public string VariablesStr
        {
            get
            {
                return Variables.Aggregate("", (current, variable) => current + variable.ToString() + " ");
            }
        }

        //Get Component Directory
        public string ComponentFolder { get; private set; }

        //Get Document Directory
        public string DocumentPath { get { return _doc.FilePath; } }

        //Get Document Name
        public string DocumentName { get { return _doc.DisplayName; } }

        public GrasshopperInOut(OptimizationComponent component)
        {
            OptimizationComponent = component;
            ComponentFolder = Path.GetDirectoryName(Grasshopper.Instances.ComponentServer.FindAssemblyByObject(OptimizationComponent).Location);

            _doc = OptimizationComponent.OnPingDocument();
            _inputGuids = new List<Guid>();
        }

        //Get Variables
        public bool SetInputs()
        {
            Sliders = new List<GH_NumberSlider>();
            _genepools = new List<GalapagosGeneListObject>();

            var s = new GH_NumberSlider();
            var g = new GalapagosGeneListObject();

            foreach (var source in OptimizationComponent.Params.Input[0].Sources)
            {
                var guid = source.InstanceGuid;
                _inputGuids.Add(guid);
            }

            if (_inputGuids.Count == 0)
            {
                MessageBox.Show("FrOG needs at least one variable input (defined as a number slider or gene pool, of type integer or float", "FrOG Error");
                return false;
            }

            foreach (var guid in _inputGuids)
            {
                var input = _doc.FindObject(guid, true);

                if (input == null)
                {
                    MessageBox.Show(
                        "The variables connected to FrOG are inconsistent. This error typically occurs after removing one or more connected number sliders. Please consider deleting and setting up all variables connections again.",
                        "FrOG Error");
                    return false;

                }

                if (input.ComponentGuid == s.ComponentGuid)
                {
                    var slider = (GH_NumberSlider)input;
                    Sliders.Add(slider);
                }

                if (input.ComponentGuid == g.ComponentGuid)
                {
                    var genepool = (GalapagosGeneListObject)input;
                    _genepools.Add(genepool);
                }
            }

            SetVariables();
            return true;
        }

        public bool SetOutput()
        {
            if (OptimizationComponent.Params.Input[1].Sources.Count != 1)
            {
                MessageBox.Show("FrOG needs exactly one objective value", "FrOG Error");
                return false;
            }

            _output = OptimizationComponent.Params.Input[1].Sources[0];
         
            if (_output == null)
            {
                return false;
            }

            return true;
        }

        //Get Variable String
        public void SetVariables()
        {
            var variables = new List<Variable>();

            //Sliders
            foreach (var slider in Sliders)
            {
                //Slider Type
                var min = slider.Slider.Minimum;
                var max = slider.Slider.Maximum;

                decimal lowerB;
                decimal upperB;
                bool integer;

                switch (slider.Slider.Type)
                {
                    case Grasshopper.GUI.Base.GH_SliderAccuracy.Even:
                        lowerB = min / 2;
                        upperB = max / 2;
                        integer = true;
                        break;
                    case Grasshopper.GUI.Base.GH_SliderAccuracy.Odd:
                        lowerB = (min - 1) / 2;
                        upperB = (max - 1) / 2;
                        integer = true;
                        break;
                    case Grasshopper.GUI.Base.GH_SliderAccuracy.Integer:
                        lowerB = min;
                        upperB = max;
                        integer = true;
                        break;
                    default:
                        lowerB = min;
                        upperB = max;
                        integer = false;
                        break;
                }

                variables.Add(new Variable(lowerB, upperB, integer));
            }
            //Genepools
            foreach (var genepool in _genepools)
            {
                var integer = genepool.Decimals == 0;
                var lowerB = genepool.Minimum;
                var upperB = genepool.Maximum;

                for (var i = 0; i < genepool.Count; i++)
                    variables.Add(new Variable(lowerB, upperB, integer));

            }

            Variables = variables;
        }

        //Check Variable Values
        private bool CheckVariableValues(IList<decimal> parameters)
        {       

            //Check Variable Number
            if (parameters.Count != VariableN)
            {
                MessageBox.Show(String.Format("Wrong number of parameters({0}): {1}" + Environment.NewLine + "Parameters: {2}", Sliders.Count, parameters.Count, VariableN), "FrOG Error");
                return false;
            }


            for (var i = 0; i < VariableN; i++)
                if (CheckVariableValue(parameters[i], Variables[i]) == false) return false;

            return true;
        }

        private static bool CheckVariableValue(decimal param, Variable variable)
        {
            var lowerB = variable.LowerB;
            var upperB = variable.UpperB;
            var integer = variable.Integer;

            //Check Integer
            if (integer && param % 1 != 0)
            {
                MessageBox.Show(String.Format("Wrong parameter type(int: {0})", param), "FrOG Error");
                return false;
            }
            //Check lower Bound
            if (param < lowerB)
            {
                MessageBox.Show(String.Format("Parameter is too small (lower Bound {0}: {1})", lowerB, param), "FrOG Error");
                return false;
            }
            //Check upper Bound
            if (param > upperB)
            {
                MessageBox.Show(String.Format("Parameter is too large (upper Bound {0}: {1})", upperB, param), "FrOG Error");
                return false;
            }

            return true;
        }

        //Get Variable Values
        public decimal[] GetSliderValues()
        {
            return Sliders.Select(slider => slider.CurrentValue).ToArray();
        }

        public double[] GetSliderValuesDouble()
        {
            return  Array.ConvertAll(GetSliderValues(), x => (double)x);
        }

        //Set Variable Values
        public bool SetSliderValues(IList<decimal> parameters)
        {
            if (!CheckVariableValues(parameters)) return false;

            var i = 0;

            //Sliders
            foreach (var slider in Sliders)
            {
                if (slider == null)
                {
                    MessageBox.Show("Slider is null", "FrOG Error");
                    return false;
                }
 
                decimal val;

                switch (slider.Slider.Type)
                {
                    case Grasshopper.GUI.Base.GH_SliderAccuracy.Even:
                        val = (int)parameters[i++] * 2;
                        break;
                    case Grasshopper.GUI.Base.GH_SliderAccuracy.Odd:
                        val = (int)(parameters[i++] * 2) + 1;
                        break;
                    case Grasshopper.GUI.Base.GH_SliderAccuracy.Integer:
                        val = (int)parameters[i++];
                        break;
                    case Grasshopper.GUI.Base.GH_SliderAccuracy.Float:
                        val = parameters[i++];
                        break;
                    default:
                        val = parameters[i++];
                        break;
                }

                //string strMessage = $"Setting slider {slider.InstanceGuid} to {val}" + Environment.NewLine;
                //strMessage += _doc.SolutionState + Environment.NewLine;
                //MessageBox.Show(strMessage);

                slider.Slider.RaiseEvents = false;
                slider.SetSliderValue(val);
                slider.ExpireSolution(false);
                slider.Slider.RaiseEvents = true;


                //strMessage = $"Set slider {slider} to {slider.CurrentValue}" + Environment.NewLine;
                //strMessage += _doc.SolutionState;
                //MessageBox.Show(strMessage);
            }

            //Genepools
            foreach (var genepool in _genepools)
                for (var j = 0; j < genepool.Count; j++)
                {
                    genepool.set_NormalisedValue(j, GetNormalizedGenepoolValue(parameters[i++], genepool));
                    genepool.ExpireSolution(false);
                }

            return true;
        }

        //Recalculate Grasshopper Solution
        public void Recalculate()
        {
            //Wait until the grasshopper solution in finished
            while (_doc.SolutionState != GH_ProcessStep.PreProcess || _doc.SolutionDepth != 0) { }

            //var strMessage = "Starting new solution" + Environment.NewLine;
            //MessageBox.Show(strMessage);

            _doc.ScheduleSolution(1);

            //strMessage = "Started new solution" + Environment.NewLine;
            //MessageBox.Show(strMessage);

            //Wait until the grasshopper solution in finished
            while (_doc.SolutionState != GH_ProcessStep.PostProcess || _doc.SolutionDepth != 0) { }

            //strMessage += "Finished solution" + Environment.NewLine;
            //MessageBox.Show(strMessage);
        }

        //New Solution
        public void NewSolution(IList<decimal> parameters)
        {
            //MessageBox.Show("Setting values!");
            SetSliderValues(parameters);
            //MessageBox.Show("Recalculating!");
            Recalculate();
        }

        //Get Objective Value
        public double GetObjectiveValue()
        {
            double objectiveValue;

            if (_output == null)
            {
                MessageBox.Show("No objective value found", "FrOG Error");
                return double.NaN;
            }

            if (_output.VolatileDataCount != 1)
            {
                MessageBox.Show("Please provide exactly one objective value, instead of " + _output.VolatileDataCount + " values.", "FrOG Error");
                return double.NaN;
            }

            var objectiveGoo = _output.VolatileData.AllData(false).First();

            if (objectiveGoo == null)
            {
                MessageBox.Show("Objective value is null", "FrOG Error");
                return double.NaN;
            }

            var bolCast = objectiveGoo.CastTo(out objectiveValue);

            if (bolCast) return objectiveValue;

            MessageBox.Show("Failed to cast objective value to double", "FrOG Error");
            return double.NaN;
        }

        //Genepool Helper Functions
        private static decimal GetNormalizedGenepoolValue(decimal unnormalized, GalapagosGeneListObject genepool)
        {
            return (unnormalized - genepool.Minimum) / (genepool.Maximum - genepool.Minimum);
        }
    }
}
