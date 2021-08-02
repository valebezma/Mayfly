using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    public static class SpeciesCompositionExtensions
    {
        /// <summary>
        /// Compiles cenosis report using UI formats
        /// </summary>
        /// <param name="data"></param>
        /// <param name="report"></param>
        /// <param name="samplerType"></param>
        /// <param name="ue"></param>
        public static void AppendCenosisSectionTo(this SpeciesComposition composition, Report report, FishSamplerType samplerType, UnitEffort ue)
        {
            report.AddSectionTitle(Resources.Reports.Section.Cenosis.Subtitle);

            report.AddParagraph(Resources.Reports.Section.Cenosis.Paragraph_1, samplerType.ToDisplay());

            report.AddEquation("\\text{Abundance} = \\frac{\\overline{CPUE}}{q}", ",");

            report.AddParagraph(Resources.Reports.Section.Cenosis.Paragraph_2, report.NextTableNumber);

            Report.Table table = new Report.Table(Resources.Reports.Section.Cenosis.Table_1);

            table.StartRow();
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .25, 2);
            table.AddHeaderCell(Resources.Reports.Section.Cenosis.ColumnCatchability, 2, CellSpan.Rows);
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Abundance, 2);
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Biomass, 2);
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Occurrence, 2, CellSpan.Rows);
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Dominance + "*", 2, CellSpan.Rows);
            table.EndRow();

            table.StartRow();
            table.AddHeaderCell(Resources.Reports.Common.Ind + " / " + ue.Unit + (ue.Variant == ExpressionVariant.Efforts ? "**" : string.Empty));
            table.AddHeaderCell("%");
            table.AddHeaderCell(Resources.Reports.Common.Kg + " / " + ue.Unit + (ue.Variant == ExpressionVariant.Efforts ? "**" : string.Empty));
            table.AddHeaderCell("%");
            table.EndRow();

            foreach (SpeciesSwarm species in composition)
            {
                table.StartRow();
                table.AddCell(species.GetLocalName());
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

            report.AddTable(table);

            report.AddComment(string.Format(Resources.Reports.Section.Cenosis.Notice_1,
                UserSettings.DominanceIndexName) +
                (ue.Variant == ExpressionVariant.Efforts ?
                string.Format(Resources.Reports.Section.Cenosis.Notice_2,
                    ue.Unit, ue.UnitCost, Resources.Reports.Common.m3) : string.Empty));

            report.AddParagraph(Resources.Reports.Section.Cenosis.Paragraph_3,
                Wild.Resources.Interface.Diversity.ResourceManager.GetString(Wild.UserSettings.Diversity.ToString()),
                composition.Diversity);
        }
    }
}
