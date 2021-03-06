using Mayfly.Fish;
using Mayfly.Wild;
using System.Collections.Generic;
using Meta.Numerics.Statistics;
using Mayfly.Extensions;
using System;

namespace Mayfly.Fish.Explorer
{
    public static class CardRowExtensions
    {
        public static object Get(this Wild.Survey.CardRow cardRow, string field)
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

                    case "Effort":
                        return cardRow.GetEffort();

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

        public static string GetEffortFormat(this Wild.Survey.CardRow cardRow)
        {
            return cardRow.IsSamplerNull() ? string.Empty : "0.00 " + (cardRow.GetGearType().GetDefaultUnitEffort().Unit).Replace(".", "\\.");
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

        public static string GetAbundanceUnits(this Wild.Survey.CardRow cardRow)
        {
            return Resources.Reports.Common.Ind + "/" + cardRow.GetGearType().GetDefaultUnitEffort().Unit;
        }

        public static string GetBiomassUnits(this Wild.Survey.CardRow cardRow)
        {
            return Resources.Reports.Common.Kg + "/" + cardRow.GetGearType().GetDefaultUnitEffort().Unit;
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
