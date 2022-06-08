using Mayfly.Species;

namespace Mayfly.Wild
{
    partial class CardStack
    {
        public SpeciesSwarm GetSwarm(SpeciesKey.SpeciesRow speciesRow)
        {
            SpeciesSwarm result = new SpeciesSwarm(speciesRow)
            {
                LengthSample = Lengths(speciesRow),
                MassSample = Masses(speciesRow),
                SamplesCount = GetLogRows(speciesRow).Length,
                Quantity = (int)Quantity(speciesRow),
                Mass = Mass(speciesRow),
                Name = speciesRow.Species
            };

            result.SetSexualComposition(
                this.Quantity(speciesRow, Sex.Juvenile),
                this.Quantity(speciesRow, Sex.Male),
                this.Quantity(speciesRow, Sex.Female));

            return result;
        }

        public SpeciesComposition GetBasicCenosisComposition()
        {
            SpeciesKey.SpeciesRow[] species = GetSpeciesSorted();
            SpeciesComposition result = new SpeciesComposition(Resources.Reports.Caption.Species, species.Length);

            foreach (SpeciesKey.SpeciesRow speciesRow in species)
            {
                result.AddCategory(GetSwarm(speciesRow));
            }

            result.SamplesCount = this.Count;
            return result;
        }

        public TaxaComposition GetBasicTaxonomicComposition(SpeciesKey.BaseRow baseRow)
        {
            TaxaComposition result = new TaxaComposition(GetBasicCenosisComposition(), baseRow, true);

            foreach (SpeciesSwarmPool pool in result)
            {
                pool.SamplesCount = GetOccurrenceCases(pool.SpeciesRows);
            }

            return result;
        }
    }
}
