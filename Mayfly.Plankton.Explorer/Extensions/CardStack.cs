using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;

namespace Mayfly.Plankton.Explorer
{
    public static partial class CardStackExtensions
    {
        public static Samplers.SamplerRow[] GetSamplers(this CardStack stack)
        {
            List<Samplers.SamplerRow> result = new List<Samplers.SamplerRow>();

            foreach (Wild.Survey.CardRow cardRow in stack)
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

            foreach (Wild.Survey.DefinitionRow speciesRow in stack.GetSpecies())
            {
                result += stack.GetAverageAbundance(speciesRow);
            }

            return result;
        }

        public static double GetAverageAbundance(this CardStack stack, Wild.Survey.DefinitionRow speciesRow)
        {
            double result = 0.0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.GetAbundance();
            }

            return result / (double)stack.Count;
        }

        public static double GetTotalBiomass(this CardStack stack)
        {
            double result = 0.0;

            foreach (Wild.Survey.DefinitionRow speciesRow in stack.GetSpecies())
            {
                result += stack.GetAverageBiomass(speciesRow);
            }

            return result;
        }

        public static double GetAverageBiomass(this CardStack stack, Wild.Survey.DefinitionRow speciesRow)
        {
            double result = 0.0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.GetBiomass();
            }

            return result / (double)stack.Count;
        }

        public static SpeciesComposition GetCenosisComposition(this CardStack stack)
        {
            return stack.GetCenosisComposition(Plankton.UserSettings.SpeciesIndex);
        }

        public static SpeciesComposition GetCenosisComposition(this CardStack stack, Species.TaxonomicIndex key)
        {
            SpeciesComposition result = stack.GetBasicCenosisComposition();

            foreach (SpeciesSwarm category in result)
            {
                Wild.Survey.DefinitionRow speciesRow = stack.Parent.Definition.FindByName(category.Name);

                category.SpeciesRow = speciesRow;

                category.Quantity = (int)stack.Quantity(speciesRow);
                category.Mass = stack.Mass(speciesRow);
                category.Abundance = stack.GetAverageAbundance(speciesRow);
                category.Biomass = stack.GetAverageBiomass(speciesRow);

                category.SetSexualComposition(
                    stack.Quantity(speciesRow, Sex.Juvenile),
                    stack.Quantity(speciesRow, Sex.Male),
                    stack.Quantity(speciesRow, Sex.Female));

            }

            result.SamplesCount = stack.Count;
            result.Sort();

            return result;
        }
    }
}
