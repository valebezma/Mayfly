using Mayfly.Extensions;
using Mayfly.Geographics;
using System;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Globalization;
using Mayfly.Software;
using System.Collections;
using System.Collections.Generic;

namespace Mayfly.Wild
{
    public abstract partial class Settings : Form
    {
        protected abstract void SaveSettings();



        public Settings()
        {
            InitializeComponent();

            listViewAddtFctr.Shine();
            listViewAddtVars.Shine();

            #region references

            textBoxWaters.Text = UserSettings.WatersIndexPath;
            textBoxSpecies.Text = UserSettings.TaxonomicIndexPath;

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
                if (Array.IndexOf(UserSettings.CurrentVariables, item.Text) > -1)
                    item.Checked = true;
            }

            numericUpDownRecentCount.Value = UserSettings.RecentSpeciesCount;

            #endregion

            #region print

            checkBoxCardOdd.Checked = UserSettings.OddCardStart;
            checkBoxBreakBeforeIndividuals.Checked = UserSettings.BreakBeforeIndividuals;
            checkBoxBreakBetweenSpecies.Checked = UserSettings.BreakBetweenSpecies;
            checkBoxOrderLog.Checked = UserSettings.LogOrder != LogSortOrder.AsInput;
            if (UserSettings.LogOrder != LogSortOrder.AsInput)
                comboBoxLogOrder.SelectedIndex = (int)UserSettings.LogOrder;

            #endregion           
        }



        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (!tabPageReferences.IsDisposed)
            {
                UserSettings.WatersIndexPath = textBoxWaters.Text;
                UserSettings.TaxonomicIndexPath = textBoxSpecies.Text;
                UserSettings.SpeciesAutoExpand = checkBoxSpeciesExpand.Checked;
                UserSettings.SpeciesAutoExpandVisual = checkBoxSpeciesExpandVisualControl.Checked;
            }

            if (!tabPageInput.IsDisposed)
            {
                UserSettings.FixTotals = checkBoxFixTotals.Checked;
                UserSettings.AutoIncreaseBio = checkBoxAutoIncreaseBio.Checked;
                UserSettings.AutoDecreaseBio = checkBoxAutoDecreaseBio.Checked;
                UserSettings.AutoLogOpen = checkBoxAutoLog.Checked;
                UserSettings.RecentSpeciesCount = (int)numericUpDownRecentCount.Value;
            }

            List<string> addvars = new List<string>();
            foreach (ListViewItem item in listViewAddtVars.Items)
                addvars.Add(item.Text);
            UserSettings.AddtVariables = addvars.ToArray();

            List<string> currvars = new List<string>();
            foreach (ListViewItem item in listViewAddtVars.CheckedItems)
                currvars.Add(item.Text);
            UserSettings.CurrentVariables = currvars.ToArray();


            if (!tabPagePrint.IsDisposed)
            {
                UserSettings.OddCardStart = checkBoxCardOdd.Checked;
                UserSettings.BreakBeforeIndividuals = checkBoxBreakBeforeIndividuals.Checked;
                UserSettings.BreakBetweenSpecies = checkBoxBreakBetweenSpecies.Checked;
                UserSettings.LogOrder = checkBoxOrderLog.Checked ? (LogSortOrder)comboBoxLogOrder.SelectedIndex : LogSortOrder.AsInput;
            }

            SaveSettings();

            Log.Write(EventType.Maintenance, UserSettings.ObjectType + " settings changed");
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonApply_Click(sender, e);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; 
            Close();
        }

        public void buttonBrowseWaters_Click(object sender, EventArgs e)
        {
            if (Waters.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            { 
                textBoxWaters.Text = Waters.UserSettings.Interface.OpenDialog.FileName;
            }
        }

        private void buttonOpenWaters_Click(object sender, EventArgs e)
        {
            IO.RunFile(textBoxWaters.Text); 
        }

        private void buttonBrowseSpecies_Click(object sender, EventArgs e)
        {
            OpenFileDialog SetSpecies = Mayfly.Species.UserSettings.Interface.OpenDialog;

            if (SetSpecies.ShowDialog() == DialogResult.OK)
            {
                textBoxSpecies.Text = SetSpecies.FileName;
            }
        }

        private void buttonOpenSpecies_Click(object sender, EventArgs e)
        {
            IO.RunFile(textBoxSpecies.Text);
        }

        private void checkBoxSpeciesExpand_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxSpeciesExpandVisualControl.Enabled = checkBoxSpeciesExpand.Checked;

            if (!checkBoxSpeciesExpand.Checked)
            {
                checkBoxSpeciesExpandVisualControl.Checked = false;
            }
        }

        private void buttonRemoveVar_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listViewAddtVars.SelectedItems)
            {
                listViewAddtVars.Items.Remove(li);
            }
        }

        private void buttonNewVar_Click(object sender, EventArgs e)
        {
            ListViewItem newitem = new ListViewItem();
            listViewAddtVars.Items.Add(newitem);
            newitem.BeginEdit();
        }

        private void listViewAddVars_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (!e.Label.IsAcceptable()) listViewAddtVars.Items[e.Item].Remove();
        }

        private void checkBoxAutoIncreaseBio_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAutoDecreaseBio.Enabled = !checkBoxFixTotals.Checked && checkBoxAutoIncreaseBio.Checked;

            if (!checkBoxAutoIncreaseBio.Checked)
            {
                checkBoxAutoDecreaseBio.Checked = false;
            }
        }

        private void checkBoxFixTotals_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAutoLog.Enabled = !checkBoxFixTotals.Checked;
            checkBoxAutoIncreaseBio.Enabled = !checkBoxFixTotals.Checked;
            checkBoxAutoDecreaseBio.Enabled = !checkBoxFixTotals.Checked;

            if (checkBoxFixTotals.Checked)
            {
                checkBoxAutoLog.Checked = true;
                checkBoxAutoIncreaseBio.Checked = true;
                checkBoxAutoDecreaseBio.Checked = true;
            }
        }

        private void checkBoxBreakBeforeIndividuals_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxBreakBetweenSpecies.Enabled = checkBoxBreakBeforeIndividuals.Checked;
            if (!checkBoxBreakBeforeIndividuals.Checked) checkBoxBreakBetweenSpecies.Checked = false;
        }

        private void checkBoxOrderLog_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxLogOrder.Enabled = checkBoxOrderLog.Checked;
        }

        private void buttonClearRecent_Click(object sender, EventArgs e)
        {
            string[] species = UserSettings.GetKeys(Species.UserSettings.Path, Path.GetFileNameWithoutExtension(UserSettings.TaxonomicIndexPath));

            tdClearRecent.Content = string.Format(Resources.Interface.Messages.ClearRecent, species.Length);

            if (tdClearRecent.ShowDialog() == tdbRecentClear)
            {
                UserSettings.ClearFolder(UserSettings.FeatureKey, Path.GetFileNameWithoutExtension(UserSettings.TaxonomicIndexPath));
            }
        }

        private void buttonBasicSettings_Click(object sender, EventArgs e)
        {
            Software.Settings settings = new Software.Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            settings.ShowDialog();
        }
    }
}
