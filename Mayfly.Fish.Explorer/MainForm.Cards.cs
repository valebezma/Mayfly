using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Mayfly.Geographics;

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



        private void LoadCardLog()
        {
            IsBusy = true;
            spreadSheetCard.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.CardsProcessing);

            spreadSheetCard.Rows.Clear();

            loaderCard.RunWorkerAsync();
        }

        private CardStack GetStack(IList rows)
        {
            spreadSheetCard.EndEdit();
            CardStack stack = new CardStack();
            foreach (DataGridViewRow gridRow in rows) {
                if (gridRow.IsNewRow) continue;
                stack.Add(CardRow(gridRow));
            }

            return stack;
        }

        private DataGridViewRow GetLine(Data.CardRow cardRow)
        {
            DataGridViewRow result = new DataGridViewRow();
            result.CreateCells(spreadSheetCard);
            result.HeaderCell.Value = Math.Abs(cardRow.ID);
            UpdateCardRow(cardRow, result);
            return result;
        }



        private Data.CardRow CardRow(DataGridViewRow gridRow)
        {
            return CardRow(gridRow, columnCardID);
        }

        private Data.CardRow CardRow(DataGridViewRow gridRow, DataGridViewColumn gridColumn)
        {
            int id = (int)gridRow.Cells[gridColumn.Index].Value;
            return data.Card.FindByID(id);
        }

        private void SaveCardRow(DataGridViewRow gridRow)
        {
            Data.CardRow cardRow = CardRow(gridRow);

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

            object height = (object)gridRow.Cells[columnCardHeight.Name].Value;
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

            // Change info in IndGrid
            foreach (DataGridViewRow indGridRow in IndividualRows(cardRow))
            {
                SetCardValue(cardRow, indGridRow, spreadSheetInd.GetInsertedColumns());
            }

            RememberChanged(cardRow);
        }

        private void UpdateCardRow(Data.CardRow cardRow, DataGridViewRow result)
        {
            SetCardValue(cardRow, result, columnCardID, "ID");
            SetCardValue(cardRow, result, columnCardInvestigator, "Investigator");
            SetCardValue(cardRow, result, columnCardLabel, "Label");
            SetCardValue(cardRow, result, columnCardWater, "Water");
            SetCardValue(cardRow, result, columnCardWhen, "When");
            SetCardValue(cardRow, result, columnCardWhere, "Where");

            SetCardValue(cardRow, result, ColumnCardWeather, "Weather");
            SetCardValue(cardRow, result, ColumnCardTempSurface, "Surface");

            SetCardValue(cardRow, result, columnCardGear, "Gear");
            SetCardValue(cardRow, result, columnCardMesh, "Mesh");
            SetCardValue(cardRow, result, columnCardHook, "Hook");
            SetCardValue(cardRow, result, columnCardLength, "Length");
            SetCardValue(cardRow, result, columnCardOpening, "Opening");
            SetCardValue(cardRow, result, columnCardHeight, "Height");
            SetCardValue(cardRow, result, columnCardSquare, "Square");
            SetCardValue(cardRow, result, columnCardSpan, "Span");
            SetCardValue(cardRow, result, columnCardVelocity, "Velocity");
            SetCardValue(cardRow, result, columnCardExposure, "Exposure");

            //if (Wild.UserSettings.InstalledPermissions.IsPermitted(cardRow.Investigator))
            //{
                SetCardValue(cardRow, result, columnCardEffort, "Effort");
                SetCardValue(cardRow, result, columnCardDepth, "Depth");
                SetCardValue(cardRow, result, columnCardWealth, "Wealth");
                SetCardValue(cardRow, result, columnCardQuantity, "Quantity");
                SetCardValue(cardRow, result, columnCardMass, "Mass");
                SetCardValue(cardRow, result, columnCardAbundance, "Abundance");
                SetCardValue(cardRow, result, columnCardBiomass, "Biomass");
                SetCardValue(cardRow, result, columnCardDiversityA, "DiversityA");
                SetCardValue(cardRow, result, columnCardDiversityB, "DiversityB");
            //}

            SetCardValue(cardRow, result, columnCardComments, "Comments");

            foreach (Data.FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
            {
                SetCardValue(cardRow, result, spreadSheetCard.GetColumn(factorValueRow.FactorRow.Factor));
            }
        }
    }
}
