using Mayfly.Extensions;
using Mayfly.Wild;
using System.Collections.Generic;

namespace Mayfly.Benthos
{
    public static partial class DataExtensions
    {
        public static SubstrateSample GetSubstrate(this Survey.CardRow cardRow) {
            return cardRow.IsSubstrateNull() ? null : new SubstrateSample(cardRow.Substrate);
        }

        public static double GetSquare(this Survey.EquipmentRow eqpRow) {
            return eqpRow.GetVirtue("Square") * .0001; // Converting sq. cm to sq. m
        }

        public static double GetWidth(this Survey.EquipmentRow eqpRow) {
            return eqpRow.GetVirtue("Width") * .01; // Converting cm to m
        }

        public static double GetCoveredArea(this Survey.EquipmentRow eqpRow, int replications) {

            if (replications == -1) return double.NaN;

            return replications * eqpRow.GetSquare();
        }

        public static double GetExposureArea(this Survey.EquipmentRow eqpRow, double e) {

            if (double.IsNaN(e)) return double.NaN;

            double w = eqpRow.GetWidth();

            return w * e;
        }

        public static double GetArea(this Survey.CardRow cardRow) {

            if (cardRow.IsEffortNull()) return double.NaN;

            if (cardRow.Effort < 0) return -cardRow.Effort;

            if (cardRow.IsEqpIDNull()) return double.NaN;

            switch (cardRow.SamplerRow.GetSamplerType()) {

                case BenthosSamplerType.Grabber:
                    return cardRow.EquipmentRow.GetCoveredArea(cardRow.Portions);

                case BenthosSamplerType.Scraper:
                    return cardRow.EquipmentRow.GetExposureArea(cardRow.Exposure);
            }

            return double.NaN;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Quantity per square meter in individuals</returns>
        public static double GetAbundance(this Survey.LogRow logRow) {

            if (logRow.IsQuantityNull()) return double.NaN;

            double n = logRow.IsQuantityNull() ? logRow.DetailedQuantity : logRow.Quantity;
            double s = logRow.CardRow.GetArea();

            return System.Math.Round(n / (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) / s, 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per square meter in grams</returns>
        public static double GetBiomass(this Survey.LogRow logRow) {

            double m = 0.001 * (logRow.IsMassNull() ? logRow.DetailedMass : logRow.Mass);
            double s = logRow.CardRow.GetArea();

            return m / (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) / s;
        }

        public static string GetDescription(this Survey.IndividualRow indRow) {
            List<string> result = new List<string>();
            result.Add(indRow.LogRow.DefinitionRow.KeyRecord.CommonName);
            if (!indRow.IsTallyNull()) result.Add(string.Format("#{0}", indRow.Tally));
            if (!indRow.IsLengthNull()) result.Add(string.Format("L = {0}", indRow.Length));
            if (!indRow.IsMassNull()) result.Add(string.Format("W = {0}", indRow.Mass));
            return result.Merge();
        }

        public static string GetDescription(this Survey.LogRow logRow) {
            return string.Format(Wild.Resources.Interface.Interface.LogMask, logRow.DefinitionRow.KeyRecord, logRow.CardRow);
        }
    }
}