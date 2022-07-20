using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace Mayfly.Fish.Explorer
{
    public partial class Settings : Form
    {

        public event EventHandler SettingsSaved;



        public Settings()
        {
            InitializeComponent();

            checkBoxSuggestAge.Checked = UserSettings.SuggestAge;
            checkBoxSuggestMass.Checked = UserSettings.SuggestMass;


            if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced)
            {
            }
        }

        private void SaveSettings()
        {
            UserSettings.SuggestAge = checkBoxSuggestAge.Checked;
            UserSettings.SuggestMass = checkBoxSuggestMass.Checked;
            //UserSettings.VisualConfirmation = checkBoxVisualConfirmation.Checked;

            if (SettingsSaved != null)
            {
                SettingsSaved.Invoke(this, new EventArgs());
            }
        }



        private void spreadSheetCatchability_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (selectedSampler != null)
            {
                spreadSheetCatchability.Rows[e.RowIndex].Tag = selectedSampler;
            }

            if (e.ColumnIndex == columnCatchabilityValue.Index &&
                spreadSheetCatchability[e.ColumnIndex, e.RowIndex].Value is double &&
                (double)spreadSheetCatchability[e.ColumnIndex, e.RowIndex].Value == (double)numericUpDownCatchabilityDefault.Value)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Show("Default value", spreadSheetCatchability, spreadSheetCatchability.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex + 1, true).Location);
            }
        }

        private void buttonFish_Click(object sender, EventArgs e)
        {
            Fish.Settings settings = new Fish.Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            settings.Show();
        }

        private void buttonMath_Click(object sender, EventArgs e)
        {
            Mathematics.Settings settings = new Mathematics.Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            settings.Show();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < spreadSheetAge.RowCount; i++)
            {
                if (spreadSheetAge.Rows[i].IsNewRow) continue;

                if (spreadSheetAge[ColumnAgeSpecies.Index, i].Value == null || 
                    spreadSheetAge[ColumnAgeValue.Index, i].Value == null )
                {
                    spreadSheetAge.Rows.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < spreadSheetMeasure.RowCount; i++)
            {
                if (spreadSheetMeasure.Rows[i].IsNewRow) continue;

                if (spreadSheetMeasure[ColumnMeasureSpecies.Index, i].Value == null ||
                    spreadSheetMeasure[ColumnMeasureValue.Index, i].Value == null)
                {
                    spreadSheetMeasure.Rows.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < spreadSheetCatchability.RowCount; i++)
            {
                if (spreadSheetCatchability.Rows[i].IsNewRow) continue;

                if (spreadSheetCatchability[columnCatchabilitySpecies.Index, i].Value == null ||
                    spreadSheetCatchability[columnCatchabilityValue.Index, i].Value == null)
                {
                    spreadSheetCatchability.Rows.RemoveAt(i);
                    i--;
                }
            }

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

        private void checkBoxBioAutoLoad_CheckedChanged(object sender, EventArgs e)
        {
            listViewBio.Enabled = buttonBioBrowse.Enabled = buttonBioRemove.Enabled =
                checkBoxBioAutoLoad.Checked;
        }

        private void buttonBioBrowse_Click(object sender, EventArgs e)
        {
            if (Wild.UserSettings.InterfaceBio.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                ListViewItem li = listViewBio.CreateItem(Wild.UserSettings.InterfaceBio.OpenDialog.FileName,
                    Path.GetFileNameWithoutExtension(Wild.UserSettings.InterfaceBio.OpenDialog.FileName));
                listViewBio.Items.Add(li);
            }
        }

        private void listViewBio_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonBioRemove.Enabled = listViewBio.SelectedItems.Count > 0;
        }

        private void buttonBioRemove_Click(object sender, EventArgs e)
        {
            while (listViewBio.SelectedItems.Count > 0)
            {
                listViewBio.Items.Remove(listViewBio.SelectedItems[0]);
            }
        }
    }
}
