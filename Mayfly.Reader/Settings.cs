using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Mayfly.Reader
{
    class Settings : Mayfly.Wild.Settings
    {
        public Settings() : base()
        {
            SaveSettings = saveSettings;
            RecentClear = clearRecent;

            #region references

            textBoxWaters.Text = Wild.UserSettings.WatersIndexPath;
            textBoxSpecies.Text = UserSettings.SpeciesIndexPath;

            #endregion

            #region input

            checkBoxAutoLog.Checked = UserSettings.AutoLogOpen;
            checkBoxFixTotals.Checked = UserSettings.FixTotals;
            checkBoxAutoIncreaseBio.Checked = UserSettings.AutoIncreaseBio;
            checkBoxAutoDecreaseBio.Checked = UserSettings.AutoDecreaseBio;

            foreach (string item in UserSettings.AddtVariables)
            {
                ListViewItem li = new ListViewItem(item);
                listViewAddtVars.Items.Add(li);
            }

            foreach (ListViewItem item in listViewAddtVars.Items)
            {
                if (UserSettings.CurrentVariables.Contains(item.Text))
                    item.Checked = true;
            }

            numericUpDownRecentCount.Value = UserSettings.RecentSpeciesCount;

            #endregion

            #region print

            checkBoxCardOdd.Checked = UserSettings.OddCardStart;
            checkBoxBreakBeforeIndividuals.Checked = UserSettings.BreakBeforeIndividuals;
            checkBoxBreakBetweenSpecies.Checked = UserSettings.BreakBetweenSpecies;

            #endregion
        }

        private void saveSettings()
        {
            #region References

            Wild.UserSettings.WatersIndexPath = textBoxWaters.Text;
            UserSettings.SpeciesIndexPath = textBoxSpecies.Text;

            #endregion

            #region Input

            UserSettings.FixTotals = checkBoxFixTotals.Checked;
            UserSettings.AutoLogOpen = checkBoxAutoLog.Checked;
            UserSettings.AutoIncreaseBio = checkBoxAutoIncreaseBio.Checked;
            UserSettings.AutoDecreaseBio = checkBoxAutoDecreaseBio.Checked;

            UserSettings.RecentSpeciesCount = (int)numericUpDownRecentCount.Value;

            List<string> addvars = new List<string>();
            foreach (ListViewItem item in listViewAddtVars.Items)
                addvars.Add(item.Text);
            UserSettings.AddtVariables = addvars.ToArray();

            List<string> currvars = new List<string>();
            foreach (ListViewItem item in listViewAddtVars.CheckedItems)
                currvars.Add(item.Text);
            UserSettings.CurrentVariables = currvars.ToArray();

            #endregion

            #region Print

            UserSettings.OddCardStart = checkBoxCardOdd.Checked;
            UserSettings.BreakBeforeIndividuals = checkBoxBreakBeforeIndividuals.Checked;
            UserSettings.BreakBetweenSpecies = checkBoxBreakBetweenSpecies.Checked;

            #endregion
        }

        private void clearRecent()
        {
            string[] species = UserSettingInterface.GetKeys(Species.UserSettingPaths.Path,
                Path.GetFileNameWithoutExtension(UserSettings.SpeciesIndexPath));
            tdClearRecent.Content = string.Format(Wild.Resources.Messages.ClearRecent, species.Length);

            if (tdClearRecent.ShowDialog() == tdbRecentClear)
            {
                UserSettingInterface.ClearFolder(UserSettingPaths.Path,
                    Path.GetFileNameWithoutExtension(UserSettings.SpeciesIndexPath));
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).BeginInit();
            this.tabPageReferences.SuspendLayout();
            this.tabPageIndividuals.SuspendLayout();
            this.tabPageFactors.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPagePrint.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSpecies.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxSpecies
            // 
            this.textBoxSpecies.Location = new System.Drawing.Point(48, 81);
            // 
            // textBoxWaters
            // 
            this.textBoxWaters.Location = new System.Drawing.Point(48, 131);
            this.textBoxWaters.Visible = false;
            // 
            // labelSpecies
            // 
            this.labelSpecies.Location = new System.Drawing.Point(45, 65);
            // 
            // buttonOpenSpecies
            // 
            this.buttonOpenSpecies.Location = new System.Drawing.Point(332, 79);
            // 
            // labelWaters
            // 
            this.labelWaters.Location = new System.Drawing.Point(45, 115);
            this.labelWaters.Visible = false;
            // 
            // buttonOpenWaters
            // 
            this.buttonOpenWaters.Location = new System.Drawing.Point(332, 129);
            this.buttonOpenWaters.Visible = false;
            // 
            // buttonBrowseSpecies
            // 
            this.buttonBrowseSpecies.Location = new System.Drawing.Point(251, 79);
            // 
            // buttonBrowseWaters
            // 
            this.buttonBrowseWaters.Location = new System.Drawing.Point(251, 129);
            this.buttonBrowseWaters.Visible = false;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Name = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).EndInit();
            this.tabPageReferences.ResumeLayout(false);
            this.tabPageReferences.PerformLayout();
            this.tabPageIndividuals.ResumeLayout(false);
            this.tabPageIndividuals.PerformLayout();
            this.tabPageFactors.ResumeLayout(false);
            this.tabPageFactors.PerformLayout();
            this.tabControlSettings.ResumeLayout(false);
            this.tabPagePrint.ResumeLayout(false);
            this.tabPagePrint.PerformLayout();
            this.tabPageInput.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSpecies.ResumeLayout(false);
            this.tabPageSpecies.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
