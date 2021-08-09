using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using AeroWizard;
using Mayfly.Wild;
using Mayfly.Waters;
using Mayfly.Mathematics;
using System.Data;
using Mayfly;
using Meta.Numerics.Statistics;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Fish;
using Mayfly.Fish.Explorer;
using Mayfly.Software;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardExtrapolation : Form
    {
        public CardStack Data { get; set; }

        private WizardComposition compositionWizard;

        private WizardGearSet gearWizard;

        public Data.SpeciesRow SpeciesRow;

        public double Area 
        {
            get
            {
                return (double)numericUpDownArea.Value * 10000;
            }

            set
            {
                if (numericUpDownArea.Maximum < (decimal)value)
                {
                    numericUpDownArea.Maximum = (decimal)value * 2;
                }
                numericUpDownArea.Value = (decimal)value / 10000;
            }
        }

        public double Depth 
        {
            get { return (double)numericUpDownDepth.Value; }
            set { numericUpDownDepth.Value = (decimal)value; }
        }

        public double Volume 
        {
            get
            {
                return (double)numericUpDownVolume.Value * 1000;
            }

            set
            {
                if (numericUpDownVolume.Maximum < (decimal)value)
                {
                    numericUpDownVolume.Maximum = (decimal)value * 2;
                }
                numericUpDownVolume.Value = (decimal)value / 1000;
            }
        }

        Category[] catches;

        public double Abundance { get; set; }

        public double Biomass { get; set; }

        public double Catchability { get; set; }

        public double StockNumber { get; set; }

        public double StockMass { get; set; }

        public Composition Stock { get; set; }

        //private bool treatable;

        //Composition example;



        private WizardExtrapolation()
        {
            InitializeComponent();

            columnCpueEffort.ValueType =
                columnCpueN.ValueType =
                columnCpueB.ValueType =
                columnCpueNpue.ValueType =
                columnCpueBpue.ValueType =

                columnStockNP.ValueType =
                columnStockBP.ValueType =
                columnStockN.ValueType =
                columnStockB.ValueType =
                typeof(double);

            columnCpueMesh.ValueType =
                typeof(int);

            //pageComposition.Suppress = true;

            this.RestoreAllCheckStates();
        }

        public WizardExtrapolation(CardStack data, Data.SpeciesRow speciesRow) : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.KeyRecord.ShortName);
            labelStart.ResetFormatted(SpeciesRow.KeyRecord.ShortName);
            labelStockInstruction.ResetFormatted(SpeciesRow.KeyRecord.ShortName);

            Log.Write(EventType.WizardStarted, "Extrapolation wizard is started for {0}.", speciesRow.Species);
        }


        
        private void UpdateVolume()
        {
            Volume = Area * Depth;
            numericUpDownVolume.Value = (decimal)(Volume / 1000.0);

            //UpdateStock();
        }

        private void UpdateAbundances()
        {
            textBoxNpue.Text = string.Empty;
            textBoxBpue.Text = string.Empty;

            Abundance = 0;
            Biomass = 0;

            for (int i = 0; i < catches.Length; i++)
            {
                Abundance += catches[i].Abundance;
                Biomass += catches[i].Biomass;
            }

            Abundance /= catches.Length;
            Biomass /= catches.Length;

            Abundance /= Catchability;
            Biomass /= Catchability;

            textBoxNpue.Text = Abundance.ToString("N3");
            textBoxBpue.Text = Biomass.ToString("N3");

            UpdateStock();
        }

        private void UpdateStock()
        {
            //if (populationWizard == null) return;

            switch (gearWizard.SelectedUnit.Variant)
            {
                case ExpressionVariant.Square:
                    StockNumber = Abundance * Area / gearWizard.SelectedUnit.UnitCost;
                    StockMass = Biomass * Area / gearWizard.SelectedUnit.UnitCost;
                    break;
                case ExpressionVariant.Volume:
                case ExpressionVariant.Efforts:
                    StockNumber = Abundance * Volume / gearWizard.SelectedUnit.UnitCost;
                    StockMass = Biomass * Volume / gearWizard.SelectedUnit.UnitCost;
                    break;
            }

            textBoxStockQ.Text = (StockNumber / 1000.0).ToString("N2");
            textBoxStockM.Text = (StockMass / 1000.0).ToString("N2");
        }

        private void UpdateStockStructure()
        {
            Stock = new Composition(compositionWizard.CatchesComposition);

            for (int i = 0; i < compositionWizard.CatchesComposition.Count; i++)
            {
                Stock[i].Quantity = (int) (StockNumber * compositionWizard.CatchesComposition[i].AbundanceFraction);
                Stock[i].Mass = StockMass * compositionWizard.CatchesComposition[i].BiomassFraction;

                spreadSheetStock[columnStockNP.Index, i].Value = compositionWizard.CatchesComposition[i].AbundanceFraction;
                spreadSheetStock[columnStockBP.Index, i].Value = compositionWizard.CatchesComposition[i].BiomassFraction;
                spreadSheetStock[columnStockN.Index, i].Value = (double)Stock[i].Quantity / 1000.0;
                spreadSheetStock[columnStockB.Index, i].Value = Stock[i].Mass / 1000.0;

                spreadSheetStock.Rows[i].DefaultCellStyle.ForeColor = compositionWizard.CatchesComposition[i].Quantity == 0 ?
                    Constants.InfantColor : spreadSheetStock.ForeColor;

                //switch (Stock.Type)
                //{
                //    case CategoryType.Age:

                //        Age age = Service.GetGamingAge(SpeciesRow.Name);
                //        if (age != null)
                //        {
                //            spreadSheetStock.Rows[i].Selected = age <= new Age(Stock[i].Name);
                //        }

                //        break;

                //    case CategoryType.Length:

                //        double sizeClass = Service.GetMeasure(SpeciesRow.Name);
                //        if (!double.IsNaN(sizeClass))
                //        {
                //            string categoryName = sizeClass.ToString(Resources.Interface.SizeClassMask);
                //            spreadSheetStock.Rows[i].Selected = new Mayfly.OmniSorter().Compare(
                //                categoryName, Stock[i].Name) < 1;
                //        }
                //        break;
                //}

            }

            spreadSheetStock_SelectionChanged(spreadSheetStock, new EventArgs());
        }

        public Report GetReport()
        {
            Report report = new Report(
                    string.Format(Resources.Reports.Sections.Extrapolation.Title,
                    SpeciesRow.KeyRecord.FullNameReport));
            gearWizard.SelectedData.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            //report.AddParagraph(Resources.Reports.CatchComposition.ParagraphGear,
            //    gearWizard.SelectedSamplerType.Name));

            if (checkBoxGears.Checked)
            {
                gearWizard.AddEffortSection(report);
            }

            if (checkBoxAbundance.Checked)
            {
                AddStock(report);
            }

            if (checkBoxComposition.Checked)
            {
                report.AddParagraph(Resources.Reports.Sections.Extrapolation.Paragraph1,
                    SpeciesRow.KeyRecord.FullNameReport, gearWizard.SelectedSamplerType.ToDisplay(),
                    compositionWizard.CategoryType);

                if (checkBoxCatches.Checked)
                {
                    compositionWizard.AppendPopulationSectionTo(report);
                }

                AddComposition(report);

                //if (gearWizard.IsMultipleClasses && checkBoxApp.Checked)
                //{
                //    compositionWizard.AddCatchesRoutines(report);
                //}
            }


            if (checkBoxApp.Checked)
            {
                compositionWizard.AppendCalculationSectionTo(report);
            }

            report.EndBranded();

            return report;
        }

        public void AddStock(Report report)
        {
            if (gearWizard.IsMultipleClasses)
            {
                report.AddParagraph(Resources.Reports.Sections.Extrapolation.Paragraph2 +
                    (gearWizard.IsSpatialOn ? Resources.Reports.Sections.Extrapolation.Paragraph2_1 : string.Empty),
                    SpeciesRow.KeyRecord.FullNameReport, Catchability, SpeciesRow.KeyRecord.FullNameReport, report.NextTableNumber,
                    (gearWizard.IsSpatialOn ? (report.NextTableNumber - 1).ToString() : string.Empty));

                Report.Table table1 = new Report.Table(Resources.Reports.Sections.Extrapolation.Table1,
                    SpeciesRow.KeyRecord.FullNameReport, gearWizard.SelectedSamplerType.ToDisplay());

                table1.StartRow();
                table1.AddHeaderCell(Resources.Reports.Caption.GearClass, .25, 2);
                table1.AddHeaderCell(string.Format(Resources.Reports.Caption.Efforts, gearWizard.SelectedUnit.Unit), 2, CellSpan.Rows);
                table1.AddHeaderCell(Resources.Reports.Sections.Extrapolation.ColumnCatch, 2);
                table1.AddHeaderCell(Resources.Reports.Sections.Extrapolation.ColumnCpue, 2);
                table1.EndRow();

                table1.StartRow();
                table1.AddHeaderCell(Resources.Reports.Common.Ind);
                table1.AddHeaderCell(Resources.Reports.Common.Kg);
                table1.AddHeaderCell(gearWizard.AbundanceUnits);
                table1.AddHeaderCell(gearWizard.BiomassUnits);
                table1.EndRow();

                foreach (Category _catch in catches)
                {
                    if (_catch.Quantity == 0) continue;

                    table1.StartRow();

                    table1.AddCellValue(_catch.Name);
                    table1.AddCellRight(_catch.Index, columnCpueEffort.DefaultCellStyle.Format);

                    table1.AddCellRight(_catch.Quantity, 0);
                    table1.AddCellRight(_catch.Mass, 3);
                    table1.AddCellRight(_catch.Abundance, 3);
                    table1.AddCellRight(_catch.Biomass, 3);

                    table1.EndRow();
                }

                table1.StartRow();
                table1.AddCell(Mayfly.Resources.Interface.Total);
                table1.AddCellRight(Constants.Null); //gearWizard.TotalEffort, Textual.Mask(3));
                table1.AddCellRight(catches.GetTotalQuantity());
                table1.AddCellRight(catches.GetTotalMass());
                table1.AddCellRight(Constants.Null);
                table1.AddCellRight(Constants.Null);
                table1.EndRow();
                report.AddTable(table1);
            }
            else
            {
                report.AddParagraph(Resources.Reports.Sections.Extrapolation.Paragraph3,
                    catches[0].Abundance, gearWizard.AbundanceUnits,
                    catches[0].Biomass, gearWizard.BiomassUnits,
                    SpeciesRow.KeyRecord.FullNameReport, Catchability);
            }

            report.AddParagraph(Resources.Reports.Sections.Extrapolation.Paragraph4);

            if (gearWizard.IsMultipleClasses)
            {

                string latexA = string.Empty;
                string latexB = string.Empty;

                foreach (Category _catch in catches)
                {
                    if (_catch.Quantity == 0) continue;

                    if (latexA != string.Empty)
                    {
                        latexA += " + ";
                        latexB += " + ";
                    }

                    latexA += "{{" + _catch.Abundance.ToString("N3") + "}}\\\\";
                    latexB += "{{" + _catch.Biomass.ToString("N3") + "}}\\\\";
                }

                latexA = latexA.TrimEnd("\\".ToCharArray());
                latexB = latexB.TrimEnd("\\".ToCharArray());

                report.AddEquation("\\text{" + Wild.Resources.Reports.Caption.Abundance + "} = " +
                    "\\frac{ \\left( \\begin{split} " + latexA + " \\end{split} \\right) / {" + catches.Length + "}}{" + Catchability.ToString("N3") + "} = " +
                    //"\\frac{{" + totalNpue.ToString("N3") + "}/{" + gearWizard.SelectedCount + "}}{" + Catchability.ToString("N3") + "} = " +
                    "\\frac{" + (catches.GetTotalAbundance() / (double)catches.Length).ToString("N3") + "}{" + Catchability.ToString("N3") + "} = " +
                    "{" + Abundance.ToString("N3") + "}\\text{ " + gearWizard.AbundanceUnits + "}");

                report.AddEquation("\\text{" + Wild.Resources.Reports.Caption.Biomass + "} = " +
                    "\\frac{ \\left( \\begin{split} " + latexB + " \\end{split} \\right) / {" + catches.Length + "}}{" + Catchability.ToString("N3") + "} = " +
                    //"\\frac{{" + totalBpue.ToString("N3") + "}/{" + gearWizard.SelectedCount + "}}{" + Catchability.ToString("N3") + "} = " +
                    "\\frac{" + (catches.GetTotalBiomass() / (double)catches.Length).ToString("N3") + "}{" + Catchability.ToString("N3") + "} = " +
                    "{" + Biomass.ToString("N3") + "}\\text{ " + gearWizard.BiomassUnits + "}");
            }
            else
            {
                report.AddEquation("\\text{" + Wild.Resources.Reports.Caption.Abundance + "} = " +
                    "\\frac{" + catches[0].Abundance.ToString("N3") + "}{" + Catchability.ToString("N3") + "} = " +
                    "{" + Abundance.ToString("N3") + "}\\text{ " + gearWizard.AbundanceUnits + "}");

                report.AddEquation("\\text{" + Wild.Resources.Reports.Caption.Biomass + "} = " +
                    "\\frac{" + catches[0].Biomass.ToString("N3") + "}{" + Catchability.ToString("N3") + "} = " +
                    "{" + Biomass.ToString("N3") + "}\\text{ " + gearWizard.BiomassUnits + "}");
            }


            string waterSize = string.Empty;

            switch (gearWizard.SelectedUnit.Variant)
            {
                case ExpressionVariant.Efforts:
                    waterSize = string.Format(Resources.Reports.Sections.Extrapolation.Paragraph5_E, Volume,
                        gearWizard.SelectedUnit.Unit, gearWizard.SelectedUnit.UnitCost);
                    break;

                case ExpressionVariant.Volume:
                    waterSize = string.Format(Resources.Reports.Sections.Extrapolation.Paragraph5_V, Volume / 1000.0);
                    break;

                case ExpressionVariant.Square:
                    waterSize = string.Format(Resources.Reports.Sections.Extrapolation.Paragraph5_S, Area / 10000.0);
                    break;
            }

            report.AddParagraph(Resources.Reports.Sections.Extrapolation.Paragraph6,
                waterSize, SpeciesRow.KeyRecord.FullNameReport);

            switch (gearWizard.SelectedUnit.Variant)
            {
                case ExpressionVariant.Efforts:
                    report.AddEquation("{N} = {" + Abundance.ToString("N3") +
                        "}\\times\\frac{" + Volume + "}{" + gearWizard.SelectedUnit.UnitCost.ToString("N1") +
                        "} = {" + StockNumber.ToString("N0") + "}{\\text{ " + Resources.Reports.Common.Ind + "}}");

                    report.AddEquation("{M} = {" + Biomass.ToString("N3") +
                        "}\\times\\frac{" + Volume + "}{" + gearWizard.SelectedUnit.UnitCost.ToString("N1") +
                        "} = {" + StockMass.ToString("N1") + "}{\\text{ " + Resources.Reports.Common.Kg + "}}");

                    break;

                case ExpressionVariant.Volume:
                    report.AddEquation("{N} = {" + Abundance.ToString("N3") +
                        "}\\times{" + Volume / 1000.0 + "} = {" +
                        StockNumber.ToString("N0") + "}{\\text{ " + Resources.Reports.Common.Ind + "}}");

                    report.AddEquation("{M} = {" + Biomass.ToString("N3") +
                        "}\\times{" + Volume / 1000.0 + "} = {" +
                        StockMass.ToString("N1") + "}{\\text{ " + Resources.Reports.Common.Kg + "}}");
                    break;

                case ExpressionVariant.Square:
                    report.AddEquation("{N} = {" + Abundance.ToString("N3") +
                        "}\\times{" + Area / 10000.0 + "} = {" +
                        StockNumber.ToString("N0") + "}{\\text{ " + Resources.Reports.Common.Ind + "}}");

                    report.AddEquation("{M} = {" + Biomass.ToString("N3") +
                        "}\\times{" + Area / 10000.0 + "} = {" +
                        StockMass.ToString("N1") + "}{\\text{ " + Resources.Reports.Common.Kg + "}}");
                    break;
            }
        }

        public void AddComposition(Report report)
        {
            report.AddParagraph(Resources.Reports.Sections.Extrapolation.Paragraph7,
                report.NextTableNumber);

            Report.Table table1 = new Report.Table(Resources.Reports.Sections.Extrapolation.Table2,
                compositionWizard.CategoryType);

            table1.StartRow();
            table1.AddHeaderCell(compositionWizard.CatchesComposition.Name, .25, 2);
            table1.AddHeaderCell(Resources.Reports.Sections.Extrapolation.ColumnStockN, 2);
            table1.AddHeaderCell(Resources.Reports.Sections.Extrapolation.ColumnStockB, 2);
            table1.EndRow();

            table1.StartRow();
            table1.AddHeaderCell("%");
            table1.AddHeaderCell(Resources.Reports.Common.Ind);
            table1.AddHeaderCell("%");
            table1.AddHeaderCell(Resources.Reports.Common.Kg);
            table1.EndRow();

            List<string> selectedCategories = new List<string>();
            int selectedQuantity = 0;
            double selectedMass = 0.0;

            for (int i = 0; i < compositionWizard.CatchesComposition.Count; i++)
            {
                table1.StartRow();
                table1.AddCellValue(compositionWizard.CatchesComposition[i].Name);
                table1.AddCellRight(Stock[i].Quantity > 0 ? compositionWizard.CatchesComposition[i].AbundanceFraction.ToString(columnStockNP.DefaultCellStyle.Format) : Constants.Null);
                table1.AddCellRight(Stock[i].Quantity > 0 ? Stock[i].Quantity.ToString("N0") : Constants.Null);
                table1.AddCellRight(Stock[i].Mass > 0 ? compositionWizard.CatchesComposition[i].BiomassFraction.ToString(columnStockBP.DefaultCellStyle.Format) : Constants.Null);
                table1.AddCellRight(Stock[i].Mass > 0 ? Stock[i].Mass.ToString("N1") : Constants.Null);
                table1.EndRow();

                if (spreadSheetStock.Rows[i].Selected)
                {
                    selectedCategories.Add(compositionWizard.CatchesComposition[i].Name);
                    selectedQuantity += Stock[i].Quantity;
                    selectedMass += Stock[i].Mass;
                }
            }

            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Total);
            table1.AddCellRight(1, columnStockNP.DefaultCellStyle.Format);
            table1.AddCellRight(Stock.TotalQuantity, 0);
            table1.AddCellRight(1, columnStockBP.DefaultCellStyle.Format);
            table1.AddCellRight(Stock.TotalMass, 1);
            table1.EndRow();
            report.AddTable(table1);

            if (selectedCategories.Count > 1)
                report.AddParagraph(Resources.Reports.Sections.Extrapolation.Paragraph8,
                    compositionWizard.CategoryType, selectedCategories.Merge(", "), selectedQuantity, selectedMass);
        }

        public void RunComposition(Composition example)
        {
            pageCategory.NextPage = pageComposition;
            pageComposition.NextPage = pageReport;
            checkBoxComposition.Enabled = true;
            checkBoxComposition.Checked = true;

            wizardExplorer.NextPage();

            compositionWizard = new WizardComposition(Data, example, SpeciesRow, CompositionColumn.MassSample | CompositionColumn.LengthSample);
            compositionWizard.Returned += compositionWizard_Returned;
            compositionWizard.Finished += compositionWizard_Finished;
            compositionWizard.Replace(this);
            compositionWizard.Run(gearWizard);
        }

        

        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            gearWizard = new WizardGearSet(Data, SpeciesRow);
            gearWizard.Returned += gearWizard_Returned;
            gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
            gearWizard.Replace(this);
        }

        private void gearWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageStart);
            this.Replace(gearWizard);
        }

        private void gearWizard_AfterDataSelected(object sender, EventArgs e)
        {
            columnCpueEffort.ResetFormatted(gearWizard.SelectedUnit.Unit);
            columnCpueNpue.ResetFormatted(gearWizard.SelectedUnit.Unit);
            columnCpueBpue.ResetFormatted(gearWizard.SelectedUnit.Unit);
            labelAbundance.ResetFormatted(gearWizard.SelectedUnit.Unit);
            labelBiomass.ResetFormatted(gearWizard.SelectedUnit.Unit);

            labelAbundanceNotice.Visible = gearWizard.SelectedUnit.Variant == ExpressionVariant.Efforts;
            if (gearWizard.SelectedUnit.Variant == ExpressionVariant.Efforts)
            {
                labelAbundanceNotice.ResetFormatted(gearWizard.SelectedUnit.Unit,
                    gearWizard.SelectedUnit.UnitCost);
            }

            numericUpDownDepth.Enabled = gearWizard.SelectedUnit.Variant != ExpressionVariant.Square;

            labelAbundanceInstruction.ResetFormatted(gearWizard.SelectedSamplerType.ToDisplay(), SpeciesRow.KeyRecord.ShortName);

            this.Replace(gearWizard);
            //if (wizardExplorer.SelectedPage != pageCpue) wizardExplorer.SetSelectedPage(pageCpue);

            cpueCalc.RunWorkerAsync();
        }

        private void cpueCalc_DoWork(object sender, DoWorkEventArgs e)
        {
            catches = gearWizard.SelectedStacks.ToArray().GetWeightedCatches(
                gearWizard.WeightType, gearWizard.SelectedSamplerType,
                gearWizard.SelectedUnit.Variant,
                SpeciesRow);
        }

        private void cpueCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetCpue.Rows.Clear();

            for (int i = 0; i < catches.Length; i++)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetCpue);
                gridRow.Cells[columnCpueMesh.Index].Value = catches[i].Name;
                gridRow.Cells[columnCpueEffort.Index].Value = catches[i].Index;
                spreadSheetCpue.Rows.Add(gridRow);

                if (catches[i].Quantity > 0)
                {
                    gridRow.Cells[columnCpueN.Index].Value = catches[i].Quantity;
                    gridRow.Cells[columnCpueB.Index].Value = catches[i].Mass;
                    gridRow.Cells[columnCpueNpue.Index].Value = catches[i].Abundance;
                    gridRow.Cells[columnCpueBpue.Index].Value = catches[i].Biomass;
                }

                gridRow.DefaultCellStyle.ForeColor =
                    catches[i].Quantity > 0 ? spreadSheetCpue.ForeColor : Constants.InfantColor;
            }

            int total = catches.GetTotalQuantity();

            if (total > 0)
            {
                DataGridViewRow totalRow = new DataGridViewRow();
                totalRow.CreateCells(spreadSheetCpue);
                totalRow.Cells[columnCpueMesh.Index].Value = Mayfly.Resources.Interface.Total;
                totalRow.Cells[columnCpueEffort.Index].Value = gearWizard.TotalEffort;
                totalRow.Cells[columnCpueN.Index].Value = total;
                totalRow.Cells[columnCpueB.Index].Value = catches.GetTotalMass();
                spreadSheetCpue.Rows.Add(totalRow);
            }

            UpdateAbundances();

            numericUpDownCatchability.Value = (decimal)Service.GetCatchability(
                gearWizard.SelectedSamplerType,
                SpeciesRow.Species);

            buttonLength.Enabled = Data.MeasuredAnyhow(SpeciesRow) > 0;
            buttonAge.Enabled = Data.Aged(SpeciesRow) > 0 ||
                Data.Parent.GrowthModels.IsAvailable(SpeciesRow.Species);
            buttonSex.Enabled = Data.Sexed(SpeciesRow) > 0;
        }

        private void pageWater_Initialize(object sender, WizardPageInitEventArgs e)
        {
            numericUpDownArea.Value = (decimal)(UserSettings.MemorizedWaterArea / 10000.0);
            numericUpDownDepth.Value = (decimal)UserSettings.MemorizedWaterDepth;
        }

        private void numericUpDownArea_ValueChanged(object sender, EventArgs e)
        {
            Area = 10000 * (double)numericUpDownArea.Value;
            UpdateVolume();
        }

        private void numericUpDownDepth_ValueChanged(object sender, EventArgs e)
        {
            Depth = (double)numericUpDownDepth.Value;
            UpdateVolume();
        }

        private void pageWater_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            UpdateVolume();

            UserSettings.MemorizedWaterArea = Area;
            UserSettings.MemorizedWaterDepth = Depth;

            UpdateStock();
        }

        private void pageCpue_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) gearWizard.Replace(this);
        }

        private void numericUpDownCatchability_ValueChanged(object sender, EventArgs e)
        {
            Catchability = (double)numericUpDownCatchability.Value;
            UpdateAbundances();
        }

        private void pageStock_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            Service.SaveCatchability(gearWizard.SelectedSamplerType, SpeciesRow.Species, Catchability);
        }




        private void buttonAge_Click(object sender, EventArgs e)
        {
            Composition example = Data.GetAgeCompositionFrame(SpeciesRow);
            example.Name = Wild.Resources.Reports.Caption.Age;
            RunComposition(example);
        }

        private void buttonLength_Click(object sender, EventArgs e)
        {
            Composition example = Data.GetLengthCompositionFrame(SpeciesRow, UserSettings.SizeInterval);
            example.Name = Fish.Resources.Common.SizeUnits;
            RunComposition(example);
        }

        private void buttonSex_Click(object sender, EventArgs e)
        {
            Composition example = Data.GetSexualCompositionFrame(SpeciesRow);
            example.Name = "Sex";
            RunComposition(example);
        }

        private void pageCategory_Commit(object sender, WizardPageConfirmEventArgs e)
        { }

        private void compositionWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageCategory);
            this.Replace(compositionWizard);

            pageCategory.NextPage = pageReport;
            checkBoxComposition.Enabled = false;
        }

        private void compositionWizard_Finished(object sender, EventArgs e)
        {
            this.Replace(compositionWizard);
            checkBoxCatches_CheckedChanged(sender, e);
            //checkBoxAgeRecovery.Enabled = compositionWizard.IsAgeRecovered;

            compositionWizard.CatchesComposition.SetLines(columnStockCategory);

            DataGridViewRow totalRow = new DataGridViewRow();
            totalRow.CreateCells(spreadSheetStock);
            totalRow.Cells[columnStockCategory.Index].Value = Mayfly.Resources.Interface.Total;
            totalRow.Cells[columnStockN.Index].Value = StockNumber / 1000.0;
            totalRow.Cells[columnStockNP.Index].Value = 1;
            totalRow.Cells[columnStockB.Index].Value = StockMass / 1000.0;
            totalRow.Cells[columnStockBP.Index].Value = 1;
            spreadSheetStock.Rows.Add(totalRow);

            DataGridViewRow selectedTotalRow = new DataGridViewRow();
            selectedTotalRow.CreateCells(spreadSheetStock);
            selectedTotalRow.Cells[columnStockCategory.Index].Value = "Total selected";
            spreadSheetStock.Rows.Add(selectedTotalRow);

            if (spreadSheetStock.Rows.Count > 0)
            {
                UpdateStockStructure();
                spreadSheetStock_SelectionChanged(spreadSheetStock, new EventArgs());
            }
        }


        
        private void spreadSheetStock_SelectionChanged(object sender, EventArgs e)
        {
            if (spreadSheetStock.Rows.Count < compositionWizard.CatchesComposition.Count + 1) return;

            DataGridViewRow totalRow = spreadSheetStock.Rows[compositionWizard.CatchesComposition.Count + 1];

            double fractionQ = 0;
            double fractionM = 0;

            foreach (DataGridViewRow gridRow in spreadSheetStock.SelectedRows)
            {
                if (gridRow.Index >= compositionWizard.CatchesComposition.Count) continue;

                fractionQ += compositionWizard.CatchesComposition[gridRow.Index].AbundanceFraction;
                fractionM += compositionWizard.CatchesComposition[gridRow.Index].BiomassFraction;
            }

            totalRow.Cells[columnStockN.Index].Value = fractionQ * StockNumber / 1000;
            totalRow.Cells[columnStockNP.Index].Value = fractionQ;
            totalRow.Cells[columnStockB.Index].Value = fractionM * StockMass / 1000;
            totalRow.Cells[columnStockBP.Index].Value = fractionM;
        }

        private void contextMenuStripSplit_Opening(object sender, CancelEventArgs e)
        {
            int ri = spreadSheetStock.SelectedRows[0].Index;

            if (compositionWizard.CatchesComposition[ri] is AgeGroup age)
            {
                double measure = Service.GetMeasure(SpeciesRow.Species) * 10;

                contextStockSplit.Enabled = (!double.IsNaN(measure) &&
                    age.LengthSample.Maximum >= measure &&
                    age.LengthSample.Minimum <= measure);
            }
            else
            {
                contextStockSplit.Enabled = false;
            }
        }

        private void contextStockSplit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetStock.SelectedRows)
            {
                compositionWizard.Split(gridRow.Index);

                Age age = ((AgeGroup)compositionWizard.CatchesComposition[gridRow.Index]).Age;
                double l = Service.GetMeasure(SpeciesRow.Species);

                spreadSheetStock[columnStockCategory.Index, gridRow.Index].Value =
                    string.Format("{0} (<{1})", age.Group, l);

                spreadSheetStock.Rows.Insert(gridRow.Index + 1,
                    string.Format("{0} (≥{1})", age.Group, l));

                UpdateStockStructure();
            }
        }

        private void pageStockComposition_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) compositionWizard.Replace(this);
        }

        private void checkBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Enabled)
            {
                ((CheckBox)sender).Checked = false;
            }
        }

        private void checkBoxComposition_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxCatches.Enabled = checkBoxComposition.Checked;
        }

        private void checkBoxCatches_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxApp.Enabled = checkBoxCatches.Checked && gearWizard.IsMultipleClasses;
            //checkBoxAgeRecovery.Enabled = checkBoxCatches.Checked && compositionWizard.IsAgeRecovered;
        }

        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
            reporter.RunWorkerAsync();
            e.Cancel = true;
        }

        private void reporter_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetReport();
        }

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((Report)e.Result).Run();
            pageReport.SetNavigation(true);
            Log.Write(EventType.WizardEnded, "Extrapolation wizard is finished for {0} with result {1:N2} t.",
                SpeciesRow.Species, StockMass / 1000.0);
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
