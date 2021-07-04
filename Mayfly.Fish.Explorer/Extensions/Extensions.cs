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

namespace Mayfly.Fish.Explorer
{
    public static class Extensions
    {
        public static void AddCatchDescription(this Composition composition, Data data, Report report,
            string aunit, string bunit)
        {
            report.AddParagraph(
                string.Format(Resources.Reports.Community.ParagraphBio,
                composition.Count,
                data.Species.FindBySpecies(composition.MostAbundant.Name).ToHTML(),
                composition.MostAbundant.Quantity,
                composition.MostAbundant.Quantity / composition.TotalQuantity,
                report.NextTableNumber)
                );

            composition.AddCatchDescription(data, report, aunit, bunit,
                Mayfly.Service.GetFormat("spreadSheetSelectivity", "columnSelectivityLength", "G"),
                Mayfly.Service.GetFormat("spreadSheetSelectivity", "columnSelectivityMass", "G"),
                Mayfly.Service.GetFormat("spreadSheetSelectivity", "columnSelectivityB", "N3"),
                Mayfly.Service.GetFormat("spreadSheetSelectivity", "columnSelectivityNpue", "N0"),
                Mayfly.Service.GetFormat("spreadSheetSelectivity", "columnSelectivityBpue", "N3"),
                Mayfly.Service.GetFormat("spreadSheetSelectivity", "columnSelectivityNPer", "P1"),
                Mayfly.Service.GetFormat("spreadSheetSelectivity", "columnSelectivityBPer", "P1")
                );
        }

