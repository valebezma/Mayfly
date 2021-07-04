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
using Mayfly.Fish.Explorer.Survey;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Fish.Explorer.Survey
{
    public partial class WizardGearClass : Form
    {
        public CardStack Data { get; set; }

        private WizardGearSet gearWizard;

        private WizardCommunityComposition compositionWizard;

        public CardStack SelectedStack { get; private set; }



        private WizardGearClass()
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
        }

        public WizardGearClass(CardStack data)
            : this()
        {
            Data = data;

            Log.Write(EventType.WizardStarted, "Gear class wizard is started.");
        }



        private Report GetReport()
        {
            Report report = new Report(Resources.Reports.GearClass.Title);

            gearWizard.SelectedData.AddCommon(report);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddEffortDescription(report);
            }

            if (checkBoxCatchesPerClass.Checked)
            {
                if (checkBoxCatches.Checked) report.AddSubtitle(string.Format(Resources.Reports.GearClass.Title_2, gearWizard.SelectedSamplerType.ToDisplay()));

                report.AddParagraph(Resources.Reports.GearClass.Paragraph1_1);
                
                foreach (Composition composition in compositionWizard.CatchesComposition.SeparateCompositions)
                {
                    AddCompositionDescription(report, composition);
                }
            }

            if (checkBoxCatches.Checked)
            {
                if (checkBoxCatchesPerClass.Checked) report.AddSubtitle(Resources.Reports.GearClass.Title_3);

                AddCompositionDescription(report, compositionWizard.CatchesComposition);

                //compositionWizard.AddCatchesComposition(report);
            }

            report.EndBranded();

            return report;
        }

        public void AddCompositionDescription(Report report, Composition composition)
        {
            composition.AddCatchDescription(Data.Parent, report,
                gearWizard.AbundanceUnits, gearWizard.BiomassUnits,
                columnSelectivityLength.DefaultCellStyle.Format, columnSelectivityMass.DefaultCellStyle.Format,
                columnSelectivityB.DefaultCellStyle.Format, columnSelectivityNpue.DefaultCellStyle.Format, columnSelectivityBpue.DefaultCellStyle.Format,
                columnSelectivityNPer.DefaultCellStyle.Format, columnSelectivityBPer.DefaultCellStyle.Format);

            if (composition.NonEmptyCount > 1)
            {
                Data.SpeciesRow mostAbundant = Data.Parent.Species.FindBySpecies(composition.MostAbundant.Name);
                Data.SpeciesRow mostAbundantByMass = Data.Parent.Species.FindBySpecies(composition.MostAbundantByMass.Name);

                report.AddParagraph(
                    string.Format(Resources.Reports.GearClass.Paragraph2,
                        mostAbundant.ToHTML(), composition.MostAbundant.AbundanceFraction,
                        mostAbundantByMass.ToHTML(), composition.MostAbundantByMass.BiomassFraction)
                        );
            }
        }




        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (gearWizard == null)
            {
                gearWizard = new WizardGearSet(Data);
                gearWizard.Returned += gearWizard_Returned;
                gearWizard.AllowSpatialRatios = false;
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
            Composition composition = SelectedStack.GetCommunityComposition();
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
            if (compositionWizard == null)
            {
                compositionWizard = new WizardCommunityComposition(gearWizard.SelectedData);
                compositionWizard.Returned += compositionWizard_Returned;
                compositionWizard.CatchesEstimated += compositionWizard_CatchesEstimated;
            }

            compositionWizard.Replace(this);
            compositionWizard.Run(gearWizard, null);
        }

        private void compositionWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageSelectivity);
            this.Replace(compositionWizard);
        }

        private void compositionWizard_CatchesEstimated(object sender, EventArgs e)
        {
            this.Replace(compositionWizard);
            checkBoxCatchesPerClass.Enabled =
                checkBoxCatchesPerClass.Checked = 
                compositionWizard.CatchesComposition.SeparateCompositions.Count > 1;
        }

        private void pageReport_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) compositionWizard.Replace(this);
        }

        private void PageReport_Initialize(object sender, WizardPageInitEventArgs e)
        {
        }

        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
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

            e.Result = result.ToArray();
        }

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Report rep in (Report[])e.Result)
            {
                rep.Run();
            }
            pageReport.SetNavigation(true);
            Log.Write(EventType.WizardEnded, "Gear class wizard is finished for {0}.", gearWizard.CurrentStack.Name);
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
