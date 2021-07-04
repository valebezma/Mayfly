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

            foreach (Data.CardRow cardRow in stack)
            {
                if (cardRow.IsSamplerNull()) continue;
                if (result.Contains(cardRow.GetSamplerRow())) continue;
                result.Add(cardRow.GetSamplerRow());
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

            foreach (Data.SpeciesRow speciesRow in stack.GetSpecies())
            {
                result += stack.GetAverageAbundance(speciesRow);
            }

            return result;
        }

        public static double GetAverageAbundance(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            double result = 0.0;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.GetAbundance();
            }

            return result / (double)stack.Count;
        }

        public static double GetTotalBiomass(this CardStack stack)
        {
            double result = 0.0;

            foreach (Data.SpeciesRow speciesRow in stack.GetSpecies())
            {
                result += stack.GetAverageBiomass(speciesRow);
            }

            return result;
        }

        public static double GetAverageBiomass(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            double result = 0.0;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.GetBiomass();
            }

            return result / (double)stack.Count;
        }

        public static SpeciesComposition GetCommunityComposition(this CardStack stack)
        {
            return stack.GetCommunityComposition(Plankton.UserSettings.SpeciesIndex);
        }

        public static SpeciesComposition GetCommunityComposition(this CardStack stack, Species.SpeciesKey key)
        {
            SpeciesComposition result = stack.GetCommunityCompositionFrame();

            foreach (SpeciesSwarm category in result)
            {
                Data.SpeciesRow speciesRow = stack.Parent.Species.FindBySpecies(category.Name);

                category.DataRow = key.Species.FindBySpecies(speciesRow.Species);

                category.Quantity = (int)stack.Quantity(speciesRow);
                category.Mass = stack.Mass(speciesRow);
                category.Abundance = stack.GetAverageAbundance(speciesRow);
                category.Biomass = stack.GetAverageBiomass(speciesRow);

                category.SetSexualComposition(stack.Quantity(speciesRow, Sex.Juvenile),
                    stack.Quantity(speciesRow, Sex.Male), stack.Quantity(speciesRow, Sex.Female));

            }

            result.SamplesCount = stack.Count;
            result.Sort();

            return result;
        }
    }
}
