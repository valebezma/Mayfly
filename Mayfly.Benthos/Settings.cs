using Mayfly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Benthos
{
    public class Settings : Mayfly.Wild.Settings
    {
        public Settings()
            : base()
        {
            SaveSettings = saveSettings;

            #region references

            textBoxWaters.Text = Wild.UserSettings.WatersIndexPath;
            textBoxSpecies.Text = UserSettings.SpeciesIndexPath;

            checkBoxSpeciesExpand.Checked = UserSettings.SpeciesAutoExpand;
            checkBoxSpeciesExpandVisualControl.Checked = UserSettings.SpeciesAutoExpandVisual;

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

            UserSettings.SpeciesAutoExpand = checkBoxSpeciesExpand.Checked;
            UserSettings.SpeciesAutoExpandVisual = checkBoxSpeciesExpandVisualControl.Checked;

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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
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
            resources.ApplyResources(this.textBoxSpecies, "textBoxSpecies");
            // 
            // textBoxWaters
            // 
            resources.ApplyResources(this.textBoxWaters, "textBoxWaters");
            // 
            // checkBoxBreakBetweenSpecies
            // 
            resources.ApplyResources(this.checkBoxBreakBetweenSpecies, "checkBoxBreakBetweenSpecies");
            // 
            // checkBoxBreakBeforeIndividuals
            // 
            resources.ApplyResources(this.checkBoxBreakBeforeIndividuals, "checkBoxBreakBeforeIndividuals");
            // 
            // checkBoxCardOdd
            // 
            resources.ApplyResources(this.checkBoxCardOdd, "checkBoxCardOdd");
            // 
            // numericUpDownRecentCount
            // 
            resources.ApplyResources(this.numericUpDownRecentCount, "numericUpDownRecentCount");
            // 
            // checkBoxAutoLog
            // 
            resources.ApplyResources(this.checkBoxAutoLog, "checkBoxAutoLog");
            // 
            // listViewAddtVars
            // 
            resources.ApplyResources(this.listViewAddtVars, "listViewAddtVars");
            // 
            // checkBoxAutoDecreaseBio
            // 
            resources.ApplyResources(this.checkBoxAutoDecreaseBio, "checkBoxAutoDecreaseBio");
            // 
            // checkBoxFixTotals
            // 
            resources.ApplyResources(this.checkBoxFixTotals, "checkBoxFixTotals");
            // 
            // checkBoxAutoIncreaseBio
            // 
            resources.ApplyResources(this.checkBoxAutoIncreaseBio, "checkBoxAutoIncreaseBio");
            // 
            // tdClearRecent
            // 
            resources.ApplyResources(this.tdClearRecent, "tdClearRecent");
            // 
            // tdbRecentClear
            // 
            resources.ApplyResources(this.tdbRecentClear, "tdbRecentClear");
            // 
            // listViewAddtFctr
            // 
            resources.ApplyResources(this.listViewAddtFctr, "listViewAddtFctr");
            // 
            // tabPageReferences
            // 
            resources.ApplyResources(this.tabPageReferences, "tabPageReferences");
            // 
            // tabPageIndividuals
            // 
            resources.ApplyResources(this.tabPageIndividuals, "tabPageIndividuals");
            // 
            // tabPageFactors
            // 
            resources.ApplyResources(this.tabPageFactors, "tabPageFactors");
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            // 
            // labelRef
            // 
            resources.ApplyResources(this.labelRef, "labelRef");
            // 
            // labelSpecies
            // 
            resources.ApplyResources(this.labelSpecies, "labelSpecies");
            // 
            // buttonOpenSpecies
            // 
            resources.ApplyResources(this.buttonOpenSpecies, "buttonOpenSpecies");
            // 
            // labelWaters
            // 
            resources.ApplyResources(this.labelWaters, "labelWaters");
            // 
            // buttonOpenWaters
            // 
            resources.ApplyResources(this.buttonOpenWaters, "buttonOpenWaters");
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            // 
            // buttonBrowseSpecies
            // 
            resources.ApplyResources(this.buttonBrowseSpecies, "buttonBrowseSpecies");
            // 
            // buttonBrowseWaters
            // 
            resources.ApplyResources(this.buttonBrowseWaters, "buttonBrowseWaters");
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            // 
            // tabPagePrint
            // 
            resources.ApplyResources(this.tabPagePrint, "tabPagePrint");
            // 
            // labelPrintCaption
            // 
            resources.ApplyResources(this.labelPrintCaption, "labelPrintCaption");
            // 
            // tabPageInput
            // 
            resources.ApplyResources(this.tabPageInput, "tabPageInput");
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            // 
            // tabPageSpecies
            // 
            resources.ApplyResources(this.tabPageSpecies, "tabPageSpecies");
            // 
            // labelRecent
            // 
            resources.ApplyResources(this.labelRecent, "labelRecent");
            // 
            // labelInputFish
            // 
            resources.ApplyResources(this.labelInputFish, "labelInputFish");
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // buttonRemoveVar
            // 
            resources.ApplyResources(this.buttonRemoveVar, "buttonRemoveVar");
            // 
            // buttonNewVar
            // 
            resources.ApplyResources(this.buttonNewVar, "buttonNewVar");
            // 
            // labelCommonVars
            // 
            resources.ApplyResources(this.labelCommonVars, "labelCommonVars");
            // 
            // buttonClearRecent
            // 
            resources.ApplyResources(this.buttonClearRecent, "buttonClearRecent");
            // 
            // labelAddtsVars
            // 
            resources.ApplyResources(this.labelAddtsVars, "labelAddtsVars");
            // 
            // tdbRecentCancel
            // 
            resources.ApplyResources(this.tdbRecentCancel, "tdbRecentCancel");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // buttonAddtFctrsDelete
            // 
            resources.ApplyResources(this.buttonAddtFctrsDelete, "buttonAddtFctrsDelete");
            // 
            // buttonAddtFctrsAdd
            // 
            resources.ApplyResources(this.buttonAddtFctrsAdd, "buttonAddtFctrsAdd");
            // 
            // labelAddtFctrs
            // 
            resources.ApplyResources(this.labelAddtFctrs, "labelAddtFctrs");
            // 
            // checkBoxSpeciesExpand
            // 
            resources.ApplyResources(this.checkBoxSpeciesExpand, "checkBoxSpeciesExpand");
            // 
            // checkBoxSpeciesExpandVisualControl
            // 
            resources.ApplyResources(this.checkBoxSpeciesExpandVisualControl, "checkBoxSpeciesExpandVisualControl");
            // 
            // checkBoxOrderLog
            // 
            resources.ApplyResources(this.checkBoxOrderLog, "checkBoxOrderLog");
            // 
            // comboBoxLogOrder
            // 
            resources.ApplyResources(this.comboBoxLogOrder, "comboBoxLogOrder");
            // 
            // buttonBasicSettings
            // 
            resources.ApplyResources(this.buttonBasicSettings, "buttonBasicSettings");
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
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
