using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using Mayfly.Mathematics.Charts;
using Meta.Numerics.Statistics;
using System.Windows.Forms;
using Mayfly.Fish;      
using Mayfly.Fish.Explorer;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Controls;
using Mayfly.Mathematics.Statistics;
using Mayfly.Extensions;

namespace Mayfly.Fish.Explorer
{
    public static class CompositionExtensions
    {
        public static void Split(this Composition composition, int j, string name1, string name2)
        {
            composition[j].Name = name1;

            Category cat = composition[j].GetEmptyCopy();
            cat.Name = name2;
            composition.AddCategory(cat, j + 1);
        }

        public static Scatterplot[] GetCatchCurve(this Composition composition)
        {
            return composition.GetCatchCurve(false);
        }

        public static Scatterplot[] GetCatchCurve(this Composition composition, int _break)
        {
            return composition.GetCatchCurve(false, _break);
        }

        public static Scatterplot[] GetCatchCurve(this Composition composition, bool isChronic)
        {
            return composition.GetCatchCurve(isChronic, composition.IndexOf(composition.MostAbundant));
        }

        public static Scatterplot[] GetCatchCurve(this Composition composition, bool isChronic, int _break)
        {
            if (composition is Cohort &&    isChronic)
                throw new ArgumentException("Only this provide chronic catch curves.");

            BivariateSample unusedAbundances = new BivariateSample();
            BivariateSample usedAbundances = new BivariateSample();

            bool peakPassed = false;

            for (int i = 0; i < composition.Count; i++)
            {
                if (composition[i].AbundanceFraction == 0) continue;

                peakPassed |= i == _break;

                double x = ((AgeGroup)composition[i]).Age.Years;

                if (isChronic)
                {
                    x += ((Cohort)composition).Birth;
                    x = Service.GetCatchDate((int)x).ToOADate();
                }

                if (peakPassed)
                {
                    usedAbundances.Add(x, composition[i].Abundance);
                }
                else
                {
                    unusedAbundances.Add(x, composition[i].Abundance);
                }
            }

            Scatterplot useful = new Scatterplot(usedAbundances, composition.Name);
            if (isChronic) useful.Series.ChartType = SeriesChartType.Line;
            useful.IsChronic = isChronic;
            Scatterplot unused = new Scatterplot(unusedAbundances, composition.Name + " (unused)");
            unused.IsChronic = isChronic;

            return new Scatterplot[] { unused, useful };
        }

        public static string GetLocalName(this SpeciesSwarm swarm)
        {
            return Fish.UserSettings.SpeciesIndex.Species.FindBySpecies(swarm.Name).ShortNameReport;
        }






        /// <summary>
        /// Returns textual representation of its categories type
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public static string GetCategoryType(this Composition composition)
        {
            if (composition.Count == 0) return string.Empty;

            if (composition[0] is AgeGroup)
            {
                return Resources.Reports.PopulationCompositionType.Age;
            }
            else if (composition[0] is SizeClass)
            {
                return Resources.Reports.PopulationCompositionType.Length;
            }

            return string.Empty;
        }

