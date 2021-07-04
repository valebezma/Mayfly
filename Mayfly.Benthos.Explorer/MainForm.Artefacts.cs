using Mayfly.Benthos;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Wild;
using Mayfly.Wild.Controls;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Software;
using Mayfly.Waters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Statistics;

namespace Mayfly.Benthos.Explorer
{
    public partial class MainForm
    {
        bool showArtefacts;

        private void ShowCardArtefacts(CardArtefact[] artefacts)
        {
            spreadSheetArtefactCard.Rows.Clear();
            int artefactCount = 0;

            foreach (CardArtefact artefact in artefacts)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetArtefactCard);
                
                gridRow.Cells[columnArtCardName.Index].Value = artefact.Card;                    
                gridRow.Cells[columnArtCardName.Index].ToolTipText = artefact.Card.Path;

                spreadSheetArtefactCard.Rows.Add(gridRow);

                if (artefact.SamplingSquareMissing)
                {
                    ((TextAndImageCell)gridRow.Cells[columnArtCardSquareMissing.Index]).Image =
                        Mathematics.Properties.Resources.None;
                    gridRow.Cells[columnArtCardSquareMissing.Index].ToolTipText = Resources.Artefact.SampleSquare;
                    artefactCount++;
                }
                else
                {
                    ((TextAndImageCell)gridRow.Cells[columnArtCardSquareMissing.Index]).Image =
                        Mathematics.Properties.Resources.Check;
                    gridRow.Cells[columnArtCardSquareMissing.Index].Value = artefact.Card.Square;
                }
            }

            labelArtefactCard.Visible = pictureBoxArtefactCard.Visible = artefactCount == 0;
            spreadSheetArtefactCard.UpdateStatus(artefactCount);
        }

        private void ShowSpeciesArtefacts(SpeciesArtefact[] artefacts)
        {
            spreadSheetArtefactSpecies.Rows.Clear();

            int total = 0;

            foreach (SpeciesArtefact artefact in artefacts)
            {
                total += artefact.GetFacts();

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetArtefactSpecies);
                gridRow.Cells[columnArtefactSpecies.Index].Value = artefact.SpeciesRow.Species;
                gridRow.Cells[columnArtefactN.Index].Value = artefact.Quantity;
                spreadSheetArtefactSpecies.Rows.Add(gridRow);

                if (artefact.ReferenceMissing)
                {
                    gridRow.Cells[columnArtefactValidName.Index].Value = null;
                    ((TextAndImageCell)gridRow.Cells[columnArtefactValidName.Index]).Image = 
                        Mathematics.Properties.Resources.None;
                    gridRow.Cells[columnArtefactValidName.Index].ToolTipText =
                        Resources.Artefact.ReferenceNull;
                }
                else
                {
                    gridRow.Cells[columnArtefactValidName.Index].Value = 
                        speciesValidator.Find(artefact.SpeciesRow.Species).FullName;
                    ((TextAndImageCell)gridRow.Cells[columnArtefactValidName.Index]).Image = 
                        Mayfly.Resources.Icons.Check;
                }
            }

            spreadSheetArtefactSpecies.Sort(columnArtefactSpecies, ListSortDirection.Ascending);
            labelArtefactSpecies.Visible = pictureBoxArtefactSpecies.Visible = total == 0;
            spreadSheetArtefactSpecies.UpdateStatus(total);
        }
    }
}