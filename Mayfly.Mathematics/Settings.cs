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

            comboBoxRegressionType.Items.AddRange(Service.GetRegressionTypes());

            LoadSettings();
        }

        private void LoadSettings()
        {
            numericUpDownSL.Value = (decimal)UserSettings.DefaultAlpha;

            numericUpDownStrongSize.Value = UserSettings.StrongSampleSize;
            numericUpDownSoftSize.Value = UserSettings.SoftSampleSize;

            comboBoxNormality.SelectedIndex = UserSettings.NormalityTest;
            comboBoxHomogeneity.SelectedIndex = UserSettings.HomogeneityTest;

            comboBoxLSD.SelectedIndex = UserSettings.LsdIndex;
        }

        private void SaveSettings()
        {
            UserSettings.DefaultAlpha = (double)numericUpDownSL.Value;

            UserSettings.StrongSampleSize = (int)numericUpDownStrongSize.Value;
            UserSettings.SoftSampleSize = (int)numericUpDownSoftSize.Value;

            UserSettings.NormalityTest = comboBoxNormality.SelectedIndex;
            UserSettings.HomogeneityTest = comboBoxHomogeneity.SelectedIndex;

            UserSettings.LsdIndex = comboBoxLSD.SelectedIndex;
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
    }
}
