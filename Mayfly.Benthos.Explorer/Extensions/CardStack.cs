using Mayfly.Species;
using Mayfly.Wild;
using System;

namespace Mayfly.Benthos.Explorer
{
    public static partial class CardStackExtensions
    {
        public static double GetTotalAbundance(this CardStack stack) {
            double result = 0.0;

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies()) {
                result += stack.GetAverageAbundance(speciesRow);
            }

            return result;
        }

        public static double GetAverageAbundance(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow) {
            double result = 0.0;

            foreach (Survey.LogRow logRow in stack.GetLogRows(speciesRow)) {
                result += logRow.GetAbundance();
            }

            return Math.Round(result / stack.Count, 0);
        }

        public static double GetTotalBiomass(this CardStack stack) {
            double result = 0.0;

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies()) {
                result += stack.GetAverageBiomass(speciesRow);
            }

            return result;
        }

        public static double GetAverageBiomass(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow) {
            double result = 0.0;

            foreach (Survey.LogRow logRow in stack.GetLogRows(speciesRow)) {
                result += logRow.GetBiomass();
            }

            return result / stack.Count;
        }

        public static SpeciesComposition GetCenosisComposition(this CardStack stack) {
            SpeciesComposition result = stack.GetBasicCenosisComposition();

            foreach (SpeciesSwarm category in result) {
                category.Abundance = stack.GetAverageAbundance(category.TaxonRow);
                category.Biomass = stack.GetAverageBiomass(category.TaxonRow);
            }

            return result;
        }

        public static TaxonomicComposition GetCenosisComposition(this CardStack stack, TaxonomicRank rank) {
            TaxonomicComposition result = new TaxonomicComposition(stack.GetCenosisComposition(), ReaderSettings.TaxonomicIndex, rank, true);

            foreach (SpeciesSwarmPool pool in result) {
                pool.SamplesCount = stack.GetOccurrenceCases(pool.SpeciesRows);
            }

            return result;
        }

        public static int QuantityIndividual(this Survey.LogRow logRow) {
            int result = 0;

            foreach (Survey.IndividualRow indRow in logRow.GetIndividualRows()) {
                result += indRow.IsFrequencyNull() ? 1 : indRow.Frequency;
            }

            return result;
        }

        public static int QuantityIndividual(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow) {
            int result = 0;

            foreach (Survey.LogRow logRow in stack.GetLogRows(speciesRow)) {
                result += logRow.QuantityIndividual();
            }

            return result;
        }

        public static int QuantityIndividual(this CardStack stack) {
            int result = 0;

            foreach (Survey.LogRow logRow in stack.GetLogRows()) {
                result += logRow.QuantityIndividual();
            }

            return result;
        }

        public static int Measured(this Survey.LogRow logRow) {
            int result = 0;


            foreach (Survey.IndividualRow individualRow in logRow.GetIndividualRows()) {
                if (individualRow.IsLengthNull()) continue;
                result++;
            }

            return result;
        }
    }
}
