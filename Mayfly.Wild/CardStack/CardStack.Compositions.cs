﻿namespace Mayfly.Wild
{
    partial class CardStack
    {
        public SpeciesComposition GetCommunityCompositionFrame()
        {
            SpeciesComposition result = new SpeciesComposition(string.Empty, this.GetSpecies().Length);

            foreach (Data.SpeciesRow speciesRow in this.GetSpecies())
            {
                SpeciesSwarm category = new SpeciesSwarm(speciesRow.Species);
                category.LengthSample = this.Lengths(speciesRow);
                category.MassSample = this.Masses(speciesRow);
                category.SamplesCount = this.GetLogRows(speciesRow).Length;
                result.AddCategory(category);
            }

            return result;
        }

        public SpeciesComposition GetCommunityComposition(Data addData, SpeciesComposition example)
        {
            SpeciesComposition result = this.GetCommunityCompositionFrame(example);

            foreach (Category category in result)
            {
                Data.SpeciesRow speciesRow = Parent.Species.FindBySpecies(category.Name);

                if (speciesRow == null) continue;

                category.Quantity = (int)this.Quantity(speciesRow);
                category.Mass = this.Mass(speciesRow);
                category.SetSexualComposition(this.Quantity(speciesRow, Sex.Juvenile),
                    this.Quantity(speciesRow, Sex.Male), this.Quantity(speciesRow, Sex.Female));
                category.SamplesCount = this.GetLogRows(speciesRow).Length;
            }

            result.SamplesCount = this.Count;

            return result;
        }

        public SpeciesComposition GetCommunityCompositionFrame(SpeciesComposition example)
        {
            SpeciesComposition result = new SpeciesComposition(string.Empty, example.Count);

            foreach (Category categoy in example)
            {
                Category category = new Category(categoy.Name);
                result.AddCategory(category);
            }

            return result;
        }



        public TaxaComposition GetTaxonomicCompositionFrame(Species.SpeciesKey.BaseRow baseRow)
        {
            SpeciesComposition spc = GetCommunityCompositionFrame();
            TaxaComposition result = new TaxaComposition(spc, baseRow, true);
            return result;
        }
    }
}