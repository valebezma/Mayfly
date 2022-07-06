using Mayfly.Species;

namespace Mayfly.Wild
{
    partial class CardStack
    {
        public SpeciesSwarm GetSwarm(TaxonomicIndex.TaxonRow speciesRow)
        {
            SpeciesSwarm result = new SpeciesSwarm(speciesRow)
            {
                LengthSample = Lengths(speciesRow),
                MassSample = Masses(speciesRow),
                SamplesCount = GetLogRows(speciesRow).Length,
                Quantity = (int)Quantity(speciesRow),
                Mass = Mass(speciesRow),
                Name = speciesRow.Name
            };

            result.SetSexualComposition(
                this.Quantity(speciesRow, Sex.Juvenile),
                this.Quantity(speciesRow, Sex.Male),
                this.Quantity(speciesRow, Sex.Female));

            return result;
        }

        public SpeciesComposition GetBasicCenosisComposition()
        {
            TaxonomicIndex.TaxonRow[] species = GetSpecies();
            SpeciesComposition result = new SpeciesComposition(Resources.Reports.Caption.Species, species.Length);

            foreach (TaxonomicIndex.TaxonRow speciesRow in species)
            {
                result.AddCategory(GetSwarm(speciesRow));
            }

            result.SamplesCount = this.Count;
            return result;
        }

        public TaxonomicComposition GetBasicTaxonomicComposition(TaxonomicIndex index, TaxonomicRank rank)
        {
            TaxonomicComposition result = new TaxonomicComposition(GetBasicCenosisComposition(), index, rank, true);

            foreach (SpeciesSwarmPool pool in result)
            {
                pool.SamplesCount = GetOccurrenceCases(pool.SpeciesRows);
            }

            return result;
        }
    }
}
