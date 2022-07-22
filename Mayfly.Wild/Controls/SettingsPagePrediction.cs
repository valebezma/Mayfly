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
using Mayfly.Controls;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsPagePrediction : SettingsPage, ISettingsPage
    {
        public SettingsPagePrediction() {
            InitializeComponent();
        }



        public void LoadSettings() {

            checkBoxBioAutoLoad.Checked = ExplorerSettings.AutoLoadBio;

            foreach (string bio in ExplorerSettings.Bios) {
                ListViewItem li = listViewBio.CreateItem(bio,
                    System.IO.Path.GetFileNameWithoutExtension(bio));
                listViewBio.Items.Add(li);
            }

            checkBoxBioAutoLoad.Checked = ExplorerSettings.AutoLoadBio;

            foreach (string bio in ExplorerSettings.Bios) {
                ListViewItem li = listViewBio.CreateItem(bio,
                    System.IO.Path.GetFileNameWithoutExtension(bio));
                listViewBio.Items.Add(li);
            }
        }

        public void SaveSettings() {

            ExplorerSettings.AutoLoadBio = checkBoxBioAutoLoad.Checked;

            List<string> bios = new List<string>();
            foreach (ListViewItem li in listViewBio.Items) {
                bios.Add(li.Name);
            }
            ExplorerSettings.Bios = bios.ToArray();
        }



        private void checkBoxBioAutoLoad_CheckedChanged(object sender, EventArgs e) {
            listViewBio.Enabled = buttonBioBrowse.Enabled = buttonBioRemove.Enabled =
                checkBoxBioAutoLoad.Checked;
        }

        private void buttonBioBrowse_Click(object sender, EventArgs e) {
            if (UserSettings.InterfaceBio.OpenDialog.ShowDialog() == DialogResult.OK) {
                ListViewItem li = listViewBio.CreateItem(UserSettings.InterfaceBio.OpenDialog.FileName,
                    System.IO.Path.GetFileNameWithoutExtension(UserSettings.InterfaceBio.OpenDialog.FileName));
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
