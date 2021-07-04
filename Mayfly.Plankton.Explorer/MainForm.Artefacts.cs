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
        private int FindArtefacts()
        {
            int artefactCount = 0;
            artefactCount += FindSpeciesArtefacts();
            return artefactCount;
        }

        private int FindSpeciesArtefacts()
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

            labelArtefactSpeciesCount.UpdateStatus(artefactCount);
            labelArtefactSpecies.Visible = pictureBoxArtefactSpecies.Visible = artefactCount == 0;
            return artefactCount;
        }

        private void LoadArtefacts()
        {
            spreadSheetArtefactSpecies.Rows.Clear();

            foreach (Data.SpeciesRow speciesRow in data.Species)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetArtefactSpecies);

                gridRow.Cells[columnArtefactSpecies.Index].Value = speciesRow.Species;
                double q = data.Quantity(speciesRow);
                gridRow.Cells[columnArtefactN.Index].Value = q;

                spreadSheetArtefactSpecies.Rows.Add(gridRow);

                GetSpeciesFullName(gridRow, columnArtefactSpecies, columnArtefactValidName);
                spreadSheetArtefactSpecies.Sort(columnArtefactValidName, ListSortDirection.Ascending);
            }
        }

        private void GetSpeciesFullName(DataGridViewRow gridRow,
            DataGridViewColumn gridColumnTyping, DataGridViewColumn gridColumnShowing)
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
                ((TextAndImageCell)gridRow.Cells[gridColumnShowing.Index]).Image = Mayfly.Properties.Resources.Check;
            }
        }
    }
}