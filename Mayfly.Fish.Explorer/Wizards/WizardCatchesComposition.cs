using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer.Survey
{
    public partial class WizardCatchesComposition : Form
    {
        public CardStack Data { get; set; }

        internal WizardGearSet gearWizard;

        public Data.SpeciesRow SpeciesRow;

        public CompositionEqualizer CatchesComposition;

        private Composition example;

        private Composition selectedShoal;

        public event EventHandler Finished;

        public event EventHandler Calculated;

        public event EventHandler Returned;

        public string CategoryType { get; set; }




        private WizardCatchesComposition()
        {
            InitializeComponent();

            ColumnCatchesAbundance.ValueType =
                ColumnCatchesAbundanceP.ValueType =
                ColumnCatchesBiomass.ValueType =
                ColumnCatchesBiomassP.ValueType =
                typeof(double);

            columnComposition.ValueType=
                ColumnCatchesCategory.ValueType =
                ColumnCatchesSex.ValueType =
                typeof(string);
        }

        public WizardCatchesComposition(CardStack data, Data.SpeciesRow speciesRow, Composition _example) : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            if (_example is AgeComposition) {
                CategoryType = Resources.Reports.CatchComposition.TypeAge;
            } else if (_example is LengthComposition) {
                CategoryType = Resources.Reports.CatchComposition.TypeLength;
            } else if (_example is SpeciesComposition) {
                CategoryType = Resources.Reports.CatchComposition.TypeSpecies;
            }

            example = _example;
            CatchesComposition = new CompositionEqualizer(_example);

            wizardExplorer.ResetTitle(speciesRow.GetFullName());

            _example.SetLines(columnComposition);
            _example.SetLines(ColumnCatchesCategory);

            Log.Write(EventType.WizardStarted, "Catches Composition wizard is started for {0}.", speciesRow.Species);
        }



        public void Run()
        {
            gearWizard = new WizardGearSet(Data, SpeciesRow);
            gearWizard.AfterEffortCalculated += gearWizard_AfterEffortCalculated;
            gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
            gearWizard.Returned += gearWizard_Returned;
            gearWizard.Replace(this);
        }

        private void gearWizard_AfterEffortCalculated(object sender, EventArgs e)
        {
            gearWizard.AllowFinish = gearWizard.GearData.GetSpecies().Contains(SpeciesRow);
        }

        private void gearWizard_AfterDataSelected(object sender, EventArgs e)
        {
            this.Replace(gearWizard);

            ColumnCatchesAbundance.ResetFormatted(gearWizard.SelectedUnit.Unit);
            ColumnCatchesBiomass.ResetFormatted(gearWizard.SelectedUnit.Unit);
            checkBoxPUE.ResetFormatted(gearWizard.SelectedUnit.Unit);

            pageComposition.SetNavigation(false);
            comboBoxParameter.Enabled = false;

            calculatorStructure.RunWorkerAsync();
        }

        private void gearWizard_Returned(object sender, EventArgs e)
        {
            //wizardExplorer.EnsureSelected(pageSelectivity);
            //stack.Replace(gearWizard);

            if (Returned != null) {
                Returned.Invoke(sender, e);
            }
        }

        public void Run(WizardGearSet _gearWizard)
        {
            gearWizard = _gearWizard;
            gearWizard_AfterDataSelected(gearWizard, new EventArgs());
        }


        public void AddCatchesDescription(Report report)
        {
            report.AddParagraph(
                string.Format(Resources.Reports.CatchComposition.Paragraph1,
                CategoryType, SpeciesRow.ToHTML(), report.NextTableNumber)
                );

            if (example is AgeComposition)
            {
                if (UserSettings.AgeSuggest && Data.Parent.GrowthModels.GetSpecies().Contains(SpeciesRow.Species))
                {
                    report.AddParagraph(Resources.Reports.CatchComposition.Paragraph2,
                            SpeciesRow.ToHTML());

                    report.AddEquation(Data.Parent.GrowthModels.GetCombinedScatterplot(SpeciesRow.Species).Regression.GetEquation("L", "t", "N2"));

                    if (Data.Parent.GrowthModels.GetExternalScatterplot(SpeciesRow.Species) != null)
                    {
                        report.AddParagraph(Resources.Reports.CatchComposition.Paragraph3,
                            Data.Parent.GrowthModels.Authors.Merge(),
                            Data.Parent.GrowthModels.Description);
                    }
                }
            }

            CatchesComposition.AddReport(report, 
                string.Format(Resources.Reports.CatchComposition.Table1, CategoryType), gearWizard.SelectedUnit.Unit, 
                (CompositionReportContent.Absolute | CompositionReportContent.Relative),
                ColumnCatchesAbundance.DefaultCellStyle.Format, ColumnCatchesBiomass.DefaultCellStyle.Format,
                ColumnCatchesAbundanceP.DefaultCellStyle.Format, ColumnCatchesBiomassP.DefaultCellStyle.Format);

            report.AddComment(string.Format(Mayfly.Fish.Explorer.Resources.Reports.CatchComposition.Notice1,
                CategoryType, gearWizard.SelectedUnit.Unit));
        }

        public Report GetCatchesRoutines()
        {
            Report report = new Report(
                string.Format(Resources.Reports.CatchComposition.AppendixHeader1,
                    CategoryType, SpeciesRow.ToHTML()), 
                PageBreakOption.Landscape);

            CatchesComposition.AddAppendix(report, string.Format(Resources.Reports.CatchComposition.App1,
                CategoryType, SpeciesRow.ToHTML()), Resources.Reports.GearStats.ColumnGearClass,
                ValueVariant.Quantity, string.Empty);

            CatchesComposition.AddAppendix(report, string.Format(Resources.Reports.CatchComposition.App2,
                CategoryType, SpeciesRow.ToHTML()), Resources.Reports.GearStats.ColumnGearClass,
                ValueVariant.Mass, "N3");

            CatchesComposition.AddAppendix(report, string.Format(Resources.Reports.CatchComposition.App3,
                CategoryType, SpeciesRow.ToHTML(), gearWizard.SelectedUnit.Unit), Resources.Reports.GearStats.ColumnGearClass,
                ValueVariant.Abundance, ColumnCatchesAbundance.DefaultCellStyle.Format);

            CatchesComposition.AddAppendix(report, string.Format(Resources.Reports.CatchComposition.App4,
                CategoryType, SpeciesRow.ToHTML(), gearWizard.SelectedUnit.Unit), Resources.Reports.GearStats.ColumnGearClass,
                ValueVariant.Biomass, ColumnCatchesBiomass.DefaultCellStyle.Format);

            report.EndBranded();

            return report;
        }

        public Report GetAgeRecoveryRoutines()
        {
            Report report = new Report(string.Format(Resources.Reports.CatchComposition.AppendixHeader2,
                SpeciesRow.ToHTML()));

            foreach (Composition classComposition in CatchesComposition.SeparateCompositions)
            {
                if (!(classComposition is AgeKey)) continue;

                AgeKey ageComposition = (AgeKey)classComposition;

                if (!ageComposition.IsRecovered) continue;

                report.AddTable(ageComposition.GetReport());

                //ageComposition.AddReport(report, string.Format(Resources.Reports.Selectivity.Table2, classComposition.Name), gearWizard.SelectedUnit.Unit);
            }

            report.EndBranded();

            return report;
        }


        private void UpdateResults()
        {
            for (int i = 0; i < CatchesComposition.Count; i++)
            {
                if (CatchesComposition[i].Quantity == 0)
                {
                    spreadSheetCatches[ColumnCatchesAbundance.Index, i].Value = null;
                    spreadSheetCatches[ColumnCatchesAbundanceP.Index, i].Value = null;
                    spreadSheetCatches[ColumnCatchesBiomass.Index, i].Value = null;
                    spreadSheetCatches[ColumnCatchesBiomassP.Index, i].Value = null;
                }
                else
                {
                    spreadSheetCatches[ColumnCatchesAbundance.Index, i].Value = CatchesComposition[i].Abundance;
                    spreadSheetCatches[ColumnCatchesAbundanceP.Index, i].Value = CatchesComposition[i].AbundanceFraction;
                    spreadSheetCatches[ColumnCatchesBiomass.Index, i].Value = CatchesComposition[i].Biomass;
                    spreadSheetCatches[ColumnCatchesBiomassP.Index, i].Value = CatchesComposition[i].BiomassFraction;
                }

                spreadSheetCatches.Rows[i].DefaultCellStyle.ForeColor =
                    CatchesComposition[i].Quantity == 0 ? Constants.InfantColor : spreadSheetCatches.ForeColor;
            }
        }

        internal void Split(int j)
        {
            CatchesComposition.Split(j, Service.GetMeasure(SpeciesRow.Species) * 10);
            CatchesComposition.SetLines(columnComposition);
            CatchesComposition.SetLines(ColumnCatchesCategory);
            CatchesComposition.SeparateCompositions.ToArray().UpdateValues(spreadSheetComposition, columnComposition, 
                Category.GetValueVariant(comboBoxParameter.SelectedIndex == 0,
                checkBoxPUE.Checked, checkBoxFractions.Checked));

            UpdateResults();

            spreadSheetComposition.ClearSelection();
            spreadSheetComposition.Rows[j].Selected = true;
        }




        
        private void calculatorStructure_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Mayfly.Controls.Taskbar.SetProgressValue(this.Handle, (ulong)e.ProgressPercentage, (ulong)100);
        }

        private void calculatorStructure_DoWork(object sender, DoWorkEventArgs e)
        {
            CatchesComposition = gearWizard.SelectedStacks.ToArray().GetWeightedComposition(
                gearWizard.WeightType, gearWizard.SelectedUnit.Variant, example, SpeciesRow, gearWizard.GearData.Mass(SpeciesRow));
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
                spreadSheetCatches[ColumnCatchesAbundance.Index, i].Value = CatchesComposition[i].Abundance;
                spreadSheetCatches[ColumnCatchesAbundanceP.Index, i].Value = CatchesComposition[i].AbundanceFraction;
                spreadSheetCatches[ColumnCatchesBiomass.Index, i].Value = CatchesComposition[i].Biomass;
                spreadSheetCatches[ColumnCatchesBiomassP.Index, i].Value = CatchesComposition[i].BiomassFraction;
                spreadSheetCatches[ColumnCatchesSex.Index, i].Value = CatchesComposition[i].GetSexualComposition();

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

        private void wizardPage1_Initialize(object sender, WizardPageInitEventArgs e)
        {
            wizardExplorer.NextPage();
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
            if (selectedShoal is AgeKey)
            {
                ((AgeKey)selectedShoal).ShowDialog();
            }
            else if (selectedShoal is AgeComposition)
            {
                ((AgeComposition)selectedShoal).GetHistogram().ShowOnChart(true);
            }

            if (selectedShoal is LengthComposition)
            {
                ((LengthComposition)selectedShoal).GetHistogram().ShowOnChart(true);
            }
        }

        private void contextComposition_Opening(object sender, CancelEventArgs e)
        {
            int ri = spreadSheetComposition.SelectedRows[0].Index;

            if (CatchesComposition[ri] is AgeGroup)
            {
                AgeGroup age = (AgeGroup)CatchesComposition[ri];
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
            //if (Returned != null)
            //{
            //    Returned.Invoke(sender, e);
            //}
            //else
            //{
                gearWizard.Replace(this);
            //}
        }

        private void pageCatches_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            Log.Write(EventType.WizardEnded, "Catches Composition wizard is finished for {0}, dominants are {1}.",
                SpeciesRow.Species, CatchesComposition.GetDominants().Merge());

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
    }
}
