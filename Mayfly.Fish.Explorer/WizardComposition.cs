using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Fish.Explorer
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
            wizardExplorer.ResetTitle(speciesRow.KeyRecord.ShortName);

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

        public void AppendSpeciesCatchesSectionTo(Report report)
        {
            CatchesComposition.AppendSpeciesCatchesSectionTo(report);
        }

        public void AppendCategorialCatchesSectionTo(Report report)
        {
            CatchesComposition.AppendCategorialCatchesSectionTo(report, SpeciesRow, Data.Parent);
        }

        public void AppendCalculationSectionTo(Report report)
        {
            if (CatchesComposition.SeparateCompositions.Count > 6) report.BreakPage(PageBreakOption.Landscape);

            string holder = (SpeciesRow == null) ?  Wild.Resources.Reports.Caption.Species :
                string.Format(Resources.Reports.Table.CategoriesHolder, CategoryType, SpeciesRow.KeyRecord.FullNameReport);

            string sectionTitle = string.Format(Resources.Reports.Section.TablesCompositionCatches,
                (SpeciesRow == null) ? Wild.Resources.Reports.Caption.Species : CategoryType, 
                (SpeciesRow == null) ? gearWizard.SelectedSamplerType.ToDisplay() : SpeciesRow.KeyRecord.FullNameReport);

            report.AddSectionTitle(sectionTitle);

            report.AddAppendix(
                CatchesComposition.GetAppendix(CompositionColumn.Quantity | CompositionColumn.Mass,
                string.Format(Resources.Reports.Table.SampleSizeClasses, holder),
                Resources.Reports.Caption.GearClass)
                );


            report.AddAppendix(
                CatchesComposition.GetAppendix(CompositionColumn.Abundance | CompositionColumn.Biomass,
                string.Format(Resources.Reports.Table.CpueClasses, holder), 
                Resources.Reports.Caption.GearClass)
                );


            report.AddAppendix(
                CatchesComposition.GetAppendix(CompositionColumn.AbundanceFraction | CompositionColumn.BiomassFraction,
                string.Format(Resources.Reports.Table.FractionClasses, holder),
                Resources.Reports.Caption.GearClass)
                );

            if (CatchesComposition.SeparateCompositions.Count > 6) report.BreakPage(PageBreakOption.None);
        }

        public void AddAgeRecoveryRoutines(Report report)
        {
            report.AddSectionTitle(Resources.Reports.Section.TablesALK, SpeciesRow.KeyRecord.FullNameReport);

            foreach (Composition classComposition in CatchesComposition.SeparateCompositions)
            {
                if (!(classComposition is AgeKey)) continue;

                AgeKey ageComposition = (AgeKey)classComposition;

                if (!ageComposition.IsRecovered) continue;

                report.AddTable(ageComposition.GetReport());

                //ageComposition.AddReport(report, string.Format(Resources.Reports.Sections.Selectivity.Table2, classComposition.Name), gearWizard.SelectedUnit.Unit);
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
                spreadSheetCatches[ColumnSexRatio.Index, i].Value = CatchesComposition[i].Sexes;

                spreadSheetCatches.Rows[i].DefaultCellStyle.ForeColor =
                    CatchesComposition[i].Quantity == 0 ? Constants.InfantColor : spreadSheetCatches.ForeColor;
            }

            pageComposition.SetNavigation(true);
            comboBoxParameter.Enabled = true;

            UpdateResults();
            comboBoxParameter.SelectedIndex = 0;
            displayParameter_Changed(sender, e);

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
                spreadSheetComposition.DefaultCellStyle.Format = "N3";
            }
            else
            {
                switch (comboBoxParameter.SelectedIndex)
                {
                    case 0:
                        spreadSheetComposition.DefaultCellStyle.Format = string.Empty;
                        break;

                    case 1:
                        spreadSheetComposition.DefaultCellStyle.Format = "N3";
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
                            contextShowCalculation.Text = string.Format(Resources.Interface.AgeRecMenu, selectedShoal.Name);
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

                gearWizard.SelectedUnit.Unit,
                ColumnSexRatio.DefaultCellStyle.Format);

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
