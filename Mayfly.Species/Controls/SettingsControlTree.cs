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
    public partial class SettingsControlTree : SettingsControl, ISettingsControl
    {
        public SettingsControlTree() {
            InitializeComponent();
        }

        public string Section => "Tree appearance";

        public string Group => "Taxonomics";

        public void LoadSettings() {

            textBoxHigherFormat.Text = UserSettings.HigherTaxonNameFormat;
            textBoxLowerFormat.Text = UserSettings.LowerTaxonNameFormat;
            dialogLowerTaxon.Color =
                panelLowerTaxon.BackColor =
                UserSettings.LowerTaxonColor;

            checkBoxFillLower.Checked = UserSettings.FillTreeWithLowerTaxon;
        }

        public void SaveSettings() {

            UserSettings.HigherTaxonNameFormat = textBoxHigherFormat.Text;
            UserSettings.LowerTaxonNameFormat = textBoxLowerFormat.Text;
            UserSettings.LowerTaxonColor = panelLowerTaxon.BackColor;
            UserSettings.FillTreeWithLowerTaxon = checkBoxFillLower.Checked;
        }

        private void panelLowerTaxon_Click(object sender, EventArgs e) {
            if (dialogLowerTaxon.ShowDialog(this) == DialogResult.OK) {
                panelLowerTaxon.BackColor = dialogLowerTaxon.Color;
            }
        }
    }
}
