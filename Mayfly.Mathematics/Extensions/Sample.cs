using Mayfly.Mathematics;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using Mayfly.Mathematics.Charts;
using Meta.Numerics;
using Mayfly.Extensions;

namespace Mayfly.Extensions
{
    public static class SampleExtensions
    {
        //public static Sample GetCentered(this Sample sample)
        //{
        //    return sample.GetCentered(sample.Mean);
        //}

        //public static Sample GetCentered(this Sample sample, double centroid)
        //{
        //    Sample result = new Sample();

        //    foreach (double d in sample)
        //    {
        //        result.Add(d - centroid);
        //    }

        //    return result;
        //}

        //public static Sample GetNormalized(this Sample sample)
        //{
        //    Sample centerized = sample.GetCentered();

        //    double s = 0;

        //    foreach (double d in centerized)
        //    {
        //        s += d * d;
        //    }

        //    s *= 1.0 / (double)centerized.Count;

        //    Sample result = new Sample();

        //    foreach (double d in centerized)
        //    {
        //        result.Add(d / Math.Sqrt(s));
        //    }

        //    return result;
        //}

        //#region Recombinations

        //public static void Assimilate(this Sample sample, Sample _sample)
        //{
        //    foreach (double d in _sample)
        //    {
        //        sample.Add(d);
        //    }
        //}

        ////public static Sample[] PossibleSubsamples(this Sample sample)
        ////{
        ////    List<Sample> variants = new List<Sample>();
            
        ////    SubSet<double> subs = new SubSet<double>(sample);
        ////    foreach (IList<double> l in subs)
        ////    {
        ////        variants.Add(new Sample(l));
        ////    }

        ////    return variants.ToArray();
        ////}

        //public static Sample[] PossibleSubsamples(this Sample sample, int subsampleLength)
        //{
        //    if (subsampleLength > sample.Count)
        //        throw new ArgumentException("Subsamples length should be less or equal to original sample count.");

        //    if (subsampleLength == sample.Count)
        //        return new Sample[] { sample };

        //    List<Sample> variants = new List<Sample>();

        //    IEnumerable<IEnumerable<double>> subs = Mayfly.Mathematics.Service.Combinations(sample, subsampleLength);
        //    int count = 0;

        //    foreach (IEnumerable<double> l in subs)
        //    {
        //        count++;

        //        Sample variant = new Sample(l);
        //        variant.Name = string.Format("Variant {0}", count);
        //        variants.Add(variant);
        //    }

        //    return variants.ToArray();
        //}

        //public static Sample[] PossibleSubsamples(this Sample sample, int subsampleLength, int limit)
        //{
        //    if (subsampleLength > sample.Count)
        //        throw new ArgumentException("Subsamples length should be less or equal to original sample count.");

        //    if (subsampleLength == sample.Count)
        //        return new Sample[] { sample };

        //    List<Sample> variants = new List<Sample>();

        //    IEnumerable<IEnumerable<double>> subs = Mayfly.Mathematics.Service.Combinations(sample, subsampleLength, limit);
        //    int count = 0;

        //    foreach (IEnumerable<double> l in subs)
        //    {
        //        count++;

        //        Sample variant = new Sample(l);
        //        variant.Name = string.Format("Variant {0}", count);
        //        variants.Add(variant);
        //    }

        //    return variants.ToArray();
        //}

        //public static Sample Cumulated(IList<Sample> samples)
        //{
        //    Sample result = new Sample();

        //    foreach (Sample sample in samples)
        //    {
        //        foreach (double d in sample)
        //        {
        //            result.Add(d);
        //        }
        //    }
        //    return result;
        //}

        //#endregion

        //public static double MarginOfError(this Sample sample, double p)
        //{
        //    return Mayfly.Mathematics.Service.t_cr(sample.Count - 1, (1 - p) / 2D) * sample.PopulationMean.Uncertainty;
        //}

        //public static Interval MarginsOfError(this Sample sample, double p)
        //{
        //    return Interval.FromMidpointAndWidth(sample.Mean, 2 * sample.MarginOfError(p));
        //}

