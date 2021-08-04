namespace Mayfly.Wild
{
    partial class CardStack
    {
        public SpeciesSwarm GetSwarm(Data.SpeciesRow speciesRow)
        {
            return new SpeciesSwarm(speciesRow)
            {
                LengthSample = Lengths(speciesRow),
                MassSample = Masses(speciesRow),
                SamplesCount = GetLogRows(speciesRow).Length,
                Quantity = (int)Quantity(speciesRow),
                Mass = Mass(speciesRow),
                Juveniles = this.Quantity(speciesRow, Sex.Juvenile),
                Males = this.Quantity(speciesRow, Sex.Male),
                Females = this.Quantity(speciesRow, Sex.Female)                
            };
        }

        public SpeciesComposition GetBasicCenosisComposition()
        {
            SpeciesComposition result = new SpeciesComposition(Resources.Reports.Caption.Species, GetSpecies().Length);

            foreach (Data.SpeciesRow speciesRow in GetSpecies())
            {
                result.AddCategory(GetSwarm(speciesRow));
            }

            result.SamplesCount = this.Count;
            result.Sort();
            return result;
        }

        public TaxaComposition GetTaxonomicComposition(Species.SpeciesKey.BaseRow baseRow)
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
