using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mayfly.Controls;
using Mayfly.Extensions;

namespace Mayfly.Wild
{
    public partial class Similarity
    {
        public Composition List1;

        public Composition List2;

        public Composition Common;

        public Composition Total;

        public Composition Unique1;

        public Composition Unique2;



        public Similarity(Composition list1, Composition list2)
        {
            List1 = list1;
            List2 = list2;

            Common = GetCommon(List1, List2);

            Unique1.Clear();
            foreach (Category value in List1)
            {
                if (!Common.Contains(value))
                    Unique1.Add(value);
            }
            Unique1.Sort();

            Unique2.Clear();
            foreach (Category value in List2)
            {
                if (!Common.Contains(value))
                    Unique2.Add(value);
            }
            Unique2.Sort();

            Total.Clear();
            Total.AddRange(Common);
            Total.AddRange(Unique1);
            Total.AddRange(Unique2);
            Total.Sort();
        }



        public static Composition GetCommon(Composition list1, Composition list2)
        {
            Composition result = new Composition("Common");

            foreach (Category species in list1)
            {
                if (list2.Contains(species) && !result.Contains(species))
                {
                    result.Add(species);
                }
            }

            foreach (Category species in list2)
            {
                if (list1.Contains(species) && !result.Contains(species))
                {
                    result.Add(species);
                }
            }

            return result;
        }

        public double Czekanowski1900_Sorensen1948()
        {
            return 2 * (double)Common.Count / (double)(List1.Count + List2.Count);
        }

        public double Jaccard1901()
        {
            return (double)Common.Count / (double)(List1.Count + List2.Count - Common.Count);
        }

        public double Szymkiewicz1926_Simpson1943()
        {
            return (double)Common.Count / (double)Math.Min(List1.Count, List2.Count);
        }

        public double Kulczynski1927_A()
        {
            return (double)Common.Count / 2 * (1 / (double)List1.Count + 1 / (double)List2.Count);
        }

        public double Kulczynski1927_B()
        {
            return (double)Common.Count / (double)(List1.Count + List2.Count - 2 * Common.Count);
        }

        public double BraunBlanquet1932()
        {
            return (double)Common.Count / (double)Math.Max(List1.Count, List2.Count);
        }

        public double Ochiai1957_Barkman1958()
        {
            return (double)Common.Count / Math.Sqrt((double)List1.Count * (double)List2.Count);
        }

        public double SokalSneath1963()
        {
            return (double)(List1.Count - Common.Count) / (double)(2 * (List1.Count + List2.Count - Common.Count) - Common.Count);
        }

        public double Index(int i)
        {
            switch (i)
            {
                case 0:
                    return Czekanowski1900_Sorensen1948();
                case 1:
                    return Jaccard1901();
                case 2:
                    return Szymkiewicz1926_Simpson1943();
                case 3:
                    return Kulczynski1927_A();
                case 4:
                    return Kulczynski1927_B();
                case 5:
                    return BraunBlanquet1932();
                case 6:
                    return Ochiai1957_Barkman1958();
                case 7:
                    return SokalSneath1963();
                default:
                    return double.NaN;
            }
        }
    }
}
