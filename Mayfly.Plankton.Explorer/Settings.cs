using Mayfly.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Mayfly.Wild;

namespace Mayfly.Plankton.Explorer
{
    public partial class Settings : Form
    {
        public event EventHandler SettingsSaved;



        public Settings()
        {
            InitializeComponent();
            comboBoxDiversity.DataSource = Wild.Service.GetDiversityIndices();

            LoadSettings();
        }



        private void LoadSettings()
        {
            if (Licensing.Verify("Bios"))
            {
                checkBoxBioAutoLoad.Checked = UserSettings.AutoLoadBio;

                foreach (string bio in UserSettings.Bios)
                {
                    ListViewItem li = listViewBio.CreateItem(bio,
                        Path.GetFileNameWithoutExtension(bio));
                    listViewBio.Items.Add(li);
                }
            }

            comboBoxDominance.SelectedIndex = Wild.UserSettings.Dominance;
            comboBoxDiversity.SelectedIndex = (int)Wild.UserSettings.Diversity;
        }

        private void SaveSettings()
        {
            UserSettings.AutoLoadBio = checkBoxBioAutoLoad.Checked;

            List<string> bios = new List<string>();
            foreach (ListViewItem li in listViewBio.Items)
            {
                bios.Add(li.Name);
            }
            UserSettings.Bios = bios.ToArray();

            Wild.UserSettings.Diversity = (DiversityIndex)comboBoxDiversity.SelectedIndex;
            Wild.UserSettings.Dominance = comboBoxDominance.SelectedIndex;

            if (SettingsSaved != null)
            {
                SettingsSaved.Invoke(this, new EventArgs());
            }
        }
              

        
        private void checkBoxBioAutoLoad_CheckedChanged(object sender, EventArgs e)
        {
            listViewBio.Enabled = buttonBioBrowse.Enabled =
                checkBoxBioAutoLoad.Checked;
        }

        private void buttonBioBrowse_Click(object sender, EventArgs e)
        {
            if (Wild.UserSettings.InterfaceBio.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                ListViewItem li = listViewBio.CreateItem(Wild.UserSettings.InterfaceBio.OpenDialog.FileName,
                    Path.GetFileNameWithoutExtension(Wild.UserSettings.InterfaceBio.OpenDialog.FileName));
                listViewBio.Items.Add(li);
            }
        }

        private void listViewBio_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonBioRemove.Enabled = listViewBio.SelectedItems.Count > 0;
        }

        private void buttonBioRemove_Click(object sender, EventArgs e)
        {
            while (listViewBio.SelectedItems.Count > 0)
            {
                listViewBio.Items.Remove(listViewBio.SelectedItems[0]);
            }
        }
        


        private void buttonPlankton_Click(object sender, EventArgs e)
        {
            Plankton.Settings settings = new Plankton.Settings();
            settings.Show();
        }

        private void buttonMath_Click(object sender, EventArgs e)
        {
            Mathematics.Settings settings = new Mathematics.Settings();
            settings.Show();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveSettings();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonBasicSettings_Click(object sender, EventArgs e)
        {
            Software.Settings settings = new Software.Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            settings.ShowDialog();
        }
    }
}
