using Mayfly.Wild;
using System;

namespace Mayfly.Plankton
{
    public static class DataExtensions
    {
        public static double GetCapacity(this Survey.EquipmentRow eqpRow) {
            return eqpRow.GetVirtue("Capacity");
        }

        public static double GetDiameter(this Survey.EquipmentRow eqpRow) {
            return eqpRow.GetVirtue("Diameter") * .001; // Converting mm to m
        }

        public static double GetTakenVolume(this Survey.EquipmentRow eqpRow, int replications) {

            if (replications == -1) return double.NaN;

            return replications * eqpRow.GetCapacity();
        }

        public static double GetExposureVolume(this Survey.EquipmentRow eqpRow, double e) {

            if (double.IsNaN(e)) return double.NaN;

            double d = eqpRow.GetDiameter();
            double s = Math.PI * d * d * .5;
            return s * e * 1000;
        }

        public static double GetVolume(this Survey.CardRow cardRow) {

            if (cardRow.IsEffortNull()) return double.NaN;

            if (cardRow.Effort < 0) return -cardRow.Effort;

            if (cardRow.IsEqpIDNull()) return double.NaN;

            switch (cardRow.SamplerRow.GetSamplerType()) {

                case PlanktonSamplerType.Bathometer:
                    return cardRow.EquipmentRow.GetTakenVolume(cardRow.Portions);

                case PlanktonSamplerType.Filter:
                    return cardRow.EquipmentRow.GetExposureVolume(cardRow.Exposure);
            }

            return double.NaN;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Survey.LogRow logRow) {

            if (logRow.IsQuantityNull()) return double.NaN;

            double n = logRow.IsQuantityNull() ? logRow.DetailedQuantity : logRow.Quantity;
            double v = logRow.CardRow.GetVolume();

            return System.Math.Round(n / (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) / v, 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per cubic meter in grams</returns>
        public static double GetBiomass(this Survey.LogRow logRow) {

            double m = 0.001 * (logRow.IsMassNull() ? logRow.DetailedMass : logRow.Mass);
            double v = logRow.CardRow.GetVolume();

            return m / (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) / v;
        }
    }
}

