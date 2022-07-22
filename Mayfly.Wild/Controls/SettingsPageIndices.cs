using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Windows.Forms;
using static Mayfly.Wild.ReaderSettings;
using static Mayfly.Wild.UserSettings;
using static Mayfly.UserSettings;
using System.IO;
using Mayfly.Controls;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsPageIndices : SettingsPage, ISettingsPage
    {
        public SettingsPageIndices() {

            InitializeComponent();
        }



        public void LoadSettings() {

            textBoxWaters.Text = WatersIndexPath;
            textBoxSpecies.Text = TaxonomicIndexPath;

            numericUpDownRecentCount.Value = RecentSpeciesCount;

            checkBoxSpeciesExpand.Checked = SpeciesAutoExpand;
            checkBoxSpeciesExpandVisualControl.Checked = SpeciesAutoExpandVisual;
        }

        public void SaveSettings() {

            WatersIndexPath = textBoxWaters.Text;
            TaxonomicIndexPath = textBoxSpecies.Text;

            SpeciesAutoExpand = checkBoxSpeciesExpand.Checked;
            SpeciesAutoExpandVisual = checkBoxSpeciesExpandVisualControl.Checked;
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

        private void buttonClearRecent_Click(object sender, EventArgs e) {

            string[] species = GetKeys(Species.UserSettings.FeatureKey, Path.GetFileNameWithoutExtension(TaxonomicIndexPath));

            tdClearRecent.Content = string.Format(Resources.Interface.Messages.ClearRecent, species.Length);

            if (tdClearRecent.ShowDialog() == tdbRecentClear) {
                ClearFolder(Species.UserSettings.FeatureKey, Path.GetFileNameWithoutExtension(TaxonomicIndexPath));
            }
        }
    }
}
