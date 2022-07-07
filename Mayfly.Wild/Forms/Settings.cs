using Mayfly.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using static Mayfly.UserSettings;
using static Mayfly.Wild.ReaderSettings;
using static Mayfly.Wild.UserSettings;

namespace Mayfly.Wild
{
    public partial class Settings : Form
    {
        protected EventHandler saved;

        [Category("Mayfly Events"), Browsable(true)]
        public event EventHandler OnSaved {
            add { saved += value; }
            remove { saved -= value; }
        }



        public Settings() {

            InitializeComponent();

            listViewAddtFctr.Shine();
            listViewAddtVars.Shine();
        }


        protected void Initiate() {

            #region references

            textBoxWaters.Text = WatersIndexPath;
            textBoxSpecies.Text = TaxonomicIndexPath;

            checkBoxSpeciesExpand.Checked = SpeciesAutoExpand;
            checkBoxSpeciesExpandVisualControl.Checked = SpeciesAutoExpandVisual;

            #endregion

            #region input

            checkBoxAutoLog.Checked = AutoLogOpen;
            checkBoxFixTotals.Checked = FixTotals;
            checkBoxAutoIncreaseBio.Checked = AutoIncreaseBio;
            checkBoxAutoDecreaseBio.Checked = AutoDecreaseBio;

            foreach (string item in AddtVariables) {
                ListViewItem li = new ListViewItem(item);
                listViewAddtVars.Items.Add(li);
            }

            foreach (ListViewItem item in listViewAddtVars.Items) {
                if (Array.IndexOf(CurrentVariables, item.Text) > -1)
                    item.Checked = true;
            }

            numericUpDownRecentCount.Value = RecentSpeciesCount;

            #endregion

            #region print

            checkBoxCardOdd.Checked = OddCardStart;
            checkBoxBreakBeforeIndividuals.Checked = BreakBeforeIndividuals;
            checkBoxBreakBetweenSpecies.Checked = BreakBetweenSpecies;
            checkBoxOrderLog.Checked = LogOrder != LogSortOrder.AsInput;
            if (LogOrder != LogSortOrder.AsInput)
                comboBoxLogOrder.SelectedIndex = (int)LogOrder;

            #endregion
        }