        /// <summary>
        /// Compiles catches report
        /// </summary>
        /// <param name="data"></param>
        /// <param name="report"></param>
        public static void AppendCatchesSectionTo(this Composition composition, Report report, Data data)
        {
            report.AddSectionTitle(Resources.Reports.Section.Catches.Subtitle);

            report.AddParagraph(
                string.Format(Resources.Reports.Section.Catches.Paragraph_2,
                composition.Count,
                data.Species.FindBySpecies(composition.MostAbundant.Name).ToHTML(),
                composition.MostAbundant.Quantity,
                composition.MostAbundant.Quantity / composition.TotalQuantity,
                report.NextTableNumber)
                );

            Report.Table tableCatches = composition.GetClassicCatchesTable(data);

            //report.AddParagraph(
            //    string.Format(Resources.Reports.Cenosis.ParagraphCatches, report.NextTableNumber)
            //    );

            //report.AddTable(table1);

            //report.AddComment(string.Format(Resources.Reports.Common.FormatNotice,
            //    Mathematics.Resources.FormatNotice.ResourceManager.GetString(lengthformat),
            //    Mathematics.Resources.FormatNotice.ResourceManager.GetString(massformat)));

            report.AddTable(tableCatches);

            report.AddComment(string.Format(Resources.Reports.Common.FormatNotice,
                Mathematics.Resources.FormatNotice.ResourceManager.GetString(composition.FormatSampleLength),
                Mathematics.Resources.FormatNotice.ResourceManager.GetString(composition.FormatSampleMass)));

            //if (CatchesComposition.NonEmptyCount > 1)
            //{
            //    Data.SpeciesRow mostAbundant = Data.Parent.Species.FindBySpecies(CatchesComposition.MostAbundant.Name);
            //    Data.SpeciesRow mostAbundantByMass = Data.Parent.Species.FindBySpecies(CatchesComposition.MostAbundantByMass.Name);

            //    report.AddParagraph(
            //        string.Format(Resources.Reports.GearClass.Paragraph2,
            //            mostAbundant.ToHTML(), composition.MostAbundant.AbundanceFraction,
            //            mostAbundantByMass.ToHTML(), composition.MostAbundantByMass.BiomassFraction)
            //            );
            //}
        }

        public static void AppendPopulationSectionTo(this Composition composition, Report report, Data data,
            Data.SpeciesRow speciesRow, UnitEffort ue)
        {
            string categoryType = composition.GetCategoryType();

            report.AddSectionTitle(Resources.Reports.Section.Population.Subtitle, categoryType);

            report.AddParagraph(Resources.Reports.Section.Population.Paragraph1, categoryType, speciesRow.ToHTML(), report.NextTableNumber);

            if (composition is AgeComposition)
            {
                if (UserSettings.AgeSuggest && data.GrowthModels.GetSpecies().Contains(speciesRow.Species))
                {
                    report.AddParagraph(Resources.Reports.Section.Population.Paragraph2,
                            speciesRow.ToHTML());

                    report.AddEquation(data.GrowthModels.GetCombinedScatterplot(speciesRow.Species).Regression.GetEquation("L", "t", "N2"));

                    if (data.GrowthModels.GetExternalScatterplot(speciesRow.Species) != null)
                    {
                        report.AddParagraph(Resources.Reports.Section.Population.Paragraph3,
                            data.GrowthModels.Authors.Merge(),
                            data.GrowthModels.Description);
                    }
                }
            }

            Report.Table tableCatches = composition.GetTable(
                string.Format(Resources.Reports.Section.Population.Table1, categoryType),
                CompositionColumn.SampleSize | CompositionColumn.CPUE | CompositionColumn.Percentage);

            report.AddTable(tableCatches);

            report.AddComment(string.Format(Resources.Reports.Common.FormatNotice + Resources.Reports.Section.Population.Notice1,
                Mathematics.Resources.FormatNotice.ResourceManager.GetString(composition.FormatSampleLength.ToLowerInvariant()),
                Mathematics.Resources.FormatNotice.ResourceManager.GetString(composition.FormatSampleMass.ToLowerInvariant()),
                categoryType, ue.Unit));
        }


        /// <summary>
        /// Returns standard table with catches description
        /// </summary>
        /// <param name="composition"></param>
        /// <param name="data"></param>
        /// <param name="aunit"></param>
        /// <param name="bunit"></param>
        /// <param name="lengthformat"></param>
        /// <param name="massformat"></param>
        /// <param name="bformat"></param>
        /// <param name="npueformat"></param>
        /// <param name="bpueformat"></param>
        /// <param name="npformat"></param>
        /// <param name="bpformat"></param>
        /// <returns></returns>
        public static Report.Appendix GetTable(this Composition composition, string tableCaption, CompositionColumn content)
        {
            if (composition.Count == 0) return null;

            Report.Appendix table = new Report.Appendix(tableCaption);

            table.StartRow();

            table.AddHeaderCell(composition.Name, .25, 2);

            if (content.HasFlag(CompositionColumn.LengthSample)) table.AddHeaderCell(Wild.Resources.Reports.Caption.Length + " *", .12, 2);
            if (content.HasFlag(CompositionColumn.MassSample)) table.AddHeaderCell(Fish.Resources.Reports.Caption.Mass + " **", .12, 2);
            int u = (content.HasFlag(CompositionColumn.SampleSize) ? 1 : 0) + (content.HasFlag(CompositionColumn.CPUE) ? 1 : 0) + (content.HasFlag(CompositionColumn.Percentage) ? 1 : 0);
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Quantity, u);
            table.AddHeaderCell(Fish.Resources.Reports.Caption.Mass, u);

