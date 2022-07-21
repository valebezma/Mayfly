using Mayfly.Species;
using Mayfly.Wild;
using System.Windows.Forms;
using static Mayfly.UserSettings;
using System;
using Mayfly.Controls;

namespace Mayfly.Fish.Explorer
{
    public partial class SettingsControlCatchability : SettingsControl, ISettingsControl
    {
        Survey.SamplerRow selectedSampler;


        public SettingsControlCatchability() {

            InitializeComponent();

            comboBoxGear.DataSource = SettingsReader.SamplersIndex.Sampler.Select();
            columnCatchabilitySpecies.ValueType = typeof(string);
            columnCatchabilityValue.ValueType = typeof(double);
            speciesSelectorCatchability.IndexPath = SettingsReader.TaxonomicIndexPath;
        }



        public void LoadSettings() {

            comboBoxGear_SelectedIndexChanged(comboBoxGear, new EventArgs());

            spreadSheetCatchability.Rows.Clear();

            foreach (TaxonomicIndex.TaxonRow speciesRow in SettingsReader.TaxonomicIndex.GetSpeciesRows()) {
                LoadCatchability(speciesRow);
            }

            numericUpDownCatchabilityDefault.Value = (decimal)UserSettings.DefaultCatchability;
        }

        public void SaveSettings() {

            UserSettings.DefaultCatchability = (double)numericUpDownCatchabilityDefault.Value;
            ClearFolder(FeatureKey, nameof(Service.Catchability));
            foreach (DataGridViewRow gridRow in spreadSheetCatchability.Rows) {
                if (gridRow.IsNewRow) continue;
                if (gridRow.Cells[columnCatchabilitySpecies.Index].Value == null) continue;
                if (gridRow.Cells[columnCatchabilityValue.Index].Value == null) continue;
                if ((double)gridRow.Cells[columnCatchabilityValue.Index].Value == UserSettings.DefaultCatchability) continue;
                Service.SaveCatchability(((Survey.SamplerRow)gridRow.Tag).GetSamplerType(),
                    (string)gridRow.Cells[columnCatchabilitySpecies.Index].Value,
                    (double)gridRow.Cells[columnCatchabilityValue.Index].Value);
            }
        }

        private void LoadCatchability(TaxonomicIndex.TaxonRow speciesRow) {

            foreach (Survey.SamplerRow samplerRow in SettingsReader.SamplersIndex.Sampler) {
                LoadCatchability(speciesRow, samplerRow);
            }
        }

        private void LoadCatchability(TaxonomicIndex.TaxonRow speciesRow, Survey.SamplerRow samplerRow) {

            double catchability = Service.GetCatchability(samplerRow.GetSamplerType(), speciesRow.Name);
            if (catchability == UserSettings.DefaultCatchability) return;

            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetCatchability);
            gridRow.Cells[columnCatchabilitySpecies.Index].Value = speciesRow.Name;
            gridRow.Cells[columnCatchabilityValue.Index].Value = catchability;
            gridRow.Tag = samplerRow;
            spreadSheetCatchability.Rows.Add(gridRow);
        }



        private void comboBoxGear_SelectedIndexChanged(object sender, EventArgs e) {

            selectedSampler = (Samplers.SamplerRow)comboBoxGear.SelectedItem;

            foreach (DataGridViewRow gridRow in spreadSheetCatchability.Rows) {
                if (gridRow.IsNewRow) continue;
                gridRow.Visible = gridRow.Tag == selectedSampler;
            }

            spreadSheetCatchability.ClearSelection();
        }

        private void spreadSheetCatchability_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            if (selectedSampler != null) {
                spreadSheetCatchability.Rows[e.RowIndex].Tag = selectedSampler;
            }

            if (e.ColumnIndex == columnCatchabilityValue.Index &&
                spreadSheetCatchability[e.ColumnIndex, e.RowIndex].Value is double &&
                (double)spreadSheetCatchability[e.ColumnIndex, e.RowIndex].Value == (double)numericUpDownCatchabilityDefault.Value) {
                ToolTip toolTip = new ToolTip();
                toolTip.Show("Default value", spreadSheetCatchability, spreadSheetCatchability.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex + 1, true).Location);
            }
        }
    }
}
