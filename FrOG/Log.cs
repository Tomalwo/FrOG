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
            LogNewLine($"Iteration {iteration}" + Environment.NewLine, true);
        }

        public void LogParameters(string parameters, Stopwatch stopwatchLoop)
        {
            LogNewLine($"Time to get parameters: {stopwatchLoop.Elapsed:hh\\:mm\\:ss}", false);
            LogNewLine($"Parameters to evaluate: {parameters}", false);
        }

        public void LogFunctionValue(double value, Stopwatch stopwatchLoop)
        {
            LogNewLine($"Time to get function value: {stopwatchLoop.Elapsed:hh\\:mm\\:ss}", false);
            LogNewLine("Function Value: " + value + Environment.NewLine, false);
        }

        public void LogCurrentBest(IList<decimal> minParams, double minValue, Stopwatch stopwatchTotal, int j)
        {
            LogNewLine($"Current Time: {stopwatchTotal.Elapsed:hh\\:mm\\:ss}", false);
            LogNewLine($"Current Best Value: {minValue} ({j})", false);
            LogNewLine("Current Best Parameters: " + string.Join(",", minParams) + Environment.NewLine, true);
        }

        public void LogResult(OptimizationResult result, Stopwatch stopwatchTotal, int maxIter, int maxIterNoProgress, double maxDuration)
        {
            LogNewLine(GetResultString(result, maxIter, maxIterNoProgress, maxDuration) + Environment.NewLine, false);
            LogNewLine($"Total time: {stopwatchTotal.Elapsed:hh\\:mm\\:ss}", false);
            LogNewLine($"Best Value: {result.Value}", false);
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
                    strResult = $"Optimization stopped by solver after {result.Iterations} iterations";
                    break;
                case OptimizationResult.ResultType.NoImprovement:
                    strResult = $"No improvement after ({maxIterNoProgress}) iterations";
                    break;
                case OptimizationResult.ResultType.MaximumEvals:
                    strResult = $"Maximum number of iterations ({maxIter}) reached";
                    break;
                case OptimizationResult.ResultType.MaximumTime:
                    strResult = $"Maximum number of seconds ({maxDuration}) reached";
                    break;
                case OptimizationResult.ResultType.Unknown:
                    strResult = "Optimization stopped for an unknown reason";
                    break;
                default:
                    strResult = "Optimization stopped for an unknown reason";
                    break;
            }
            return strResult + Environment.NewLine + $"Best Value: {result.Value}";
        }
    }
}
