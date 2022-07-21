using Mayfly.Species;
using Mayfly.Wild;
using System.Windows.Forms;
using Mayfly.Controls;

namespace Mayfly.Fish.Explorer
{
    public partial class SettingsControlMeasure : SettingsControl, ISettingsControl
    {
        public SettingsControlMeasure() {

            InitializeComponent();

            ColumnMeasureSpecies.ValueType = typeof(string);
            ColumnMeasureValue.ValueType = typeof(double);
            speciesSelectorMeasure.IndexPath = SettingsReader.TaxonomicIndexPath;
        }

        public void LoadSettings() {

            spreadSheetMeasure.Rows.Clear();

            foreach (TaxonomicIndex.TaxonRow speciesRow in SettingsReader.TaxonomicIndex.GetSpeciesRows()) {
                LoadMeasure(speciesRow);
            }
        }

        public void SaveSettings() {

            UserSettings.ClearFolder(UserSettings.FeatureKey, nameof(Service.GamingLength));
            foreach (DataGridViewRow gridRow in spreadSheetMeasure.Rows) {
                if (gridRow.IsNewRow) continue;
                if (gridRow.Cells[ColumnMeasureSpecies.Index].Value == null) continue;
                if (gridRow.Cells[ColumnMeasureValue.Index].Value == null) continue;
                Service.SaveMeasure((string)gridRow.Cells[ColumnMeasureSpecies.Index].Value,
                    (double)gridRow.Cells[ColumnMeasureValue.Index].Value);
            }
        }

        private void LoadMeasure(TaxonomicIndex.TaxonRow speciesRow) {

            double measure = Service.GetMeasure(speciesRow.Name);
            if (double.IsNaN(measure)) return;

            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetMeasure);
            gridRow.Cells[ColumnMeasureSpecies.Index].Value = speciesRow.Name;
            gridRow.Cells[ColumnMeasureValue.Index].Value = measure;

            spreadSheetMeasure.Rows.Add(gridRow);
        }
    }
}
