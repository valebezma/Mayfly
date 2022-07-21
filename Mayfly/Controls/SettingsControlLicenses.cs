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

namespace Mayfly.Controls
{
    public partial class SettingsControlLicenses : SettingsControl, ISettingsControl
    {
        public SettingsControlLicenses() {

            InitializeComponent();
            listViewLicenses.Shine();
        }



        public void LoadSettings() {

            listViewLicenses.Items.Clear();

            foreach (License lic in License.InstalledLicenses) {
                ListViewItem li = new ListViewItem(lic.Feature);
                li.Name = lic.Feature.ToString();
                li.SubItems.Add(((int)lic.ExpiresIn.TotalDays).ToCorrectString(Resources.Interface.ExpirationMask));
                listViewLicenses.Items.Add(li);
            }
        }

        public void SaveSettings() {
        }
    }
}