            table.EndRow();

            table.StartRow();

            if (content.HasFlag(CompositionColumn.SampleSize)) table.AddHeaderCell(Resources.Reports.Common.Ind);
            if (content.HasFlag(CompositionColumn.CPUE)) table.AddHeaderCell(composition.UnitAbundance + " ***");
            if (content.HasFlag(CompositionColumn.Percentage)) table.AddHeaderCell("%");

            if (content.HasFlag(CompositionColumn.SampleSize)) table.AddHeaderCell(Resources.Reports.Common.Kg);
            if (content.HasFlag(CompositionColumn.CPUE)) table.AddHeaderCell(composition.UnitBiomass + " ***");
            if (content.HasFlag(CompositionColumn.Percentage)) table.AddHeaderCell("%");

            table.EndRow();

            foreach (Category category in composition)
            {
                if (category.Quantity < 1) continue;

                table.StartRow();
                //table.AddCellSider(data.Species.FindBySpecies(category.Name).ToShortHTML());
                table.AddCellSider(category is SpeciesSwarm ? ((SpeciesSwarm)category).GetLocalName() : category.Name);

                if (content.HasFlag(CompositionColumn.LengthSample)) table.AddCellValue(new SampleDisplay(category.LengthSample), composition.FormatSampleLength);
                if (content.HasFlag(CompositionColumn.MassSample))table.AddCellValue(new SampleDisplay(category.MassSample), composition.FormatSampleMass);

                if (content.HasFlag(CompositionColumn.SampleSize)) table.AddCellRight(category.Quantity);
                if (content.HasFlag(CompositionColumn.CPUE)) table.AddCellRight(category.Abundance, composition.AbundanceFormat);
                if (content.HasFlag(CompositionColumn.Percentage)) table.AddCellRight(category.AbundanceFraction, composition.AbundanceFractionFormat);

                if (content.HasFlag(CompositionColumn.SampleSize)) table.AddCellRight(category.Mass, composition.MassFormat);
                if (content.HasFlag(CompositionColumn.CPUE)) table.AddCellRight(category.Biomass, composition.BiomassFormat);
                if (content.HasFlag(CompositionColumn.Percentage)) table.AddCellRight(category.BiomassFraction, composition.BiomassFractionFormat);
                table.EndRow();
            }

            table.StartRow();
            table.AddCell(Mayfly.Resources.Interface.Total);

            if (content.HasFlag(CompositionColumn.LengthSample)) table.AddCell();
            if (content.HasFlag(CompositionColumn.MassSample))table.AddCell();
            if (content.HasFlag(CompositionColumn.SampleSize)) table.AddCellRight(composition.TotalQuantity);
            if (content.HasFlag(CompositionColumn.CPUE)) table.AddCellRight(composition.TotalAbundance, composition.AbundanceFormat);
            if (content.HasFlag(CompositionColumn.Percentage)) table.AddCellRight(1, composition.AbundanceFractionFormat);
            if (content.HasFlag(CompositionColumn.SampleSize))table.AddCellRight(composition.TotalMass, composition.MassFormat);
            if (content.HasFlag(CompositionColumn.CPUE)) table.AddCellRight(composition.TotalBiomass, composition.BiomassFormat);
            if (content.HasFlag(CompositionColumn.Percentage)) table.AddCellRight(1, composition.BiomassFractionFormat);
            table.EndRow();

