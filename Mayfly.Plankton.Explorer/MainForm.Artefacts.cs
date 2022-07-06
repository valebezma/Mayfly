using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Species;
using System.ComponentModel;
using System.Windows.Forms;
using Mayfly.Wild;

namespace Mayfly.Plankton.Explorer
{
    public partial class MainForm
    {
        private int FindArtifacts()
        {
            int artifactCount = 0;
            artifactCount += FindSpeciesArtifacts();
            return artifactCount;
        }

        private int FindSpeciesArtifacts()
        {
            int artifactCount = 0;

            foreach (Data.DefinitionRow speciesRow in data.Species)
            {
                double q = data.Quantity(speciesRow);

                #region In key missing

                TaxonomicIndex.SpeciesRow spcRow = SpeciesIndex.Definition.FindByName(
                    speciesRow.Species);

                if (spcRow == null)
                {
                    artifactCount++;
                }

                #endregion
            }

            labelArtifactSpeciesCount.UpdateStatus(artifactCount);
            labelArtifactSpecies.Visible = pictureBoxArtifactSpecies.Visible = artifactCount == 0;
            return artifactCount;
        }

        private void LoadArtifacts()
        {
            spreadSheetArtifactSpecies.Rows.Clear();

            foreach (Data.DefinitionRow speciesRow in data.Species)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetArtifactSpecies);

                gridRow.Cells[columnArtifactSpecies.Index].Value = speciesRow.Species;
                double q = data.Quantity(speciesRow);
                gridRow.Cells[columnArtifactN.Index].Value = q;

                spreadSheetArtifactSpecies.Rows.Add(gridRow);

                GetSpeciesFullName(gridRow, columnArtifactSpecies, columnArtifactValidName);
                spreadSheetArtifactSpecies.Sort(columnArtifactValidName, ListSortDirection.Ascending);
            }
        }

        private void GetSpeciesFullName(DataGridViewRow gridRow, DataGridViewColumn gridColumnTyping, DataGridViewColumn gridColumnShowing)
        {
            if (gridRow.Cells[gridColumnTyping.Index].Value == null)
            {
                return;
            }

            if (gridRow.Cells[gridColumnTyping.Index].Value as string == Species.Resources.Interface.UnidentifiedTitle)
            {
                return;
            }

            string speciesEntered = (string)gridRow.Cells[gridColumnTyping.Index].Value;

            TaxonomicIndex.SpeciesRow speciesRow = speciesValidator.Find(speciesEntered);

            if (speciesRow == null)
            {
                gridRow.Cells[gridColumnShowing.Index].Value = null;
                ((TextAndImageCell)gridRow.Cells[gridColumnShowing.Index]).Image = Mathematics.Properties.Resources.None;
                gridRow.Cells[gridColumnShowing.Index].ToolTipText = "Species is missing";
            }
            else
            {
                gridRow.Cells[gridColumnShowing.Index].Value = speciesRow.FullName;
                ((TextAndImageCell)gridRow.Cells[gridColumnShowing.Index]).Image = Pictogram.Check;
            }
        }
    }
}