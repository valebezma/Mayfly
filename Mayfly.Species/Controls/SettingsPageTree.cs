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
    public partial class SettingsPageTree : SettingsPage, ISettingsPage
    {
        public SettingsPageTree() {
            InitializeComponent();
        }



        public void LoadSettings() {

            textBoxHigherFormat.Text = UserSettings.HigherTaxonNameFormat;
            textBoxLowerFormat.Text = UserSettings.LowerTaxonNameFormat;
            colorBoxLower.Color = UserSettings.LowerTaxonColor;

            checkBoxFillLower.Checked = UserSettings.FillTreeWithLowerTaxon;
        }

        public void SaveSettings() {

            UserSettings.HigherTaxonNameFormat = textBoxHigherFormat.Text;
            UserSettings.LowerTaxonNameFormat = textBoxLowerFormat.Text;
            UserSettings.LowerTaxonColor = colorBoxLower.Color;
            UserSettings.FillTreeWithLowerTaxon = checkBoxFillLower.Checked;
        }
    }
}