            return table;
        }

        public static Report.Appendix GetClassicCatchesTable(this Composition composition, Data data)
        {
            return composition.GetTable(
                string.Format(Resources.Reports.Section.Catches.Table_1, composition.Name),
                CompositionColumn.LengthSample | CompositionColumn.MassSample | CompositionColumn.SampleSize |
                CompositionColumn.CPUE | CompositionColumn.Percentage | CompositionColumn.SexualRatio);
        }

        public static void AddTable(this Composition[] compositions, Report report, 
            string tableCaption, string sideHeader, string separatesHeader, ValueVariant valueVariant, string format)
        {
            Report.Table table = new Report.Table(tableCaption);

            table.StartRow();
            table.AddHeaderCell(sideHeader, .15, 2);
            table.AddHeaderCell(separatesHeader, compositions.Length);
            table.EndRow();

            table.StartRow();

            bool asterisk1 = false;
            bool asterisk2 = false;

            foreach (Composition composition in compositions)
            {
                bool rec = (composition is AgeComposition) && ((AgeComposition)composition).IsRecovered;
                asterisk1 |= rec;
                bool add = composition.AdditionalDistributedMass > 0;
                asterisk2 |= add;

                if (rec && add) { table.AddHeaderCell(composition.Name + " * **"); }
                else if (rec || add) { table.AddHeaderCell(composition.Name + " *"); }
                else { table.AddHeaderCell(composition.Name); }
            }
            table.EndRow();

            Composition example = compositions[0];

            for (int i = 0; i < example.Count; i++)
            {
                table.StartRow();
                table.AddCellValue(example[i].Name);
                for (int j = 0; j < compositions.Length; j++)
                {
                    double value = 0;

                    switch (valueVariant)
                    {
                        case ValueVariant.Quantity:
                            value = compositions[j][i].Quantity;
                            break;
                        case ValueVariant.Mass:
                            value = compositions[j][i].Mass;
                            break;
                        case ValueVariant.Abundance:
                            value = compositions[j][i].Abundance;
                            break;
                        case ValueVariant.Biomass:
                            value = compositions[j][i].Biomass;
                            break;
                    }

                    if (value > 0) { table.AddCellRight(value, format); }
                    else { table.AddCell(); }
                }
                table.EndRow();

            }

            table.StartRow();
            table.AddCell(Mayfly.Resources.Interface.Total);
            for (int j = 0; j < compositions.Length; j++)
            {
                double value = 0;

                switch (valueVariant)
                {
                    case ValueVariant.Quantity:
                        value = compositions[j].TotalQuantity;
                        break;
                    case ValueVariant.Mass:
                        value = compositions[j].TotalMass;
                        break;
                    case ValueVariant.Abundance:
                        value = compositions[j].TotalAbundance;
                        break;
                    case ValueVariant.Biomass:
                        value = compositions[j].TotalBiomass;
                        break;
                }

                table.AddCellRight(value, format);
            }
            table.EndRow();
            report.AddTable(table);

            if (asterisk1 && asterisk2)
            {
                report.AddComment(Fish.Explorer.Resources.Reports.CatchComposition.Notice2 + ". *" +
                    Fish.Explorer.Resources.Reports.CatchComposition.Notice3);
            }
            else if (asterisk1)
            {
                report.AddComment(Fish.Explorer.Resources.Reports.CatchComposition.Notice2);
            }
            else if (asterisk2)
            {
                report.AddComment(Fish.Explorer.Resources.Reports.CatchComposition.Notice3);
            }
        }

        //public static void AddReport(this Composition composition, Report report, string tableheader,
        //    string u)
        //{
        //    composition.AddReport(report, tableheader, u,
        //        (CompositionReportContent.Absolute | CompositionReportContent.Relative/* | CompositionReportContent.Sexual*/),
        //        "N3", "N3", "P1", "P1");
        //}

