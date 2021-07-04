using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Mayfly.Geographics;

namespace Mayfly.Bacterioplankton.Explorer
{
    partial class MainForm
    {
        private void UpdateCardTotals()
        {
            labelCardCount.UpdateStatus(spreadSheetCard.VisibleRowCount);
        }

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



        private void PrintCards(IList gridRows, CardReportLevel cardReport)
        {
            spreadSheetCard.EndEdit();

            Report report = new Report(Bacterioplankton.Resources.Interface.SampleNote);

            //foreach (DataGridViewRow gridRow in gridRows)
            //{
            //    if (gridRow.IsNewRow) continue;

            //    report.StartPage(Bacterioplankton.UserSettings.OddCardStart ? PageBreakOption.Odd : PageBreakOption.None);
            //    Data.CardRow cardRow = CardRow(gridRow);
            //    report.AddHeader(cardRow.Path == null ? String.Empty : System.IO.Path.GetFileNameWithoutExtension(cardRow.Path));
            //    data.Add(report, cardRow, cardReport);
            //    report.CloseDiv();
            //}

            report.EndBranded();
            report.Run();
        }



        private DataGridViewRow GetLine(Data.CardRow cardRow)
        {
            return columnCardID.GetRow(cardRow.ID, true, true);
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

            object Label = gridRow.Cells[columnCardLabel.Name].Value;
            if (Label == null) cardRow.SetLabelNull();
            else cardRow.Label = (string)Label;

            object Volume = (object)gridRow.Cells[columnCardVolume.Name].Value;
            if (Volume == null) cardRow.SetVolumeNull();
            else cardRow.Volume = (double)Volume;

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
            foreach (DataGridViewRow indGridRow in GetCardIndividualRows(cardRow))
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
            SetCardValue(cardRow, result, columnCardVolume, "Volume");
            SetCardValue(cardRow, result, columnCardDepth, "Depth");
            SetCardValue(cardRow, result, columnCardAbundance, "Abundance");
            SetCardValue(cardRow, result, columnCardBiomass, "Biomass");
            SetCardValue(cardRow, result, columnCardComments, "Comments");

            foreach (Data.FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
            {
                SetCardValue(cardRow, result, spreadSheetCard.GetColumn(factorValueRow.FactorRow.Factor));
            }

            return result;
        }
    }
}
