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

namespace Mayfly.Fish.Explorer
{
    public partial class SettingsControlOther : SettingsControl, ISettingsControl
    {
        public string Group => "Explorer";

        public string Section => "Other";

        public SettingsControlOther() {

            InitializeComponent();

            numericUpDownInterval.Minimum = numericUpDownInterval.Increment =
                (decimal)Fish.UserSettings.DefaultStratifiedInterval;
        }



        public void LoadSettings() {

            numericUpDownInterval.Value = (decimal)UserSettings.SizeInterval;
            comboBoxAlk.SelectedIndex = (int)UserSettings.SelectedAgeLengthKeyType;
        }

        public void SaveSettings() {

            UserSettings.SizeInterval = (double)numericUpDownInterval.Value;
            UserSettings.SelectedAgeLengthKeyType = (AgeLengthKeyType)comboBoxAlk.SelectedIndex;
        }
    }
}
