using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics;
using Meta.Numerics.Statistics;

namespace Mayfly.Wild
{
    public static class DiversityExtensions
    {
        public static double Shannon_Entropy(this Sample sample, double Base)
        {
            double result = 0;
            foreach (double value in sample)
            {
                double role = value / sample.Sum();
                result += role * Math.Log(role, Base);
            }
            return -result;
        }

        public static double ShannonIndex(this Sample sample)
        {
            return sample.Shannon_Entropy(2);
        }

        public static double Simpson_1949(this Sample sample)
        {
            double result = 0;
            foreach (double value in sample)
            {
                double role = value / sample.Sum();
                result += role * role;
            }

            return result;
        }

        public static double Margalef_1958(this Sample sample)
        {
            double S = sample.Count;
            double N = sample.Sum();

            return (S - 1) / Math.Log(N);
        }

        public static double Shannon_Weaver_1963(this Sample sample)
        {
            return sample.Shannon_Entropy(Math.E);
        }

        public static double Menhinick_1964(this Sample sample)
        {
            double S = sample.Count;
            double N = sample.Sum();

            return S / Math.Sqrt(N);
        }

        public static double McIntosh_1967(this Sample sample)
        {
            double n = sample.Sum();
            double u = 0;

            foreach (double value in sample)
            {
                u += value * value;
            }

            u = Math.Sqrt(u);

            return (n - u) / (n - Math.Sqrt(n));
        }

        public static double Pielou_1966(this Sample sample)
        {
            double h = sample.Shannon_Weaver_1963();
            double S = sample.Count;

            return h / Math.Log(S);
        }

        public static double Berger_Parker_1970(this Sample sample)
        {
            return sample.Maximum / sample.Sum();
        }

        public static double Foerster_1974(this Sample sample)
        {
            double h = sample.Shannon_Weaver_1963();
            double hm = sample.Complexity();

            return 1 -  h / hm;
        }

        public static double Diversity(this Sample sample)
        {
            double h = double.NaN;

            switch (Wild.UserSettings.Diversity)
            {
                case DiversityIndex.D1949_Simpson:
                    h = sample.Simpson_1949();
                    break;
                case DiversityIndex.D1958_Margalef:
                    h = sample.Margalef_1958();
                    break;
                case DiversityIndex.D1963_Shannon:
                    h = sample.Shannon_Weaver_1963();
                    break;
                case DiversityIndex.D1964_Menhinick:
                    h = sample.Menhinick_1964();
                    break;
                case DiversityIndex.D1967_McIntosh:
                    h = sample.McIntosh_1967();
                    break;
                case DiversityIndex.D1966_Pielou:
                    h = sample.Pielou_1966();
                    break;
                case DiversityIndex.D1970_BergerParker:
                    h = sample.Berger_Parker_1970();
                    break;
                case DiversityIndex.D1974_Foerster:
                    h = sample.Foerster_1974();
                    break;
            }

            return Math.Round(h, 3);
        }

        public static double Complexity(this Sample sample)
        {
            return Math.Log(sample.Count);
        }
    }

    public enum DiversityIndex
    {
        D1949_Simpson,
        D1958_Margalef,
        D1963_Shannon,
        D1964_Menhinick,
        D1966_Pielou,
        D1967_McIntosh,
        D1970_BergerParker,
        D1974_Foerster
    }
}