        //public static void AddReport(this Composition composition, Report report, string tableheader, UnitEffort ue, CompositionReportContent content)
        //{
        //    Report.Table table1 = new Report.Table(tableheader);

        //    table1.StartRow();
        //    table1.AddHeaderCell(composition.Name, .25, 2);
        //    table1.AddHeaderCell(Resources.Reports.Common.Npue, 2);
        //    table1.AddHeaderCell(Resources.Reports.Common.Bpue, 2);
        //    if (content.HasFlag(CompositionReportContent.SexualRatio))
        //        table1.AddHeaderCell(Resources.Reports.Common.SexualComposition, 2, CellSpan.Rows);
        //    table1.EndRow();

        //    table1.StartRow();
        //    table1.AddHeaderCell(Resources.Reports.Common.Ind + " / " + ue.Unit + "*");
        //    table1.AddHeaderCell("%");
        //    table1.AddHeaderCell(Resources.Reports.Common.Kg + " / " + ue.Unit + "*");
        //    table1.AddHeaderCell("%");
        //    table1.EndRow();

        //    foreach (Category category in composition)
        //    {
        //        table1.StartRow();
        //        table1.AddCellValue(category.Name);
        //        table1.AddCellRight(category.Abundance > 0 ? category.Abundance.ToString(composition.npueformat) : Constants.Null);
        //        table1.AddCellRight(category.Abundance > 0 ? category.AbundanceFraction.ToString(composition.npformat) : Constants.Null);
        //        table1.AddCellRight(category.Biomass > 0 ? category.Biomass.ToString(composition.bpueformat) : Constants.Null);
        //        table1.AddCellRight(category.Biomass > 0 ? category.BiomassFraction.ToString(composition.bpformat) : Constants.Null);
        //        if (content.HasFlag(CompositionReportContent.SexualRatio))
        //            table1.AddCellValue(category.GetSexualComposition());
        //        table1.EndRow();
        //    }

        //    table1.StartRow();
        //    table1.AddCell(Mayfly.Resources.Interface.Total);

        //    table1.AddCellRight(composition.TotalAbundance, composition.npueformat);
        //    table1.AddCellRight(1, composition.npformat);
        //    table1.AddCellRight(composition.TotalBiomass, composition.bpueformat);
        //    table1.AddCellRight(1, composition.bpformat);
        //    if (content.HasFlag(CompositionReportContent.SexualRatio)) table1.AddCell();
        //    table1.EndRow();
        //    report.AddTable(table1);
        //}

        //public static void AddCompositionReport(this Composition compos, Report report, CardStack data,
        //    string unit, string formatn, string formatb)
        //{
        //    compos.AddCompositionReport(report, data, unit, formatn, formatb, "P1");
        //}

        //public static void AddCompositionReport(this Composition compos, Report report, CardStack data,
        //    string unit, string formatn, string formatb, string formatp)
        //{
        //    Report.Table table1 = new Report.Table(Resources.Reports.Cenosis.TableCatches);

        //    table1.StartRow();
        //    table1.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .25, 2);
        //    table1.AddHeaderCell(Resources.Reports.Common.Npue, 2);
        //    table1.AddHeaderCell(Resources.Reports.Common.Bpue, 2);
        //    table1.EndRow();

        //    table1.StartRow();
        //    table1.AddHeaderCell(Resources.Reports.Common.Ind + " / " + unit + "*");
        //    table1.AddHeaderCell("%");
        //    table1.AddHeaderCell(Resources.Reports.Common.Kg + " / " + unit + "*");
        //    table1.AddHeaderCell("%");
        //    table1.EndRow();

        //    foreach (Category species in compos)
        //    {
        //        Data.SpeciesRow speciesRow = data.Parent.Species.FindBySpecies(species.Name);

