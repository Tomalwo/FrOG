using System;
using System.Collections.Generic;
using System.Linq;

namespace FrOG
{
    internal static class SolverList
    {
        public static List<ISolver> GetSolverList = new List<ISolver>() { new HillclimberInterface() };

        public static List<string> PresetNames
        {
            get
            {
                var presetNames = new List<string>();

                foreach (var solver in GetSolverList) presetNames.AddRange(solver.GetPresetNames());
                          
                return presetNames;
            }
        }

        private static List<ISolver> PresetSolvers
        {
            get
            {
                var solvers = new List<ISolver>();

                foreach (var solver in GetSolverList) solvers.AddRange(solver.GetPresetNames().Select(preset => solver));

                return solvers;
            }
        }

        public static string GetPresetByIndex(int index)
        {
                return PresetNames[index];
        }

        public static ISolver GetSolverByIndex(int index)
        {
            return PresetSolvers[index];
        }
    }

    public struct Variable
    {
        public readonly decimal LowerB;
        public readonly decimal UpperB;
        public readonly bool Integer;

        public Variable(decimal lowerB, decimal upperB, bool integer)
        {
            LowerB = lowerB;
            UpperB = upperB;
            Integer = integer;
        }

        public override string ToString()
        {
            return String.Format("LowerB {0} UpperB {1} Integer {2}", LowerB, UpperB, Integer);
        }
    }

    internal interface ISolver
    {
        //"variables" define the variables

        //Use "evaluate" for function evaluations.
        //Important: 
        //When "evaluate" returns Nan, stop the solver.
        //When the solver stops, call "evaluate" with null.

        //"preset" defines which settings to use. For RBFOpt, these presets are stored as a dictonary.

        //Return False when the solver cannot be initalised.
        //Return True after the solver stopped.

        //Note:
        //This is where most of the work is done.
        //The constructor should do only minimal work, since all solvers are instanced when the FrOG window loads.

        bool RunSolver(List<Variable> variables, Func<IList<decimal>,double> evaluate, string preset, string expertsettings, string installFolder, string documentPath);

        //Return eventual error messages to show in MessageBox after the solver stopped, otherwise return empty.
        string GetErrorMessage();

        //Return a list of strings with names for the available presets of settings
        //(There should be at least one.)
        IEnumerable<string> GetPresetNames();
    }
}
