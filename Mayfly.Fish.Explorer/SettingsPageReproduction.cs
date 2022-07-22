using Mayfly.Species;
using Mayfly.Wild;
using System.Windows.Forms;
using Mayfly.Controls;

namespace Mayfly.Fish.Explorer
{
    public partial class SettingsPageReproduction : SettingsPage, ISettingsPage
    {
        public SettingsPageReproduction() {

            InitializeComponent();

            ColumnAgeSpecies.ValueType = typeof(string);
            ColumnAgeValue.ValueType = typeof(Age);
            speciesSelectorAge.IndexPath = SettingsReader.TaxonomicIndexPath;
        }

        public void LoadSettings() {

            spreadSheetAge.Rows.Clear();

            foreach (TaxonomicIndex.TaxonRow speciesRow in SettingsReader.TaxonomicIndex.GetSpeciesRows()) {
                LoadAge(speciesRow);
            }
        }

        public void SaveSettings() {

            UserSettings.ClearFolder(UserSettings.FeatureKey, nameof(Service.GamingAge));
            foreach (DataGridViewRow gridRow in spreadSheetAge.Rows) {
                if (gridRow.IsNewRow) continue;
                if (gridRow.Cells[ColumnAgeSpecies.Index].Value == null) continue;
                if (gridRow.Cells[ColumnAgeValue.Index].Value == null) continue;
                Service.SaveGamingAge((string)gridRow.Cells[ColumnAgeSpecies.Index].Value,
                    (Age)gridRow.Cells[ColumnAgeValue.Index].Value);
            }
        }

        private void LoadAge(TaxonomicIndex.TaxonRow speciesRow) {

            Age age = Service.GetGamingAge(speciesRow.Name);

            if (age == null) return;

            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetAge);
            gridRow.Cells[ColumnAgeSpecies.Index].Value = speciesRow.Name;
            gridRow.Cells[ColumnAgeValue.Index].Value = age;

            spreadSheetAge.Rows.Add(gridRow);
        }
    }
}
