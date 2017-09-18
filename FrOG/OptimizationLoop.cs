using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FrOG
{
    internal static class OptimizationLoop
    {
        //Settings
        public static string solversettings;

        public static bool BolMaximize;
        public static bool BolMaxIter;
        public static int MaxIter;
        public static bool BolMaxIterNoProgress;
        public static int MaxIterNoProgress;
        public static bool BolMaxDuration;
        public static double MaxDuration;
        public static bool BolRuns;
        public static int Runs;
        public static int PresetIndex;
        public static bool BolRandomize = true;

        //Expertsettings for RBFOpt
        public static string ExpertSettings;

        //BolLog Settings
        public static bool BolLog;
        public static string LogName;
        public static string SaveStateName;
        public static int SaveStateFrequency;

        //Variables
        private static BackgroundWorker _worker;
        private static GrasshopperInOut _ghInOut;
        private static int _iterations;
        private static int _iterationsCurrentBest;
        private static double _bestValue;
        private static IList<decimal> _bestParams;
        private static OptimizationResult.ResultType _resultType;

        private static Log _log;
        private static Stopwatch _stopwatchTotal;
        private static Stopwatch _stopwatchLoop;

        //List of Best Values
        private static readonly List<double> BestValues = new List<double>();

        //Run MultipleOptimizationRuns(Entry point: Run RunOptimizationLoop (more than) once)
        public static void RunOptimizationLoopMultiple(object sender, DoWorkEventArgs e)
        {
            var logBaseName = LogName;
            var bestResult = new OptimizationResult(BolMaximize ? double.NegativeInfinity : double.PositiveInfinity, new List<decimal>(), 0, OptimizationResult.ResultType.Unknown);

            //Get worker and component
            _worker = sender as BackgroundWorker;
            var component = (OptimizationComponent)e.Argument;

            if (component == null)
            {
                MessageBox.Show("FrOG Component not set to an object", "FrOG Error");
                return;
            }

            //Setup Variables
            _ghInOut = new GrasshopperInOut(component);
            if (!_ghInOut.SetInputs() || !_ghInOut.SetOutput()) return;
            //MessageBox.Show(_ghInOut.VariablesStr, "FrOG Variables");

            //Main Loop
            var finishedRuns = 0;

            while (finishedRuns < Runs)
            {
                //MessageBox.Show(finishedRuns.ToString());
                if (_worker == null)
                {
                    //MessageBox.Show("worker is null");
                    break;
                }
                if (_worker.CancellationPending)
                {
                    //MessageBox.Show("worker cancellation pending");
                    break;
                }
                //Log
                if (BolLog) LogName = logBaseName;
                if (BolRuns) LogName += String.Format("_{0}", finishedRuns + 1);

                //Run RBFOpt
                var result = RunOptimizationLoop(_worker, PresetIndex);

                //Exit if there is no result
                if (result == null)
                {
                    //MessageBox.Show("result is null");
                    break;
                }
                //Check is there is a better result
                if ((!BolMaximize && result.Value < bestResult.Value) || (BolMaximize && result.Value > bestResult.Value))
                    bestResult = result;

                //Very important to keep FrOG from crashing (probably needed to dispose the process in Run)
                System.Threading.Thread.Sleep(1000);

                finishedRuns++;
            }

            //Exit when there is no result
            if (double.IsPositiveInfinity(bestResult.Value) || double.IsNegativeInfinity(bestResult.Value)) return;

            //Set Grasshopper model to best value
            _ghInOut.NewSolution(bestResult.Parameters);

            //Show Result Message Box
            if (!BolRuns)
                MessageBox.Show(Log.GetResultString(bestResult, MaxIter, MaxIterNoProgress, MaxDuration), "FrOG Result");
            else
                MessageBox.Show(String.Format("Finished {0} runs" + Environment.NewLine + "Overall best value {1}", finishedRuns, bestResult.Value), "FrOG Result");
            
            if(_worker != null) _worker.CancelAsync();
            //_worker?.CancelAsync();
        }

        //Run RBFOpt(Main function)
        private static OptimizationResult RunOptimizationLoop(BackgroundWorker worker, int presetIndex)
        {
            _iterations = 0;
            _iterationsCurrentBest = 0;
            _bestValue = BolMaximize ? double.NegativeInfinity : double.PositiveInfinity;
            _bestParams = new List<decimal>();
            _resultType = OptimizationResult.ResultType.Unknown;

            //Get variables
            var variables = _ghInOut.Variables;
            //MessageBox.Show($"Parameter String: {variables}", "FrOG Parameters");

            //Stopwatches
            _stopwatchTotal = Stopwatch.StartNew();
            _stopwatchLoop = Stopwatch.StartNew();
            _stopwatchTotal.Start();

            //Clear Best Value List
            BestValues.Clear();

            //Prepare Solver
            if (worker.CancellationPending) return null;
            var solver = SolverList.GetSolverByIndex(PresetIndex);
            var preset = SolverList.GetPresetByIndex(presetIndex);

            //Prepare Log
            _log = BolLog ? new Log(String.Format("{0}\\{1}.txt", Path.GetDirectoryName(_ghInOut.DocumentPath), LogName)) : null;

            //Log Settings
            if(_log!= null) _log.LogSettings(preset);
            //_log?.LogSettings(preset);

            //Run Solver
            //MessageBox.Show("Starting Solver", "FrOG Debug");
            var bolSolverStarted = solver.RunSolver(variables, EvaluateFunction, preset, solversettings, _ghInOut.ComponentFolder, _ghInOut.DocumentPath);

            if (!bolSolverStarted)
            {
                MessageBox.Show("Solver could not be started!");
                return null;
            }

            //Show Messagebox with RBFOpt error
            if (worker.CancellationPending)
                _resultType = OptimizationResult.ResultType.UserStopped;
            else if (_resultType == OptimizationResult.ResultType.SolverStopped || _resultType == OptimizationResult.ResultType.Unknown)
            {
                var strError = solver.GetErrorMessage();
                if (!string.IsNullOrEmpty(strError)) MessageBox.Show(strError, "FrOG Error");
            }

            //Result
            _stopwatchLoop.Stop();
            _stopwatchTotal.Stop();

            var result = new OptimizationResult(_bestValue, _bestParams, _iterations, _resultType);
            if (_log != null) _log.LogResult(result, _stopwatchTotal, MaxIter, MaxIterNoProgress, MaxDuration);
            //_log?.LogResult(result, _stopwatchTotal, MaxIter, MaxIterNoProgress, MaxDuration);

            return result;
        }

        public static double EvaluateFunction(IList<decimal> values)
        {
            if(_log!=null) _log.LogIteration(_iterations + 1);
            //_log?.LogIteration(_iterations + 1);
            //var strMessage = "Iteration " + _iterations + Environment.NewLine;
            //strMessage += $"Maximize: {BolMaximize}" + Environment.NewLine;
            //MessageBox.Show(strMessage);
            //MessageBox.Show("Variable Values: " + string.Join(" ",values));

            if (values == null)
            {
                _resultType = OptimizationResult.ResultType.SolverStopped;
                return double.NaN;
            }

            //Log Parameters
            if(_log!=null) _log.LogParameters(string.Join(",", values), _stopwatchLoop);
            //_log?.LogParameters(string.Join(",", values), _stopwatchLoop);

            _stopwatchLoop.Reset();
            _stopwatchLoop.Start();

            //Run a new solution
            if (_worker.CancellationPending) return double.NaN;
            _ghInOut.NewSolution(values);

            //Evaluate Function
            var objectiveValue = _ghInOut.GetObjectiveValue();
            if (double.IsNaN(objectiveValue))
            {
                _resultType = OptimizationResult.ResultType.FrogStopped;
                return double.NaN;
            }

            _stopwatchLoop.Stop();

            //MessageBox.Show($"Function value: {objectiveValue}");

            //BolLog Solution
            if(_log!=null) _log.LogFunctionValue(objectiveValue, _stopwatchLoop);
            //_log?.LogFunctionValue(objectiveValue, _stopwatchLoop);

            _iterations += 1;
            _iterationsCurrentBest += 1;

            //Keep track of best value
            if ((!BolMaximize && objectiveValue < _bestValue) || (BolMaximize && objectiveValue > _bestValue))
            {
                _bestValue = objectiveValue;
                _bestParams = values;
                _iterationsCurrentBest = 0;
            }

            BestValues.Add(_bestValue);

            //Report Best Values
            _worker.ReportProgress(0, BestValues);

            //BolLog Minimum
            if(_log!=null) _log.LogCurrentBest(_bestParams, _bestValue, _stopwatchTotal, _iterationsCurrentBest);
            //_log?.LogCurrentBest(_bestParams, _bestValue, _stopwatchTotal, _iterationsCurrentBest);

            //Optimization Results
            //No Improvement
            if (BolMaxIterNoProgress && _iterationsCurrentBest >= MaxIterNoProgress)
            {
                _resultType = OptimizationResult.ResultType.NoImprovement;
                return double.NaN;
            }
            //Maximum Evaluations reached
            if (BolMaxIter && _iterations >= MaxIter)
            {
                //_worker.CancelAsync();
                _resultType = OptimizationResult.ResultType.MaximumEvals;
                return double.NaN;
            }
            //Maximum Duration reached
            if (BolMaxDuration && _stopwatchTotal.Elapsed.TotalSeconds >= MaxDuration)
            {
                //_worker.CancelAsync();
                _resultType = OptimizationResult.ResultType.MaximumTime;
                return double.NaN;
            }
            //Else: Pass result to RBFOpt
            _stopwatchLoop.Reset();
            _stopwatchLoop.Start();

            if (BolMaximize) objectiveValue = -objectiveValue;
            return objectiveValue;
        }
    }
}
