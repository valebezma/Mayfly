using Mayfly.Waters;
using System.Collections.Generic;
using Meta.Numerics.Statistics;
using Mayfly.Wild;
using System;
using Mayfly.Benthos;
using Mayfly.Benthos.Explorer;

namespace Mayfly.Extensions
{
    public static class CardRowExtensions
    {
        public static int AbundanceRating(this Data.CardRow cardRow, Data.SpeciesRow speciesRow)
        {
            int s = 0;

            foreach (Data.LogRow logRow in 
                ((Data)cardRow.Table.DataSet).Log.Select("CardID = " + cardRow.ID, "Quantity desc"))
            {
                s++;

                if (logRow.SpeciesRow.Species == speciesRow.Species)
                {
                    return s;
                }
            }

            return -1;
        }
        
        public static object Get(this Data.CardRow cardRow, string field)
        {
            switch (field)
            {
                case "Sampler":
                    return cardRow.SamplerRow == null ? null : cardRow.SamplerRow.Sampler;

                case "Substrate":
                    if (cardRow.IsSubstrateNull()) return null;
                    else return cardRow.GetSubstrate().TypeName;

                case "Abundance": // Abundance in thousands of individuals divided by area (m2) or consumer mass (g)
                    return cardRow.GetTotalAbundance() / 1000d;

                case "Biomass": // Biomass in (g) divided by area (m2) or consumer mass (g)
                    return cardRow.GetTotalBiomass();

                case "DiversityA":
                    return cardRow.DiversityA();

                case "DiversityB":
                    return cardRow.DiversityB();

                default:
                    try
                    {
                        return cardRow.GetValue(field);
                    }
                    catch (ArgumentNullException)
                    {
                        return null;
                    }
            }
        }

        public static double GetTotalAbundance(this Data.CardRow cardRow)
        {
            double result = 0;

            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                result += logRow.GetAbundance();
            }

            return result;
        }

        public static double GetTotalBiomass(this Data.CardRow cardRow)
        {
            double result = 0;

            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                result += logRow.GetBiomass();
            }

            return result;
        }

        public static double DiversityA(this Data.CardRow cardRow)
        {
            List<double> values = new List<double>();
            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                values.Add(logRow.GetAbundance());
            }
            return new Sample(values).Diversity();
        }

        public static double DiversityB(this Data.CardRow cardRow)
        {
            List<double> values = new List<double>();
            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                values.Add(logRow.GetBiomass());
            }
            return new Sample(values).Diversity();
        }
    }
}
