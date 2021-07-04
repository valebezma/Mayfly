﻿using Mayfly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;
using Mayfly.Wild;

namespace Mayfly.Benthos.Explorer
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            comboBoxDiversity.DataSource = Wild.Service.GetDiversityIndices();

            LoadSettings();
        }

        private void LoadSettings()
        {
            comboBoxDominance.SelectedIndex = Wild.UserSettings.Dominance;
            comboBoxDiversity.SelectedIndex = (int)Wild.UserSettings.Diversity;
        }

        private void SaveSettings()
        {
            Wild.UserSettings.Diversity = (DiversityIndex)comboBoxDiversity.SelectedIndex;
            Wild.UserSettings.Dominance = comboBoxDominance.SelectedIndex;
        }

        private void buttonBenthos_Click(object sender, EventArgs e)
        {
            Benthos.Settings settings = new Benthos.Settings();
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

        private void buttonBasicSettings_Click(object sender, EventArgs e)
        {
            Software.Settings settings = new Software.Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            settings.ShowDialog();
        }
    }
}