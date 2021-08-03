﻿using System;
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
            if (composition is Cohort && isChronic)
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
        /// <param name="compositions"></param>
        /// <param name="tableCaption"></param>
        /// <param name="siderCaption"></param>
        /// <param name="separatesHeader"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Report.Table GetTable(this Composition[] compositions, CompositionColumn content, string tableCaption, string siderCaption, string separatesHeader)
        {
            int n = 0;

            if (content.HasFlag(CompositionColumn.LengthSample)) n++;
            if (content.HasFlag(CompositionColumn.MassSample)) n++;
            if (content.HasFlag(CompositionColumn.Quantity)) n++;
            if (content.HasFlag(CompositionColumn.Abundance)) n++;
            if (content.HasFlag(CompositionColumn.AbundanceFraction)) n++;
            if (content.HasFlag(CompositionColumn.Mass)) n++;
            if (content.HasFlag(CompositionColumn.Biomass)) n++;
            if (content.HasFlag(CompositionColumn.BiomassFraction)) n++;
            if (content.HasFlag(CompositionColumn.SexRatio)) n++;

            if (n == 0) return null;

            Report.Table table = new Report.Table(tableCaption);

            // Header top level

            table.StartRow();
            table.AddHeaderCell(siderCaption, .15, compositions.Length > 1 ? 4 : 2);

            if (compositions.Length > 1)
            {
                table.AddHeaderCell(separatesHeader, compositions.Length * n);
                table.EndRow();

                // Header middle level

                table.StartRow();

                foreach (Composition composition in compositions)
                {
                    string header = composition.Name;
                    bool rec = (composition is AgeComposition) && ((AgeComposition)composition).IsRecovered;
                    bool add = composition.AdditionalDistributedMass > 0;
                    if (rec) { header += table.AddNotice(Resources.Reports.CatchComposition.Notice2).Holder; }
                    if (add) { header += table.AddNotice(Resources.Reports.CatchComposition.Notice3).Holder; }
                    table.AddHeaderCell(header, n);
                }

                table.EndRow();

                // Header bottom level

                table.StartRow();
            }

            foreach (Composition composition in compositions)
            {
                if (content.HasFlag(CompositionColumn.LengthSample))
                {
                    table.AddHeaderCell(Wild.Resources.Reports.Caption.Length + table.AddNotice(Resources.Reports.Notice.SampleFormat,
                        Mathematics.Resources.FormatNotice.ResourceManager.GetString(Mayfly.Service.StripNumbers(composition.FormatSampleLength.ToLowerInvariant()))).Holder, .12, 2);
                }

                if (content.HasFlag(CompositionColumn.MassSample))
                {
                    table.AddHeaderCell(Fish.Resources.Reports.Caption.Mass + table.AddNotice(Resources.Reports.Notice.SampleFormat,
                        Mathematics.Resources.FormatNotice.ResourceManager.GetString(Mayfly.Service.StripNumbers(composition.FormatSampleMass.ToLowerInvariant()))).Holder, .12, 2);
                }

                int q = (content.HasFlag(CompositionColumn.Quantity) ? 1 : 0) + (content.HasFlag(CompositionColumn.Abundance) ? 1 : 0) + (content.HasFlag(CompositionColumn.AbundanceFraction) ? 1 : 0);
                if (q > 0) table.AddHeaderCell(Wild.Resources.Reports.Caption.Quantity, q);

                int w = (content.HasFlag(CompositionColumn.Mass) ? 1 : 0) + (content.HasFlag(CompositionColumn.Biomass) ? 1 : 0) + (content.HasFlag(CompositionColumn.BiomassFraction) ? 1 : 0);
                if (w > 0) table.AddHeaderCell(Wild.Resources.Reports.Caption.Mass, w);

                if (content.HasFlag(CompositionColumn.SexRatio))
                {
                    table.AddHeaderCell(Resources.Reports.Caption.SexualRatio + table.AddNotice(Resources.Reports.Notice.SexualLegend, Sex.Juvenile, Sex.Male, Sex.Female).Holder, .12, 2);
                }
            }
            table.EndRow();

            table.StartRow();

            foreach (Composition composition in compositions)
            {
                if (content.HasFlag(CompositionColumn.Quantity)) table.AddHeaderCell(Resources.Reports.Common.Ind);
                if (content.HasFlag(CompositionColumn.Abundance))
                {
                    table.AddHeaderCell(string.Format("{0}/{1}{2}", Resources.Reports.Common.Ind, composition.Unit, table.AddNotice(Resources.Reports.Notice.NPUE, composition.Unit, composition.Unit).Holder));
                    ;
                }
                if (content.HasFlag(CompositionColumn.AbundanceFraction)) table.AddHeaderCell("%");

                if (content.HasFlag(CompositionColumn.Mass)) table.AddHeaderCell(Resources.Reports.Common.Kg);
                if (content.HasFlag(CompositionColumn.Biomass))
                {
                    table.AddHeaderCell(string.Format("{0}/{1}{2}", Resources.Reports.Common.Kg, composition.Unit, table.AddNotice(Resources.Reports.Notice.BPUE, composition.Unit, composition.Unit).Holder));                    
                }
                if (content.HasFlag(CompositionColumn.BiomassFraction)) table.AddHeaderCell("%");
            }

            table.EndRow();

            for (int i = 0; i < compositions[0].Count; i++)
            {
                if (compositions.Length == 1 && compositions[0][i].Quantity == 0) continue;

                table.StartRow();
                table.AddCellSider(compositions[0][i]);

                foreach (Composition composition in compositions)
                {
                    Category category = composition[i];

                    if (content.HasFlag(CompositionColumn.LengthSample)) if (category.Quantity == 0) table.AddCell(); else table.AddCellValue(new SampleDisplay(category.LengthSample), composition.FormatSampleLength);
                    if (content.HasFlag(CompositionColumn.MassSample)) if (category.Quantity == 0) table.AddCell(); else table.AddCellValue(new SampleDisplay(category.MassSample), composition.FormatSampleMass);

                    if (content.HasFlag(CompositionColumn.Quantity)) if (category.Quantity == 0) table.AddCell(); else table.AddCellRight(category.Quantity);
                    if (content.HasFlag(CompositionColumn.Abundance)) if (category.Quantity == 0) table.AddCell(); else table.AddCellRight(category.Abundance, composition.AbundanceFormat);
                    if (content.HasFlag(CompositionColumn.AbundanceFraction)) if (category.Quantity == 0) table.AddCell(); else table.AddCellRight(category.AbundanceFraction, composition.AbundanceFractionFormat);

                    if (content.HasFlag(CompositionColumn.Mass)) if (category.Quantity == 0) table.AddCell(); else table.AddCellRight(category.Mass, composition.MassFormat);
                    if (content.HasFlag(CompositionColumn.Biomass)) if (category.Quantity == 0) table.AddCell(); else table.AddCellRight(category.Biomass, composition.BiomassFormat);
                    if (content.HasFlag(CompositionColumn.BiomassFraction)) if (category.Quantity == 0) table.AddCell(); else table.AddCellRight(category.BiomassFraction, composition.BiomassFractionFormat);

                    if (content.HasFlag(CompositionColumn.SexRatio)) if (category.Quantity == 0) table.AddCell(); else table.AddCellValue(category.GetSexualComposition());
                }

                table.EndRow();
            }

            table.StartRow();
            table.AddCell(Mayfly.Resources.Interface.Total);


            foreach (Composition composition in compositions)
            {
                if (content.HasFlag(CompositionColumn.LengthSample)) table.AddCell();
                if (content.HasFlag(CompositionColumn.MassSample)) table.AddCell();
                if (content.HasFlag(CompositionColumn.Quantity)) table.AddCellRight(composition.TotalQuantity);
                if (content.HasFlag(CompositionColumn.Abundance)) table.AddCellRight(composition.TotalAbundance, composition.AbundanceFormat);
                if (content.HasFlag(CompositionColumn.AbundanceFraction)) table.AddCellRight(1, composition.AbundanceFractionFormat);
                if (content.HasFlag(CompositionColumn.Mass)) table.AddCellRight(composition.TotalMass, composition.MassFormat);
                if (content.HasFlag(CompositionColumn.Biomass)) table.AddCellRight(composition.TotalBiomass, composition.BiomassFormat);
                if (content.HasFlag(CompositionColumn.BiomassFraction)) table.AddCellRight(1, composition.BiomassFractionFormat);
                if (content.HasFlag(CompositionColumn.SexRatio)) table.AddCell();
            }
            table.EndRow();

            return table;
        }

        public static Report.Table GetTable(this Composition composition, CompositionColumn content, string tableCaption, string siderCaption)
        {
            if (composition.Count == 0) return null;

            return GetTable(new Composition[] { composition }, content, tableCaption, siderCaption, string.Empty);
        }

        public static Report.Table GetTable(this Composition composition, CompositionColumn content, string tableCaption)
        {
            return GetTable(new Composition[] { composition }, content, tableCaption, composition.Name, string.Empty);
        }

        /// <summary>
        /// Construct table with samples description using standard columns set
        /// </summary>
        /// <param name="composition"></param>
        /// <param name="tableCaption"></param>
        /// <returns></returns>
        public static Report.Table GetStandardCatchesTable(this Composition composition, string tableCaption, string siderCaption)
        {
            return composition.GetTable(CompositionColumn.LengthSample | CompositionColumn.MassSample | 
                CompositionColumn.Quantity | CompositionColumn.Abundance | CompositionColumn.AbundanceFraction | 
                CompositionColumn.Mass | CompositionColumn.Biomass | CompositionColumn.BiomassFraction |
                CompositionColumn.SexRatio,
                tableCaption, siderCaption);
        }

        public static Report.Table GetStandardCatchesTable(this Composition composition, string tableCaption)
        {
            return composition.GetStandardCatchesTable(tableCaption, composition.Name);
        }



        public static void AppendPopulationSectionTo(this Composition composition, Report report, Data.SpeciesRow speciesRow, Data data)
        {
            string categoryType = composition.GetCategoryType();

            report.AddSectionTitle(Resources.Reports.Section.Population.Header, categoryType);

            report.AddParagraph(Resources.Reports.Section.Population.Paragraph1, categoryType, speciesRow.KeyRecord.FullNameReport, report.NextTableNumber);

            if (composition is AgeComposition)
            {
                if (UserSettings.AgeSuggest && data.GrowthModels.GetSpecies().Contains(speciesRow.Species))
                {
                    report.AddParagraph(Resources.Reports.Section.Population.Paragraph2,
                            speciesRow.KeyRecord.FullNameReport);

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
                CompositionColumn.Quantity | CompositionColumn.Abundance | CompositionColumn.AbundanceFraction,
                string.Format(Resources.Reports.Section.Population.Table, categoryType));

            report.AddTable(tableCatches);
        }

    }
}