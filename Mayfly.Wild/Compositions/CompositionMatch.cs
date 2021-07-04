using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mayfly.Controls;
using Mayfly.Extensions;

namespace Mayfly.Wild
{
    public partial class CompositionMatch
    {
        public Composition List1;

        public Composition List2;

        public Composition CommonPart;

        public Composition UniquePart1;

        public Composition UniquePart2;

        public Composition Totalized;



        public CompositionMatch(Composition list1, Composition list2)
        {
            List1 = list1;
            List2 = list2;

            CommonPart = new Composition("Common");

            foreach (Category species in List1)
            {
                if (!List2.ContainsNamed(species.Name)) continue;

                Category cat = new Category(species.Name);
                cat.Abundance = List1.GetCategory(species.Name).Abundance + List2.GetCategory(species.Name).Abundance;
                cat.Biomass = List1.GetCategory(species.Name).Biomass + List2.GetCategory(species.Name).Biomass;
                CommonPart.AddCategory(cat);
            }

            UniquePart1 = new Composition("Unique1");

            foreach (Category value in List1)
            {
                if (CommonPart.ContainsNamed(value.Name)) continue;

                Category cat = new Category(value.Name);
                cat.Abundance = value.Abundance;
                cat.Biomass = value.Biomass;
                UniquePart1.AddCategory(cat);
            }

            UniquePart2 = new Composition("Unique2");

            foreach (Category value in List2)
            {
                if (CommonPart.ContainsNamed(value.Name)) continue;

                Category cat = new Category(value.Name);
                cat.Abundance = value.Abundance;
                cat.Biomass = value.Biomass;
                UniquePart2.AddCategory(cat);
            }

            Totalized = new Composition("Total");

            foreach (Composition com in new Composition[] { CommonPart, UniquePart1, UniquePart2 })
            {
                foreach (Category value in com)
                {
                    Category cat = new Category(value.Name);
                    cat.Abundance = value.Abundance;
                    cat.Biomass = value.Biomass;
                    Totalized.AddCategory(cat);
                }
            }
        }



        public double Czekanowski1900_Sorensen1948
        {
            get
            {

                return 2 * (double)CommonPart.Count / (double)(List1.Count + List2.Count);
            }
        }

        public double Jaccard1901
        {
            get
            {

                return (double)CommonPart.Count / (double)(List1.Count + List2.Count - CommonPart.Count);
            }
        }

        public double Szymkiewicz1926_Simpson1943
        {
            get
            {

                return (double)CommonPart.Count / (double)Math.Min(List1.Count, List2.Count);
            }
        }

        public double Kulczynski1927_A
        {
            get
            {

                return (double)CommonPart.Count / 2 * (1 / (double)List1.Count + 1 / (double)List2.Count);
            }
        }

        public double Kulczynski1927_B
        {
            get
            {

                return (double)CommonPart.Count / (double)(List1.Count + List2.Count - 2 * CommonPart.Count);
            }
        }

        public double BraunBlanquet1932
        {
            get
            {

                return (double)CommonPart.Count / (double)Math.Max(List1.Count, List2.Count);
            }
        }

        public double Ochiai1957_Barkman1958
        {
            get
            {

                return (double)CommonPart.Count / Math.Sqrt((double)List1.Count * (double)List2.Count);
            }
        }

        public double SokalSneath1963
        {
            get
            {

                return (double)(List1.Count - CommonPart.Count) / (double)(2 * (List1.Count + List2.Count - CommonPart.Count) - CommonPart.Count);
            }
        }



        public double Czekanowcki1911
        {
            get
            {
                double s = 0;

                foreach (Category species in Totalized)
                {
                    double a1 = 0;
                    try { a1 = List1.GetCategory(species.Name).Abundance; }
                    catch { }

                    double a2 = 0;
                    try { a2 = List2.GetCategory(species.Name).Abundance; }
                    catch { }

                    s += Math.Min(a1, a2);
                }

                return s / (List1.TotalAbundance + List2.TotalAbundance);
            }
        }

        public double Shorygin1939
        {
            get
            {
                double s = 0;

                foreach (Category species in Totalized)
                {
                    double a1 = 0;
                    try { a1 = List1.GetCategory(species.Name).AbundanceFraction; }
                    catch { }

                    double a2 = 0;
                    try { a2 = List2.GetCategory(species.Name).AbundanceFraction; }
                    catch { }

                    s += Math.Min(a1, a2);
                }

                return s;
            }
        }

        public double Weinstein1976
        {
            get
            {
                return this.Czekanowcki1911 * this.Jaccard1901;
            }
        }



        public double Pianka1973
        {
            get
            {
                double s  = 0;
                double j2 = 0;
                double k2 = 0;

                foreach (Category species in Totalized)
                {
                    double pj = 0;
                    try { pj = List1.GetCategory(species.Name).AbundanceFraction; }
                    catch { }

                    double pk = 0;
                    try { pk = List2.GetCategory(species.Name).AbundanceFraction; }
                    catch { }

                    s += pj * pk;
                    j2 += pj * pj;
                    k2 += pk * pk;
                }

                return s / Math.Sqrt(j2 * k2);
            }
        }

        public double Morisita1959
        {
            get
            {
                double s = 0;
                double sj = 0;
                double sk = 0;

                foreach (Category species in Totalized)
                {
                    double pj = 0;
                    try
                    {
                        Category cat1 = List1.GetCategory(species.Name);
                        pj = cat1.AbundanceFraction;
                        sj += pj * cat1.AbundanceSample.Count / cat1.AbundanceSample.Count;
                    }
                    catch { }

                    double pk = 0;
                    try
                    {
                        Category cat2 = List2.GetCategory(species.Name);
                        pk = cat2.AbundanceFraction;
                        sk = pk * cat2.AbundanceSample.Count / cat2.AbundanceSample.Count;
                    }
                    catch { }

                    s += pj * pk;
                }

                return (2 * s) / (sj + sk);
            }
        }

        public double Horn1966_MorisitaSimplified
        {
            get
            {
                double s = 0;
                double j2 = 0;
                double k2 = 0;

                foreach (Category species in Totalized)
                {
                    double pj = 0;
                    try { pj = List1.GetCategory(species.Name).AbundanceFraction; }
                    catch { }

                    double pk = 0;
                    try { pk = List2.GetCategory(species.Name).AbundanceFraction; }
                    catch { }

                    s += pj * pk;
                    j2 += pj * pj;
                    k2 += pk * pk;
                }

                return 2 * s / (j2 + k2);
            }
        }

        public double Horn1966
        {
            get
            {
                double s = 0;
                double sj = 0;
                double sk = 0;

                foreach (Category species in Totalized)
                {
                    double pj = 0;
                    try { pj = List1.GetCategory(species.Name).AbundanceFraction; }
                    catch { }

                    double pk = 0;
                    try { pk = List2.GetCategory(species.Name).AbundanceFraction; }
                    catch { }

                    s += (pj + pk) * Math.Log10(pj + pk);
                    sj += pj * Math.Log10(pj);
                    sk += pk * Math.Log10(pk);
                }

                return (s - sj - sk) / (2 * Math.Log10(2));
            }
        }

        public double Hurlbert1978
        {
            get
            {
                double s = 0;

                foreach (Category species in Totalized)
                {
                    double pj = 0;
                    try { pj = List1.GetCategory(species.Name).AbundanceFraction; }
                    catch { }

                    double pk = 0;
                    try { pk = List2.GetCategory(species.Name).AbundanceFraction; }
                    catch { }

                    s += (pj * pk) / species.AbundanceFraction;
                }

                return s;
            }
        }
    }
}