using Mayfly.Bacterioplankton;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using Mayfly.Wild;
using System;

namespace Mayfly.Extensions
{
    public static class CardRowExtensions
    {
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
                    case "Abundance":
                        return cardRow.GetTotalAbundance() / 1000d;

                    case "Biomass":
                        return cardRow.GetTotalBiomass();

                    default:
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
    }
}
