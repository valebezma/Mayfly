using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer.Survey
{
    public partial class WizardCenosisComposition : Form
    {
        public CardStack Data { get; set; }

        private WizardGearSet gearWizard;



        public CompositionEqualizer CatchesComposition;

        public event EventHandler CatchesEstimated;

        public event EventHandler Finished;

        public event EventHandler Returned;



        private WizardCenosisComposition()
        {
            InitializeComponent();

            ColumnCatchesSex.ValueType =
                ColumnCatchesSpecies.ValueType =
                typeof(string);

            ColumnCatchesL.ValueType =
                ColumnCatchesW.ValueType =
                typeof(SampleDisplay);

            ColumnCatchesN.ValueType =
                typeof(int);

            ColumnCatchesNPUE.ValueType =
                ColumnCatchesNPUEp.ValueType =
                ColumnCatchesB.ValueType =
                ColumnCatchesBPUE.ValueType =
                ColumnCatchesBPUEp.ValueType =
                typeof(double);

            ColumnCommSpecies.ValueType =
                typeof(string);

            ColumnCommAbundance.ValueType =
                ColumnCommAbundanceP.ValueType =
                ColumnCommBiomass.ValueType =
                ColumnCommBiomassP.ValueType =
                ColumnCommQ.ValueType =
                ColumnCommDominance.ValueType =
                typeof(double);

            this.RestoreAllCheckStates();
        }

        public WizardCenosisComposition(CardStack data) : this()
        {
            Data = data;

            Log.Write(EventType.WizardStarted, "Cenosis composition wizard is started.");
        }



        public void Run(WizardGearSet _gearWizard, SpeciesComposition _example)
        {
            gearWizard = _gearWizard;
            //gearWizard.Replace(this);

            ColumnCatchesNPUE.ResetFormatted(gearWizard.SelectedUnit.Unit);
            ColumnCatchesBPUE.ResetFormatted(gearWizard.SelectedUnit.Unit);

            checkBoxPUE.ResetFormatted(gearWizard.SelectedUnit.Unit);

            comboBoxParameter.Enabled = checkBoxFractions.Enabled = checkBoxPUE.Enabled = false;
            pageComposition.SetNavigation(false);
        }



        private void wizardPage1_Initialize(object sender, WizardPageInitEventArgs e)
        {
            wizardExplorer.NextPage();
        }

        private void pageComposition_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Returned != null)
            {
                e.Cancel = true;
                Returned.Invoke(sender, e);
            }
        }

        private void displayParameter_Changed(object sender, EventArgs e)
        {
            if (comboBoxParameter.SelectedIndex == -1) return;

            if (checkBoxFractions.Checked)
            {
                spreadSheetComposition.DefaultCellStyle.Format = "P1";
            }
            //else if (checkBoxPUE.Checked)
            //{
            //    spreadSheetComposition.DefaultCellStyle.Format = Mayfly.Service.Mask(3);
            //}
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
                Category.GetValueVariant(comboBoxParameter.SelectedIndex == 0,
                checkBoxPUE.Checked, checkBoxFractions.Checked));
        }

        private void checkBoxFractions_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxPUE.Enabled = !checkBoxFractions.Checked;

            displayParameter_Changed(sender, e);
        }

        private void pageCatches_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (CatchesEstimated != null)
            {
                CatchesEstimated.Invoke(this, e);
                e.Cancel = true;
            }
        }

        private void pageCenosis_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            Log.Write(EventType.WizardEnded, "Cenosis composition wizard is finished.");

            if (Finished != null)
            {
                e.Cancel = true;
                Finished.Invoke(sender, e);
            }
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}