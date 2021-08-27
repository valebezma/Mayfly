using Mayfly.Waters;
using System.Collections.Generic;
using Meta.Numerics.Statistics;
using Mayfly.Wild;
using Mayfly.Plankton;
using Mayfly.Plankton.Explorer;
using System;

namespace Mayfly.Extensions
{
    public static class CardRowExtensions
    {
        public static Data SingleCardDataset(this Data.CardRow cardRow)
        {
            Data result = ((Data)cardRow.Table.DataSet).Copy();

            for (int i = 0; i < result.Card.Rows.Count; i++)
            {
                if (result.Card[i].ID != cardRow.ID)
                {
                    result.Card.Rows.RemoveAt(i);
                    i--;
                }
            }

            result.ClearUseless();
            return result;
        }

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
            try
            {
                return cardRow.GetValue(field);
            }
            catch (ArgumentNullException)
            {
                switch (field)
                {
                    case "Gear":
                        return cardRow.SamplerRow.Sampler;

                    case "Abundance":
                        return cardRow.GetTotalAbundance() / 1000d;

                    case "Biomass":
                        return cardRow.GetTotalBiomass();

                    case "DiversityA":
                        return cardRow.DiversityA();

                    case "DiversityB":
                        return cardRow.DiversityB();

                    default:
                        return null;
                }
            }
        }

        public static double Wealth(this Data.CardRow cardRow)
        {
            return cardRow.GetLogRows().Length;
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
    }
}
