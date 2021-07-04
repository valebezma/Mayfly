using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics;
using Meta.Numerics.Analysis;
using Meta.Numerics.Functions;

namespace Mayfly.Fish.Explorer
{
    public class YieldPerRecruitModel
    {
        public double W { get; set; }

        public double K { get; set; }

        public double T0 { get; set; }


        public double Tr { get; set; }

        public double Tc { get; set; }

        public double M { get; set; }

        public bool IsOK
        {
            get 
            {
                return !double.IsNaN(W) && !double.IsNaN(K) && !double.IsNaN(T0) &&
                    !double.IsNaN(Tr) && !double.IsNaN(Tc) && !double.IsNaN(M); 
            }
        }

        public double FMSY { get; private set; }

        public double MaximumSustainableYieldPerRecruit { get; private set; }

        public double VirginBiomassPerRecruit { get; private set; }

        public double OptimalBiomassPerRecruit { get; private set; }



        public YieldPerRecruitModel(Power massModel, Growth growthModel)
        {
            W = massModel.Predict(growthModel.Linf);
            K = growthModel.K;
            T0 = growthModel.T0;

            Tr = 1;
            Tc = 2;

            M = 0.2;
        }

        

        public void Run()
        {
            Run(Interval.FromEndpoints(0, 6));
        }

        public void Run(Interval interval)
        {
            try
            {
                Extremum x = FunctionMath.FindMaximum(GetYieldPerRecruit, interval);
                FMSY = x.Location;
                MaximumSustainableYieldPerRecruit = x.Value;
                OptimalBiomassPerRecruit = GetBiomassPerRecruit(FMSY);
            }
            catch
            {
                FMSY = 
                    MaximumSustainableYieldPerRecruit = 
                    OptimalBiomassPerRecruit = 
                    double.NaN;
            }
            
            VirginBiomassPerRecruit = GetBiomassPerRecruit(0);
        }

        public double GetBiomassPerRecruit(double f)
        {
            double z = f + M;
            double s = Math.Exp(-K * (Tc - T0));
            return Math.Exp(-M * (Tc - Tr)) * W * ((1 / z) - ((3 * s) / (z + K)) + ((3 * s * s) / (z + 2 * K)) - ((s * s * s) / (z + 3 * K)));
        }

        public double GetYieldPerRecruit(double f)
        {
            return f * GetBiomassPerRecruit(f);
        }
    }
}
