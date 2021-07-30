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

        private WizardCenosisComposition compositionWizard;

        public CardStack SelectedStack { get; private set; }

        private SpeciesComposition Structure;



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

            columnBioB.ValueType =
                typeof(double);

            columnBioLength.ValueType =
                columnBioMass.ValueType =
                typeof(SampleDisplay);

            columnBioN.ValueType =
                typeof(int);

            columnBioSpecies.ValueType =
                typeof(string);

            labelBioInstruction.ResetFormatted(spreadSheetBiology.RowVisibilityKey);

            this.RestoreAllCheckStates();
        }

        public WizardCenosis(CardStack data) : this()
        {
            Data = data;

            Log.Write(EventType.WizardStarted, "Cenosis wizard is started.");
        }



        private Report GetReport()
        {
            Report report = new Report(Resources.Reports.Title.RepCenosis);

            gearWizard.SelectedData.AddCommon(report);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddSummarySection(report);
            }

            int catchesTableNo = -1;

            if (checkBoxCatches.Checked)
            {
                report.AddSectionTitle(Resources.Reports.Section.Catches.Subtitle);

                report.AddParagraph(
                    string.Format(Resources.Reports.Section.Catches.Paragraph_2,
                    compositionWizard.CatchesComposition.Count,
                    Data.Parent.Species.FindBySpecies(compositionWizard.CatchesComposition.MostAbundant.Name).ToHTML(),
                    compositionWizard.CatchesComposition.MostAbundant.Quantity,
                    compositionWizard.CatchesComposition.MostAbundant.Quantity / compositionWizard.CatchesComposition.TotalQuantity,
                    report.NextTableNumber)
                    );

                Report.Table tableCatches = compositionWizard.CatchesComposition.GetSummaryTable(Data.Parent, report,
                    gearWizard.AbundanceUnits, 
                    gearWizard.BiomassUnits,
                    columnSelectivityLength.DefaultCellStyle.Format, 
                    columnSelectivityMass.DefaultCellStyle.Format,
                    columnSelectivityB.DefaultCellStyle.Format, 
                    columnSelectivityNpue.DefaultCellStyle.Format,
                    columnSelectivityBpue.DefaultCellStyle.Format,
                    columnSelectivityNPer.DefaultCellStyle.Format, 
                    columnSelectivityBPer.DefaultCellStyle.Format);

                catchesTableNo = report.NextTableNumber;

                report.AddTable(tableCatches);

                report.AddComment(string.Format(Resources.Reports.Common.FormatNotice,
                    Mathematics.Resources.FormatNotice.ResourceManager.GetString(columnSelectivityLength.DefaultCellStyle.Format),
                    Mathematics.Resources.FormatNotice.ResourceManager.GetString(columnSelectivityMass.DefaultCellStyle.Format)));

                

                //if (compositionWizard.CatchesComposition.NonEmptyCount > 1)
                //{
                //    Data.SpeciesRow mostAbundant = Data.Parent.Species.FindBySpecies(compositionWizard.CatchesComposition.MostAbundant.Name);
                //    Data.SpeciesRow mostAbundantByMass = Data.Parent.Species.FindBySpecies(compositionWizard.CatchesComposition.MostAbundantByMass.Name);

                //    report.AddParagraph(
                //        string.Format(Resources.Reports.GearClass.Paragraph2,
                //            mostAbundant.ToHTML(), composition.MostAbundant.AbundanceFraction,
                //            mostAbundantByMass.ToHTML(), composition.MostAbundantByMass.BiomassFraction)
                //            );
                //}
            }

            if (checkBoxCenosis.Checked)
            {
                compositionWizard.AddSummarySection(report);
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
                    if (composition.TotalQuantity < 1)
                    {
                        //report.AddParagraph(
                        //    string.Format(Resources.Reports.Section.Catches.Paragraph_1, composition.Name)
                        //    );
                        continue;
                    }

                    //composition.Name = string.Format("{0} {1}", gearWizard.SelectedSamplerType.ToDisplay(), composition.Name);

                    Report.Appendix tableSingleClassCatches =  composition.GetSummaryTable(Data.Parent, report,
                        gearWizard.AbundanceUnits, 
                        gearWizard.BiomassUnits,
                        columnSelectivityLength.DefaultCellStyle.Format,
                        columnSelectivityMass.DefaultCellStyle.Format,
                        columnSelectivityB.DefaultCellStyle.Format, 
                        columnSelectivityNpue.DefaultCellStyle.Format, 
                        columnSelectivityBpue.DefaultCellStyle.Format,
                        columnSelectivityNPer.DefaultCellStyle.Format, 
                        columnSelectivityBPer.DefaultCellStyle.Format);

                    report.AddAppendix(tableSingleClassCatches);

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
            wizardExplorer.EnsureSelected(pageSelectivity);
            this.Replace(gearWizard);

            comboBoxDataset.DataSource = gearWizard.SelectedStacks;

            pageSelectivity.SetNavigation(true);
            comboBoxDataset.SelectedIndex = 0;

            columnSelectivityNpue.ResetFormatted(gearWizard.SelectedUnit.Unit);
            columnSelectivityBpue.ResetFormatted(gearWizard.SelectedUnit.Unit);
        }

        private void comboBoxDataset_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageSelectivity.SetNavigation(false);

            spreadSheetSelectivity.Rows.Clear();
            labelNoticeGearsSelectivity.Text = Wild.Resources.Interface.Interface.GetData;
            labelNoticeGearsSelectivity.Visible = true;
            SelectedStack = gearWizard.SelectedStacks[comboBoxDataset.SelectedIndex];

            comboBoxDataset.Enabled = false;

            calculatorSelectivity.RunWorkerAsync();
        }

        private void selectivityCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            Composition composition = SelectedStack.GetCenosisComposition();
            composition.Weight = SelectedStack.GetEffort(gearWizard.SelectedSamplerType, gearWizard.SelectedUnit.Variant);

            e.Result = composition;
        }

        private void selectivityCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Category species in (Composition)e.Result)
            {
                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.Height = spreadSheetSelectivity.RowTemplate.Height;

                gridRow.CreateCells(spreadSheetSelectivity);
                gridRow.Cells[columnSelectivitySpecies.Index].Value = species.Name;
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

            pageSelectivity.SetNavigation(true);
        }

        private void pageSelectivity_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) gearWizard.Replace(this);
        }

        private void pageSelectivity_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageBiology.SetNavigation(false);
            checkBoxAppCPUE.Enabled = gearWizard.IsMultipleClasses;
            calculatorCenosis.RunWorkerAsync();
        }



        private void cenosisCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            Structure = gearWizard.SelectedData.GetCenosisComposition(gearWizard.SelectedUnit.Variant);
        }

        private void cenosisCalculator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Taskbar.SetProgressValue(this.Handle, (ulong)e.ProgressPercentage, (ulong)100);
        }

        private void cenosisCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Structure.SetLines(columnBioSpecies);

            for (int i = 0; i < Structure.Count; i++)
            {
                if (Structure[i].Quantity == 0)
                {
                    spreadSheetBiology[columnBioLength.Index, i].Value = null;
                    spreadSheetBiology[columnBioMass.Index, i].Value = null;
                    spreadSheetBiology[columnBioN.Index, i].Value = null;
                    spreadSheetBiology[columnBioB.Index, i].Value = null;
                }
                else
                {
                    spreadSheetBiology[columnBioLength.Index, i].Value = new SampleDisplay(Structure[i].LengthSample);
                    spreadSheetBiology[columnBioMass.Index, i].Value = new SampleDisplay(Structure[i].MassSample);
                    spreadSheetBiology[columnBioN.Index, i].Value = Structure[i].Quantity;
                    spreadSheetBiology[columnBioB.Index, i].Value = Structure[i].Mass;
                }

                spreadSheetBiology.Rows[i].Height = spreadSheetBiology.RowTemplate.Height;
                spreadSheetBiology.Rows[i].DefaultCellStyle.ForeColor =
                    Structure[i].Quantity == 0 ? Constants.InfantColor : spreadSheetBiology.ForeColor;
            }

            pageBiology.SetNavigation(true);
        }

        private void pageBiology_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            // Change Structure according to hidden rows
            foreach (DataGridViewRow gridRow in spreadSheetBiology.Rows)
            {
                if (spreadSheetBiology.IsHidden(gridRow))
                {
                    Category spc = Structure.GetCategory((string)gridRow.Cells[columnBioSpecies.Index].Value);
                    Structure.Remove(spc);
                }
            }

            if (compositionWizard == null)
            {
                compositionWizard = new WizardCenosisComposition(Data);
                compositionWizard.Returned += compositionWizard_Returned;
                compositionWizard.Finished += compositionWizard_Finished;
            }

            compositionWizard.Replace(this);
            compositionWizard.Run(gearWizard, Structure);
        }

        private void compositionWizard_Returned(object sender, EventArgs e)
        {
            this.Sync(gearWizard);
            wizardExplorer.EnsureSelected(pageBiology);
            this.Replace(compositionWizard);
        }

        private void compositionWizard_Finished(object sender, EventArgs e)
        {
            this.Replace(compositionWizard);
            checkBoxAppCatches.Enabled =
                compositionWizard.CatchesComposition.SeparateCompositions.Count > 1;
        }

        private void pageReport_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) compositionWizard.Replace(this);
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
            Log.Write(EventType.WizardEnded, "Cenosis wizard is finished, dominants are {0}.", 
                Structure.GetDominantNames().Merge());
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}