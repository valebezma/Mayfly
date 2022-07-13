using Mayfly.Plankton;
using Mayfly.Species;
using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using Mayfly.Extensions;

namespace Mayfly.Plankton
{
    public static class DataExtensions
    {
        public static Survey.DefinitionRow[] GetSpeciesForWeightRecovery(this Survey data) {
            List<Survey.DefinitionRow> result = new List<Wild.Survey.DefinitionRow>();

            foreach (Wild.Survey.LogRow logRow in data.Log) {
                if (!logRow.IsMassNull()) continue;
                if (!result.Contains(logRow.DefinitionRow)) {
                    result.Add(logRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }

        public static Survey.DefinitionRow[] GetSpeciesWithUnweightedIndividuals(this Survey data) {
            List<Survey.DefinitionRow> result = new List<Wild.Survey.DefinitionRow>();

            foreach (Wild.Survey.IndividualRow individualRow in data.Individual) {
                if (!individualRow.IsMassNull()) continue;
                if (!result.Contains(individualRow.LogRow.DefinitionRow)) {
                    result.Add(individualRow.LogRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Find species log record and returns its abundance
        /// </summary>
        /// <param name="cardRow"></param>
        /// <param name="speciesRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Survey.CardRow cardRow, Survey.DefinitionRow speciesRow) {
            foreach (Survey.LogRow logRow in cardRow.GetLogRows()) {
                if (logRow.DefinitionRow == speciesRow) {
                    return logRow.GetAbundance();
                }
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Survey.LogRow logRow) {
            if (logRow.IsQuantityNull()) return double.NaN;
            if (logRow.CardRow.IsVolumeNull()) return double.NaN;

            return logRow.IsQuantityNull() ? (double)logRow.DetailedQuantity : (double)logRow.Quantity /
                (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) /
                logRow.CardRow.Volume;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per cubic meter in grams</returns>
        public static double GetBiomass(this Wild.Survey.LogRow logRow) {
            if (logRow.CardRow.IsVolumeNull()) return double.NaN;

            return logRow.IsMassNull() ? logRow.DetailedMass : logRow.Mass /
                (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) /
                logRow.CardRow.Volume;
        }
    }
}

