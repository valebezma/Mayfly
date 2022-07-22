using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Controls;

namespace Mayfly.Species.Controls
{
    public partial class SettingsPagePrint : SettingsPage, ISettingsPage
    {
        public SettingsPagePrint() {
            InitializeComponent();
        }

        public string Section => "Printing";

        public string Group => "Taxonomics";

        public void LoadSettings() {

            textBoxCoupletChar.Text = UserSettings.CoupletChar;

            if (UserSettings.UseClassicKeyReport) {
                radioButtonClassic.Checked = true;
            } else {
                radioButtonModern.Checked = true;
            }
        }

        public void SaveSettings() {

            UserSettings.CoupletChar = textBoxCoupletChar.Text;
            UserSettings.UseClassicKeyReport = radioButtonClassic.Checked;
        }

        private void radioButtonModern_CheckedChanged(object sender, EventArgs e) {
            labelCoupletChar.Enabled = textBoxCoupletChar.Enabled =
                radioButtonModern.Checked;
        }
    }
}
