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
    public partial class SettingsPageTests : SettingsPage, ISettingsPage
    {
        public SettingsPageTests() {
            InitializeComponent();
        }

        public void LoadSettings() {

            comboBoxNormality.SelectedIndex = UserSettings.NormalityTest;
            comboBoxHomogeneity.SelectedIndex = UserSettings.HomogeneityTest;
            comboBoxLSD.SelectedIndex = UserSettings.LsdIndex;
        }

        public void SaveSettings() {

            UserSettings.NormalityTest = comboBoxNormality.SelectedIndex;
            UserSettings.HomogeneityTest = comboBoxHomogeneity.SelectedIndex;
            UserSettings.LsdIndex = comboBoxLSD.SelectedIndex;
        }
    }
}
