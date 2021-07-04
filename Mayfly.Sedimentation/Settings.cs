using Mayfly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Sedimentation
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
            comboBoxGrainSize.SelectedIndex = (int)UserSettings.SizeSource;
            textBoxWaterDensity.Text = UserSettings.WaterDensity.ToString();
            textBoxSolidDensity.Text = UserSettings.SolidDensity.ToString();
            textBoxGravity.Text = UserSettings.Gravity.ToString();
            textBoxSolidShape.Text = UserSettings.SolidShape.ToString();
            textBoxD.Text = UserSettings.ControlSize.ToString();
            textBoxPart.Text = (UserSettings.ControlPart * 100).ToString();
            textBoxCriticalLoad.Text = UserSettings.CriticalLoad.ToString();
            textBoxCriticalSediment.Text = UserSettings.CriticalSediment.ToString();
        }

        private void SaveSettings()
        {
            UserSettings.SizeSource = (GrainSizeType)comboBoxGrainSize.SelectedIndex;
            UserSettings.WaterDensity = Convert.ToDouble(textBoxWaterDensity.Text);
            UserSettings.SolidDensity = Convert.ToDouble(textBoxSolidDensity.Text);
            UserSettings.Gravity = Convert.ToDouble(textBoxGravity.Text);
            UserSettings.SolidShape = Convert.ToDouble(textBoxSolidShape.Text);
            UserSettings.ControlSize = Convert.ToDouble(textBoxD.Text);
            UserSettings.ControlPart = Convert.ToDouble(textBoxPart.Text) / 100;
            UserSettings.CriticalSediment = Convert.ToDouble(textBoxCriticalSediment.Text);
            UserSettings.CriticalLoad = Convert.ToDouble(textBoxCriticalLoad.Text);
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

        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        private void textBoxPart_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
