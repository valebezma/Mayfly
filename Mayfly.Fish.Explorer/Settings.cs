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
        Samplers.SamplerRow selectedSampler;

        public event EventHandler SettingsSaved;



        public Settings()
        {
            InitializeComponent();

            tabPageTreat.Parent =
                tabPageGamingAge.Parent =
                tabPageGamingMeasure.Parent =
                tabPageCatchability.Parent =
                License.AllowedFeaturesLevel >= FeatureLevel.Advanced ? tabControl : null;

            UI.SetControlClickability(
                License.AllowedFeaturesLevel >= FeatureLevel.Advanced,
                checkBoxBioAutoLoad,
                label7);

            comboBoxDiversity.DataSource = Wild.Service.GetDiversityIndices();
            comboBoxGear.DataSource = Fish.UserSettings.ReaderSettings.SamplersIndex.Sampler.Select();

            ColumnAgeSpecies.ValueType = typeof(string);
            ColumnAgeValue.ValueType = typeof(Age);
            ColumnMeasureSpecies.ValueType = typeof(string);
            ColumnMeasureValue.ValueType = typeof(double);
            columnCatchabilitySpecies.ValueType = typeof(string);
            columnCatchabilityValue.ValueType = typeof(double);

            numericUpDownInterval.Minimum = numericUpDownInterval.Increment =
                (decimal)Fish.UserSettings.DefaultStratifiedInterval;

            checkBoxSuggestAge.Checked = UserSettings.SuggestAge;
            checkBoxSuggestMass.Checked = UserSettings.SuggestMass;
            checkBoxKeepWizards.Checked = UserSettings.KeepWizard;
            comboBoxReportCriticality.SelectedIndex = (int)UserSettings.ReportCriticality;
            checkBoxConsistency.Checked = UserSettings.CheckConsistency;

            spreadSheetAge.Rows.Clear();
            spreadSheetMeasure.Rows.Clear();
            spreadSheetCatchability.Rows.Clear();

            if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced)
            {
                speciesSelectorAge.IndexPath = Fish.UserSettings.ReaderSettings.TaxonomicIndexPath;
                speciesSelectorMeasure.IndexPath = Fish.UserSettings.ReaderSettings.TaxonomicIndexPath;
                speciesSelectorCatchability.IndexPath = Fish.UserSettings.ReaderSettings.TaxonomicIndexPath;

                foreach (TaxonomicIndex.TaxonRow speciesRow in Fish.UserSettings.ReaderSettings.TaxonomicIndex.GetSpeciesRows())
                {
                    LoadAge(speciesRow);
                    LoadMeasure(speciesRow);
                    LoadCatchability(speciesRow);
                }

                comboBoxGear_SelectedIndexChanged(comboBoxGear, new EventArgs());
                numericUpDownCatchabilityDefault.Value = (decimal)UserSettings.DefaultCatchability;

                numericUpDownInterval.Value = (decimal)UserSettings.SizeInterval;

                comboBoxDominance.SelectedIndex = Wild.UserSettings.Dominance;
                comboBoxDiversity.SelectedIndex = (int)Wild.UserSettings.Diversity;

                comboBoxAlk.SelectedIndex = (int)UserSettings.SelectedAgeLengthKeyType;
            }

            if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced)
            {
                checkBoxBioAutoLoad.Checked = UserSettings.AutoLoadBio;

                foreach (string bio in UserSettings.Bios)
                {
                    ListViewItem li = listViewBio.CreateItem(bio,
                        Path.GetFileNameWithoutExtension(bio));
                    listViewBio.Items.Add(li);
                }
            }
        }

        private void LoadAge(TaxonomicIndex.TaxonRow speciesRow)
        {
            Age age = Service.GetGamingAge(speciesRow.Name);

            if (age == null) return;

            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetAge);
            gridRow.Cells[ColumnAgeSpecies.Index].Value = speciesRow.Name;
            gridRow.Cells[ColumnAgeValue.Index].Value = age;

            spreadSheetAge.Rows.Add(gridRow);
        }

        private void LoadMeasure(TaxonomicIndex.TaxonRow speciesRow)
        {
            double measure = Service.GetMeasure(speciesRow.Name);
            if (double.IsNaN(measure)) return;

            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetMeasure);
            gridRow.Cells[ColumnMeasureSpecies.Index].Value = speciesRow.Name;
            gridRow.Cells[ColumnMeasureValue.Index].Value = measure;

            spreadSheetMeasure.Rows.Add(gridRow);
        }

        private void LoadCatchability(TaxonomicIndex.TaxonRow speciesRow)
        {
            foreach (Samplers.SamplerRow samplerRow in Fish.UserSettings.ReaderSettings.SamplersIndex.Sampler)
            {
                LoadCatchability(speciesRow, samplerRow);
            }
        }

        private void LoadCatchability(TaxonomicIndex.TaxonRow speciesRow, Samplers.SamplerRow samplerRow)
        {
            double catchability = Service.GetCatchability(samplerRow.GetSamplerType(), speciesRow.Name);
            if (catchability == UserSettings.DefaultCatchability) return;

            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetCatchability);
            gridRow.Cells[columnCatchabilitySpecies.Index].Value = speciesRow.Name;
            gridRow.Cells[columnCatchabilityValue.Index].Value = catchability;
            gridRow.Tag = samplerRow;
            spreadSheetCatchability.Rows.Add(gridRow);
        }

        private void SaveSettings()
        {
            Wild.UserSettings.Diversity = (DiversityIndex)comboBoxDiversity.SelectedIndex;
            Wild.UserSettings.Dominance = comboBoxDominance.SelectedIndex;

            UserSettings.DefaultCatchability = (double)numericUpDownCatchabilityDefault.Value;

            UserSetting.ClearFolder(UserSettings.Path, nameof(Service.GamingAge));
            foreach (DataGridViewRow gridRow in spreadSheetAge.Rows)
            {
                if (gridRow.IsNewRow) continue;
                if (gridRow.Cells[ColumnAgeSpecies.Index].Value == null) continue;
                if (gridRow.Cells[ColumnAgeValue.Index].Value == null) continue;
                Service.SaveGamingAge((string)gridRow.Cells[ColumnAgeSpecies.Index].Value,
                    (Age)gridRow.Cells[ColumnAgeValue.Index].Value);
            }

            UserSetting.ClearFolder(UserSettings.Path, nameof(Service.GamingLength));
            foreach (DataGridViewRow gridRow in spreadSheetMeasure.Rows)
            {
                if (gridRow.IsNewRow) continue;
                if (gridRow.Cells[ColumnMeasureSpecies.Index].Value == null) continue;
                if (gridRow.Cells[ColumnMeasureValue.Index].Value == null) continue;
                Service.SaveMeasure((string)gridRow.Cells[ColumnMeasureSpecies.Index].Value,
                    (double)gridRow.Cells[ColumnMeasureValue.Index].Value);
            }

            UserSetting.ClearFolder(UserSettings.Path, nameof(Service.Catchability));
            foreach (DataGridViewRow gridRow in spreadSheetCatchability.Rows)
            {
                if (gridRow.IsNewRow) continue;
                if (gridRow.Cells[columnCatchabilitySpecies.Index].Value == null) continue;
                if (gridRow.Cells[columnCatchabilityValue.Index].Value == null) continue;
                if ((double)gridRow.Cells[columnCatchabilityValue.Index].Value == UserSettings.DefaultCatchability) continue;
                Service.SaveCatchability(((Samplers.SamplerRow)gridRow.Tag).GetSamplerType(),
                    (string)gridRow.Cells[columnCatchabilitySpecies.Index].Value,
                    (double)gridRow.Cells[columnCatchabilityValue.Index].Value);
            }

            UserSettings.SizeInterval = (double)numericUpDownInterval.Value;

            UserSettings.SuggestAge = checkBoxSuggestAge.Checked;
            UserSettings.SuggestMass = checkBoxSuggestMass.Checked;
            //UserSettings.VisualConfirmation = checkBoxVisualConfirmation.Checked;

            UserSettings.AutoLoadBio = checkBoxBioAutoLoad.Checked;

            List<string> bios = new List<string>();
            foreach (ListViewItem li in listViewBio.Items)
            {
                bios.Add(li.Name);
            }
            UserSettings.Bios = bios.ToArray();

            UserSettings.SelectedAgeLengthKeyType = (AgeLengthKeyType)comboBoxAlk.SelectedIndex;

            UserSettings.KeepWizard = checkBoxKeepWizards.Checked;
            UserSettings.CheckConsistency = checkBoxConsistency.Checked;
            UserSettings.ReportCriticality = (ArtifactCriticality)comboBoxReportCriticality.SelectedIndex;

            if (SettingsSaved != null)
            {
                SettingsSaved.Invoke(this, new EventArgs());
            }
        }



        private void comboBoxGear_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedSampler = (Samplers.SamplerRow)comboBoxGear.SelectedItem;

            foreach (DataGridViewRow gridRow in spreadSheetCatchability.Rows)
            {
                if (gridRow.IsNewRow) continue;
                gridRow.Visible = gridRow.Tag == selectedSampler;
            }

            spreadSheetCatchability.ClearSelection();
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
