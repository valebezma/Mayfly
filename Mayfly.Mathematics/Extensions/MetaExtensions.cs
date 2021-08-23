using Mayfly.Mathematics.Statistics;
using Meta.Numerics;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using Mayfly.Mathematics;
using System.Resources;
using System.Globalization;

namespace Mayfly.Extensions
{
    public static class MetaExtensions
    {
        public static bool OpenContains(this UncertainValue unc_value, double value)
        {
            return Interval.FromMidpointAndWidth(unc_value.Value, 2 * unc_value.Uncertainty).OpenContains(value);
        }

        //public static double Variance(this AnovaRow anovaRow)
        //{
        //    return anovaRow.SumOfSquares / anovaRow.DegreesOfFreedom;
        //}

        public static string ToString(this UncertainValue value, string format)
        {
            return string.Format("{0:" + format + "} ± {1:" + format + "}", value.Value, value.Uncertainty);
        }

        public static UncertainMeasurementSample GetMeasurements(this BivariateSample data)
        {
            return data.GetMeasurements(1.0);
        }

        public static UncertainMeasurementSample GetMeasurements(this BivariateSample data, double lambda)
        {
            UncertainMeasurementSample set = new UncertainMeasurementSample();

            for (int i = 0; i < data.Count; i++)
            {
                double y = data.Y.ElementAt(i);
                double x = data.X.ElementAt(i);
                set.Add(new UncertainMeasurement<double>(x, y, lambda));
            }

            return set;
        }



        public static bool LeftClosedContains(this Interval interval, double x)
        {
            return ((x >= interval.LeftEndpoint) && (x < interval.RightEndpoint));
        }

        public static bool RightClosedContains(this Interval interval, double x)
        {
            return ((x > interval.LeftEndpoint) && (x <= interval.RightEndpoint));
        }

        public static BivariateSample GetCombinedBivariate(BivariateSample[] samples)
        {
            BivariateSample result = new BivariateSample();

            foreach (BivariateSample sample in samples)
            {
                for (int i = 0; i < sample.Count; i++)
                {
                    result.Add(sample.ElementAt(i));
                }
            }

            return result;
        }

        public static BivariateSample GetAveragedByX(this BivariateSample sample, double pace)
        {
            BivariateSample result = new BivariateSample();

            for (double x = sample.X.Minimum; x < sample.X.Maximum; x += pace)
            {
                Interval interval = Interval.FromEndpointAndWidth(x, pace);

                Sample y = new Sample();

                for (int i = 0; i < sample.X.Count; i++)
                {
                    if (interval.LeftClosedContains(sample.X.ElementAt(i)))
                        y.Add(sample.Y.ElementAt(i));
                }

                result.Add(x, y.Mean);
            }

            return result;
        }

        public static Sample GetSabsample(this Sample sample, Interval interval)
        {
            List<double> result = new List<double>();
            foreach (double d in sample)
            {
                if (interval.LeftClosedContains(d))
                    result.Add(d);
            }

            return new Sample(result.ToArray());
        }

        public static Mathematics.Charts.Histogramma GetHistogram(this Sample sample)
        {
            Mathematics.Charts.Histogramma result = new Mathematics.Charts.Histogramma(sample);
            return result;
        }
    }
}
