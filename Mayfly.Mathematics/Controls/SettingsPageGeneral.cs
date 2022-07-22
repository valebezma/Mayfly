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
    public partial class SettingsPageGeneral : SettingsPage, ISettingsPage
    {
        public SettingsPageGeneral() {

            InitializeComponent();
        }



        public void LoadSettings() {

            numericUpDownSL.Value = (decimal)UserSettings.DefaultAlpha;
            numericUpDownStrongSize.Value = UserSettings.RequiredSampleSize;
        }

        public void SaveSettings() {

            UserSettings.DefaultAlpha = (double)numericUpDownSL.Value;
            UserSettings.RequiredSampleSize = (int)numericUpDownStrongSize.Value;
        }
    }
}
