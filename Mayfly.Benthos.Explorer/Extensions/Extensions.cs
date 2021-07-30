using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using Mayfly.Mathematics.Charts;
using Meta.Numerics.Statistics;
using System.Windows.Forms;
using Mayfly.Benthos;
using Mayfly.Benthos.Explorer;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Controls;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Benthos.Explorer
{
    public static class Extensions
    {
        public static void AddCompositionReport(this Composition compos, Report report, CardStack data,
            string formatn, string formatb)
        {
            compos.AddCompositionReport(report, data, formatn, formatb, "P1");
        }

        public static void AddCompositionReport(this Composition compos, Report report, CardStack data,
            string formatn, string formatb, string formatp)
        {
            Report.Table table1 = new Report.Table(Resources.Reports.Cenosis.Header2);

            table1.StartRow();
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .5, 2);
            table1.AddHeaderCell(Benthos.Resources.Reports.Caption.Abundance, 2);
            table1.AddHeaderCell(Benthos.Resources.Reports.Caption.Biomass, 2);
            table1.EndRow();

            table1.StartRow();
            table1.AddHeaderCell(Benthos.Resources.Reports.Caption.AbundanceUnits);
            table1.AddHeaderCell("%");
            table1.AddHeaderCell(Benthos.Resources.Reports.Caption.BiomassUnits);
            table1.AddHeaderCell("%");
            table1.EndRow();

            foreach (Category species in compos)
            {
                table1.StartRow();
                table1.AddCell(species.Name);
                table1.AddCellRight(species.Abundance, formatn);
                table1.AddCellRight(species.AbundanceFraction, formatp);
                table1.AddCellRight(species.Biomass, formatb);
                table1.AddCellRight(species.BiomassFraction, formatp);
                table1.EndRow();
            }

            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Total);

            table1.AddCellRight(compos.TotalAbundance, formatn);
            table1.AddCellRight(1, formatp);
            table1.AddCellRight(compos.TotalBiomass, formatb);
            table1.AddCellRight(1, formatp);
            table1.EndRow();
            report.AddTable(table1);
        }

        //public static void AddCompositionTable(this Composition[] compositions, Report report, 
        //    string sideHeader, string separatesHeader, ValueVariant valueVariant, string format)
        //{
        //    table1.StartRow();
        //    report.AddHeaderCell(sideHeader, CellSpan.Rows, 2, .2);
        //    report.AddHeaderCell(separatesHeader, CellSpan.Columns, compositions.Length);
        //    table1.EndRow();

        //    table1.StartRow();

        //    bool asterisk1 = false;
        //    bool asterisk2 = false;

        //    foreach (Composition composition in compositions)
        //    {
        //        bool rec = (composition is AgeComposition) && ((AgeComposition)composition).IsRecovered;
        //        asterisk1 |= rec;
        //        bool add = composition.AdditionalDistributedMass > 0;
        //        asterisk2 |= add;

        //        if (rec && add) { report.AddHeaderCell(composition.Name + " * **"); }
        //        else if (rec || add) { report.AddHeaderCell(composition.Name + " *"); }
        //        else { report.AddHeaderCell(composition.Name); }
        //    }
        //    table1.EndRow();

        //    Composition example = compositions[0];

        //    for (int i = 0; i < example.Count; i++)
        //    {
        //        table1.StartRow();
        //        table1.AddCellValue(example[i].Name);
        //        for (int j = 0; j < compositions.Length; j++)
        //        {
        //            double value = 0;

        //            switch (valueVariant)
        //            {
        //                case ValueVariant.Quantity:
        //                    value = compositions[j][i].Quantity;
        //                    break;
        //                case ValueVariant.Mass:
        //                    value = compositions[j][i].Mass;
        //                    break;
        //                case ValueVariant.Abundance:
        //                    value = compositions[j][i].Abundance;
        //                    break;
        //                case ValueVariant.Biomass:
        //                    value = compositions[j][i].Biomass;
        //                    break;
        //            }

        //            if (value > 0) { table1.AddCellRight(value, format); }
        //            else { table1.AddCell(); }
        //        }
        //        table1.EndRow();

        //    }

        //    table1.StartRow();
        //    table1.AddCell(Mayfly.Resources.Interface.Total);
        //    for (int j = 0; j < compositions.Length; j++)
        //    {
        //        double value = 0;

        //        switch (valueVariant)
        //        {
        //            case ValueVariant.Quantity:
        //                value = compositions[j].TotalQuantity;
        //                break;
        //            case ValueVariant.Mass:
        //                value = compositions[j].TotalMass;
        //                break;
        //            case ValueVariant.Abundance:
        //                value = compositions[j].TotalAbundance;
        //                break;
        //            case ValueVariant.Biomass:
        //                value = compositions[j].TotalBiomass;
        //                break;
        //        }

        //        table1.AddCellRight(value, format);
        //    }
        //    table1.EndRow();
        //    report.AddTable(table1);

        //    if (asterisk1 && asterisk2)
        //    {
        //        report.AddComment(Fish.Explorer.Resources.Reports.CatchComposition.Notice2 + ". *" +
        //            Fish.Explorer.Resources.Reports.CatchComposition.Notice3);
        //    }
        //    else if (asterisk1)
        //    {
        //        report.AddComment(Fish.Explorer.Resources.Reports.CatchComposition.Notice2);
        //    }
        //    else if (asterisk2)
        //    {
        //        report.AddComment(Fish.Explorer.Resources.Reports.CatchComposition.Notice3);
        //    }
        //}


        //public static void AddReport(this Composition composition, Report report, string tableheader,
        //    string u)
        //{
        //    composition.AddReport(report, tableheader, u, 
        //        (CompositionReportContent.Absolute | CompositionReportContent.Relative/* | CompositionReportContent.Sexual*/),
        //        "N3", "N3", "P1", "P1");
        //}

        //public static void AddReport(this Composition composition, Report report, string tableheader,
        //    string u, CompositionReportContent content, string a, string b, string ap, string bp)
        //{
        //    Report.Table table1 = new Report.Table(tableheader);

        //    table1.StartRow();
        //    report.AddHeaderCell(composition.Name, CellSpan.Rows, 2, 0.2);
        //    report.AddHeaderCell(Mayfly.Benthos.Explorer.Resources.Reports.Common.Npue, CellSpan.Columns, 2);
        //    report.AddHeaderCell(Mayfly.Benthos.Explorer.Resources.Reports.Common.Bpue, CellSpan.Columns, 2);
        //    if (content.HasFlag(CompositionReportContent.Sexual)) 
        //        report.AddHeaderCell(Mayfly.Benthos.Explorer.Resources.Reports.Common.SexualComposition, CellSpan.Rows, 2);
        //    table1.EndRow();

        //    table1.StartRow();
        //    report.AddHeaderCell(Mayfly.Benthos.Explorer.Resources.Reports.Common.Ind + " / " + u + "*");
        //    report.AddHeaderCell("%");
        //    report.AddHeaderCell(Mayfly.Benthos.Explorer.Resources.Reports.Common.Kg + " / " + u + "*");
        //    report.AddHeaderCell("%");
        //    table1.EndRow();

        //    foreach (Category category in composition)
        //    {
        //        table1.StartRow();
        //        table1.AddCellValue(category.Name);
        //        table1.AddCellRight(category.Abundance, a);
        //        table1.AddCellRight(category.AbundanceFraction, ap);
        //        table1.AddCellRight(category.Biomass, b);
        //        table1.AddCellRight(category.BiomassFraction, bp);
        //        if (content.HasFlag(CompositionReportContent.Sexual)) 
        //            table1.AddCellValue(category.GetSexualComposition());
        //        table1.EndRow();
        //    }

        //    table1.StartRow();
        //    table1.AddCell(Mayfly.Resources.Interface.Total);

        //    table1.AddCellRight(composition.TotalAbundance, a);
        //    table1.AddCellRight(1, ap);
        //    table1.AddCellRight(composition.TotalBiomass, b);
        //    table1.AddCellRight(1, bp);
        //    if (content.HasFlag(CompositionReportContent.Sexual)) table1.AddCell();
        //    table1.EndRow();
        //    report.AddTable(table1);
        //}
    }
}
