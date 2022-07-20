using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Wild
{
    public partial class SettingsControlPrediction : UserControl, ISettingControl
    {
        public SettingsControlPrediction() {
            InitializeComponent();
        }

        public void LoadSettings() {

            checkBoxBioAutoLoad.Checked = UserSettings.AutoLoadBio;

            foreach (string bio in UserSettings.Bios) {
                ListViewItem li = listViewBio.CreateItem(bio,
                    Path.GetFileNameWithoutExtension(bio));
                listViewBio.Items.Add(li);
            }
        }

        public void SaveSettings() {

            UserSettings.AutoLoadBio = checkBoxBioAutoLoad.Checked;

            List<string> bios = new List<string>();
            foreach (ListViewItem li in listViewBio.Items) {
                bios.Add(li.Name);
            }
            UserSettings.Bios = bios.ToArray();
        }
    }
}