        //        table1.StartRow();
        //        table1.AddCell(speciesRow.ToShortHTML());
        //        table1.AddCellRight(species.Abundance > 0 ? species.Abundance.ToString(formatn) : Constants.Null);
        //        table1.AddCellRight(species.Abundance > 0 ? species.AbundanceFraction.ToString(formatp) : Constants.Null);
        //        table1.AddCellRight(species.Biomass > 0 ? species.Biomass.ToString(formatb) : Constants.Null);
        //        table1.AddCellRight(species.Biomass > 0 ? species.BiomassFraction.ToString(formatp) : Constants.Null);
        //        table1.EndRow();
        //    }

        //    table1.StartRow();
        //    table1.AddCell(Mayfly.Resources.Interface.Total);

        //    table1.AddCellRight(compos.TotalAbundance, formatn);
        //    table1.AddCellRight(1, formatp);
        //    table1.AddCellRight(compos.TotalBiomass, formatb);
        //    table1.AddCellRight(1, formatp);
        //    table1.EndRow();
        //    report.AddTable(table1);

        //    report.AddComment(string.Format(Resources.Reports.Cenosis.CatchesNotice,
        //        unit));
        //}

        //public static Report.Table GetCompositionTable(this SpeciesComposition composition)
        //{
        //    Report.Table table1 = new Report.Table(Resources.Reports.GearClass.Table1);

        //    table1.StartRow();

        //    table1.AddTableHeaderCell(Wild.Resources.Reports.Common.Species, .25, 2);
        //    table1.AddTableHeaderCell(Wild.Resources.Reports.Common.LengthUnits, .12, 2);
        //    table1.AddTableHeaderCell(Fish.Resources.Common.MassUnits, .12, 2);
        //    table1.AddTableHeaderCell(Wild.Resources.Reports.Common.QuantityUnits, 3);
        //    table1.AddTableHeaderCell(Fish.Resources.Common.MassUnits, 3);

        //    table1.EndRow();

        //    table1.StartRow();

        //    table1.AddTableHeaderCell(Resources.Reports.Common.Ind);
        //    table1.AddTableHeaderCell(gearWizard.AbundanceUnits);
        //    table1.AddTableHeaderCell("%");

        //    table1.AddTableHeaderCell(Resources.Reports.Common.Kg);
        //    table1.AddTableHeaderCell(gearWizard.BiomassUnits);
        //    table1.AddTableHeaderCell("%");

        //    table1.EndRow();

        //    foreach (Category species in composition)
        //    {
        //        Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(species.Name);

        //        table1.StartRow();
        //        table1.AddCell(speciesRow.GetReportShortPresentation());

        //        table1.AddCellValue(species.LengthSample);
        //        table1.AddCellValue(species.MassSample);

        //        table1.AddCellRight(species.Quantity);
        //        table1.AddCellRight(species.Abundance, columnSelectivityNpue.DefaultCellStyle.Format);
        //        table1.AddCellRight(species.AbundanceFraction, columnSelectivityNPer.DefaultCellStyle.Format);

        //        table1.AddCellRight(species.Mass, columnSelectivityMass.DefaultCellStyle.Format);
        //        table1.AddCellRight(species.Biomass, columnSelectivityBpue.DefaultCellStyle.Format);
        //        table1.AddCellRight(species.BiomassFraction, columnSelectivityBPer.DefaultCellStyle.Format);
        //        table1.EndRow();
        //    }

        //    table1.StartRow();
        //    table1.AddCell(Mayfly.Resources.Interface.Total);

        //    table1.AddCell();
        //    table1.AddCell();
        //    table1.AddCellRight(composition.TotalQuantity);
        //    table1.AddCellRight(composition.TotalAbundance, columnSelectivityNpue.DefaultCellStyle.Format);
        //    table1.AddCellRight(1, columnSelectivityNPer.DefaultCellStyle.Format);
        //    table1.AddCellRight(composition.TotalMass, columnSelectivityMass.DefaultCellStyle.Format);
        //    table1.AddCellRight(composition.TotalBiomass, columnSelectivityBpue.DefaultCellStyle.Format);
        //    table1.AddCellRight(1, columnSelectivityBPer.DefaultCellStyle.Format);
        //    table1.EndRow();
        //}
    }
}