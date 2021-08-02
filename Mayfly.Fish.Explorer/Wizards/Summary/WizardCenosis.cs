using AeroWizard;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer.Survey
{
    public partial class WizardCenosis : Form
    {
        public CardStack Data { get; set; }

        private WizardGearSet gearWizard;

        private WizardComposition compositionWizard;

        public CardStack SelectedStack { get; private set; }

        public SpeciesComposition NaturalComposition;



        private WizardCenosis()
        { 
            InitializeComponent();

            columnSelectivityLength.ValueType =
            columnSelectivityMass.ValueType = typeof(SampleDisplay);

            columnSelectivitySpecies.ValueType = typeof(string);

            columnSelectivityN.ValueType =
            columnSelectivityNPer.ValueType =
            columnSelectivityNpue.ValueType =
            columnSelectivityB.ValueType =
            columnSelectivityBPer.ValueType =
            columnSelectivityBpue.ValueType = typeof(double);

            this.RestoreAllCheckStates();
        }

        public WizardCenosis(CardStack data) : this()
        {
            Data = data;

            Log.Write(EventType.WizardStarted, "Cenosis wizard is started.");
        }





        private void RecalculateCenosis()
        {
            for (int i = 0; i < NaturalComposition.Count; i++)
            {
                double catchability = Service.GetCatchability(gearWizard.SelectedSamplerType, compositionWizard.CatchesComposition[i].Name);
                NaturalComposition[i].Abundance = compositionWizard.CatchesComposition[i].Abundance / catchability;
                //NaturalComposition[i].Abundance = Math.Round(CatchesComposition[i].Abundance / catchability, 0);
                NaturalComposition[i].Biomass = compositionWizard.CatchesComposition[i].Biomass / catchability;
            }
        }
        
        private Report GetReport()
        {
            Report report = new Report(Resources.Reports.Title.RepCenosis);

            gearWizard.SelectedData.AddCommon(report);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddEffortSection(report);
            }

            int catchesTableNo = -1;

            if (checkBoxCatches.Checked)
            {
                catchesTableNo = report.NextTableNumber;

                compositionWizard.AddCatchesSection(report);
            }

            if (checkBoxCenosis.Checked)
            {
                AddCenosisSection(report);
            }

            // Start Appendices

            if (checkBoxAppCatches.Checked | checkBoxAppCPUE.Checked | checkBoxAppAB.Checked)
            {
                report.AddChapterTitle(Resources.Reports.Title.Appendices);
            }

            if (checkBoxAppCatches.Checked)
            {
                report.AddSectionTitle(string.Format(Resources.Reports.Section.Catches.Subtitle_single, gearWizard.SelectedSamplerType.ToDisplay()));

                foreach (Composition composition in compositionWizard.CatchesComposition.SeparateCompositions)
                {
                    if (composition.TotalQuantity < 1) continue;

                    composition.SetFormats(
                        gearWizard.AbundanceUnits, 
                        gearWizard.BiomassUnits,

                        columnSelectivityLength.DefaultCellStyle.Format,
                        columnSelectivityMass.DefaultCellStyle.Format,

                        columnSelectivityNpue.DefaultCellStyle.Format, 
                        columnSelectivityNPer.DefaultCellStyle.Format, 

                        columnSelectivityB.DefaultCellStyle.Format, 
                        columnSelectivityBpue.DefaultCellStyle.Format,
                        columnSelectivityBPer.DefaultCellStyle.Format);

                    report.AddAppendix(composition.GetClassicCatchesTable(Data.Parent));

                    report.AddComment(string.Format("Notification as in table {0}", catchesTableNo));
                }
            }

            if (checkBoxAppCPUE.Checked)
            {
                report.AddSectionTitle(Resources.Reports.Title.AppCompositionSpreadsheets);

                foreach (Report.Appendix app in compositionWizard.GetCpueAppendices())
                {
                    report.AddAppendix(app);
                }
            }

            if (checkBoxAppAB.Checked)
            {
                compositionWizard.AddAbundanceAppendices(report);
            }

            report.EndBranded();

            return report;
        }

        public void AddCenosisSection(Report report)
        {
            NaturalComposition.SetFormats(
                gearWizard.AbundanceUnits,
                gearWizard.BiomassUnits,

                "", "",

                ColumnCompositionA.DefaultCellStyle.Format,
                ColumnCompositionAP.DefaultCellStyle.Format,

                "",
                ColumnCompositionB.DefaultCellStyle.Format,
                ColumnCompositionBP.DefaultCellStyle.Format,

                ColumnCompositionOccurrance.DefaultCellStyle.Format,
                ColumnCompositionDominance.DefaultCellStyle.Format);

            NaturalComposition.AppendCenosisSectionTo(report,
                gearWizard.SelectedSamplerType,
                gearWizard.SelectedUnit);
        }



        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (gearWizard == null)
            {
                gearWizard = new WizardGearSet(Data);
                gearWizard.Returned += gearWizard_Returned;
                gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
            }

            gearWizard.Replace(this);
        }

        private void gearWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageStart);
            this.Replace(gearWizard);
        }

        private void gearWizard_AfterDataSelected(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageGearClass);
            this.Replace(gearWizard);

            NaturalComposition = gearWizard.SelectedData.GetCenosisCompositionFrame();

            comboBoxDataset.DataSource = gearWizard.SelectedStacks;

            comboBoxDataset.SelectedIndex = 0;

            columnSelectivityNpue.ResetFormatted(gearWizard.SelectedUnit.Unit);
            columnSelectivityBpue.ResetFormatted(gearWizard.SelectedUnit.Unit);
            ColumnCompositionA.ResetFormatted(gearWizard.SelectedUnit.Unit);
            ColumnCompositionB.ResetFormatted(gearWizard.SelectedUnit.Unit);
        }

        private void structureCalculator_DoWork(object sender, DoWorkEventArgs e)
        { }

        private void structureCalculator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        { }

        private void structureCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { }

        private void comboBoxDataset_SelectedIndexChanged(object sender, EventArgs e)
        {
            spreadSheetSelectivity.Rows.Clear();
            labelNoticeGearsSelectivity.Text = Wild.Resources.Interface.Interface.GetData;
            labelNoticeGearsSelectivity.Visible = true;
            SelectedStack = gearWizard.SelectedStacks[comboBoxDataset.SelectedIndex];

            comboBoxDataset.Enabled = false;

            calculatorSelectivity.RunWorkerAsync();
        }

        private void selectivityCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            SpeciesComposition composition = SelectedStack.GetCenosisComposition();
            composition.Weight = SelectedStack.GetEffort(gearWizard.SelectedSamplerType, gearWizard.SelectedUnit.Variant);
            e.Result = composition;
        }

        private void selectivityCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (SpeciesSwarm species in (SpeciesComposition)e.Result)
            {
                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.Height = spreadSheetSelectivity.RowTemplate.Height;

                gridRow.CreateCells(spreadSheetSelectivity);
                gridRow.Cells[columnSelectivitySpecies.Index].Value = species.GetLocalName();
                gridRow.Cells[columnSelectivityLength.Index].Value = new SampleDisplay(species.LengthSample);
                gridRow.Cells[columnSelectivityMass.Index].Value = new SampleDisplay(species.MassSample);
                gridRow.Cells[columnSelectivityN.Index].Value = species.Quantity;
                gridRow.Cells[columnSelectivityNPer.Index].Value = species.AbundanceFraction;
                gridRow.Cells[columnSelectivityNpue.Index].Value = species.Abundance;
                gridRow.Cells[columnSelectivityB.Index].Value = species.Mass;
                gridRow.Cells[columnSelectivityBPer.Index].Value = species.BiomassFraction;
                gridRow.Cells[columnSelectivityBpue.Index].Value = species.Biomass;

                spreadSheetSelectivity.Rows.Add(gridRow);
            }

            labelNoticeGearsSelectivity.Visible = spreadSheetSelectivity.RowCount == 0;

            comboBoxDataset.Enabled = true;
        }

        private void pageSelectivity_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) gearWizard.Replace(this);
        }

        private void pageSelectivity_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            checkBoxAppCPUE.Enabled = gearWizard.IsMultipleClasses;

            if (compositionWizard == null)
            {
                compositionWizard = new WizardComposition(Data, NaturalComposition);
                compositionWizard.Returned += compositionWizard_Returned;
                compositionWizard.Finished += compositionWizard_Finished;
            }

            compositionWizard.Replace(this);
            compositionWizard.Run(gearWizard); //, Structure);
        }

        private void compositionWizard_Returned(object sender, EventArgs e)
        {
            this.Sync(gearWizard);
            wizardExplorer.EnsureSelected(pageGearClass);
            this.Replace(compositionWizard);
        }

        private void compositionWizard_Finished(object sender, EventArgs e)
        {
            this.Replace(compositionWizard);

            checkBoxAppCatches.Enabled =
                compositionWizard.CatchesComposition.SeparateCompositions.Count > 1;
            calculatorCenosis.RunWorkerAsync();
        }

        private void pageComposition_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) compositionWizard.Replace(this);
        }

        private void cenosisCalculator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Taskbar.SetProgressValue(this.Handle, (ulong)e.ProgressPercentage, (ulong)100);
        }

        private void cenosisCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            RecalculateCenosis();
        }

        private void cenosisCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            NaturalComposition.SetLines(ColumnCompositionSpecies);

            spreadSheetComposition.ClearInsertedColumns();

            for (int i = 0; i < NaturalComposition.Count; i++)
            {
                spreadSheetComposition[ColumnCompositionA.Index, i].Value = NaturalComposition[i].Abundance;
                spreadSheetComposition[ColumnCompositionAP.Index, i].Value = NaturalComposition[i].AbundanceFraction;
                spreadSheetComposition[ColumnCompositionB.Index, i].Value = NaturalComposition[i].Biomass;
                spreadSheetComposition[ColumnCompositionBP.Index, i].Value = NaturalComposition[i].BiomassFraction;
                spreadSheetComposition[ColumnCompositionQ.Index, i].Value = 
                    Service.GetCatchability(gearWizard.SelectedSamplerType, NaturalComposition[i].Name);
                spreadSheetComposition[ColumnCompositionOccurrance.Index, i].Value = NaturalComposition[i].Occurrence;
                spreadSheetComposition[ColumnCompositionDominance.Index, i].Value = NaturalComposition[i].Dominance;
            }

            textBoxDiversity.Text = NaturalComposition.Diversity.ToString("N3");

            pageComposition.SetNavigation(true);
        }

        private void spreadSheetComposition_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnCompositionQ.Index && spreadSheetComposition.ContainsFocus)
            {
                if (spreadSheetComposition[e.ColumnIndex, e.RowIndex].Value == null)
                {
                    spreadSheetComposition[e.ColumnIndex, e.RowIndex].Value =
                        Service.GetCatchability(gearWizard.SelectedSamplerType, NaturalComposition[e.RowIndex].Name);
                }
                else
                {
                    double value = (double)spreadSheetComposition[e.ColumnIndex, e.RowIndex].Value;
                    Service.SaveCatchability(gearWizard.SelectedSamplerType, NaturalComposition[e.RowIndex].Name, value);

                    RecalculateCenosis();
                }

                for (int i = 0; i < NaturalComposition.Count; i++)
                {
                    spreadSheetComposition[ColumnCompositionA.Index, i].Value = NaturalComposition[i].Abundance;
                    spreadSheetComposition[ColumnCompositionAP.Index, i].Value = NaturalComposition[i].AbundanceFraction;
                    spreadSheetComposition[ColumnCompositionB.Index, i].Value = NaturalComposition[i].Biomass;
                    spreadSheetComposition[ColumnCompositionBP.Index, i].Value = NaturalComposition[i].BiomassFraction;
                }
            }
        }

        private void checkBoxCatches_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAppCatches.Enabled = checkBoxAppCPUE.Enabled = checkBoxCatches.Checked && gearWizard.IsMultipleClasses;
        }

        private void checkBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Enabled)
            {
                ((CheckBox)sender).Checked = false;
            }
        }

        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
            checkBoxCatches_CheckedChanged(sender, e);
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
            Log.Write(EventType.WizardEnded, "Cenosis wizard is finished.");
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}