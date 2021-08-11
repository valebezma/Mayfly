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
                setCardValue(cardRow, indGridRow, spreadSheetInd.GetInsertedColumns());
            }

            rememberChanged(cardRow);
        }

        private void UpdateCardRow(Data.CardRow cardRow, DataGridViewRow result)
        {
            setCardValue(cardRow, result, columnCardID, "ID");
            setCardValue(cardRow, result, columnCardInvestigator, "Investigator");
            setCardValue(cardRow, result, columnCardLabel, "Label");
            setCardValue(cardRow, result, columnCardWater, "Water");
            setCardValue(cardRow, result, columnCardWhen, "When");
            setCardValue(cardRow, result, columnCardWhere, "Where");

            setCardValue(cardRow, result, ColumnCardWeather, "Weather");
            setCardValue(cardRow, result, ColumnCardTempSurface, "Surface");

            setCardValue(cardRow, result, columnCardGear, "Gear");
            setCardValue(cardRow, result, columnCardMesh, "Mesh");
            setCardValue(cardRow, result, columnCardHook, "Hook");
            setCardValue(cardRow, result, columnCardLength, "Length");
            setCardValue(cardRow, result, columnCardOpening, "Opening");
            setCardValue(cardRow, result, columnCardHeight, "Height");
            setCardValue(cardRow, result, columnCardSquare, "Square");
            setCardValue(cardRow, result, columnCardSpan, "Span");
            setCardValue(cardRow, result, columnCardVelocity, "Velocity");
            setCardValue(cardRow, result, columnCardExposure, "Exposure");

            //if (Wild.UserSettings.InstalledPermissions.IsPermitted(cardRow.Investigator))
            //{
                setCardValue(cardRow, result, columnCardEffort, "Effort");
                setCardValue(cardRow, result, columnCardDepth, "Depth");
                setCardValue(cardRow, result, columnCardWealth, "Wealth");
                setCardValue(cardRow, result, columnCardQuantity, "Quantity");
                setCardValue(cardRow, result, columnCardMass, "Mass");
                setCardValue(cardRow, result, columnCardAbundance, "Abundance");
                setCardValue(cardRow, result, columnCardBiomass, "Biomass");
                setCardValue(cardRow, result, columnCardDiversityA, "DiversityA");
                setCardValue(cardRow, result, columnCardDiversityB, "DiversityB");
            //}

            setCardValue(cardRow, result, columnCardComments, "Comments");

            foreach (Data.FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
            {
                setCardValue(cardRow, result, spreadSheetCard.GetColumn(factorValueRow.FactorRow.Factor));
            }
        }
    }
}
