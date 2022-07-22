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

            colorPickerSelected.Color = UserSettings.ColorSelected;
            colorPickerTrend.Color = UserSettings.ColorAccent;
        }

        public void SaveSettings() {

            UserSettings.ColorSelected = colorPickerSelected.Color;
            UserSettings.ColorAccent = colorPickerTrend.Color;
        }
    }
}
