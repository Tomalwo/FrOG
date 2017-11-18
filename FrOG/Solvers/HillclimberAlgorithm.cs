using System;

namespace FrOG.Solvers
{
    public class HillclimberAlgorithm
    {
        //source: Stochastic Hill-Climbing, in: Clever Algorithms: Nature-Inspired Programming Recipes (Jason Brownlee)
        //
        //Input: Itermax, ProblemSize 
        //Output: Current 
        //Current  <- RandomSolution(ProblemSize)
        //For (iteri ∈ Itermax )
        //    Candidate  <- RandomNeighbor(Current)
        //    If (Cost(Candidate) >= Cost(Current))
        //        Current  <- Candidate
        //    End
        //End
        //Return (Current)

        /// <summary>
        /// Lower bound for each variable.
        /// </summary>
        public double[] lb { get; private set; }
        /// <summary>
        /// Upper bound for each variable.
        /// </summary>
        public double[] ub { get; private set; }
        /// <summary>
        /// Stepsize.
        /// </summary>
        public double stepsize { get; private set; }
        /// <summary>
        /// Maximum iterations.
        /// </summary>
        public int itermax { get; private set; }
        /// <summary>
        /// Evaluation function.
        /// </summary>
        public Func<double[], double> evalfnc { get; private set; }
        /// <summary>
        /// Variable vector of final solution.
        /// </summary>
        public double[] xopt { get; private set; }
        /// <summary>
        /// Cost of final solution.
        /// </summary>
        public double fxopt { get; private set; }

        private RandomDistributions rnd;

        /// <summary>
        /// Initialize a stochastic hill climber optimization algorithm. Assuming minimization problems.
        /// </summary>
        /// <param name="lb">Lower bound for each variable.</param>
        /// <param name="ub">Upper bound for each variable.</param>
        /// <param name="stepsize">Stepsize.</param>
        /// <param name="itermax">Maximum iterations.</param>
        /// <param name="evalfnc">Evaluation function.</param>
        /// <param name="seed">Seed for random number generator.</param>
        public HillclimberAlgorithm(double [] lb, double [] ub, double stepsize, int itermax, Func<double[], double> evalfnc, int seed)
        {
            this.lb = lb;
            this.ub = ub;
            this.stepsize = stepsize;
            this.itermax = itermax;
            this.evalfnc = evalfnc;

            this.rnd = new RandomDistributions(seed);
        }

        /// <summary>
        /// Minimizes an evaluation function using stochastic hill climbing.
        /// </summary>
        public void Solve()
        {
            int n = lb.Length;
            double[] x = new double[n];
            double[] stdev = new double[n];

            for (int i = 0; i < n; i++)
            {
                x[i] = rnd.NextDouble() * (ub[i] - lb[i]) + lb[i];
                stdev[i] = stepsize * (ub[i] - lb[i]); 
            }
            double fx = evalfnc(x);

            for (int t = 0; t < itermax; t++)
            {
                double[] xtest = new double[n];
                for (int i = 0; i < n; i++)
                {
                    xtest[i] = rnd.NextGaussian(x[i], stdev[i]);
                    if (xtest[i] > ub[i]) xtest[i] = ub[i];
                    else if (xtest[i] < lb[i]) xtest[i] = lb[i];
                }
                double fxtest = evalfnc(xtest);

                if (double.IsNaN(fxtest)) return;

                if (fxtest < fx)
                {
                    xtest.CopyTo(x, 0);
                    fx = fxtest;

                    xopt = new double[n];
                    x.CopyTo(xopt, 0);
                    fxopt = fx;
                }
            }
        }

        /// <summary>
        /// Get the variable vector of the final solution.
        /// </summary>
        /// <returns>Variable vector.</returns>
        public double[] get_Xoptimum()
        {
            return this.xopt;
        }

        /// <summary>
        /// Get the cost value of the final solution.
        /// </summary>
        /// <returns>Cost value.</returns>
        public double get_fxoptimum()
        {
            return this.fxopt;
        }
    }

    public class RandomDistributions : Random
    {
        public RandomDistributions(int rndSeed)
            : base(rndSeed)
        { }

        /// <summary>
        /// Normal distributed random number.
        /// </summary>
        /// <param name="mean">Mean of the distribution.</param>
        /// <param name="stdDev">Standard deviation of the distribution.</param>
        /// <returns>Normal distributed random number.</returns>
        public double NextGaussian(double mean, double stdDev)
        {
            //Random rand = new Random(); //reuse this if you are generating many
            double u1 = base.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = base.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

            return randNormal;

        }


    }
}
