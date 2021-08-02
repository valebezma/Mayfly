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

        public static void SetLines(this Composition composition, DataGridViewColumn gridColumn)
        {
            gridColumn.HeaderText = composition.Name;
            gridColumn.DataGridView.Rows.Clear();

            for (int i = 0; i < composition.Count; i++)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(gridColumn.DataGridView);
                gridRow.Cells[gridColumn.Index].Value = composition[i];
                gridRow.Height = gridColumn.DataGridView.RowTemplate.Height;
                gridColumn.DataGridView.Rows.Add(gridRow);
            }
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
        /// Construct table with samples description according to specified columns list, uncluding notice below
        /// </summary>
        /// <param name="composition"></param>
        /// <param name="tableCaption"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Report.Table GetTable(this Composition composition, string tableCaption, CompositionColumn content, string siderCaption)
        {
            if (composition.Count == 0) return null;

            Report.Table table = new Report.Table(tableCaption);

            table.StartRow();

            table.AddHeaderCell(siderCaption, .25, 2);

            if (content.HasFlag(CompositionColumn.LengthSample))
            {
                table.AddHeaderCell(Wild.Resources.Reports.Caption.Length + Constants.NoticeHolder, .12, 2);
                table.AddNotice(Resources.Reports.Notice.SampleFormat, 
                    Mathematics.Resources.FormatNotice.ResourceManager.GetString(Mayfly.Service.StripNumbers(composition.FormatSampleLength.ToLowerInvariant())));
            }

            if (content.HasFlag(CompositionColumn.MassSample))
            {
                table.AddHeaderCell(Fish.Resources.Reports.Caption.Mass + Constants.NoticeHolder, .12, 2);
                table.AddNotice(Resources.Reports.Notice.SampleFormat, 
                    Mathematics.Resources.FormatNotice.ResourceManager.GetString(Mayfly.Service.StripNumbers(composition.FormatSampleMass.ToLowerInvariant())));
            }

            int u = (content.HasFlag(CompositionColumn.SampleSize) ? 1 : 0) + (content.HasFlag(CompositionColumn.CPUE) ? 1 : 0) + (content.HasFlag(CompositionColumn.Percentage) ? 1 : 0);
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Quantity, u);
            table.AddHeaderCell(Fish.Resources.Reports.Caption.Mass, u);
            if (content.HasFlag(CompositionColumn.SexualRatio))
            {
                table.AddHeaderCell(Resources.Reports.Caption.SexualRatio + Constants.NoticeHolder, .12, 2);
                table.AddNotice(Resources.Reports.Notice.SexualLegend, Sex.Juvenile, Sex.Male, Sex.Female);
            }
            table.EndRow();

            table.StartRow();

            if (content.HasFlag(CompositionColumn.SampleSize)) table.AddHeaderCell(Resources.Reports.Common.Ind);
            if (content.HasFlag(CompositionColumn.CPUE))
            {
                table.AddHeaderCell(string.Format("{0}/{1}{2}", Resources.Reports.Common.Ind, composition.Unit, Constants.NoticeHolder));
                table.AddNotice(Resources.Reports.Notice.NPUE, composition.Unit, composition.Unit);
            }
            if (content.HasFlag(CompositionColumn.Percentage)) table.AddHeaderCell("%");

            if (content.HasFlag(CompositionColumn.SampleSize)) table.AddHeaderCell(Resources.Reports.Common.Kg);
            if (content.HasFlag(CompositionColumn.CPUE))
            {
                table.AddHeaderCell(string.Format("{0}/{1}{2}", Resources.Reports.Common.Kg, composition.Unit, Constants.NoticeHolder));
                table.AddNotice(Resources.Reports.Notice.BPUE, composition.Unit, composition.Unit);
            }
            if (content.HasFlag(CompositionColumn.Percentage)) table.AddHeaderCell("%");

            table.EndRow();

            foreach (Category category in composition)
            {
                if (category.Quantity < 1) continue;

                table.StartRow();
                table.AddCellSider(category);

                if (content.HasFlag(CompositionColumn.LengthSample)) table.AddCellValue(new SampleDisplay(category.LengthSample), composition.FormatSampleLength);
                if (content.HasFlag(CompositionColumn.MassSample)) table.AddCellValue(new SampleDisplay(category.MassSample), composition.FormatSampleMass);

                if (content.HasFlag(CompositionColumn.SampleSize)) table.AddCellRight(category.Quantity);
                if (content.HasFlag(CompositionColumn.CPUE)) table.AddCellRight(category.Abundance, composition.AbundanceFormat);
                if (content.HasFlag(CompositionColumn.Percentage)) table.AddCellRight(category.AbundanceFraction, composition.AbundanceFractionFormat);

                if (content.HasFlag(CompositionColumn.SampleSize)) table.AddCellRight(category.Mass, composition.MassFormat);
                if (content.HasFlag(CompositionColumn.CPUE)) table.AddCellRight(category.Biomass, composition.BiomassFormat);
                if (content.HasFlag(CompositionColumn.Percentage)) table.AddCellRight(category.BiomassFraction, composition.BiomassFractionFormat);

                if (content.HasFlag(CompositionColumn.SexualRatio)) table.AddCellValue(category.GetSexualComposition());


                table.EndRow();
            }

            table.StartRow();
            table.AddCell(Mayfly.Resources.Interface.Total);

            if (content.HasFlag(CompositionColumn.LengthSample)) table.AddCell();
            if (content.HasFlag(CompositionColumn.MassSample)) table.AddCell();
            if (content.HasFlag(CompositionColumn.SampleSize)) table.AddCellRight(composition.TotalQuantity);
            if (content.HasFlag(CompositionColumn.CPUE)) table.AddCellRight(composition.TotalAbundance, composition.AbundanceFormat);
            if (content.HasFlag(CompositionColumn.Percentage)) table.AddCellRight(1, composition.AbundanceFractionFormat);
            if (content.HasFlag(CompositionColumn.SampleSize)) table.AddCellRight(composition.TotalMass, composition.MassFormat);
            if (content.HasFlag(CompositionColumn.CPUE)) table.AddCellRight(composition.TotalBiomass, composition.BiomassFormat);
            if (content.HasFlag(CompositionColumn.Percentage)) table.AddCellRight(1, composition.BiomassFractionFormat);
            if (content.HasFlag(CompositionColumn.SexualRatio)) table.AddCell();
            table.EndRow();

            return table;
        }

        /// <summary>
        /// Construct table with samples description using standard columns set
        /// </summary>
        /// <param name="composition"></param>
        /// <param name="tableCaption"></param>
        /// <returns></returns>
        public static Report.Table GetStandardCatchesTable(this Composition composition, string tableCaption, string siderCaption)
        {
            return composition.GetTable(tableCaption,
                CompositionColumn.LengthSample | CompositionColumn.MassSample | CompositionColumn.SampleSize |
                CompositionColumn.CPUE | CompositionColumn.Percentage | CompositionColumn.SexualRatio,
                siderCaption);
        }
        public static Report.Table GetStandardCatchesTable(this Composition composition, string tableCaption)
        {
            return composition.GetStandardCatchesTable(tableCaption);
        }



        public static void AppendPopulationSectionTo(this Composition composition, Report report, Data.SpeciesRow speciesRow, Data data)
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
                CompositionColumn.SampleSize | CompositionColumn.CPUE | CompositionColumn.Percentage, 
                composition.Name);

            report.AddTable(tableCatches);
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

            foreach (Composition composition in compositions)
            {
                bool rec = (composition is AgeComposition) && ((AgeComposition)composition).IsRecovered;
                bool add = composition.AdditionalDistributedMass > 0;

                table.AddHeaderCell(composition.Name + (rec ? Constants.NoticeHolder : string.Empty) + (add ? Constants.NoticeHolder : string.Empty));
                if (rec) table.AddNotice(Resources.Reports.CatchComposition.Notice2);
                if (add) table.AddNotice(Resources.Reports.CatchComposition.Notice3);
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
        }
    }
}