        public static void AddCatchDescription(this Composition composition, Data data, Report report,
            string aunit, string bunit,
            string lengthformat, string massformat, string bformat,
            string npueformat, string bpueformat,
            string npformat, string bpformat)
        {
            if (composition.TotalQuantity < 1)
            {
                report.AddParagraph(
                    string.Format(Resources.Reports.GearClass.Paragraph2_1, composition.Name)
                    );
                return;
            }

            Report.Table table1 = new Report.Table(string.Format(Resources.Reports.GearClass.Table2, composition.Name));

            table1.StartRow();

            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .25, 2);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Length + " *", .12, 2);
            table1.AddHeaderCell(Fish.Resources.Reports.Caption.Mass + " **", .12, 2);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Quantity, 3);
            table1.AddHeaderCell(Fish.Resources.Reports.Caption.Mass, 3);

            table1.EndRow();

            table1.StartRow();

            table1.AddHeaderCell(Resources.Reports.Common.Ind);
            table1.AddHeaderCell(aunit);
            table1.AddHeaderCell("%");

            table1.AddHeaderCell(Resources.Reports.Common.Kg);
            table1.AddHeaderCell(bunit);
            table1.AddHeaderCell("%");

            table1.EndRow();

            foreach (Category species in composition)
            {
                if (species.Quantity < 1) continue;

                table1.StartRow();                
                table1.AddCell(data.Species.FindBySpecies(species.Name).ToShortHTML());

                table1.AddCellValue(new SampleDisplay(species.LengthSample), lengthformat);
                table1.AddCellValue(new SampleDisplay(species.MassSample), massformat);

                table1.AddCellRight(species.Quantity);
                table1.AddCellRight(species.Abundance, npueformat);
                table1.AddCellRight(species.AbundanceFraction, npformat);

                table1.AddCellRight(species.Mass, bformat);
                table1.AddCellRight(species.Biomass, bpueformat);
                table1.AddCellRight(species.BiomassFraction, bpformat);
                table1.EndRow();
            }

            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Total);

            table1.AddCell();
            table1.AddCell();
            table1.AddCellRight(composition.TotalQuantity);
            table1.AddCellRight(composition.TotalAbundance, npueformat);
            table1.AddCellRight(1, npformat);
            table1.AddCellRight(composition.TotalMass, bformat);
            table1.AddCellRight(composition.TotalBiomass, bpueformat);
            table1.AddCellRight(1, bpformat);
            table1.EndRow();

            //report.AddParagraph(
            //    string.Format(Resources.Reports.Community.ParagraphCatches, report.NextTableNumber)
            //    );

            report.AddTable(table1);

            report.AddComment(string.Format(Resources.Reports.Common.FormatNotice,
                Mathematics.Resources.FormatNotice.ResourceManager.GetString(lengthformat),
                Mathematics.Resources.FormatNotice.ResourceManager.GetString(massformat)));
        }

        //public static void AddCompositionReport(this Composition compos, Report report, CardStack data,
        //    string unit, string formatn, string formatb)
        //{
        //    compos.AddCompositionReport(report, data, unit, formatn, formatb, "P1");
        //}

        //public static void AddCompositionReport(this Composition compos, Report report, CardStack data,
        //    string unit, string formatn, string formatb, string formatp)
        //{
        //    Report.Table table1 = new Report.Table(Resources.Reports.Community.TableCatches);

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

        //    report.AddComment(string.Format(Resources.Reports.Community.CatchesNotice,
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



        public static Scatterplot GetGrowthScatterplot(this AgeComposition cohort)
        {
            return cohort.GetGrowthScatterplot(false);
        }

        public static Scatterplot GetGrowthScatterplot(this AgeComposition cohort, bool isChronic)
        {
            if (isChronic && !(cohort is Cohort))
                throw new ArgumentException("Only cohorts provide chronic growth charts.");

            BivariateSample sample = new BivariateSample(
                isChronic ? "Year" : Wild.Resources.Reports.Caption.Age,
                Wild.Resources.Reports.Caption.Length);

            foreach (AgeGroup cat in cohort)
            {
                if (cat.LengthSample == null) continue;
                if (cat.LengthSample.Count == 0) continue;

                double x = cat.Age.Years;
                double y = cat.LengthSample.Mean;

                if (isChronic)
                {
                    x += ((Cohort)cohort).Birth;
                    x = new DateTime((int)x, 3, 1).ToOADate();
                }

                sample.Add(x, y);
            }

            if (sample.Count == 0) return null;

            Scatterplot result = new Scatterplot(sample, cohort.Name);
            result.IsChronic = isChronic;
            result.Properties.ShowTrend = true;
            result.Properties.SelectedApproximationType = TrendType.Growth;
            return result;
        }


        public static Scatterplot[] GetCatchCurve(this Composition cohort)
        {
            return cohort.GetCatchCurve(false);
        }

        public static Scatterplot[] GetCatchCurve(this Composition cohort, int _break)
        {
            return cohort.GetCatchCurve(false, _break);
        }

        public static Scatterplot[] GetCatchCurve(this Composition cohort, bool isChronic)
        {
            return cohort.GetCatchCurve(isChronic, cohort.IndexOf(cohort.MostAbundant));
        }

        public static Scatterplot[] GetCatchCurve(this Composition cohort, bool isChronic, int _break)
        {
            if (isChronic && !(cohort is Cohort))
                throw new ArgumentException("Only cohorts provide chronic catch curves.");

            BivariateSample unusedAbundances = new BivariateSample();
            BivariateSample usedAbundances = new BivariateSample();

            bool peakPassed = false;

            for (int i = 0; i < cohort.Count; i++)
            {
                if (cohort[i].AbundanceFraction == 0) continue;

                peakPassed |= i == _break;

                double x = ((AgeGroup)cohort[i]).Age.Years;

                if (isChronic)
                {
                    x += ((Cohort)cohort).Birth;
                    x = Fish.Explorer.Service.GetCatchDate((int)x).ToOADate();
                }

                if (peakPassed) {
                    usedAbundances.Add(x, cohort[i].Abundance);
                } else {
                    unusedAbundances.Add(x, cohort[i].Abundance);
                }
            }

            Scatterplot useful = new Scatterplot(usedAbundances, cohort.Name);
            if (isChronic) useful.Series.ChartType = SeriesChartType.Line;
            useful.IsChronic = isChronic;
            Scatterplot unused = new Scatterplot(unusedAbundances, cohort.Name + " (unused)");
            unused.IsChronic = isChronic;

            return new Scatterplot[] { unused, useful };
        }

        public static Scatterplot GetHistory(this Cohort cohort, bool isChronic)
        {
            BivariateSample data = new BivariateSample();

            for (int i = 0; i < cohort.Count; i++)
            {
                if (cohort[i].Quantity == 0) continue;

                double x = cohort[i].Age;

                if (isChronic)
                {
                    x += cohort.Birth;
                    x = Fish.Explorer.Service.GetCatchDate((int)x).ToOADate();
                }
                
                data.Add(x, (double)cohort[i].Quantity / 1000);
            }

            Scatterplot result = new Scatterplot(data, cohort.Name);
            result.Series.ChartType = SeriesChartType.Line;
            result.IsChronic = isChronic;

            return result;
        }

        public static Scatterplot GetSurvivorsHistory(this Cohort cohort, bool isChronic)
        {
            BivariateSample data = new BivariateSample();

            for (int i = 0; i < cohort.Count; i++)
            {
                if (cohort.Survivors[i].Quantity == 0) continue;

                double x = cohort[i].Age;

                if (isChronic)
                {
                    x += cohort.Birth;
                    x = Fish.Explorer.Service.GetCatchDate((int)x).ToOADate();
                }
                
                data.Add(x, (double)cohort.Survivors[i].Quantity / 1000);
            }

            Scatterplot result = new Scatterplot(data, cohort.Name);
            result.Series.ChartType = SeriesChartType.Line;
            result.IsChronic = isChronic;

            return result;
        }




        public static int GetTotalQuantity(this IEnumerable<Category> categories)
        {
            int result = 0;

            foreach (Category category in categories)
            {
                result += category.Quantity;
            }

            return result;
        }

        public static double GetTotalMass(this IEnumerable<Category> categories)
        {
            double result = 0;

            foreach (Category category in categories)
            {
                result += category.Mass;
            }

            return result;
        }

        public static double GetTotalAbundance(this IEnumerable<Category> categories)
        {
            double result = 0;

            foreach (Category category in categories)
            {
                result += category.Abundance;
            }

            return result;
        }

        public static double GetTotalBiomass(this IEnumerable<Category> categories)
        {
            double result = 0;

            foreach (Category category in categories)
            {
                result += category.Biomass;
            }

            return result;
        }


        public static void AddSpeciesMenus(this ToolStripMenuItem item, CardStack stack, EventHandler command)
        {
            for (int i = 0; i < item.DropDownItems.Count; i++ )
            {
                if (item.DropDownItems[i].Tag != null)
                {
                    item.DropDownItems.RemoveAt(i);
                    i--;
                }
            }

            if (item.DropDownItems.Count > 0 && !(item.DropDownItems[item.DropDownItems.Count - 1] is ToolStripSeparator))
            {
                item.DropDownItems.Add(new ToolStripSeparator());
            }

            foreach (Data.SpeciesRow speciesRow in stack.GetSpecies())
            {
                ToolStripItem _item = new ToolStripMenuItem();
                _item.Tag = speciesRow;
                _item.Text = speciesRow.GetFullName();
                _item.Click += command;
                item.DropDownItems.Add(_item);
            }

        }

        public static void Split(this Composition composition, int j, string name1, string name2)
        {
            composition[j].Name = name1;

            Category cat = composition[j].GetEmptyCopy();
            cat.Name = name2;
            //cat.Parent = composition;
            composition.AddCategory(cat, j + 1);
        }





        internal static Cohort GetCohort(this List<Cohort> cohorts, int birth)
        {
            foreach (Cohort cohort in cohorts)
            {
                if (cohort.Birth == birth)
                    return cohort;
            }

            return null;
        }

        internal static Cohort GetCohort(this List<Cohort> cohorts, int birth, ref AgeComposition frame)
        {
            Cohort result = cohorts.GetCohort(birth);

            if (result == null)
            {
                result = new Cohort(birth, frame);
                cohorts.Add(result);
            }

            return result;
        }

        public static List<Cohort> GetCohorts(this Composition[] annualCompositions)
        {
            Composition example = annualCompositions[0];
            AgeComposition ac = new AgeComposition(string.Empty,
                ((AgeGroup)example[0]).Age, ((AgeGroup)example.Last()).Age);
            return annualCompositions.GetCohorts(ac);
        }

        public static List<Cohort> GetCohorts(this Composition[] annualCompositions, AgeComposition example)
        {
            List<Cohort> result = new List<Cohort>();
            int youngest = example.Youngest.Years;

            foreach (Composition cross in annualCompositions)
            {
                int year = int.Parse(cross.Name);

                // Distribute to cohorts
                foreach (AgeGroup ageGroup in cross)
                {
                    int birth = year - ageGroup.Age.Years;
                    result.GetCohort(birth, ref example)[ageGroup.Age.Years - youngest] = ageGroup;
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].TotalQuantity == 0)
                {
                    result.RemoveAt(i);
                    i--;
                }
            }

            result.Sort();
            return result;
        }

        public static void AddCompositionTable(this Composition[] compositions, Report report, string tableCaption,
            string sideHeader, string separatesHeader, ValueVariant valueVariant, string format)
        {
            Report.Table table1 = new Report.Table(tableCaption);

            table1.StartRow();
            table1.AddHeaderCell(sideHeader, .15, 2);
            table1.AddHeaderCell(separatesHeader, compositions.Length);
            table1.EndRow();

            table1.StartRow();

            bool asterisk1 = false;
            bool asterisk2 = false;

            foreach (Composition composition in compositions)
            {
                bool rec = (composition is AgeComposition) && ((AgeComposition)composition).IsRecovered;
                asterisk1 |= rec;
                bool add = composition.AdditionalDistributedMass > 0;
                asterisk2 |= add;

                if (rec && add) { table1.AddHeaderCell(composition.Name + " * **"); }
                else if (rec || add) { table1.AddHeaderCell(composition.Name + " *"); }
                else { table1.AddHeaderCell(composition.Name); }
            }
            table1.EndRow();

            Composition example = compositions[0];

            for (int i = 0; i < example.Count; i++)
            {
                table1.StartRow();
                table1.AddCellValue(example[i].Name);
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

                    if (value > 0) { table1.AddCellRight(value, format); }
                    else { table1.AddCell(); }
                }
                table1.EndRow();

            }

            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Total);
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

                table1.AddCellRight(value, format);
            }
            table1.EndRow();
            report.AddTable(table1);

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


        public static void AddReport(this Composition composition, Report report, string tableheader,
            string u)
        {
            composition.AddReport(report, tableheader, u, 
                (CompositionReportContent.Absolute | CompositionReportContent.Relative/* | CompositionReportContent.Sexual*/),
                "N3", "N3", "P1", "P1");
        }

        public static void AddReport(this Composition composition, Report report, string tableheader,
            string u, CompositionReportContent content, string a, string b, string ap, string bp)
        {
            Report.Table table1 = new Report.Table(tableheader);

            table1.StartRow();
            table1.AddHeaderCell(composition.Name, .25, 2);
            table1.AddHeaderCell(Resources.Reports.Common.Npue, 2);
            table1.AddHeaderCell(Resources.Reports.Common.Bpue, 2);
            if (content.HasFlag(CompositionReportContent.Sexual)) 
                table1.AddHeaderCell(Resources.Reports.Common.SexualComposition, 2, CellSpan.Rows);
            table1.EndRow();

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Common.Ind + " / " + u + "*");
            table1.AddHeaderCell("%");
            table1.AddHeaderCell(Resources.Reports.Common.Kg + " / " + u + "*");
            table1.AddHeaderCell("%");
            table1.EndRow();

            foreach (Category category in composition)
            {
                table1.StartRow();
                table1.AddCellValue(category.Name);
                table1.AddCellRight(category.Abundance > 0 ? category.Abundance.ToString(a) : Constants.Null);
                table1.AddCellRight(category.Abundance > 0 ? category.AbundanceFraction.ToString(ap) : Constants.Null);
                table1.AddCellRight(category.Biomass > 0 ? category.Biomass.ToString(b) : Constants.Null);
                table1.AddCellRight(category.Biomass > 0 ? category.BiomassFraction.ToString(bp) : Constants.Null);
                if (content.HasFlag(CompositionReportContent.Sexual)) 
                    table1.AddCellValue(category.GetSexualComposition());
                table1.EndRow();
            }

            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Total);

            table1.AddCellRight(composition.TotalAbundance, a);
            table1.AddCellRight(1, ap);
            table1.AddCellRight(composition.TotalBiomass, b);
            table1.AddCellRight(1, bp);
            if (content.HasFlag(CompositionReportContent.Sexual)) table1.AddCell();
            table1.EndRow();
            report.AddTable(table1);
        }
    }

    public enum CompositionReportContent
    {
        Absolute,
        Relative,
        Sexual
    }
}
