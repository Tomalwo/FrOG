using System.Collections.Generic;

namespace FrOG
{
    internal class OptimizationResult
    {
        public readonly double Value;
        public readonly IList<decimal> Parameters;
        public readonly int Iterations;
        public readonly ResultType Type;

        public OptimizationResult(double value, IList<decimal> parameters, int iterations, ResultType type)
        {
            Value = value;
            Parameters = parameters;
            Type = type;
            Iterations = iterations;
        }

        public enum ResultType
        {
            Unknown,
            UserStopped,
            SolverStopped,
            FrogStopped,
            NoImprovement,
            MaximumEvals,
            MaximumTime
        }
    }
}
