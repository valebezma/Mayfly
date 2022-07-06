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
        protected ReaderUserSettings ReaderSettings;

        protected abstract void SaveSettings();



        public Settings(ReaderUserSettings readerSettings)
        {
            InitializeComponent();

            listViewAddtFctr.Shine();
            listViewAddtVars.Shine();

            ReaderSettings = readerSettings;

            #region references

            textBoxWaters.Text = UserSettings.WatersIndexPath;
            textBoxSpecies.Text = ReaderSettings.TaxonomicIndexPath;

            checkBoxSpeciesExpand.Checked = ReaderSettings.SpeciesAutoExpand;
            checkBoxSpeciesExpandVisualControl.Checked = ReaderSettings.SpeciesAutoExpandVisual;

            #endregion

            #region input

            checkBoxAutoLog.Checked = ReaderSettings.AutoLogOpen;
            checkBoxFixTotals.Checked = ReaderSettings.FixTotals;
            checkBoxAutoIncreaseBio.Checked = ReaderSettings.AutoIncreaseBio;
            checkBoxAutoDecreaseBio.Checked = ReaderSettings.AutoDecreaseBio;

            foreach (string item in ReaderSettings.AddtVariables)
            {
                ListViewItem li = new ListViewItem(item);
                listViewAddtVars.Items.Add(li);
            }

            foreach (ListViewItem item in listViewAddtVars.Items)
            {
                if (Array.IndexOf(ReaderSettings.CurrentVariables, item.Text) > -1)
                    item.Checked = true;
            }

            numericUpDownRecentCount.Value = ReaderSettings.RecentSpeciesCount;

            #endregion

            #region print

            checkBoxCardOdd.Checked = ReaderSettings.OddCardStart;
            checkBoxBreakBeforeIndividuals.Checked = ReaderSettings.BreakBeforeIndividuals;
            checkBoxBreakBetweenSpecies.Checked = ReaderSettings.BreakBetweenSpecies;
            checkBoxOrderLog.Checked = UserSettings.LogOrder != LogOrder.AsInput;
            if (UserSettings.LogOrder != LogOrder.AsInput)
                comboBoxLogOrder.SelectedIndex = (int)UserSettings.LogOrder;

            #endregion           
        }



        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (!tabPageReferences.IsDisposed)
            {
                UserSettings.WatersIndexPath = textBoxWaters.Text;
                ReaderSettings.TaxonomicIndexPath = textBoxSpecies.Text;
                ReaderSettings.SpeciesAutoExpand = checkBoxSpeciesExpand.Checked;
                ReaderSettings.SpeciesAutoExpandVisual = checkBoxSpeciesExpandVisualControl.Checked;
            }

            if (!tabPageInput.IsDisposed)
            {
                ReaderSettings.FixTotals = checkBoxFixTotals.Checked;
                ReaderSettings.AutoIncreaseBio = checkBoxAutoIncreaseBio.Checked;
                ReaderSettings.AutoDecreaseBio = checkBoxAutoDecreaseBio.Checked;
                ReaderSettings.AutoLogOpen = checkBoxAutoLog.Checked;
                ReaderSettings.RecentSpeciesCount = (int)numericUpDownRecentCount.Value;
            }

            List<string> addvars = new List<string>();
            foreach (ListViewItem item in listViewAddtVars.Items)
                addvars.Add(item.Text);
            ReaderSettings.AddtVariables = addvars.ToArray();

            List<string> currvars = new List<string>();
            foreach (ListViewItem item in listViewAddtVars.CheckedItems)
                currvars.Add(item.Text);
            ReaderSettings.CurrentVariables = currvars.ToArray();


            if (!tabPagePrint.IsDisposed)
            {
                ReaderSettings.OddCardStart = checkBoxCardOdd.Checked;
                ReaderSettings.BreakBeforeIndividuals = checkBoxBreakBeforeIndividuals.Checked;
                ReaderSettings.BreakBetweenSpecies = checkBoxBreakBetweenSpecies.Checked;
                UserSettings.LogOrder = checkBoxOrderLog.Checked ? (LogOrder)comboBoxLogOrder.SelectedIndex : LogOrder.AsInput;
            }

            SaveSettings();

            Log.Write(EventType.Maintenance, ReaderSettings.ObjectType + " settings changed");
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
            string[] species = UserSetting.GetKeys(Species.UserSettings.Path, Path.GetFileNameWithoutExtension(ReaderSettings.TaxonomicIndexPath));

            tdClearRecent.Content = string.Format(Resources.Interface.Messages.ClearRecent, species.Length);

            if (tdClearRecent.ShowDialog() == tdbRecentClear)
            {
                UserSetting.ClearFolder(UserSettings.Path, Path.GetFileNameWithoutExtension(ReaderSettings.TaxonomicIndexPath));
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
