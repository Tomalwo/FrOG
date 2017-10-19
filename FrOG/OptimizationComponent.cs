using System;
using System.Windows.Forms;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;

namespace FrOG
{
    public class OptimizationComponent : GH_Component
    {
        #region Variables
        private OptimizationWindow _optimizationWindow;

        public override Guid ComponentGuid 
        {
            get { return new Guid("{9975d89e-9b54-4de5-95ac-d672f3998f69}"); }
        }
        #endregion

        #region Constructor
        public OptimizationComponent() : base("FrOG 0.1", "Framework for Optimization in Grasshopper", "FrOG provides an interface for the easy implementation of black-box optimization algorithms in Grasshopper.\n\nBlack-box algorithms optimize problems soley based on inputs and outputs.", "Params", "Util")
        {
            NewInstanceGuid();
            _optimizationWindow = null;
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Variables", "Variables", "The value of the optimization problem's objective function depends on variables, the unknowns. Connect an arbitrary number of number sliders and gene pools as the variables of the optimization problem.\n\nThe number sliders' bounds are the bounds of the variables. After optimization, the number sliders (variables) are set to the optimal position.\n\nIn Galapagos, the variables are called genes.", GH_ParamAccess.list);
            pManager.AddNumberParameter("Objective", "Objective", "The objective value depends on the values of the variables. The algorithm searches for the objective's optimal value by changing the variables' values.\n\nThe objective value should be a single Number Parameter that yields a single value for any configuration of the variables.\n\nIn Galapagos, the objective value is called fitness.", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
        }
        #endregion

        #region Attributes
        public override void CreateAttributes()
        {
            m_attributes = new AttributesA(this);
        }

        private class AttributesA : GH_ComponentAttributes
        {
            public AttributesA(IGH_Component component) : base(component)
            {
            }

            public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
            {
                ((OptimizationComponent)Owner).ShowOptimizationWindow();
                return GH_ObjectResponse.Handled;
            }
        }

        public void ShowOptimizationWindow()
        {
            if (SolverList.PresetNames.Count == 0)
            {
                MessageBox.Show("No solvers found!", "FrOG Error");
                return;
            }

            var owner = Grasshopper.Instances.DocumentEditor;

            if (_optimizationWindow == null || _optimizationWindow.IsDisposed)
            {
                _optimizationWindow = new OptimizationWindow(this) { StartPosition = FormStartPosition.Manual };

                GH_WindowsFormUtil.CenterFormOnWindow(_optimizationWindow, owner, true);
                owner.FormShepard.RegisterForm(_optimizationWindow);

            }
            _optimizationWindow.Show(owner);
        }

        protected override void SolveInstance(IGH_DataAccess da)
        {
            //da.SetData(0, this);
        }
        #endregion

    }
}

