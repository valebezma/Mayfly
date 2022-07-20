using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Mayfly.Wild;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsControlExplorer : UserControl, ISettingControl
    {
        public SettingsControlExplorer() {

            InitializeComponent();

            comboBoxDiversity.DataSource = Wild.Service.GetDiversityIndices();
            comboBoxGear.DataSource = SettingsReader.SamplersIndex.Sampler.Select();
        }

        public void LoadSettings() {

            checkBoxBioAutoLoad.Checked = SettingsReader.AutoLoadBio;

            foreach (string bio in SettingsReader.Bios) {
                ListViewItem li = listViewBio.CreateItem(bio,
                    Path.GetFileNameWithoutExtension(bio));
                listViewBio.Items.Add(li);
            }

            comboBoxDominance.SelectedIndex = Wild.UserSettings.Dominance;
            comboBoxDiversity.SelectedIndex = (int)Wild.UserSettings.Diversity;
            comboBoxDominance.SelectedIndex = Wild.UserSettings.Dominance;
            comboBoxDiversity.SelectedIndex = (int)Wild.UserSettings.Diversity;

            checkBoxKeepWizards.Checked = UserSettings.KeepWizard;
            comboBoxReportCriticality.SelectedIndex = (int)UserSettings.ReportCriticality;
            checkBoxConsistency.Checked = UserSettings.CheckConsistency;
        }

        public void SaveSettings() {

            UserSettings.KeepWizard = checkBoxKeepWizards.Checked;
            UserSettings.CheckConsistency = checkBoxConsistency.Checked;
            UserSettings.ReportCriticality = (ArtifactCriticality)comboBoxReportCriticality.SelectedIndex;

            Wild.UserSettings.Diversity = (DiversityIndex)comboBoxDiversity.SelectedIndex;
            Wild.UserSettings.Dominance = comboBoxDominance.SelectedIndex;
            Wild.UserSettings.Diversity = (DiversityIndex)comboBoxDiversity.SelectedIndex;
            Wild.UserSettings.Dominance = comboBoxDominance.SelectedIndex;

            if (SettingsSaved != null) {
                SettingsSaved.Invoke(this, new EventArgs());
            }
        }

        private void checkBoxBioAutoLoad_CheckedChanged(object sender, EventArgs e) {
            listViewBio.Enabled = buttonBioBrowse.Enabled =
                checkBoxBioAutoLoad.Checked;
        }

        private void buttonBioBrowse_Click(object sender, EventArgs e) {
            if (Wild.UserSettings.InterfaceBio.OpenDialog.ShowDialog() == DialogResult.OK) {
                ListViewItem li = listViewBio.CreateItem(Wild.UserSettings.InterfaceBio.OpenDialog.FileName,
                    Path.GetFileNameWithoutExtension(Wild.UserSettings.InterfaceBio.OpenDialog.FileName));
                listViewBio.Items.Add(li);
            }
        }

        private void listViewBio_SelectedIndexChanged(object sender, EventArgs e) {
            buttonBioRemove.Enabled = listViewBio.SelectedItems.Count > 0;
        }

        private void buttonBioRemove_Click(object sender, EventArgs e) {
            while (listViewBio.SelectedItems.Count > 0) {
                listViewBio.Items.Remove(listViewBio.SelectedItems[0]);
            }
        }
    }
}
