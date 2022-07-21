using System;
using System.Windows.Forms;
using static Mayfly.Wild.SettingsReader;
using Mayfly.Controls;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsControlPrint : SettingsControl, ISettingsControl
    {
        public SettingsControlPrint() {

            InitializeComponent();
        }



        public void LoadSettings() {

            checkBoxCardOdd.Checked = OddCardStart;
            checkBoxBreakBeforeIndividuals.Checked = BreakBeforeIndividuals;
            checkBoxBreakBetweenSpecies.Checked = BreakBetweenSpecies;
            checkBoxOrderLog.Checked = LogOrder != LogSortOrder.AsInput;
            if (LogOrder != LogSortOrder.AsInput)
                comboBoxLogOrder.SelectedIndex = (int)LogOrder;
        }

        public void SaveSettings() {

            OddCardStart = checkBoxCardOdd.Checked;
            BreakBeforeIndividuals = checkBoxBreakBeforeIndividuals.Checked;
            BreakBetweenSpecies = checkBoxBreakBetweenSpecies.Checked;
            LogOrder = checkBoxOrderLog.Checked ? (LogSortOrder)comboBoxLogOrder.SelectedIndex : LogSortOrder.AsInput;

        }



        private void checkBoxBreakBeforeIndividuals_CheckedChanged(object sender, EventArgs e) {
            checkBoxBreakBetweenSpecies.Enabled = checkBoxBreakBeforeIndividuals.Checked;
            if (!checkBoxBreakBeforeIndividuals.Checked) checkBoxBreakBetweenSpecies.Checked = false;
        }

        private void checkBoxOrderLog_CheckedChanged(object sender, EventArgs e) {
            comboBoxLogOrder.Enabled = checkBoxOrderLog.Checked;
        }
    }
}
