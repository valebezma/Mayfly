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
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Fish.Explorer.Survey
{
    public partial class WizardCommunity : Form
    {
        public CardStack Data { get; set; }

        private WizardGearSet gearWizard;

        private WizardCommunityComposition compositionWizard;

        private SpeciesComposition Structure;



        private WizardCommunity()
        { 
            InitializeComponent();

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
        }

        public WizardCommunity(CardStack data) : this()
        {
            Data = data;

            Log.Write(EventType.WizardStarted, "Community wizard is started.");
        }

        
        
        private Report GetReport()
        {
            Report report = new Report(Resources.Reports.Community.Title);

            gearWizard.SelectedData.AddCommon(report);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddEffortDescription(report);
            }

            if (checkBoxCatches.Checked)
            {
                compositionWizard.CatchesComposition.AddCatchDescription(Data.Parent, report,
                    gearWizard.AbundanceUnits, gearWizard.BiomassUnits);
            }

            if (checkBoxCommunity.Checked)
            {
                compositionWizard.AddEstimatedComposition(report);
            }

            report.EndBranded();

            return report;
        }

        //private void AddBiology(Report report)
        //{
        //    report.AddParagraph(
        //        string.Format(Resources.Reports.Community.ParagraphBio,
        //        Structure.Count,
        //        Data.Parent.Species.FindBySpecies(Structure.MostAbundant.Name).ToHTML(),
        //        Structure.MostAbundant.Quantity,
        //        Structure.MostAbundant.Quantity / Structure.TotalQuantity,
        //        report.NextTableNumber)
        //        );

        //    Report.Table table1 = new Report.Table(Resources.Reports.Community.TableBio);

        //    table1.StartRow();

        //    table1.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .25);
        //    table1.AddHeaderCell(Wild.Resources.Reports.Caption.Quantity);
        //    table1.AddHeaderCell(Resources.Reports.Common.MassUnits);
        //    table1.AddHeaderCell(Wild.Resources.Reports.Caption.Length + " *");
        //    table1.AddHeaderCell(Fish.Resources.Reports.Caption.Mass + " **");

        //    table1.EndRow();

        //    foreach (Category species in Structure)
        //    {
        //        Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(species.Name);

        //        table1.StartRow();
        //        table1.AddCell(speciesRow.ToShortHTML());
        //        table1.AddCellRight(species.Quantity, columnBioN.DefaultCellStyle.Format);
        //        table1.AddCellRight(species.Mass, columnBioB.DefaultCellStyle.Format);
        //        table1.AddCellValue(new SampleDisplay(species.LengthSample), columnBioLength.DefaultCellStyle.Format);
        //        table1.AddCellValue(new SampleDisplay(species.MassSample), columnBioMass.DefaultCellStyle.Format);
        //        table1.EndRow();
        //    }

        //    table1.StartRow();
        //    table1.AddCell(Mayfly.Resources.Interface.Total);

        //    table1.AddCellRight(Structure.TotalQuantity, columnBioN.DefaultCellStyle.Format);
        //    table1.AddCellRight(Structure.TotalMass, columnBioB.DefaultCellStyle.Format);
        //    table1.AddCell();
        //    table1.AddCell();
        //    table1.EndRow();
        //    report.AddTable(table1);

        //    report.AddComment(string.Format(Resources.Reports.Common.FormatNotice,
        //        Mathematics.Resources.FormatNotice.ResourceManager.GetString(columnBioLength.DefaultCellStyle.Format),
        //        Mathematics.Resources.FormatNotice.ResourceManager.GetString(columnBioMass.DefaultCellStyle.Format)));
            
        //}







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
            this.Replace(gearWizard);
            pageBiology.SetNavigation(false);
            checkBoxAppendices.Enabled = gearWizard.IsMultipleClasses;
            calculatorCommunity.RunWorkerAsync();
        }

        private void communityCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            Structure = gearWizard.SelectedData.GetCommunityComposition(gearWizard.SelectedUnit.Variant);
        }

        private void communityCalculator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Taskbar.SetProgressValue(this.Handle, (ulong)e.ProgressPercentage, (ulong)100);
        }

        private void communityCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        private void pageBiology_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) gearWizard.Replace(this);
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
                compositionWizard = new WizardCommunityComposition(Data);
                compositionWizard.Returned += compositionWizard_Returned;
                compositionWizard.Finished += compositionWizard_Finished;
            }

            compositionWizard.Replace(this);
            compositionWizard.Run(gearWizard, Structure);
        }

        private void compositionWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageBiology);
            this.Replace(compositionWizard);
        }

        private void compositionWizard_Finished(object sender, EventArgs e)
        {
            this.Replace(compositionWizard);
        }

        private void pageReport_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) compositionWizard.Replace(this);
        }

        private void checkBoxCatches_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAppendices.Enabled = checkBoxCatches.Checked && gearWizard.IsMultipleClasses;
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
            List<Report> result = new List<Report>();

            result.Add(GetReport());

            if (checkBoxAppendices.Checked)
            {
                result.Add(compositionWizard.GetCpueAppendices());
            }

            if (checkBoxAbundance.Checked)
            {
                result.Add(compositionWizard.GetAbundanceAppendices());
            }

            e.Result = result.ToArray();
        }

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Report rep in (Report[])e.Result) {
                rep.Run();
            }
            pageReport.SetNavigation(true);
            Log.Write(EventType.WizardEnded, "Community wizard is finished, dominants are {0}.", 
                Structure.GetDominantNames().Merge());
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}