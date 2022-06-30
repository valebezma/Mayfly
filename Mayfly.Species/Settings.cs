using Mayfly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly.Species
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            textBoxHigherFormat.Text = UserSettings.HigherTaxonNameFormat;
            textBoxLowerFormat.Text = UserSettings.LowerTaxonNameFormat;
            dialogLowerTaxon.Color =
                panelLowerTaxon.BackColor = 
                UserSettings.LowerTaxonColor;

            checkBoxFillLower.Checked = UserSettings.FillTreeWithLowerTaxon;

            textBoxCoupletChar.Text = UserSettings.CoupletChar;

            if (UserSettings.UseClassicKeyReport) {
                radioButtonClassic.Checked = true; 
            } else {
                radioButtonModern.Checked = true;
            }
        }

        private void SaveSettings()
        {
            UserSettings.HigherTaxonNameFormat = textBoxHigherFormat.Text;
            UserSettings.LowerTaxonNameFormat = textBoxLowerFormat.Text;
            UserSettings.LowerTaxonColor = panelLowerTaxon.BackColor;
            UserSettings.FillTreeWithLowerTaxon = checkBoxFillLower.Checked;

            UserSettings.CoupletChar = textBoxCoupletChar.Text;
            UserSettings.UseClassicKeyReport = radioButtonClassic.Checked;
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

        private void radioButtonModern_CheckedChanged(object sender, EventArgs e)
        {
            labelCoupletChar.Enabled = textBoxCoupletChar.Enabled =
                radioButtonModern.Checked;
        }

        private void panelLowerTaxon_Click(object sender, EventArgs e)
        {
            if (dialogLowerTaxon.ShowDialog(this) == DialogResult.OK)
            {
                panelLowerTaxon.BackColor = dialogLowerTaxon.Color;
            }
        }
    }
}
