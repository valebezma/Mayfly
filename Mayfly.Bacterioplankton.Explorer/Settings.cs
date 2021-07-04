using Mayfly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Bacterioplankton.Explorer
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
        }

        private void SaveSettings()
        {
        }

        private void buttonPlankton_Click(object sender, EventArgs e)
        {
            Bacterioplankton.Settings settings = new Bacterioplankton.Settings();
            settings.Show();
        }

        private void buttonMath_Click(object sender, EventArgs e)
        {
            Mathematics.Settings settings = new Mathematics.Settings();
            settings.Show();
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
