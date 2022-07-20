using System;
using System.Windows.Forms;
using static Mayfly.Wild.SettingsReader;
using static Mayfly.Wild.UserSettings;
using static Mayfly.UserSettings;
using System.IO;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsControlReader : UserControl, ISettingControl
    {
        public SettingsControlReader() {

            InitializeComponent();
        }

        public void LoadSettings() {

            textBoxWaters.Text = WatersIndexPath;
            textBoxSpecies.Text = TaxonomicIndexPath;

            checkBoxSpeciesExpand.Checked = SpeciesAutoExpand;
            checkBoxSpeciesExpandVisualControl.Checked = SpeciesAutoExpandVisual;

            checkBoxAutoLog.Checked = AutoLogOpen;
            checkBoxFixTotals.Checked = FixTotals;
            checkBoxAutoIncreaseBio.Checked = AutoIncreaseBio;
            checkBoxAutoDecreaseBio.Checked = AutoDecreaseBio;

            numericUpDownRecentCount.Value = RecentSpeciesCount;
        }

        public void SaveSettings() {

            WatersIndexPath = textBoxWaters.Text;
            TaxonomicIndexPath = textBoxSpecies.Text;
            SpeciesAutoExpand = checkBoxSpeciesExpand.Checked;
            SpeciesAutoExpandVisual = checkBoxSpeciesExpandVisualControl.Checked;


            FixTotals = checkBoxFixTotals.Checked;
            AutoIncreaseBio = checkBoxAutoIncreaseBio.Checked;
            AutoDecreaseBio = checkBoxAutoDecreaseBio.Checked;
            AutoLogOpen = checkBoxAutoLog.Checked;
            RecentSpeciesCount = (int)numericUpDownRecentCount.Value;

        }

        public void buttonBrowseWaters_Click(object sender, EventArgs e) {
            if (Waters.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK) {
                textBoxWaters.Text = Waters.UserSettings.Interface.OpenDialog.FileName;
            }
        }

        private void buttonOpenWaters_Click(object sender, EventArgs e) {
            IO.RunFile(textBoxWaters.Text);
        }

        private void buttonBrowseSpecies_Click(object sender, EventArgs e) {
            OpenFileDialog SetSpecies = Species.UserSettings.Interface.OpenDialog;

            if (SetSpecies.ShowDialog() == DialogResult.OK) {
                textBoxSpecies.Text = SetSpecies.FileName;
            }
        }

        private void buttonOpenSpecies_Click(object sender, EventArgs e) {
            IO.RunFile(textBoxSpecies.Text);
        }

        private void checkBoxSpeciesExpand_CheckedChanged(object sender, EventArgs e) {
            checkBoxSpeciesExpandVisualControl.Enabled = checkBoxSpeciesExpand.Checked;

            if (!checkBoxSpeciesExpand.Checked) {
                checkBoxSpeciesExpandVisualControl.Checked = false;
            }
        }

        private void checkBoxAutoIncreaseBio_CheckedChanged(object sender, EventArgs e) {
            checkBoxAutoDecreaseBio.Enabled = !checkBoxFixTotals.Checked && checkBoxAutoIncreaseBio.Checked;

            if (!checkBoxAutoIncreaseBio.Checked) {
                checkBoxAutoDecreaseBio.Checked = false;
            }
        }

        private void checkBoxFixTotals_CheckedChanged(object sender, EventArgs e) {
            checkBoxAutoLog.Enabled = !checkBoxFixTotals.Checked;
            checkBoxAutoIncreaseBio.Enabled = !checkBoxFixTotals.Checked;
            checkBoxAutoDecreaseBio.Enabled = !checkBoxFixTotals.Checked;

            if (checkBoxFixTotals.Checked) {
                checkBoxAutoLog.Checked = true;
                checkBoxAutoIncreaseBio.Checked = true;
                checkBoxAutoDecreaseBio.Checked = true;
            }
        }

        private void buttonClearRecent_Click(object sender, EventArgs e) {

            string[] species = GetKeys(Species.UserSettings.FeatureKey, Path.GetFileNameWithoutExtension(TaxonomicIndexPath));

            tdClearRecent.Content = string.Format(Resources.Interface.Messages.ClearRecent, species.Length);

            if (tdClearRecent.ShowDialog() == tdbRecentClear) {
                ClearFolder(Species.UserSettings.FeatureKey, Path.GetFileNameWithoutExtension(TaxonomicIndexPath));
            }
        }
    }
}
