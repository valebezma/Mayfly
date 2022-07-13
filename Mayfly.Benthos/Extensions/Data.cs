﻿using Mayfly.Benthos;
using Mayfly.Species;
using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using Mayfly.Extensions;

namespace Mayfly.Benthos
{
    public static partial class DataExtensions
    {
        public static Survey.DefinitionRow[] GetUnweightedSpecies(this Survey data) {
            List<Survey.DefinitionRow> result = new List<Survey.DefinitionRow>();

            foreach (Survey.LogRow logRow in data.Log) {
                if (!logRow.IsMassNull()) continue;
                if (!result.Contains(logRow.DefinitionRow)) {
                    result.Add(logRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }

        public static Survey.DefinitionRow[] GetSpeciesWithUnweightedIndividuals(this Survey data) {
            List<Survey.DefinitionRow> result = new List<Survey.DefinitionRow>();

            foreach (Survey.IndividualRow individualRow in data.Individual) {
                if (!individualRow.IsMassNull()) continue;
                if (!result.Contains(individualRow.LogRow.DefinitionRow)) {
                    result.Add(individualRow.LogRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }

        public static SubstrateSample GetSubstrate(this Survey.CardRow cardRow) {
            return cardRow.IsSubstrateNull() ? null : new SubstrateSample(cardRow.Substrate);
        }

        public static double GetAverageMass(this Survey.DefinitionRow spcRow) {
            double result = 0;
            int divider = 0;

            foreach (Survey.IndividualRow individualRow in spcRow.GetIndividualRows()) {
                if (individualRow.IsMassNull()) continue;
                result += individualRow.Mass;
                divider++;
            }

            if (divider > 0) // There are some weighted individuals of given length class
            {
                return result / divider;
            } else {
                return double.NaN;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Survey.LogRow logRow) {
            if (logRow.IsQuantityNull()) return double.NaN;
            if (logRow.CardRow.IsSquareNull()) return double.NaN;

            return System.Math.Round((logRow.IsQuantityNull() ? (double)logRow.DetailedQuantity : (double)logRow.Quantity) /
                (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) /
                logRow.CardRow.Square, 6);
            //return System.Math.Round((logRow.IsQuantityNull() ? (double)logRow.DetailedQuantity : (double)logRow.Quantity) /
            //    (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) /
            //    logRow.CardRow.Square, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per square meter in grams</returns>
        public static double GetBiomass(this Survey.LogRow logRow) {
            if (logRow.CardRow.IsSquareNull()) return double.NaN;

            return 0.001 * (logRow.IsMassNull() ? logRow.DetailedMass : logRow.Mass /
                (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) /
                logRow.CardRow.Square);
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
            return string.Format(Resources.Interface.Interface.LogMask, logRow.DefinitionRow.KeyRecord, logRow.CardRow);
        }
    }
}