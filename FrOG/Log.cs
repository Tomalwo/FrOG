using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FrOG
{
    internal class Log
    {
        private readonly StreamWriter _log;

        public Log(string logPath)
        {
            _log = new StreamWriter(logPath, false);
        }

        private void LogNewLine(string line, bool bolFlush)
        {
            _log.WriteLine(line);
            if (bolFlush) _log.Flush();
        }

        public void LogSettings(string settings)
        {
            LogNewLine("Settings", false);
            LogNewLine(settings + Environment.NewLine, true);
        }

        public void LogIteration(int iteration)
        {
            LogNewLine(String.Format("Iteration {0}" + Environment.NewLine, iteration), true);
        }

        public void LogParameters(string parameters, Stopwatch stopwatchLoop)
        {
            LogNewLine(String.Format("Time to get parameters: {0}", stopwatchLoop.Elapsed), false);
            LogNewLine(String.Format("Parameters to evaluate: {0}", parameters), false);
        }

        public void LogFunctionValue(double value, Stopwatch stopwatchLoop)
        {
            LogNewLine(String.Format("Time to get function value: {0}", stopwatchLoop.Elapsed), false);
            LogNewLine("Function Value: " + value + Environment.NewLine, false);
        }

        public void LogCurrentBest(IList<decimal> minParams, double minValue, Stopwatch stopwatchTotal, int j)
        {
            LogNewLine(String.Format("Current Time: {0}", stopwatchTotal.Elapsed), false);
            LogNewLine(String.Format("Current Best Value: {0} ({1})", minValue, j), false);
            LogNewLine("Current Best Parameters: " + string.Join(",", minParams) + Environment.NewLine, true);
        }

        public void LogResult(OptimizationResult result, Stopwatch stopwatchTotal, int maxIter, int maxIterNoProgress, double maxDuration)
        {
            LogNewLine(GetResultString(result, maxIter, maxIterNoProgress, maxDuration) + Environment.NewLine, false);
            LogNewLine(String.Format("Total time: {0}", stopwatchTotal.Elapsed), false);
            LogNewLine(String.Format("Best Value: {0}", result.Value), false);
            LogNewLine("Best Parameters: " + string.Join(",", result.Parameters), true);

            _log.Close();
        }

        public static string GetResultString(OptimizationResult result, int maxIter, int maxIterNoProgress, double maxDuration)
        {
            string strResult;

            switch (result.Type)
            {
                case OptimizationResult.ResultType.UserStopped:
                    strResult = "Optimization stopped by User";
                    break;
                case OptimizationResult.ResultType.FrogStopped:
                    strResult = "Optimization stopped by FrOG";
                    break;
                case OptimizationResult.ResultType.SolverStopped:
                    strResult = String.Format("Optimization stopped by solver after {0} iterations", result.Iterations);
                    break;
                case OptimizationResult.ResultType.NoImprovement:
                    strResult = String.Format("No improvement after ({0}) iterations", maxIterNoProgress);
                    break;
                case OptimizationResult.ResultType.MaximumEvals:
                    strResult = String.Format("Maximum number of iterations ({0}) reached", maxIter);
                    break;
                case OptimizationResult.ResultType.MaximumTime:
                    strResult = String.Format("Maximum number of seconds ({0}) reached", maxDuration);
                    break;
                case OptimizationResult.ResultType.Unknown:
                    strResult = "Optimization stopped for an unknown reason";
                    break;
                default:
                    strResult = "Optimization stopped for an unknown reason";
                    break;
            }
            return strResult + Environment.NewLine + String.Format("Best Value: {0}", result.Value);
        }
    }
}