        //public static Interval MarginsOfError(this Sample sample)
        //{
        //    return  sample.MarginsOfError(Mayfly.Mathematics.UserSettings.DefaultConfidenceLevel);
        //}

        //public static TestResult Normality(this Sample sample)
        //{
        //    return sample.KolmogorovSmirnovTest(new NormalDistribution(sample.Mean, sample.StandardDeviation));
        //}

        //public static double GetKurtosis(this Sample sample)
        //{
        //    double n = (double)sample.Count;
        //    double m = sample.Mean;
        //    double m4 = 0.0d;
        //    double m2 = 0.0d;

        //    foreach (double d in sample)
        //    {
        //        double s1 = d - m;
        //        m2 += Math.Pow(s1, 2);
        //        m4 += Math.Pow(s1, 4);
        //    }

        //    double result = ((n + 1) * n * (n - 1)) / ((n - 2) * (n - 3)) * (m4 / Math.Pow(m2, 2)) -
        //        3 * Math.Pow(n - 1, 2) / ((n - 2) * (n - 3)); // second last formula for ErrorFormat

        //    return result;
        //}

        //public static double GetVariation(this Sample sample)
        //{
        //    return sample.Variance / sample.Mean;
        //}

        //public static Sample GetSabsample(this Sample sample, Meta.Numerics.Interval interval)
        //{
        //    List<double> result = new List<double>();
        //    foreach (double d in sample)
        //    {
        //        if (interval.LeftClosedContains(d))
        //            result.Add(d);
        //    }
        //    return new Sample(result.ToArray());
        //}

        //public static int CountOf(this Sample sample, Meta.Numerics.Interval interval)
        //{
        //    return sample.GetSabsample(interval).Count;
        //}



        //public static double MeanOf(IList<Sample> samples)
        //{
        //    double sum = 0.0;
        //    double n = 0;

        //    for (int i = 0; i < samples.Count; i++)
        //    {
        //        n += samples[i].Count;
        //        sum += samples[i].Sum();
        //    }

        //    sum /= n;
        //    return sum;
        //}

        //#region Heteroscedasticity tests

        ////public static TestResult VarianceHomogeneity(IList<Sample> samples)
        ////{
        ////    switch (UserSettings.HomogeneityTest)
        ////    {
        ////        case 0: return BartlettTest(samples);
        ////        case 1: return HartleyTest(samples);
        ////        case 2: return LeveneTest(samples);
        ////        case 3: return BrownForsytheTest(samples);
        ////        default: return null;
        ////    }
        ////}

        ////public static TestResult BartlettTest(IList<Sample> samples)
        ////{
        ////    if (samples == null) throw new ArgumentNullException("samples");
        ////    if (samples.Count < 2) throw new InvalidOperationException();

        ////    double q2 = 0;
        ////    double q3 = 0;
        ////    double q4 = 0;
        ////    double q5 = 0;
        ////    foreach (Sample sample in samples)
        ////    {
        ////        q2 += sample.Count - 1;
        ////        q3 += (sample.Count - 1) * sample.Variance;
        ////        q4 += (sample.Count - 1) * Math.Log(sample.Variance);
        ////        q5 += 1 / (sample.Count - 1);
        ////    }

        ////    q3 /= q2;
        ////    q3 = Math.Log(q3);

        ////    double chi = q2 * q3 - q4;
        ////    double c = 1 + 1 / 3 * (samples.Count - 1) * (q5 - 1 / q2);
        ////    return new TestResult(chi / c, new ChiSquaredDistribution(samples.Count - 1));
        ////}

        ////public static TestResult HartleyTest(IList<Sample> samples)
        ////{
        ////    if (samples == null) throw new ArgumentNullException("samples");
        ////    if (samples.Count < 2) throw new InvalidOperationException();

        ////    Sample sigmas = new Sample();
        ////    double maxdf = 0;
        ////    foreach (Sample sample in samples)
        ////    {
        ////        maxdf = Math.Max(sample.Count - 1, maxdf);
        ////        sigmas.Add(sample.StandardDeviation);
        ////    }

