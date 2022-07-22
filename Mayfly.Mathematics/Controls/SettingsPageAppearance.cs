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

namespace Mayfly.Mathematics.Controls
{
    public partial class SettingsPageAppearance : SettingsPage, ISettingsPage
    {
        public SettingsPageAppearance() {
            InitializeComponent();
        }

        public void LoadSettings() {

            dialogSelected.Color = panelSelected.BackColor = UserSettings.ColorSelected;
            dialogTrend.Color = panelTrend.BackColor = UserSettings.ColorAccent;
            dialogSelected.Color =
                panelSelected.BackColor =
                UserSettings.ColorSelected;
        }

        public void SaveSettings() {

            UserSettings.ColorSelected = dialogSelected.Color;
            UserSettings.ColorAccent = dialogTrend.Color;
        }



        private void panelSelected_Click(object sender, EventArgs e) {
            if (dialogSelected.ShowDialog(this) == DialogResult.OK) {
                panelSelected.BackColor = dialogSelected.Color;
            }
        }

        private void panelTrend_Click(object sender, EventArgs e) {
            if (dialogTrend.ShowDialog(this) == DialogResult.OK) {
                panelTrend.BackColor = dialogTrend.Color;
            }
        }
    }
}
