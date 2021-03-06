using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using System;

namespace Mayfly.Extensions
{
    public enum DistributionType
    {
        Auto = 0,
        Normal = 1,
        Exponential = 2,
        Logistic = 3,
        Lognormal = 4,
    };

    public static class DistributionExtensions
    {
        public static ContinuousDistribution GetDistribution(DistributionType type, Sample sample)
        {
            switch (type)
            {
                default:
                case DistributionType.Auto:
                    return Fittest(sample);
                case DistributionType.Exponential:
                    return (sample.Mean > 0) ? new ExponentialDistribution(sample.Mean) : null;
                case DistributionType.Logistic:
                    return new LogisticDistribution(sample.Mean, sample.StandardDeviation);
                case DistributionType.Lognormal:
                    Sample summary = sample.Copy();
                    summary.Transform((x) => { return Math.Log(x); });
                    return new LognormalDistribution(summary.Mean, summary.StandardDeviation);
                case DistributionType.Normal:
                    return new NormalDistribution(sample.Mean, sample.StandardDeviation);
            }
        }

        public static ContinuousDistribution Fittest(Sample sample)
        {
            ContinuousDistribution result = new NormalDistribution(sample.Mean, sample.StandardDeviation);
            double max = sample.KolmogorovSmirnovTest(result).Probability;

            foreach (DistributionType type in Enum.GetValues(typeof(DistributionType)))
            {
                if (type == DistributionType.Auto) continue;
                if (type == DistributionType.Normal) continue;

                ContinuousDistribution distribution = GetDistribution(type, sample);

                if (distribution == null) continue;

                TestResult testResult = sample.KolmogorovSmirnovTest(distribution);

                if (testResult.Probability > max)
                {
                    result = distribution;
                    max = testResult.Probability;
                }
            }

            return result;
        }
    }
}
