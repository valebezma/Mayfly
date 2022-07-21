using System;
using System.Windows.Forms;
using static Mayfly.Wild.SettingsReader;
using static Mayfly.Wild.UserSettings;
using static Mayfly.UserSettings;
using System.IO;
using Mayfly.Controls;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsControlLog : SettingsControl, ISettingsControl
    {
        public SettingsControlLog() {

            InitializeComponent();
        }



        public void LoadSettings() {

            checkBoxAutoLog.Checked = AutoLogOpen;
            checkBoxFixTotals.Checked = FixTotals;
            checkBoxAutoIncreaseBio.Checked = AutoIncreaseBio;
            checkBoxAutoDecreaseBio.Checked = AutoDecreaseBio;
        }

        public void SaveSettings() {

            AutoLogOpen = checkBoxAutoLog.Checked;
            FixTotals = checkBoxFixTotals.Checked;
            AutoIncreaseBio = checkBoxAutoIncreaseBio.Checked;
            AutoDecreaseBio = checkBoxAutoDecreaseBio.Checked;
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
    }
}
