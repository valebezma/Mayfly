using Mayfly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Plankton
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
    }
}
