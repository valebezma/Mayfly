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
            int artefactCount = 0;
            artefactCount += FindSpeciesArtifacts();
            return artefactCount;
        }

        private int FindSpeciesArtifacts()
        {
            int artefactCount = 0;

            foreach (Data.SpeciesRow speciesRow in data.Species)
            {
                double q = data.Quantity(speciesRow);

                #region In key missing

                SpeciesKey.SpeciesRow spcRow = SpeciesIndex.Species.FindBySpecies(
                    speciesRow.Species);

                if (spcRow == null)
                {
                    artefactCount++;
                }

                #endregion
            }

            labelArtifactSpeciesCount.UpdateStatus(artefactCount);
            labelArtifactSpecies.Visible = pictureBoxArtifactSpecies.Visible = artefactCount == 0;
            return artefactCount;
        }

        private void LoadArtifacts()
        {
            spreadSheetArtifactSpecies.Rows.Clear();

            foreach (Data.SpeciesRow speciesRow in data.Species)
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

            SpeciesKey.SpeciesRow speciesRow = speciesValidator.Find(speciesEntered);

            if (speciesRow == null)
            {
                gridRow.Cells[gridColumnShowing.Index].Value = null;
                ((TextAndImageCell)gridRow.Cells[gridColumnShowing.Index]).Image = Mathematics.Properties.Resources.None;
                gridRow.Cells[gridColumnShowing.Index].ToolTipText = "Species is missing in reference";
            }
            else
            {
                gridRow.Cells[gridColumnShowing.Index].Value = speciesRow.FullName;
                ((TextAndImageCell)gridRow.Cells[gridColumnShowing.Index]).Image = Mayfly.Resources.Icons.Check;
            }
        }
    }
}