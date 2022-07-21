using Mayfly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly.Mathematics
{
    public partial class Settings : Form
    {


        public Settings()
        {
            InitializeComponent();

            loadSettings();
        }



        private void loadSettings()
        {
            numericUpDownSL.Value = (decimal)UserSettings.DefaultAlpha;
            numericUpDownStrongSize.Value = UserSettings.RequiredSampleSize;

            comboBoxNormality.SelectedIndex = UserSettings.NormalityTest;
            comboBoxHomogeneity.SelectedIndex = UserSettings.HomogeneityTest;
            comboBoxLSD.SelectedIndex = UserSettings.LsdIndex;

            dialogSelected.Color = panelSelected.BackColor = UserSettings.ColorSelected;
            dialogTrend.Color = panelTrend.BackColor = UserSettings.ColorAccent;
            dialogSelected.Color =
                panelSelected.BackColor =
                UserSettings.ColorSelected;
        }

        private void saveSettings()
        {
            UserSettings.DefaultAlpha = (double)numericUpDownSL.Value;
            UserSettings.RequiredSampleSize = (int)numericUpDownStrongSize.Value;

            UserSettings.NormalityTest = comboBoxNormality.SelectedIndex;
            UserSettings.HomogeneityTest = comboBoxHomogeneity.SelectedIndex;
            UserSettings.LsdIndex = comboBoxLSD.SelectedIndex;

            UserSettings.ColorSelected = dialogSelected.Color;
            UserSettings.ColorAccent = dialogTrend.Color;
        }



        private void panelSelected_Click(object sender, EventArgs e)
        {
            if (dialogSelected.ShowDialog(this) == DialogResult.OK)
            {
                panelSelected.BackColor = dialogSelected.Color;
            }
        }

        private void panelTrend_Click(object sender, EventArgs e)
        {
            if (dialogTrend.ShowDialog(this) == DialogResult.OK)
            {
                panelTrend.BackColor = dialogTrend.Color;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            saveSettings();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            saveSettings();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; 
            Close();
        }
    }
}
