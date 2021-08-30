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
        bool showArtifacts;

        private void ShowCardArtifacts(CardArtifact[] artifacts)
        {
            spreadSheetArtifactCard.Rows.Clear();
            int artefactCount = 0;

            foreach (CardArtifact artifact in artifacts)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetArtifactCard);
                
                gridRow.Cells[columnArtCardName.Index].Value = artifact.Card;                    
                gridRow.Cells[columnArtCardName.Index].ToolTipText = artifact.Card.Path;

                spreadSheetArtifactCard.Rows.Add(gridRow);

                if (artifact.SamplingSquareMissing)
                {
                    ((TextAndImageCell)gridRow.Cells[columnArtCardSquareMissing.Index]).Image =
                        Mathematics.Properties.Resources.None;
                    gridRow.Cells[columnArtCardSquareMissing.Index].ToolTipText = Resources.Artifact.SampleSquare;
                    artefactCount++;
                }
                else
                {
                    ((TextAndImageCell)gridRow.Cells[columnArtCardSquareMissing.Index]).Image =
                        Mathematics.Properties.Resources.Check;
                    gridRow.Cells[columnArtCardSquareMissing.Index].Value = artifact.Card.Square;
                }
            }

            labelArtifactCard.Visible = pictureBoxArtifactCard.Visible = artefactCount == 0;
            spreadSheetArtifactCard.UpdateStatus(artefactCount);
        }

        private void ShowSpeciesArtifacts(SpeciesArtifact[] artifacts)
        {
            spreadSheetArtifactSpecies.Rows.Clear();

            int total = 0;

            foreach (SpeciesArtifact artifact in artifacts)
            {
                total += artifact.GetFacts();

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetArtifactSpecies);
                gridRow.Cells[columnArtifactSpecies.Index].Value = artifact.SpeciesRow.Species;
                gridRow.Cells[columnArtifactN.Index].Value = artifact.Quantity;
                spreadSheetArtifactSpecies.Rows.Add(gridRow);

                if (artifact.ReferenceMissing)
                {
                    gridRow.Cells[columnArtifactValidName.Index].Value = null;
                    ((TextAndImageCell)gridRow.Cells[columnArtifactValidName.Index]).Image = 
                        Mathematics.Properties.Resources.None;
                    gridRow.Cells[columnArtifactValidName.Index].ToolTipText =
                        Resources.Artifact.ReferenceNull;
                }
                else
                {
                    gridRow.Cells[columnArtifactValidName.Index].Value = 
                        speciesValidator.Find(artifact.SpeciesRow.Species).FullName;
                    ((TextAndImageCell)gridRow.Cells[columnArtifactValidName.Index]).Image = 
                        Mayfly.Resources.Icons.Check;
                }
            }

            spreadSheetArtifactSpecies.Sort(columnArtifactSpecies, ListSortDirection.Ascending);
            labelArtifactSpecies.Visible = pictureBoxArtifactSpecies.Visible = total == 0;
            spreadSheetArtifactSpecies.UpdateStatus(total);
        }
    }
}