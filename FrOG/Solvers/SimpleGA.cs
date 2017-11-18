using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

/*
 * Opt_SGA.cs
 * Copyright 2017 Christoph Waibel <chwaibel@student.ethz.ch>
 * 
 * This work is licensed under the GNU GPL license version 3 or later.
*/

namespace FrOG.Solvers
{
    public class SimpleGA : ISolver
    {
        /// <summary>
        /// Variable vector of final solution.
        /// </summary>
        public double[] Xopt { get; private set; }
        /// <summary>
        /// Cost of final solution.
        /// </summary>
        public double Fxopt { get; private set; }

        //public Dictionary<string, string> settings = new Dictionary<string, string>();

        private readonly Dictionary<string, Dictionary<string, double>> _presets = new Dictionary<string, Dictionary<string, double>>();

        public SimpleGA()
        {
            var SGA_Settings_WW = new Dictionary<string, double>{
                {"maxgen", 2000},
                { "itermax", 10000},
                { "seed", 1},
                { "popsize", 14},
                { "k", 11},
                { "pcross", 1},
                { "pmut", 0.2},
                { "d", 0.01},
                { "r", 0.2},
                { "elite", 1}
            };

            var SGA_Settings_n4_A = new Dictionary<string, double>{
                {"maxgen", 2000},
                { "itermax", 10000},
                { "seed", 1},
                { "popsize", 14},
                { "k", 21.93823},
                { "pcross", 0.99301},
                { "pmut", 1},
                { "d", 0.01159},
                { "r", 0.15604},
                { "elite", 2}
            };

            var SGA_Settings_n4_B = new Dictionary<string, double>{
                {"maxgen", 2000},
                { "itermax", 10000},
                { "seed", 1},
                { "popsize", 6},
                { "k", 36.7352},
                { "pcross", 0.87624},
                { "pmut", 0.8189},
                { "d", 0.63072},
                { "r", 1.75361},
                { "elite", 1}
            };

            var SGA_Settings_n4_C = new Dictionary<string, double>{
                {"maxgen", 2000},
                { "itermax", 10000},
                { "seed", 1},
                { "popsize", 10},
                { "k", 2.24439},
                { "pcross", 0.97974},
                { "pmut", 1},
                { "d", 1.74528},
                { "r", 0.01},
                { "elite", 1}
            };

            _presets.Add("SGA_WW", SGA_Settings_WW);
            _presets.Add("SGA_n4_A", SGA_Settings_n4_A);
            _presets.Add("SGA_n4_B", SGA_Settings_n4_B);
            _presets.Add("SGA_n4_C", SGA_Settings_n4_C);
        }

        public bool RunSolver(List<Variable> variables, Func<IList<decimal>, double> evaluate, string preset, string expertsettings, string installFolder, string documentPath)
        {
            var settings = _presets[preset];

            //System.Windows.Forms.MessageBox.Show(expertsettings);     //use expertsettings to input custom solver parameters

            int? seedin = null;
            //string [] expsets = expertsettings.Split(';');
            //foreach (string strexp in expsets)
            //{
            //    string[] stre = strexp.Split('=');
            //    if(string.Equals(stre[0],"seed"))
            //    {
            //        seedin = Convert.ToInt16(stre[1]);
            //    }
            //}

            var random = new Random();
            seedin = random.Next(-32768, 32767);

            var dvar = variables.Count;
            var lb = new double[dvar];
            var ub = new double[dvar];
            var integer = new bool[dvar];

            for (var i = 0; i < dvar; i++)
            {
                lb[i] = Convert.ToDouble(variables[i].LowerB);
                ub[i] = Convert.ToDouble(variables[i].UpperB);
                integer[i] = variables[i].Integer;
            }

            Func<double[], double> eval = x =>
            {
                var decis = x.Select(Convert.ToDecimal).ToList();
                return evaluate(decis);
            };

            try
            {
                if (preset.Equals("SGA_n4_A") || preset.Equals("SGA_n4_B") || preset.Equals("SGA_n4_C") || preset.Equals("SGA_WW"))
                {
                    Dictionary<string, object> GAsettings = new Dictionary<string, object>();
                    GAsettings.Add("maxgen", (int)settings["maxgen"]);
                    GAsettings.Add("popsize", (int)settings["popsize"]);
                    GAsettings.Add("k", settings["k"]);
                    GAsettings.Add("pcross", settings["pcross"]);
                    GAsettings.Add("pmut", settings["pmut"]);
                    GAsettings.Add("d", settings["d"]);
                    GAsettings.Add("r", settings["r"]);
                    GAsettings.Add("elite", settings["elite"]);
                    int seed;

                    if (seedin != null)
                        seed = Convert.ToInt16(seedin);
                    else 
                        seed = (int)settings["seed"];
                   
                    int itermax = (int)settings["itermax"];

                    var ga = new MetaheuristicsLibrary.SolversSO.SimpleGA(lb, ub, integer, itermax, eval, seed, GAsettings);
                    ga.solve();
                    Xopt = ga.get_Xoptimum();
                    Fxopt = ga.get_fxoptimum();
                }
                else
                {
                    var seed = (int)settings["seed"];
                    var stepsize = settings["stepsize"];
                    var itermax = (int)settings["itermax"];
                    var hc = new HillclimberAlgorithm(lb, ub, stepsize, itermax, eval, seed);
                    hc.Solve();
                    Xopt = hc.get_Xoptimum();
                    Fxopt = hc.get_fxoptimum();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public string GetErrorMessage()
        {
            return "";
        }

        /// <summary>
        /// Get the variable vector of the final solution.
        /// </summary>
        /// <returns>Variable vector.</returns>
        public double[] get_Xoptimum()
        {
            return Xopt;
        }

        public IEnumerable<string> GetPresetNames()
        {
            return _presets.Keys;
        }
    }
}
