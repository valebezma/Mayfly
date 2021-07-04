using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meta.Numerics.Statistics;
using Mayfly.Extensions;

namespace Mayfly.Mathematics.Statistics
{
    public static class Test
    {
        public static double FisherLSD(AnovaRow errorRow, double alpha, Sample sample1, Sample sample2)
        {
            double result = Service.t_cr(errorRow.DegreesOfFreedom, alpha / 2);
            return result * Math.Sqrt(errorRow.Variance() * ((1D / (double)sample1.Count) + (1D  / (double)sample2.Count)));
        }

        public static double FisherLSD(AnovaRow errorRow, double alpha, int groupSize)
        {
            double result = Service.t_cr(errorRow.DegreesOfFreedom, alpha / 2);
            return result * Math.Sqrt(2 * errorRow.Variance() / groupSize);
        }

        public static double StudentizedRange(Anova anova, Sample sample1, Sample sample2)
        {
            double result = Math.Abs(sample1.Mean - sample2.Mean);
            return result / Math.Sqrt(anova.Residual.Variance() / anova.HarmonicGroupSize);
        }

        public static double TukeyHSD(AnovaRow errorRow, double alpha, int levelsCount, int groupSize)
        {
            double q = Service.q_cr(levelsCount, errorRow.DegreesOfFreedom, alpha);
            return q * Math.Sqrt(errorRow.Variance() / groupSize);
        }
                
        #region Connectance

        public static double Czekanowcki1911(this BivariateSample sample)
        {
            double s12 = 0;

            for (int i = 0; i < sample.Count; i++)
            {
                if (double.IsNaN(sample.X.ElementAt(i)) && !double.IsNaN(sample.Y.ElementAt(i)))
                {
                    sample.Y.ElementAt(i);
                }

                if (!double.IsNaN(sample.X.ElementAt(i)) && double.IsNaN(sample.Y.ElementAt(i)))
                {
                    s12 += (double)sample.X.ElementAt(i);
                }

                if (!double.IsNaN(sample.X.ElementAt(i)) && !double.IsNaN(sample.Y.ElementAt(i)))
                {
                    s12 += Math.Min((double)sample.X.ElementAt(i), (double)sample.Y.ElementAt(i));
                }
            }

            return s12 / (sample.X.Sum() + sample.Y.Sum());
        }

        public static double Shorygin1939(this BivariateSample sample)
        {
            double s1 = sample.X.Sum();
            double s2 = sample.Y.Sum();
            double s = 0;

            for (int i = 0; i < sample.Count; i++)
            {
                double Value1 = 0;

                //if (sample.X.ElementAt(i) == null) continue;
                Value1 = sample.X.ElementAt(i) / s1;

                double Value2 = 0;
                //if (sample.Y.ElementAt(i) == null) continue;
                Value2 = sample.Y.ElementAt(i) / s2;

                s += Math.Min(Value1, Value2);
            }

            return s;
        }

        //public static double Weinstein1976(this BivariateSample sample)
        //{
        //    return sample.Czekanowcki1911() * sample.Jaccard1901();
        //}

        #endregion

    }
}
