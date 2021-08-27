using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Mayfly.Geographics;
using Mayfly.Controls;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        List<DataGridViewColumn> effortSource
        {
            get
            {
                return new List<DataGridViewColumn>
                {
                    columnCardExposure,
                    columnCardHeight,
                    columnCardHook,
                    columnCardLength,
                    columnCardOpening,
                    columnCardSquare,
                    columnCardSpan,
                    columnCardVelocity
                };
            }
        }



        private void loadCards()
        {
            IsBusy = true;
            spreadSheetCard.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.CardsProcessing);
            spreadSheetCard.Rows.Clear();

            loaderCard.RunWorkerAsync();
        }

        private Data.CardRow findCardRow(DataGridViewRow gridRow)
        {
            return data.Card.FindByID((int)gridRow.Cells[columnCardID.Index].Value);
        }

        private void updateCardRow(DataGridViewRow gridRow)
        {
            Data.CardRow cardRow = findCardRow(gridRow);

            if (cardRow == null) return;

            setCardValue(cardRow, gridRow, columnCardInvestigator, "Investigator");
            setCardValue(cardRow, gridRow, columnCardLabel, "Label");
            setCardValue(cardRow, gridRow, columnCardWater, "Water");
            setCardValue(cardRow, gridRow, columnCardWhen, "When");
            setCardValue(cardRow, gridRow, columnCardWhere, "Where");

            setCardValue(cardRow, gridRow, ColumnCardWeather, "Weather");
            setCardValue(cardRow, gridRow, ColumnCardTempSurface, "Surface");

            setCardValue(cardRow, gridRow, columnCardGear, "Gear");
            setCardValue(cardRow, gridRow, columnCardMesh, "Mesh");
            setCardValue(cardRow, gridRow, columnCardHook, "Hook");
            setCardValue(cardRow, gridRow, columnCardLength, "Length");
            setCardValue(cardRow, gridRow, columnCardOpening, "Opening");
            setCardValue(cardRow, gridRow, columnCardHeight, "Height");
            setCardValue(cardRow, gridRow, columnCardSquare, "Square");
            setCardValue(cardRow, gridRow, columnCardSpan, "Span");
            setCardValue(cardRow, gridRow, columnCardVelocity, "Velocity");
            setCardValue(cardRow, gridRow, columnCardExposure, "Exposure");

            //if (Wild.UserSettings.InstalledPermissions.IsPermitted(cardRow.Investigator))
            //{
            setCardValue(cardRow, gridRow, columnCardEffort, "Effort");
            setCardValue(cardRow, gridRow, columnCardDepth, "Depth");
            setCardValue(cardRow, gridRow, columnCardWealth, "Wealth");
            setCardValue(cardRow, gridRow, columnCardQuantity, "Quantity");
            setCardValue(cardRow, gridRow, columnCardMass, "Mass");
            setCardValue(cardRow, gridRow, columnCardAbundance, "Abundance");
            setCardValue(cardRow, gridRow, columnCardBiomass, "Biomass");
            setCardValue(cardRow, gridRow, columnCardDiversityA, "DiversityA");
            setCardValue(cardRow, gridRow, columnCardDiversityB, "DiversityB");
            //}

            setCardValue(cardRow, gridRow, columnCardComments, "Comments");

            foreach (Data.FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
            {
                setCardValue(cardRow, gridRow, spreadSheetCard.GetColumn(factorValueRow.FactorRow.Factor));
            }

            updateCardArtefacts(gridRow);
        }

        private void updateCardArtefacts(DataGridViewRow gridRow)
        {
            CardArtefact artefact = findCardRow(gridRow).GetFacts();

            if (artefact.EffortCriticality > ArtefactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnCardEffort.Index]).Image = Artefact.GetImage(artefact.EffortCriticality);
                gridRow.Cells[columnCardEffort.Index].ToolTipText = artefact.GetNotices(false).Merge();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnCardEffort.Index]).Image = null;
                gridRow.Cells[columnCardEffort.Index].ToolTipText = string.Empty;
            }


            if (artefact.LogArtefacts.Length > 0)
            {
                ((TextAndImageCell)gridRow.Cells[columnCardWealth.Index]).Image = Artefact.GetImage(Artefact.GetWorst(artefact.LogWorstCriticality));
                gridRow.Cells[columnCardWealth.Index].ToolTipText = LogArtefact.GetNotices(artefact.LogArtefacts).Merge();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnCardWealth.Index]).Image = null;
                gridRow.Cells[columnCardWealth.Index].ToolTipText = string.Empty;
            }

            if (artefact.IndividualArtefacts.Length > 0)
            {
                ((TextAndImageCell)gridRow.Cells[columnCardQuantity.Index]).Image = Artefact.GetImage(Artefact.GetWorst(artefact.IndividualWorstCriticality));
                gridRow.Cells[columnCardQuantity.Index].ToolTipText = IndividualArtefact.GetNotices(artefact.IndividualArtefacts).Merge();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnCardQuantity.Index]).Image = null;
                gridRow.Cells[columnCardQuantity.Index].ToolTipText = string.Empty;
            }
        }

        private void saveCardRow(DataGridViewRow gridRow)
        {
            Data.CardRow cardRow = findCardRow(gridRow);

            if (cardRow == null) return;

            object wpt = (object)gridRow.Cells[columnCardWhere.Name].Value;
            if (wpt == null) cardRow.SetWhereNull();
            else cardRow.Where = ((Waypoint)wpt).Protocol;

            object mesh = (object)gridRow.Cells[columnCardMesh.Name].Value;
            if (mesh == null) cardRow.SetMeshNull();
            else cardRow.Mesh = (int)mesh;

            object hook = (object)gridRow.Cells[columnCardHook.Name].Value;
            if (hook == null) cardRow.SetHookNull();
            else cardRow.Hook = (int)hook;

            object length = (object)gridRow.Cells[columnCardLength.Name].Value;
            if (length == null) cardRow.SetLengthNull();
            else cardRow.Length = (double)length;

            object opening = (object)gridRow.Cells[columnCardOpening.Name].Value;
            if (opening == null) cardRow.SetOpeningNull();
            else cardRow.Opening = (double)opening;

            object height = gridRow.Cells[columnCardHeight.Name].Value;
            if (height == null) cardRow.SetHeightNull();
            else cardRow.Height = (double)height;

            object square = (object)gridRow.Cells[columnCardSquare.Name].Value;
            if (square == null) cardRow.SetSquareNull();
            else cardRow.Square = (double)square;

            object time = (object)gridRow.Cells[columnCardSpan.Name].Value;
            if (time == null) cardRow.SetSpanNull();
            else cardRow.Span = (int)((TimeSpan)time).TotalMinutes;

            object velocity = (object)gridRow.Cells[columnCardVelocity.Name].Value;
            if (velocity == null) cardRow.SetVelocityNull();
            else cardRow.Velocity = (double)velocity;

            object exposure = (object)gridRow.Cells[columnCardExposure.Name].Value;
            if (exposure == null) cardRow.SetExposureNull();
            else cardRow.Exposure = (double)exposure;

            object depth = (object)gridRow.Cells[columnCardDepth.Name].Value;
            if (depth == null) cardRow.SetDepthNull();
            else cardRow.Depth = (double)depth;

            object comments = gridRow.Cells[columnCardComments.Name].Value;
            if (comments == null) cardRow.SetCommentsNull();
            else cardRow.Comments = (string)comments;

            // Additional factors
            foreach (DataGridViewColumn gridColumn in spreadSheetCard.GetInsertedColumns())
            {
                Data.FactorRow factorRow = data.Factor.FindByFactor(gridColumn.HeaderText);
                if (factorRow == null) continue;
                object factorValue = gridRow.Cells[gridColumn.Name].Value;

                if (factorValue == null)
                {
                    if (factorRow == null) continue;

                    Data.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(cardRow.ID, factorRow.ID);

                    if (factorValueRow == null) continue;

                    factorValueRow.Delete();
                }
                else
                {
                    if (factorRow == null)
                    {
                        factorRow = data.Factor.AddFactorRow(gridColumn.HeaderText);
                    }

                    Data.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(cardRow.ID, factorRow.ID);

                    if (factorValueRow == null)
                    {
                        data.FactorValue.AddFactorValueRow(cardRow, factorRow, (double)factorValue);
                    }
                    else
                    {
                        factorValueRow.Value = (double)factorValue;
                    }
                }
            }

            rememberChanged(cardRow);

            updateCardArtefacts(gridRow);

            // Update Individuals
            foreach (DataGridViewRow indGridRow in IndividualRows(cardRow))
            {
                setCardValue(cardRow, indGridRow, spreadSheetInd.GetInsertedColumns());
            }

            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                updateLogRow(spreadSheetLog.Find(columnLogID, logRow.ID));
            }
        }



        private CardStack GetStack(IList rows)
        {
            spreadSheetCard.EndEdit();
            CardStack stack = new CardStack();
            foreach (DataGridViewRow gridRow in rows) {
                if (gridRow.IsNewRow) continue;
                stack.Add(findCardRow(gridRow));
            }

            return stack;
        }
    }
}