        private void buttonApply_Click(object sender, EventArgs e) {
            if (!tabPageReferences.IsDisposed) {
                WatersIndexPath = textBoxWaters.Text;
                TaxonomicIndexPath = textBoxSpecies.Text;
                SpeciesAutoExpand = checkBoxSpeciesExpand.Checked;
                SpeciesAutoExpandVisual = checkBoxSpeciesExpandVisualControl.Checked;
            }

            if (!tabPageInput.IsDisposed) {
                FixTotals = checkBoxFixTotals.Checked;
                AutoIncreaseBio = checkBoxAutoIncreaseBio.Checked;
                AutoDecreaseBio = checkBoxAutoDecreaseBio.Checked;
                AutoLogOpen = checkBoxAutoLog.Checked;
                RecentSpeciesCount = (int)numericUpDownRecentCount.Value;
            }

            List<string> addvars = new List<string>();
            foreach (ListViewItem item in listViewAddtVars.Items)
                addvars.Add(item.Text);
            AddtVariables = addvars.ToArray();

            List<string> currvars = new List<string>();
            foreach (ListViewItem item in listViewAddtVars.CheckedItems)
                currvars.Add(item.Text);
            CurrentVariables = currvars.ToArray();


            if (!tabPagePrint.IsDisposed) {
                OddCardStart = checkBoxCardOdd.Checked;
                BreakBeforeIndividuals = checkBoxBreakBeforeIndividuals.Checked;
                BreakBetweenSpecies = checkBoxBreakBetweenSpecies.Checked;
                LogOrder = checkBoxOrderLog.Checked ? (LogSortOrder)comboBoxLogOrder.SelectedIndex : LogSortOrder.AsInput;
            }

            if (saved != null) saved.Invoke(this, EventArgs.Empty);

            Log.Write(EventType.Maintenance, "User settings changed");
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            buttonApply_Click(sender, e);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public void buttonBrowseWaters_Click(object sender, EventArgs e) {
            if (Waters.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK) {
                textBoxWaters.Text = Waters.UserSettings.Interface.OpenDialog.FileName;
            }
        }

        private void buttonOpenWaters_Click(object sender, EventArgs e) {
            IO.RunFile(textBoxWaters.Text);
        }

        private void buttonBrowseSpecies_Click(object sender, EventArgs e) {
            OpenFileDialog SetSpecies = Species.UserSettings.Interface.OpenDialog;

            if (SetSpecies.ShowDialog() == DialogResult.OK) {
                textBoxSpecies.Text = SetSpecies.FileName;
            }
        }

        private void buttonOpenSpecies_Click(object sender, EventArgs e) {
            IO.RunFile(textBoxSpecies.Text);
        }

        private void checkBoxSpeciesExpand_CheckedChanged(object sender, EventArgs e) {
            checkBoxSpeciesExpandVisualControl.Enabled = checkBoxSpeciesExpand.Checked;

            if (!checkBoxSpeciesExpand.Checked) {
                checkBoxSpeciesExpandVisualControl.Checked = false;
            }
        }

        private void buttonRemoveVar_Click(object sender, EventArgs e) {
            foreach (ListViewItem li in listViewAddtVars.SelectedItems) {
                listViewAddtVars.Items.Remove(li);
            }
        }

        private void buttonNewVar_Click(object sender, EventArgs e) {
            ListViewItem newitem = new ListViewItem();
            listViewAddtVars.Items.Add(newitem);
            newitem.BeginEdit();
        }

        private void listViewAddVars_AfterLabelEdit(object sender, LabelEditEventArgs e) {
            if (!e.Label.IsAcceptable()) listViewAddtVars.Items[e.Item].Remove();
        }

        private void checkBoxAutoIncreaseBio_CheckedChanged(object sender, EventArgs e) {
            checkBoxAutoDecreaseBio.Enabled = !checkBoxFixTotals.Checked && checkBoxAutoIncreaseBio.Checked;

            if (!checkBoxAutoIncreaseBio.Checked) {
                checkBoxAutoDecreaseBio.Checked = false;
            }
        }

        private void checkBoxFixTotals_CheckedChanged(object sender, EventArgs e) {
            checkBoxAutoLog.Enabled = !checkBoxFixTotals.Checked;
            checkBoxAutoIncreaseBio.Enabled = !checkBoxFixTotals.Checked;
            checkBoxAutoDecreaseBio.Enabled = !checkBoxFixTotals.Checked;

            if (checkBoxFixTotals.Checked) {
                checkBoxAutoLog.Checked = true;
                checkBoxAutoIncreaseBio.Checked = true;
                checkBoxAutoDecreaseBio.Checked = true;
            }
        }

        private void checkBoxBreakBeforeIndividuals_CheckedChanged(object sender, EventArgs e) {
            checkBoxBreakBetweenSpecies.Enabled = checkBoxBreakBeforeIndividuals.Checked;
            if (!checkBoxBreakBeforeIndividuals.Checked) checkBoxBreakBetweenSpecies.Checked = false;
        }

        private void checkBoxOrderLog_CheckedChanged(object sender, EventArgs e) {
            comboBoxLogOrder.Enabled = checkBoxOrderLog.Checked;
        }

        private void buttonClearRecent_Click(object sender, EventArgs e) {
            string[] species = GetKeys(Species.UserSettings.FeatureKey, Path.GetFileNameWithoutExtension(TaxonomicIndexPath));

            tdClearRecent.Content = string.Format(Resources.Interface.Messages.ClearRecent, species.Length);

            if (tdClearRecent.ShowDialog() == tdbRecentClear) {
                ClearFolder(Species.UserSettings.FeatureKey, Path.GetFileNameWithoutExtension(TaxonomicIndexPath));
            }
        }

        private void buttonBasicSettings_Click(object sender, EventArgs e) {
            Software.Settings settings = new Software.Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            settings.ShowDialog();
        }
    }
}