        ////    try
        ////    {
        ////        return new TestResult(sigmas.Maximum / sigmas.Minimum, new FisherDistribution(samples.Count, maxdf));
        ////    }
        ////    catch
        ////    {
        ////        return null;
        ////    }
        ////}

        ////public static TestResult LeveneTest(IList<Sample> samples)
        ////{
        ////    if (samples == null) throw new ArgumentNullException("samples");
        ////    if (samples.Count < 2) throw new InvalidOperationException();

        ////    int k = samples.Count;

        ////    int n = 0;
        ////    double mean = 0.0;
        ////    for (int i = 0; i < samples.Count; i++)
        ////    {
        ////        n += samples[i].Count;
        ////        mean += samples[i].Count * samples[i].Mean;
        ////    }
        ////    mean = mean / n;

        ////    double w1 = (n - k) / (k - 1);

        ////    double w2 = 0;
        ////    double w3 = 0;
        ////    double z = 0;

        ////    foreach (Sample sample in samples)
        ////    {
        ////        double meanz = 0;

        ////        foreach (double value in sample)
        ////        {
        ////            meanz += Math.Abs(value - sample.Mean);
        ////        }

        ////        z += meanz;
        ////        meanz /= sample.Count;

        ////        foreach (double value in sample)
        ////        {
        ////            double valuez = Math.Abs(value - sample.Mean);
        ////            w3 += (valuez - meanz) * (valuez - meanz);
        ////        }
        ////    }

        ////    z /= n;

        ////    foreach (Sample sample in samples)
        ////    {
        ////        double meanz = 0;

        ////        foreach (double value in sample)
        ////        {
        ////            meanz += Math.Abs(value - sample.Mean);
        ////        }

        ////        meanz /= sample.Count;

        ////        w2 += sample.Count * (meanz - z) * (meanz - z);
        ////    }

        ////    double W = w1 * w2 / w3;

        ////    return (new TestResult(W, new FisherDistribution(k - 1, n - k)));
        ////}

        ////public static TestResult BrownForsytheTest(IList<Sample> samples)
        ////{
        ////    if (samples == null) throw new ArgumentNullException("samples");
        ////    if (samples.Count < 2) throw new InvalidOperationException();

        ////    int k = samples.Count;

        ////    int n = 0;
        ////    double mean = 0.0;
        ////    for (int i = 0; i < samples.Count; i++)
        ////    {
        ////        n += samples[i].Count;
        ////        mean += samples[i].Count * samples[i].Median;
        ////    }
        ////    mean = mean / n;

        ////    double w1 = (n - k) / (k - 1);

        ////    double w2 = 0;
        ////    double w3 = 0;
        ////    double z = 0;

        ////    foreach (Sample sample in samples)
        ////    {
        ////        double meanz = 0;

        ////        foreach (double value in sample)
        ////        {
        ////            meanz += Math.Abs(value - sample.Mean);
        ////        }

        ////        z += meanz;
        ////        meanz /= sample.Count;

        ////        foreach (double value in sample)
        ////        {
        ////            double valuez = Math.Abs(value - sample.Median);
        ////            w3 += (valuez - meanz) * (valuez - meanz);
        ////        }
        ////    }

        ////    z /= n;

        ////    foreach (Sample sample in samples)
        ////    {
        ////        double meanz = 0;

        ////        foreach (double value in sample)
        ////        {
        ////            meanz += Math.Abs(value - sample.Median);
        ////        }

        ////        meanz /= sample.Count;

        ////        w2 += sample.Count * (meanz - z) * (meanz - z);
        ////    }

        ////    double W = w1 * w2 / w3;

        ////    return (new TestResult(W, new FisherDistribution(k - 1, n - k)));
        ////}

        //#endregion

        //public static Histogramma GetHistogram(this Sample sample)
        //{
        //    Histogramma result = new Histogramma();
        //    result.Data = sample;
        //    return result;
        //}
    }
}
