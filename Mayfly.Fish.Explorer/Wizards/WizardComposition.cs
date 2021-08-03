using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Fish.Explorer.Survey
{
    public partial class WizardComposition : Form
    {
        public CardStack Data { get; set; }

        internal WizardGearSet gearWizard;

        public event EventHandler Finished;

        public event EventHandler Calculated;

        public event EventHandler Returned;



        public CompositionEqualizer CatchesComposition;

        private Composition Frame;



        public Data.SpeciesRow SpeciesRow;

        private Composition selectedShoal;

        public string CategoryType { get; set; }




        private WizardComposition()
        {
            InitializeComponent();

            ColumnN.ValueType =
                typeof(int);

            ColumnNPUE.ValueType =
                ColumnNPUEF.ValueType =
                ColumnB.ValueType =
                ColumnBPUE.ValueType =
                ColumnBPUEF.ValueType =
                typeof(double);

            columnComposition.ValueType =
                ColumnCategory.ValueType =
                ColumnSexRatio.ValueType =
                typeof(string);

            this.RestoreAllCheckStates();

            Log.Write(EventType.WizardStarted, "Cenosis composition wizard is started.");
        }

        public WizardComposition(CardStack data, Composition frame) : this()
        {
            Data = data;
            Frame = frame;
            CategoryType = Frame.GetCategoryType();
            CatchesComposition = new CompositionEqualizer(frame);
            Frame.SetLines(columnComposition);
            Frame.SetLines(ColumnCategory);
        }

        public WizardComposition(CardStack data, Composition frame, Data.SpeciesRow speciesRow, CompositionColumn column) : this(data, frame)
        {
            SpeciesRow = speciesRow;
            wizardExplorer.ResetTitle(speciesRow.KeyRecord.FullName);

            ColumnL.Visible = column.HasFlag(CompositionColumn.LengthSample);
            ColumnW.Visible = column.HasFlag(CompositionColumn.MassSample);
        }



        public void Run(WizardGearSet _gearWizard)
        {
            gearWizard = _gearWizard;

            ColumnNPUE.ResetFormatted(gearWizard.SelectedUnit.Unit);
            ColumnBPUE.ResetFormatted(gearWizard.SelectedUnit.Unit);
            checkBoxPUE.ResetFormatted(gearWizard.SelectedUnit.Unit);

            pageComposition.SetNavigation(false);
            comboBoxParameter.Enabled = false;

            calculatorStructure.RunWorkerAsync();
        }


        private void UpdateResults()
        {
            for (int i = 0; i < CatchesComposition.Count; i++)
            {
                if (CatchesComposition[i].Quantity == 0)
                {
                    spreadSheetCatches[ColumnNPUE.Index, i].Value = null;
                    spreadSheetCatches[ColumnNPUEF.Index, i].Value = null;
                    spreadSheetCatches[ColumnBPUE.Index, i].Value = null;
                    spreadSheetCatches[ColumnBPUEF.Index, i].Value = null;
                }
                else
                {
                    spreadSheetCatches[ColumnNPUE.Index, i].Value = CatchesComposition[i].Abundance;
                    spreadSheetCatches[ColumnNPUEF.Index, i].Value = CatchesComposition[i].AbundanceFraction;
                    spreadSheetCatches[ColumnBPUE.Index, i].Value = CatchesComposition[i].Biomass;
                    spreadSheetCatches[ColumnBPUEF.Index, i].Value = CatchesComposition[i].BiomassFraction;
                }

                spreadSheetCatches.Rows[i].DefaultCellStyle.ForeColor =
                    CatchesComposition[i].Quantity == 0 ? Constants.InfantColor : spreadSheetCatches.ForeColor;
            }
        }

        internal void Split(int j)
        {
            CatchesComposition.Split(j, Service.GetMeasure(SpeciesRow.Species) * 10);
            CatchesComposition.SetLines(columnComposition);
            CatchesComposition.SetLines(ColumnCategory);
            CatchesComposition.SeparateCompositions.ToArray().UpdateValues(spreadSheetComposition, columnComposition, 
                Category.GetValueVariant(comboBoxParameter.SelectedIndex == 0,
                checkBoxPUE.Checked, checkBoxFractions.Checked));

            UpdateResults();

            spreadSheetComposition.ClearSelection();
            spreadSheetComposition.Rows[j].Selected = true;
        }

        public void AppendCatchesSectionTo(Report report)
        {
            CatchesComposition.AppendCatchesSectionTo(report);
        }

        public void AppendPopulationSectionTo(Report report)
        {
            CatchesComposition.AppendPopulationSectionTo(report, SpeciesRow, Data.Parent);
        }



        //public Report.Table[] GetCpueAppendices()
        //{
        //    List<Report.Table> result = new List<Report.Table>();

        //    #region A

        //    Report.Table a_ = CatchesComposition.SeparateCompositions.ToArray().GetTable(CompositionColumn.Quantity | CompositionColumn.Mass,
        //        Resources.Reports.Title.AppA, Wild.Resources.Reports.Caption.Species, Resources.Reports.Caption.GearClass);

        //    Report.Table a = new Report.Table(Resources.Reports.Title.AppA);

        //    a.StartRow();
        //    a.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .15, 3);
        //    a.AddHeaderCell(Resources.Reports.Caption.GearClass, CatchesComposition.Dimension * 2);
        //    a.EndRow();

        //    a.StartRow();
        //    foreach (Composition gearClasscomposition in CatchesComposition.SeparateCompositions)
        //    {
        //        a.AddHeaderCell(gearClasscomposition.Name, 2);
        //    }
        //    a.EndRow();

        //    a.StartRow();
        //    for (int i = 0; i < CatchesComposition.Dimension; i++)
        //    {
        //        a.AddHeaderCell(string.Format("N, {0}", Resources.Reports.Common.Ind));
        //        a.AddHeaderCell(string.Format("B, {0}", Resources.Reports.Common.Kg));
        //    }
        //    a.EndRow();

        //    for (int i = 0; i < CatchesComposition.Count; i++)
        //    {
        //        Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(CatchesComposition[i].Name);

        //        a.StartRow();
        //        a.AddCell(speciesRow.KeyRecord.ShortName);
        //        for (int j = 0; j < CatchesComposition.Dimension; j++)
        //        {
        //            if (CatchesComposition[j, i].Quantity > 0)
        //            {
        //                a.AddCellRight(CatchesComposition[j, i].Quantity, "N0");
        //                a.AddCellRight(CatchesComposition[j, i].Mass, "N3");
        //            }
        //            else
        //            {
        //                a.AddCell();
        //                a.AddCell();
        //            }
        //        }
        //        a.EndRow();

        //    }

        //    a.StartRow();
        //    a.AddCell(Mayfly.Resources.Interface.Total);
        //    for (int j = 0; j < CatchesComposition.Dimension; j++)
        //    {
        //        a.AddCellRight(CatchesComposition.GetComposition(j).TotalQuantity);
        //        a.AddCellRight(CatchesComposition.GetComposition(j).TotalMass);
        //    }
        //    a.EndRow();

        //    #endregion

        //    result.Add(a);

        //    #region B

        //    Report.Table b = new Report.Table(Resources.Reports.Title.AppB);

        //    b.StartRow();
        //    b.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .15, 3);
        //    b.AddHeaderCell(Resources.Reports.Caption.GearClass, CatchesComposition.Dimension * 2);
        //    b.EndRow();

        //    b.StartRow();
        //    foreach (Composition gearClasscomposition in CatchesComposition.SeparateCompositions)
        //    {
        //        b.AddHeaderCell(gearClasscomposition.Name, 2);
        //    }
        //    b.EndRow();

        //    b.StartRow();
        //    for (int i = 0; i < CatchesComposition.Dimension; i++)
        //    {
        //        b.AddHeaderCell(string.Format("N, {0} / {1}", Resources.Reports.Common.Ind, gearWizard.SelectedUnit.Unit));
        //        b.AddHeaderCell(string.Format("B, {0} / {1}", Resources.Reports.Common.Kg, gearWizard.SelectedUnit.Unit));
        //    }
        //    b.EndRow();

        //    for (int i = 0; i < CatchesComposition.Count; i++)
        //    {
        //        Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(CatchesComposition[i].Name);

        //        b.StartRow();
        //        b.AddCell(speciesRow.KeyRecord.ShortName);
        //        for (int j = 0; j < CatchesComposition.Dimension; j++)
        //        {
        //            if (CatchesComposition[j, i].Quantity > 0)
        //            {
        //                b.AddCellRight(CatchesComposition[j, i].Abundance, "N1");
        //                b.AddCellRight(CatchesComposition[j, i].Biomass, "N2");
        //            }
        //            else
        //            {
        //                b.AddCell();
        //                b.AddCell();
        //            }
        //        }
        //        b.EndRow();

        //    }

        //    b.StartRow();
        //    b.AddCell(Mayfly.Resources.Interface.Total);
        //    for (int j = 0; j < CatchesComposition.Dimension; j++)
        //    {
        //        b.AddCellRight(CatchesComposition.GetComposition(j).TotalAbundance, "N0");
        //        b.AddCellRight(CatchesComposition.GetComposition(j).TotalBiomass, "N2");
        //    }
        //    b.EndRow();

        //    #endregion

        //    result.Add(b);

        //    #region E

        //    Report.Table e = new Report.Table(Resources.Reports.Title.AppC);

        //    e.StartRow();
        //    e.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .15, 3);
        //    e.AddHeaderCell(Resources.Reports.Caption.GearClass, CatchesComposition.Dimension * 2);
        //    e.EndRow();

        //    e.StartRow();
        //    foreach (Composition gearClasscomposition in CatchesComposition.SeparateCompositions)
        //    {
        //        e.AddHeaderCell(gearClasscomposition.Name, 2);
        //    }
        //    e.EndRow();

        //    e.StartRow();
        //    for (int i = 0; i < CatchesComposition.Dimension; i++)
        //    {
        //        //e.AddHeaderCell(string.Format("{0}, %", Wild.Resources.Reports.Caption.Abundance));
        //        //e.AddHeaderCell(string.Format("{0}, %", Wild.Resources.Reports.Caption.Biomass));
        //        e.AddHeaderCell("N, %");
        //        e.AddHeaderCell("B, %");
        //    }
        //    e.EndRow();

        //    for (int i = 0; i < CatchesComposition.Count; i++)
        //    {
        //        Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(CatchesComposition[i].Name);

        //        e.StartRow();
        //        e.AddCell(speciesRow.KeyRecord.ShortName);
        //        for (int j = 0; j < CatchesComposition.Dimension; j++)
        //        {
        //            if (CatchesComposition[j, i].Quantity > 0)
        //            {
        //                e.AddCellRight(CatchesComposition[j, i].AbundanceFraction * 100, "N1");
        //                e.AddCellRight(CatchesComposition[j, i].BiomassFraction * 100, "N1");
        //            }
        //            else
        //            {
        //                e.AddCell();
        //                e.AddCell();
        //            }
        //        }
        //        e.EndRow();

        //    }

        //    e.StartRow();
        //    e.AddCell(Mayfly.Resources.Interface.Total);
        //    for (int j = 0; j < CatchesComposition.Dimension; j++)
        //    {
        //        e.AddCellRight(100, "N1");
        //        e.AddCellRight(100, "N1");
        //    }
        //    e.EndRow();

        //    #endregion

        //    result.Add(e);

        //    return result.ToArray();
        //}

        public void AddAbundanceAppendices(Report report)
        {
            //report.AddSectionTitle(Resources.Reports.Title.AppAbundances);

            //#region Abundance calculations

            //for (int i = 0; i < CatchesComposition.Count; i++)
            //{
            //    Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(CatchesComposition[i].Name);
            //    report.AddSectionTitle(speciesRow.KeyRecord.FullNameReport);

            //    double catchability = Service.GetCatchability(gearWizard.SelectedSamplerType, CatchesComposition[i].Name);

            //    string latexA = string.Empty;
            //    string latexAA = string.Empty;
            //    string latexB = string.Empty;
            //    string latexBB = string.Empty;

            //    double totalNpue = 0;
            //    double totalBpue = 0;

            //    for (gearWizard.DatasetIndex = 0; gearWizard.DatasetIndex < gearWizard.SelectedCount; gearWizard.DatasetIndex++)
            //    {
            //        Composition classComposition = CatchesComposition.GetComposition(gearWizard.DatasetIndex);

            //        if (classComposition[i].Quantity == 0) continue;

            //        totalNpue += classComposition[i].Abundance;// *gearWizard.CurrentSpatialWeight;
            //        totalBpue += classComposition[i].Biomass;// *gearWizard.CurrentSpatialWeight;

            //        if (latexA != string.Empty)
            //        {
            //            latexAA += " + ";
            //            latexBB += " + ";
            //            latexA += " + ";
            //            latexB += " + ";
            //        }

            //        //latexAA += "{{" + classComposition[i].Abundance.ToString("N3") + "} \\times " +
            //        //    "{" + gearWizard.CurrentSpatialWeight.ToString("N2") + "}}\\\\";

            //        //latexBB += "{{" + classComposition[i].Biomass.ToString("N3") + "} \\times " +
            //        //    "{" + gearWizard.CurrentSpatialWeight.ToString("N2") + "}}\\\\";

            //        latexAA += "{{" + classComposition[i].Quantity.ToString("N0") + " / " + classComposition[i].Index.ToString("N2") + "}}\\\\";
            //        latexA += "{{" + classComposition[i].Abundance.ToString("N3") + "}}\\\\";

            //        latexBB += "{{" + classComposition[i].Mass.ToString("N3") + " / " + classComposition[i].Index.ToString("N2") + "}}\\\\";
            //        latexB += "{{" + classComposition[i].Biomass.ToString("N3") + "}}\\\\";
            //    }

            //    latexA = latexA.TrimEnd("\\".ToCharArray());
            //    latexB = latexB.TrimEnd("\\".ToCharArray());

            //    report.AddEquation("\\text{" + Wild.Resources.Reports.Caption.Abundance + "} = " +
            //        "\\frac{ \\left( \\begin{split} " + latexAA + " \\end{split} \\right) / {" + gearWizard.SelectedCount + "}}{" + catchability.ToString("N3") + "} = " +
            //        "\\frac{ \\left( \\begin{split} " + latexA + " \\end{split} \\right) / {" + gearWizard.SelectedCount + "}}{" + catchability.ToString("N3") + "} = " +
            //        //"\\frac{{" + totalNpue.ToString("N3") + "}/{" + gearWizard.SelectedCount + "}}{" + Catchability.ToString("N3") + "} = " +
            //        "\\frac{" + (totalNpue / (double)gearWizard.SelectedCount).ToString("N3") + "}{" + catchability.ToString("N3") + "} = " +
            //        "{" + NaturalComposition[i].Abundance.ToString("N3") + "}\\text{ " + gearWizard.AbundanceUnits + "}");

            //    report.AddEquation("\\text{" + Wild.Resources.Reports.Caption.Biomass + "} = " +
            //        "\\frac{ \\left( \\begin{split} " + latexBB + " \\end{split} \\right) / {" + gearWizard.SelectedCount + "}}{" + catchability.ToString("N3") + "} = " +
            //        "\\frac{ \\left( \\begin{split} " + latexB + " \\end{split} \\right) / {" + gearWizard.SelectedCount + "}}{" + catchability.ToString("N3") + "} = " +
            //        //"\\frac{{" + totalBpue.ToString("N3") + "}/{" + gearWizard.SelectedCount + "}}{" + Catchability.ToString("N3") + "} = " +
            //        "\\frac{" + (totalBpue / (double)gearWizard.SelectedCount).ToString("N3") + "}{" + catchability.ToString("N3") + "} = " +
            //        "{" + NaturalComposition[i].Biomass.ToString("N3") + "}\\text{ " + gearWizard.BiomassUnits + "}");

            //}

            //#endregion
        }

        public void AppendCalculationSectionTo(Report report, string sectionTitle)
        {
            if (CatchesComposition.SeparateCompositions.Count > 6) report.BreakPage(PageBreakOption.Landscape);

            report.AddSectionTitle(sectionTitle);

            string tail = (SpeciesRow == null) ? "Species" : string.Format(Resources.Reports.CatchComposition.TableHeaderTail, CategoryType, SpeciesRow.KeyRecord.FullNameReport);

            report.AddAppendix(
                CatchesComposition.GetAppendix(CompositionColumn.Quantity | CompositionColumn.Mass,
                string.Format(Resources.Reports.CatchComposition.TableSampleSize, tail),
                Resources.Reports.Caption.GearClass)
                );


            report.AddAppendix(
                CatchesComposition.GetAppendix(CompositionColumn.Abundance | CompositionColumn.Biomass,
                string.Format(Resources.Reports.CatchComposition.TableCPUE, tail, gearWizard.SelectedUnit.Unit), 
                Resources.Reports.Caption.GearClass)
                );


            report.AddAppendix(
                CatchesComposition.GetAppendix(CompositionColumn.AbundanceFraction | CompositionColumn.BiomassFraction,
                string.Format(Resources.Reports.CatchComposition.TableFraction, tail, gearWizard.SelectedUnit.Unit),
                Resources.Reports.Caption.GearClass)
                );

            if (CatchesComposition.SeparateCompositions.Count > 6) report.BreakPage(PageBreakOption.None);
        }

        public void AddAgeRecoveryRoutines(Report report)
        {
            report.AddSectionTitle(Resources.Reports.CatchComposition.AppendixHeader2,
                SpeciesRow.KeyRecord.FullNameReport);

            foreach (Composition classComposition in CatchesComposition.SeparateCompositions)
            {
                if (!(classComposition is AgeKey)) continue;

                AgeKey ageComposition = (AgeKey)classComposition;

                if (!ageComposition.IsRecovered) continue;

                report.AddTable(ageComposition.GetReport());

                //ageComposition.AddReport(report, string.Format(Resources.Reports.Selectivity.Table2, classComposition.Name), gearWizard.SelectedUnit.Unit);
            }
        }




        
        private void calculatorStructure_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Mayfly.Controls.Taskbar.SetProgressValue(this.Handle, (ulong)e.ProgressPercentage, (ulong)100);
        }

        private void calculatorStructure_DoWork(object sender, DoWorkEventArgs e)
        {
            CatchesComposition = SpeciesRow == null ? gearWizard.SelectedStacks.ToArray().GetWeightedComposition(gearWizard.WeightType, gearWizard.SelectedUnit.Variant, (SpeciesComposition)Frame) : 
                gearWizard.SelectedStacks.ToArray().GetWeightedComposition(gearWizard.WeightType, gearWizard.SelectedUnit.Variant, Frame, SpeciesRow, gearWizard.GearData.Mass(SpeciesRow));
        }

        private void calculatorStructure_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.IsDisposed) 
                return;

            spreadSheetComposition.ClearInsertedColumns();

            foreach (Composition gearClassComposition in CatchesComposition.SeparateCompositions)
            {
                spreadSheetComposition.InsertColumn(gearClassComposition.Name, gearClassComposition.Name,
                    typeof(double), spreadSheetComposition.ColumnCount, 75).ReadOnly = true;
            }

            for (int i = 0; i < CatchesComposition.Count; i++)
            {
                spreadSheetCatches[ColumnL.Index, i].Value = new SampleDisplay(CatchesComposition[i].LengthSample);
                spreadSheetCatches[ColumnW.Index, i].Value = new SampleDisplay(CatchesComposition[i].MassSample);
                spreadSheetCatches[ColumnN.Index, i].Value = CatchesComposition[i].Quantity;
                spreadSheetCatches[ColumnNPUE.Index, i].Value = CatchesComposition[i].Abundance;
                spreadSheetCatches[ColumnNPUEF.Index, i].Value = CatchesComposition[i].AbundanceFraction;
                spreadSheetCatches[ColumnB.Index, i].Value = CatchesComposition[i].Mass;
                spreadSheetCatches[ColumnBPUE.Index, i].Value = CatchesComposition[i].Biomass;
                spreadSheetCatches[ColumnBPUEF.Index, i].Value = CatchesComposition[i].BiomassFraction;
                spreadSheetCatches[ColumnSexRatio.Index, i].Value = CatchesComposition[i].GetSexualComposition();

                spreadSheetCatches.Rows[i].DefaultCellStyle.ForeColor =
                    CatchesComposition[i].Quantity == 0 ? Constants.InfantColor : spreadSheetCatches.ForeColor;
            }

            pageComposition.SetNavigation(true);
            comboBoxParameter.Enabled = true;

            UpdateResults();
            comboBoxParameter.SelectedIndex = 0;

            if (Calculated != null)
            {
                Calculated.Invoke(this, e);
            }
        }

        private void WizardCatchesComposition_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (calculatorStructure.IsBusy) calculatorStructure.CancelAsync();
        }

        private void displayParameter_Changed(object sender, EventArgs e)
        {
            if (comboBoxParameter.SelectedIndex == -1) return;

            if (checkBoxFractions.Checked)
            {
                spreadSheetComposition.DefaultCellStyle.Format = "P1";
            }
            else if (checkBoxPUE.Checked)
            {
                spreadSheetComposition.DefaultCellStyle.Format = Mayfly.Service.Mask(3);
            }
            else
            {
                switch (comboBoxParameter.SelectedIndex)
                {
                    case 0:
                        spreadSheetComposition.DefaultCellStyle.Format = string.Empty;
                        break;

                    case 1:
                        spreadSheetComposition.DefaultCellStyle.Format = Mayfly.Service.Mask(3);
                        break;
                }
            }

            CatchesComposition.SeparateCompositions.ToArray().UpdateValues(spreadSheetComposition, columnComposition,
                Category.GetValueVariant(comboBoxParameter.SelectedIndex == 0, checkBoxPUE.Checked, checkBoxFractions.Checked));
        }

        private void checkBoxFractions_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxPUE.Enabled = !checkBoxFractions.Checked;

            displayParameter_Changed(sender, e);
        }

        private void spreadSheetComposition_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo info = spreadSheetComposition.HitTest(e.X, e.Y);

                switch (info.Type)
                {
                    case DataGridViewHitTestType.ColumnHeader:
                        selectedShoal = CatchesComposition.GetComposition(info.ColumnIndex - 1);

                        if (selectedShoal is AgeKey)
                        {
                            contextShowCalculation.Text = string.Format(Resources.Interface.Interface.AgeRecMenu, selectedShoal.Name);
                        }
                        else
                        {
                            contextShowCalculation.Text = string.Format("Show composition of {0}", selectedShoal.Name);
                        }

                        break;
                }
            }
        }

        private void contextShowCalculation_Click(object sender, EventArgs e)
        {
            if (selectedShoal is AgeKey key)
            {
                key.ShowDialog();
            }
            else if (selectedShoal is AgeComposition ageComposition)
            {
                ageComposition.GetHistogram().ShowOnChart(true);
            }

            if (selectedShoal is LengthComposition lenComposition)
            {
                lenComposition.GetHistogram().ShowOnChart(true);
            }
        }

        private void contextComposition_Opening(object sender, CancelEventArgs e)
        {
            int ri = spreadSheetComposition.SelectedRows[0].Index;

            if (CatchesComposition[ri] is AgeGroup age)
            {
                double measure = Service.GetMeasure(SpeciesRow.Species) * 10;

                contextCompositionSplit.Enabled = (!double.IsNaN(measure) &&
                    age.LengthSample.Count > 0 &&
                    (age.LengthSample.Maximum >= measure &&
                    age.LengthSample.Minimum <= measure));
            }
            else
            {
                contextCompositionSplit.Enabled = false;
            }
        }

        private void menuCompositionSplit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetComposition.SelectedRows)
            {
                Split(gridRow.Index);
            }
        }

        private void buttonEqChart_Click(object sender, EventArgs e)
        {
            CatchesComposition.ShowDialog();
        }

        private void pageComposition_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Returned != null)
            {
                Returned.Invoke(sender, e);
            }
        }

        private void pageCatches_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            CatchesComposition.SetFormats(
                ColumnNPUE.DefaultCellStyle.Format,
                ColumnNPUEF.DefaultCellStyle.Format,

                ColumnB.DefaultCellStyle.Format,
                ColumnBPUE.DefaultCellStyle.Format,
                ColumnBPUEF.DefaultCellStyle.Format,

                ColumnL.DefaultCellStyle.Format,
                ColumnW.DefaultCellStyle.Format,

                gearWizard.SelectedUnit.Unit);

            if (Finished != null)
            {
                Finished.Invoke(sender, e);
            }

            e.Cancel = true;
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }

        private void pageCatches_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            //e.Cancel = true;
        }

        private void wizardPage1_Initialize(object sender, WizardPageInitEventArgs e)
        {
            wizardExplorer.NextPage();
        }
    }
}
