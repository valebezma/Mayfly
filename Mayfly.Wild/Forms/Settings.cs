using Mayfly.Extensions;
using Mayfly.Geographics;
using System;
using System.Windows.Forms;
using System.IO;
using System.Resources;

using System.Globalization;
using Mayfly.Software;

namespace Mayfly.Wild
{
    public partial class Settings : Form
    {
        protected delegate void SettingsSaver();

        protected SettingsSaver SaveSettings;

        protected SettingsSaver RecentClear;



        public Settings()
        {
            InitializeComponent();

            listViewAddtFctr.Shine();
            listViewAddtVars.Shine();

            checkBoxOrderLog.Checked = UserSettings.LogOrder != LogOrder.AsInput;
            if (UserSettings.LogOrder != LogOrder.AsInput)
                comboBoxLogOrder.SelectedIndex = (int)UserSettings.LogOrder;           
        }



        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (checkBoxOrderLog.Checked)
            {
                UserSettings.LogOrder = (LogOrder)comboBoxLogOrder.SelectedIndex;
            }
            else
            {
                UserSettings.LogOrder = LogOrder.AsInput;
            }

            Log.Write(EventType.Maintenance, "Mayfly.Wild settings changed");

            SaveSettings.Invoke();
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
            FileSystem.RunFile(textBoxWaters.Text); 
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
            FileSystem.RunFile(textBoxSpecies.Text);
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
            RecentClear.Invoke();
        }

        private void buttonBasicSettings_Click(object sender, EventArgs e)
        {
            Software.Settings settings = new Software.Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            settings.ShowDialog();
        }
    }
}
