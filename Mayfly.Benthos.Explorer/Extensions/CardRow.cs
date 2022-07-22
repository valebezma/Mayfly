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
        public static int AbundanceRating(this Wild.Survey.CardRow cardRow, Wild.Survey.DefinitionRow definitionRow)
        {
            int s = 0;

            foreach (Wild.Survey.LogRow logRow in 
                ((Wild.Survey)cardRow.Table.DataSet).Log.Select("CardID = " + cardRow.ID, "Quantity desc"))
            {
                s++;

                if (logRow.DefinitionRow.Taxon  == speciesRow.Taxon)
                {
                    return s;
                }
            }

            return -1;
        }
        
        public static object Get(this Survey.CardRow cardRow, string field)
        {
            switch (field)
            {
                case "Sampler":
                    return cardRow.SamplerRow == null ? null : cardRow.SamplerRow.Name;

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

        public static double GetTotalAbundance(this Wild.Survey.CardRow cardRow)
        {
            double result = 0;

            foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
            {
                result += logRow.GetAbundance();
            }

            return result;
        }

        public static double GetTotalBiomass(this Wild.Survey.CardRow cardRow)
        {
            double result = 0;

            foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
            {
                result += logRow.GetBiomass();
            }

            return result;
        }

        public static double DiversityA(this Wild.Survey.CardRow cardRow)
        {
            List<double> values = new List<double>();
            foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
            {
                values.Add(logRow.GetAbundance());
            }
            return new Sample(values).Diversity();
        }

        public static double DiversityB(this Wild.Survey.CardRow cardRow)
        {
            List<double> values = new List<double>();
            foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
            {
                values.Add(logRow.GetBiomass());
            }
            return new Sample(values).Diversity();
        }
    }
}
