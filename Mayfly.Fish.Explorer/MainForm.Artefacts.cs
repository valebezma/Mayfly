using Mayfly.Controls;
using Mayfly.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        bool showArtefacts;

        Data.SpeciesRow artefactSpc
        {
            set;
            get;
        }



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

                gridRow.Cells[columnArtCardOddMass.Index].Value = artefact.UnsampledMass;
                spreadSheetArtefactCard.Rows.Add(gridRow);
                
                ((TextAndImageCell)gridRow.Cells[columnArtCardEffort.Index]).Image =
                    artefact.EffortCriticality == ArtefactCriticality.NotCritical ?
                    Mathematics.Properties.Resources.None : Mathematics.Properties.Resources.Check;

                if (artefact.EffortMissing)
                {
                    gridRow.Cells[columnArtCardEffort.Index].ToolTipText = Resources.Artefact.CardEffort;
                    artefactCount++;
                }

                if (artefact.UnsampledMass < 0)
                {
                    gridRow.Cells[columnArtCardOddMass.Index].Value = -artefact.UnsampledMass;

                    ((TextAndImageCell)gridRow.Cells[columnArtCardOddMass.Index]).Image =
                        artefact.OddMassCriticality == ArtefactCriticality.Critical ?
                        Mathematics.Properties.Resources.NoneRed :
                        Mathematics.Properties.Resources.None;
                    gridRow.Cells[columnArtCardOddMass.Index].ToolTipText =
                        string.Format(Resources.Artefact.CardMassOdd, -artefact.UnsampledMass, -artefact.UnsampledMass / artefact.Mass, artefact.Mass);

                    artefactCount++;
                }
                else if (artefact.UnsampledMass > 0)
                {
                    gridRow.Cells[columnArtCardOddMass.Index].Value = artefact.UnsampledMass;
                    ((TextAndImageCell)gridRow.Cells[columnArtCardOddMass.Index]).Image =
                        Mathematics.Properties.Resources.CheckGray;
                    gridRow.Cells[columnArtCardOddMass.Index].ToolTipText =
                        string.Format(Resources.Artefact.CardMassOdd_1, artefact.UnsampledMass, artefact.Mass, artefact.UnsampledMass / artefact.Mass);
                }
                else
                {
                    gridRow.Cells[columnArtCardOddMass.Index].Value = null;
                    ((TextAndImageCell)gridRow.Cells[columnArtCardOddMass.Index]).Image =
                        Mathematics.Properties.Resources.Check;
                    gridRow.Cells[columnArtCardOddMass.Index].ToolTipText =
                        Resources.Artefact.CardMassEven;
                }
            }

            labelArtefactGear.Visible = pictureBoxArtefactGear.Visible = artefactCount == 0;
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
                //gridRow.Cells[columnArtefactN.Index].Value = artefact.Quantity;
                spreadSheetArtefactSpecies.Rows.Add(gridRow);

                //if (artefact.IndividualsMissing > 0)
                //{
                //    gridRow.Cells[columnArtefactDetailed.Index].Value = artefact.IndividualsMissing;
                //    ((TextAndImageCell)gridRow.Cells[columnArtefactDetailed.Index]).Image =
                //        Mathematics.Properties.Resources.NoneRed;

                //    gridRow.Cells[columnArtefactDetailed.Index].ToolTipText = //artefact.ToString();
                //        string.Format(Resources.Artefact.Specimen,
                //        artefact.SpeciesRow.GetFullName(), artefact.Quantity,
                //        artefact.QuantityIndividuals, artefact.QuantityStratified, artefact.IndividualsMissing);
                //}
                //else
                //{
                //    gridRow.Cells[columnArtefactDetailed.Index].Value = Constants.Null;
                //    ((TextAndImageCell)gridRow.Cells[columnArtefactDetailed.Index]).Image =
                //        Mathematics.Properties.Resources.Check;
                //}

                if (artefact.LengthMissing > 0)
                {
                    gridRow.Cells[columnArtefactLength.Index].Value = artefact.LengthMissing;
                    ((TextAndImageCell)gridRow.Cells[columnArtefactLength.Index]).Image =
                        Mathematics.Properties.Resources.None;

                    gridRow.Cells[columnArtefactLength.Index].ToolTipText = //artefact.ToString();
                        string.Format(Resources.Artefact.Length,
                        artefact.LengthMissing);
                }
                else
                {
                    gridRow.Cells[columnArtefactLength.Index].Value = Constants.Null;
                    ((TextAndImageCell)gridRow.Cells[columnArtefactLength.Index]).Image =
                        Mathematics.Properties.Resources.Check;
                }

                ShowSpeciesArtefacts(artefact.AgeArtefact, (TextAndImageCell)gridRow.Cells[columnArtefactAge.Index]);
                ShowSpeciesArtefacts(artefact.MassArtefact, (TextAndImageCell)gridRow.Cells[columnArtefactMass.Index]);
            }

            spreadSheetArtefactSpecies.Sort(columnArtefactSpecies, ListSortDirection.Ascending);
            labelArtefactSpecies.Visible = pictureBoxArtefactSpecies.Visible = total == 0;
            spreadSheetArtefactSpecies.UpdateStatus(total);
        }

        private void ShowSpeciesArtefacts(SpeciesFeatureArtefact artefact, TextAndImageCell gridCell)
        {
            switch (artefact.Criticality)
            {
                case ArtefactCriticality.Normal:
                    gridCell.Image = Mathematics.Properties.Resources.Check;
                    break;

                case ArtefactCriticality.Allowed:
                    gridCell.Image = Mathematics.Properties.Resources.CheckGray;
                    break;

                case ArtefactCriticality.NotCritical:
                    gridCell.Image = Mathematics.Properties.Resources.None;
                    break;

                case ArtefactCriticality.Critical:
                    gridCell.Image = Mathematics.Properties.Resources.NoneRed;
                    break;
            }

            if (artefact.UnmeasuredCount > 0)
                gridCell.Value = artefact.UnmeasuredCount;

            if (artefact.DeviationsCount > 0)
                gridCell.Value = artefact.DeviationsCount;
            
            if (artefact.Criticality != ArtefactCriticality.Normal)
                gridCell.ToolTipText = artefact.ToString();

            //if (artefact.HasRegression) // If sample is enough to build regression
            //{
            //    if (artefact.UnmeasuredCount == 0) // If there are no missing values
            //    {
            //        if (artefact.DeviationsCount == 0) // And there are no runouts at all
            //        {
            //            gridCell.Image = Mathematics.Properties.Resources.Check;
            //        }
            //        else // If there are runouts
            //        {
            //            gridCell.Value = artefact.DeviationsCount;
            //            gridCell.Image = Mathematics.Properties.Resources.None;
            //            gridCell.ToolTipText = artefact.ToString();
            //            gridCell.ToolTipText = string.Format(Resources.Artefact.ValueHasRunouts,
            //                Service.Localize(artefact.Column.Caption), artefact.DeviationsCount);
            //        }
            //    }
            //    else // If there are some missing values
            //    {
            //         Set value to cell
            //        gridCell.Value = artefact.UnmeasuredCount;

            //        if (artefact.DeviationsCount == 0) // If there are no runouts
            //        {
            //            gridCell.Image = Mathematics.Properties.Resources.CheckGray;
            //            gridCell.ToolTipText = artefact.ToString();
            //            gridCell.ToolTipText = string.Format(Resources.Artefact.ValueIsRecoverable,
            //                Service.Localize(artefact.Column.Caption), artefact.UnmeasuredCount);
            //        }
            //        else // Or there are runouts
            //        {
            //            gridCell.Value = artefact.DeviationsCount;
            //            gridCell.Image = Mathematics.Properties.Resources.NoneRed;
            //            gridCell.ToolTipText = artefact.ToString();
            //            gridCell.ToolTipText = string.Format(Resources.Artefact.ValueIsRecoverableButHasRunouts,
            //                Service.Localize(artefact.Column.Caption), artefact.UnmeasuredCount, artefact.DeviationsCount);
            //        }
            //    }
            //}
            //else // If sample is too small
            //{
            //    if (artefact.UnmeasuredCount == 0) // If there are no missing values
            //    {
            //        gridCell.Image = Mathematics.Properties.Resources.Check;
            //    }
            //    else // If there are some missing values
            //    {
            //        gridCell.Value = artefact.DeviationsCount;
            //        gridCell.Image = Mathematics.Properties.Resources.NoneRed;
            //        gridCell.ToolTipText = artefact.ToString();
            //        gridCell.ToolTipText = string.Format(Resources.Artefact.ValueIsCritical,
            //            artefact.UnmeasuredCount, Service.Localize(artefact.Column.Caption));
            //    }
            //}
        }

        private void ShowIndividualArtefacts(IndividualArtefact[] artefacts)
        {
            spreadSheetArtefactInd.Rows.Clear();
            int total = 0;

            foreach (IndividualArtefact artefact in artefacts)
            {
                total += artefact.GetFacts();

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetArtefactInd);
                gridRow.Cells[columnArtefactIndSpecies.Index].Value = artefact.IndividualRow.Species;
                spreadSheetArtefactInd.Rows.Add(gridRow);

                if (artefact.HasRegID && !artefact.Treated)
                {
                    gridRow.Cells[columnArtefactIndRegID.Index].Value = artefact.IndividualRow.RegID;
                    ((TextAndImageCell)gridRow.Cells[columnArtefactIndRegID.Index]).Image =
                        Mathematics.Properties.Resources.None;
                    gridRow.Cells[columnArtefactIndRegID.Index].ToolTipText = string.Format(
                        Resources.Artefact.IndividualRegID, artefact.IndividualRow.RegID);
                }
                else if (!artefact.HasRegID && artefact.Treated)
                {
                    gridRow.Cells[columnArtefactIndRegID.Index].Value = null;
                    ((TextAndImageCell)gridRow.Cells[columnArtefactIndRegID.Index]).Image =
                        Mathematics.Properties.Resources.CheckGray;
                    gridRow.Cells[columnArtefactIndRegID.Index].ToolTipText = Resources.Artefact.IndividualTreat;
                }

                if (artefact.UnweightedDietItems > 0)
                {
                    gridRow.Cells[columnArtefactIndDietUnw.Index].Value = artefact.UnweightedDietItems;

                    ((TextAndImageCell)gridRow.Cells[columnArtefactIndDietUnw.Index]).Image =
                        Mathematics.Properties.Resources.None;
                    gridRow.Cells[columnArtefactIndDietUnw.Index].ToolTipText = string.Format(
                        Resources.Artefact.IndividualUnweightedDiet, artefact.UnweightedDietItems);
                }
                else
                {
                    gridRow.Cells[columnArtefactIndDietUnw.Index].Value = null;
                    ((TextAndImageCell)gridRow.Cells[columnArtefactIndDietUnw.Index]).Image =
                        Mathematics.Properties.Resources.Check;
                }
            }

            spreadSheetArtefactInd.UpdateStatus(total);
            labelArtefactInd.Visible = pictureBoxArtefactInd.Visible = total == 0;
        }
    }
}
