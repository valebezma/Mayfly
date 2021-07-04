using Mayfly.Extensions;
using Mayfly.Wild;

namespace Mayfly.Plankton.Explorer
{
    partial class CardStackExtensions
    {
        public static void AddCommon(this CardStack stack, Report report)
        {
            stack.AddCommon(report, stack.GetSamplersList().Merge());
        }

        //public static void AddBrief(this CardStack stack, Report report, SpeciesKey.BaseRow baseRow)
        //{
        //    stack.AddCommon(report);

        //    SpeciesComposition spc = stack.GetCommunityComposition();

        //    Report.Table table1 = new Report.Table(Resources.Reports.Community.Header1);

        //    table1.StartRow();
        //    table1.AddCellPrompt(Resources.Reports.Community.SpeciesCount,
        //        stack.SpeciesWealth);
        //    table1.EndRow();
        //    table1.StartRow();
        //    table1.AddCellPrompt(Resources.Reports.Community.Dominants,
        //        spc.GetDominantNames().Merge(" + "));
        //    table1.EndRow();
        //    report.AddTable(table1);

        //    Report.Table table2 = new Report.Table(Resources.Reports.Community.Header2);

        //    table2.StartRow();
        //    table2.AddHeaderCell(baseRow == null ? Wild.Resources.Reports.Common.Species : baseRow.Base, .5);
        //    table2.AddHeaderCell(string.Format("{0}, {1}",
        //        Benthos.Resources.Common.Abundance, Benthos.Resources.Common.AbundanceUnits));
        //    table2.AddHeaderCell(string.Format("{0}, {1}",
        //        Benthos.Resources.Common.Biomass, Benthos.Resources.Common.BiomassUnits));
        //    table2.EndRow();

        //    if (baseRow != null)
        //    {
        //        Composition tax = stack.GetTaxonomicComposition(baseRow);

        //        Category coarse = tax.Find((c) => {
        //                return c.Name.Contains(UserSettings.FourageMark);
        //            });

        //        foreach (Category species in tax)
        //        {
        //            //if (species == coarse) continue;

        //            table2.StartRow();
        //            table2.AddCell(species.Name);
        //            table2.AddCellRight(species.Abundance, "N0");
        //            table2.AddCellRight(species.Biomass, "N2");
        //            table2.EndRow();
        //        }

        //        table2.StartRow();
        //        table2.AddCell(Resources.Reports.Community.Total);
        //        table2.AddCellRight(tax.TotalAbundance, "N0");
        //        table2.AddCellRight(tax.TotalBiomass, "N2");
        //        table2.EndRow();

        //        table2.StartRow();
        //        table2.AddCell(Resources.Reports.Community.TotalFeed);
        //        table2.AddCellRight(tax.TotalAbundance - (coarse == null ? 0 : coarse.Abundance), "N0");
        //        table2.AddCellRight(tax.TotalBiomass - (coarse == null ? 0 : coarse.Biomass), "N2");
        //        table2.EndRow();
        //    }
        //    else
        //    {

        //        foreach (Category species in spc)
        //        {
        //            //if (species == coarse) continue;

        //            table2.StartRow();
        //            table2.AddCell(species.Name);
        //            table2.AddCellRight(species.Abundance, "N0");
        //            table2.AddCellRight(species.Biomass, "N2");
        //            table2.EndRow();
        //        }

        //        table2.StartRow();
        //        table2.AddCell(Resources.Reports.Community.Total);
        //        table2.AddCellRight(spc.TotalAbundance, "N0");
        //        table2.AddCellRight(spc.TotalBiomass, "N2");
        //        table2.EndRow();

        //    }

        //    report.AddTable(table2);

        //    //spc.AddCompositionReport(report, this, "N0", "N2");
        //}
    }
}
