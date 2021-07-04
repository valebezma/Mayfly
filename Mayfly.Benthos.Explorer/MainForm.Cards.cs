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
    partial class MainForm
    {
        private void LoadCardLog()
        {
            IsBusy = true;
            spreadSheetCard.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.CardsProcessing);

            spreadSheetCard.Rows.Clear();

            foreach (Data.FactorRow factorRow in data.Factor)
            {
                DataGridViewColumn gridColumn = spreadSheetCard.InsertColumn(factorRow.Factor, factorRow.Factor, typeof(double), spreadSheetCard.ColumnCount - 1);
                gridColumn.Width = gridColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.ColumnHeader, true);
            }

            loaderCard.RunWorkerAsync();
        }

        private CardStack GetStack(IList rows)
        {
            spreadSheetCard.EndEdit();
            CardStack stack = new CardStack();
            foreach (DataGridViewRow gridRow in rows)
            {
                if (gridRow.IsNewRow) continue;
                stack.Add(CardRow(gridRow));
            }

            return stack;
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

        private DataGridViewRow GetLine(Data.CardRow cardRow)
        {
            return columnCardID.GetRow(cardRow.ID, true, true);
        }

        private void SaveCardRow(DataGridViewRow gridRow)
        {
            Data.CardRow cardRow = CardRow(gridRow);

            if (cardRow == null) return;

            object Label = gridRow.Cells[columnCardLabel.Name].Value;
            if (Label == null) cardRow.SetLabelNull();
            else cardRow.Label = (string)Label;

            object Mesh = (object)gridRow.Cells[columnCardMesh.Name].Value;
            if (Mesh == null) cardRow.SetMeshNull();
            else cardRow.Mesh = (int)(double)Mesh;

            object Square = (object)gridRow.Cells[columnCardSquare.Name].Value;
            if (Square == null) cardRow.SetSquareNull();
            else cardRow.Square = (double)Square;

            object Depth = (object)gridRow.Cells[columnCardDepth.Name].Value;
            if (Depth == null) cardRow.SetDepthNull();
            else cardRow.Depth = (double)Depth;

            object Comments = gridRow.Cells[columnCardComments.Name].Value;
            if (Comments == null) cardRow.SetCommentsNull();
            else cardRow.Comments = (string)Comments;

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

        private DataGridViewRow UpdateCardRow(Data.CardRow cardRow)
        {
            DataGridViewRow result = GetLine(cardRow);

            SetCardValue(cardRow, result, columnCardInvestigator, "Investigator");
            SetCardValue(cardRow, result, columnCardWater, "Water");
            SetCardValue(cardRow, result, columnCardLabel, "Label");
            SetCardValue(cardRow, result, columnCardWhen, "When");
            SetCardValue(cardRow, result, columnCardWhere, "Where");
            SetCardValue(cardRow, result, columnCardSampler, "Sampler");
            SetCardValue(cardRow, result, columnCardSubstrate, "Substrate");
            SetCardValue(cardRow, result, columnCardMesh, "Mesh");
            SetCardValue(cardRow, result, columnCardSquare, "Square");
            SetCardValue(cardRow, result, columnCardDepth, "Depth");
            SetCardValue(cardRow, result, ColumnCardCrossSection, "CrossSection");
            SetCardValue(cardRow, result, ColumnCardBank, "Bank");
            SetCardValue(cardRow, result, columnCardWealth, "Wealth");
            SetCardValue(cardRow, result, columnCardQuantity, "Quantity");
            SetCardValue(cardRow, result, columnCardAbundance, "Abundance");
            SetCardValue(cardRow, result, columnCardMass, "Mass");
            SetCardValue(cardRow, result, columnCardBiomass, "Biomass");
            SetCardValue(cardRow, result, columnCardDiversityA, "DiversityA");
            SetCardValue(cardRow, result, columnCardDiversityB, "DiversityB");
            SetCardValue(cardRow, result, columnCardComments, "Comments");

            foreach (Data.FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
            {
                SetCardValue(cardRow, result, spreadSheetCard.GetColumn(factorValueRow.FactorRow.Factor));
            }

            return result;
        }
    }
}
