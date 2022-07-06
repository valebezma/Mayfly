using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using Mayfly.Species;

namespace Mayfly.Benthos.Explorer
{
    public static partial class CardStackExtensions
    {
        public static Samplers.SamplerRow[] GetSamplers(this CardStack stack)
        {
            List<Samplers.SamplerRow> result = new List<Samplers.SamplerRow>();

            foreach (Data.CardRow cardRow in stack)
            {
                if (cardRow.IsSamplerNull()) continue;
                if (result.Contains(cardRow.SamplerRow)) continue;
                result.Add(cardRow.SamplerRow);
            }

            return result.ToArray();
        }

        public static string[] GetSamplersList(this CardStack stack)
        {
            List<string> result = new List<string>();

            foreach (Samplers.SamplerRow samplerRow in stack.GetSamplers())
            {
                result.Add(samplerRow.Sampler);
            }

            return result.ToArray();
        }

        public static double GetTotalAbundance(this CardStack stack)
        {
            double result = 0.0;

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies())
            {
                result += stack.GetAverageAbundance(speciesRow);
            }

            return result;
        }

        public static double GetAverageAbundance(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            double result = 0.0;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.GetAbundance();
            }

            return Math.Round(result / (double)stack.Count, 0);
        }

        public static double GetTotalBiomass(this CardStack stack)
        {
            double result = 0.0;

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies())
            {
                result += stack.GetAverageBiomass(speciesRow);
            }

            return result;
        }

        public static double GetAverageBiomass(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            double result = 0.0;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.GetBiomass();
            }

            return result / (double)stack.Count;
        }

        public static SpeciesComposition GetCenosisComposition(this CardStack stack)
        {
            SpeciesComposition result = stack.GetBasicCenosisComposition();

            foreach (SpeciesSwarm category in result)
            {
                category.Abundance = stack.GetAverageAbundance(category.SpeciesRow);
                category.Biomass = stack.GetAverageBiomass(category.SpeciesRow);
            }

            return result;
        }

        public static TaxonomicComposition GetCenosisComposition(this CardStack stack, TaxonomicRank rank)
        {
            TaxonomicComposition result = new TaxonomicComposition(stack.GetCenosisComposition(), Benthos.UserSettings.SpeciesIndex, rank, true);

            foreach (SpeciesSwarmPool pool in result)
            {
                pool.SamplesCount = stack.GetOccurrenceCases(pool.SpeciesRows);
            }

            return result;
        }

        public static int QuantityIndividual(this Data.LogRow logRow)
        {
            int result = 0;

            foreach (Data.IndividualRow indRow in logRow.GetIndividualRows())
            {
                result += indRow.IsFrequencyNull() ? 1 : indRow.Frequency;
            }

            return result;
        }

        public static int QuantityIndividual(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            int result = 0;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.QuantityIndividual();
            }

            return result;
        }

        public static int QuantityIndividual(this CardStack stack)
        {
            int result = 0;

            foreach (Data.LogRow logRow in stack.GetLogRows())
            {
                result += logRow.QuantityIndividual();
            }

            return result;
        }

        public static int Measured(this Data.LogRow logRow)
        {
            int result = 0;


            foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (individualRow.IsLengthNull()) continue;
                result++;
            }

            return result;
        }
    }
}
