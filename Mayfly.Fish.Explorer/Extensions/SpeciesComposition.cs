using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    public static class SpeciesCompositionExtensions
    {
        /// <summary>
        /// Compiles catches report
        /// </summary>
        /// <param name="data"></param>
        /// <param name="report"></param>
        public static void AppendCatchesSectionTo(this Composition composition, Report report)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Catches.Header);

            report.AddParagraph(
                string.Format(Resources.Reports.Sections.Catches.ParagraphDescription,
                composition.Count,
                composition.MostAbundant,
                composition.MostAbundant.Quantity,
                composition.MostAbundant.Quantity / composition.TotalQuantity,
                report.NextTableNumber)
                );

            //report.AddParagraph(
            //    string.Format(Resources.Reports.Cenosis.ParagraphCatches, report.NextTableNumber)
            //    );

            //if (CatchesComposition.NonEmptyCount > 1)
            //{
            //    Data.SpeciesRow mostAbundant = Data.Parent.Species.FindBySpecies(CatchesComposition.MostAbundant.Name);
            //    Data.SpeciesRow mostAbundantByMass = Data.Parent.Species.FindBySpecies(CatchesComposition.MostAbundantByMass.Name);

            //    report.AddParagraph(
            //        string.Format(Resources.Reports.GearClass.Paragraph2,
            //            mostAbundant.KeyRecord.FullNameReport, composition.MostAbundant.AbundanceFraction,
            //            mostAbundantByMass.KeyRecord.FullNameReport, composition.MostAbundantByMass.BiomassFraction)
            //  
            //} 

            Report.Table tableCatches = composition.GetStandardCatchesTable(Resources.Reports.Sections.Catches.Table);
            report.AddTable(tableCatches);
        }

        /// <summary>
        /// Compiles cenosis report using UI formats
        /// </summary>
        /// <param name="data"></param>
        /// <param name="report"></param>
        /// <param name="samplerType"></param>
        /// <param name="ue"></param>
        public static void AppendCenosisSectionTo(this SpeciesComposition composition, Report report, FishSamplerType samplerType, UnitEffort ue)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Cenosis.Header);

            report.AddParagraph(Resources.Reports.Sections.Cenosis.Paragraph1, samplerType.ToDisplay());

            report.AddEquation("\\text{" + Wild.Resources.Reports.Caption.Abundance + "} = \\frac{\\overline{CPUE}}{q}", ",");

            report.AddParagraph(Resources.Reports.Sections.Cenosis.Paragraph2, report.NextTableNumber);

            Report.Table table = new Report.Table(Resources.Reports.Sections.Cenosis.Table);

            table.StartRow();
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .15, 2);
            table.AddHeaderCell(Resources.Reports.Caption.Catchability, 2, CellSpan.Rows);
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Abundance, 2);
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Biomass, 2);
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Occurrence, 2, CellSpan.Rows);
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Dominance + 
                table.AddNotice(string.Format(Resources.Reports.Notice.Dominance, UserSettings.DominanceIndexName)).Holder, 2, CellSpan.Rows);
            table.EndRow();

            table.StartRow();
            table.AddHeaderCell(Resources.Reports.Common.Ind + " / " + ue.Unit + (ue.Variant == ExpressionVariant.Efforts ? 
                table.AddNotice(Resources.Reports.Notice.EffortHabitat, ue.Unit, ue.UnitCost).Holder : string.Empty));
            table.AddHeaderCell("%");
            table.AddHeaderCell(Resources.Reports.Common.Kg + " / " + ue.Unit + (ue.Variant == ExpressionVariant.Efforts ?
                table.AddNotice(Resources.Reports.Notice.EffortHabitat, ue.Unit, ue.UnitCost).Holder : string.Empty));
            table.AddHeaderCell("%");
            table.EndRow();

            foreach (SpeciesSwarm species in composition)
            {
                table.StartRow();
                table.AddCell(species);
                table.AddCellRight(Service.GetCatchability(samplerType, species.Name), "N2");
                table.AddCellRight(species.Abundance, composition.AbundanceFormat);
                table.AddCellRight(species.AbundanceFraction, composition.AbundanceFractionFormat);
                table.AddCellRight(species.Biomass, composition.BiomassFormat);
                table.AddCellRight(species.BiomassFraction, composition.BiomassFractionFormat);
                table.AddCellRight(species.Occurrence, composition.OccuranceFormat);
                table.AddCellRight(species.Dominance, composition.DominanceFormat);
                table.EndRow();
            }

            table.StartRow();
            table.AddCell(Mayfly.Resources.Interface.Total);
            table.AddCell();
            table.AddCellRight(composition.TotalAbundance, composition.AbundanceFormat);
            table.AddCellRight(1, composition.AbundanceFractionFormat);
            table.AddCellRight(composition.TotalBiomass, composition.BiomassFormat);
            table.AddCellRight(1, composition.BiomassFractionFormat);
            table.AddCell();
            table.AddCell();
            table.EndRow();            

            if (ue.Variant == ExpressionVariant.Efforts)
            {
                ;
                ;
            }

            report.AddTable(table);

            report.AddParagraph(Resources.Reports.Sections.Cenosis.Paragraph3,
                Wild.Resources.Interface.Diversity.ResourceManager.GetString(Wild.UserSettings.Diversity.ToString()),
                composition.Diversity);
        }
    }
}
