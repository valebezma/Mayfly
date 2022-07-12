using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mayfly.Plankton.Explorer
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

            foreach (Wild.Survey.FactorRow factorRow in data.Factor)
            {
                DataGridViewColumn gridColumn = spreadSheetCard.InsertColumn(factorRow.Factor, factorRow.Factor, typeof(double), spreadSheetCard.ColumnCount - 1);
                gridColumn.Width = gridColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.ColumnHeader, true);
            }

            loaderCard.RunWorkerAsync();
        }

        private DataGridViewRow GetCardRow(Wild.Survey.CardRow cardRow)
        {
            return columnCardID.GetRow(cardRow.ID, true, true);
        }

        private Wild.Survey.CardRow GetCardRow(DataGridViewRow gridRow)
        {
            return GetCardRow(gridRow, columnCardID);
        }

        private Wild.Survey.CardRow GetCardRow(DataGridViewRow gridRow, DataGridViewColumn gridColumn)
        {
            int id = (int)gridRow.Cells[gridColumn.Index].Value;
            return data.Card.FindByID(id);
        }

        private DataGridViewRow GetLine(Wild.Survey.CardRow cardRow)
        {
            return columnCardID.GetRow(cardRow.ID, true, true);
        }

        private void SaveCardRow(DataGridViewRow gridRow)
        {
            Wild.Survey.CardRow cardRow = GetCardRow(gridRow);

            if (cardRow == null) return;

            object Label = gridRow.Cells[columnCardLabel.Name].Value;
            if (Label == null) cardRow.SetLabelNull();
            else cardRow.Label = (string)Label;

            object Mesh = (object)gridRow.Cells[columnCardMesh.Name].Value;
            if (Mesh == null) cardRow.SetMeshNull();
            else cardRow.Mesh = (int)(double)Mesh;

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
                Wild.Survey.FactorRow factorRow = data.Factor.FindByFactor(gridColumn.HeaderText);
                if (factorRow == null) continue;
                object factorValue = gridRow.Cells[gridColumn.Name].Value;

                if (factorValue == null)
                {
                    if (factorRow == null) continue;

                    Wild.Survey.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(cardRow.ID, factorRow.ID);

                    if (factorValueRow == null) continue;

                    factorValueRow.Delete();
                }
                else
                {
                    if (factorRow == null)
                    {
                        factorRow = data.Factor.AddFactorRow(gridColumn.HeaderText);
                    }

                    Wild.Survey.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(cardRow.ID, factorRow.ID);

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

        private DataGridViewRow UpdateCardRow(Wild.Survey.CardRow cardRow)
        {
            DataGridViewRow result = GetCardRow(cardRow);

            SetCardValue(cardRow, result, columnCardInvestigator, "Investigator");
            SetCardValue(cardRow, result, columnCardWater, "Water");
            SetCardValue(cardRow, result, columnCardLabel, "Label");
            SetCardValue(cardRow, result, columnCardWhen, "When");
            SetCardValue(cardRow, result, columnCardSampler, "Sampler");
            SetCardValue(cardRow, result, columnCardMesh, "Mesh");
            SetCardValue(cardRow, result, columnCardVolume, "Volume");
            SetCardValue(cardRow, result, columnCardDepth, "Depth");
            SetCardValue(cardRow, result, columnCardWealth, "Wealth");
            SetCardValue(cardRow, result, columnCardQuantity, "Quantity");
            SetCardValue(cardRow, result, columnCardAbundance, "Abundance");
            SetCardValue(cardRow, result, columnCardMass, "Mass");
            SetCardValue(cardRow, result, columnCardBiomass, "Biomass");
            SetCardValue(cardRow, result, columnCardDiversityA, "DiversityA");
            SetCardValue(cardRow, result, columnCardDiversityB, "DiversityB");
            SetCardValue(cardRow, result, columnCardComments, "Comments");

            foreach (Wild.Survey.FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
            {
                SetCardValue(cardRow, result, spreadSheetCard.GetColumn(factorValueRow.FactorRow.Factor));
            }

            return result;
        }

        private void cardLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            for (int i = 0; i < data.Card.Count; i++)
            {
                result.Add(UpdateCardRow(data.Card[i]));
                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void cardLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetCard.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetCard.StopProcessing();
            UpdateCardTotals();
        }

        private void PrintCards(IList gridRows, CardReportLevel cardReport)
        {
            spreadSheetCard.EndEdit();

            Report report = new Mayfly.Report(Plankton.Resources.Interface.SampleNote);

            foreach (DataGridViewRow gridRow in gridRows)
            {
                if (gridRow.IsNewRow) continue;

                report.StartPage(Plankton.UserSettings.OddCardStart ? PageBreakOption.Odd : PageBreakOption.None);
                Wild.Survey.CardRow cardRow = GetCardRow(gridRow);
                report.AddHeader(cardRow.Path == null ? String.Empty : System.IO.Path.GetFileNameWithoutExtension(cardRow.Path));
                //data.AddHTML(report, cardRow, cardReport);
                report.CloseDiv();
            }

            report.EndBranded();
            report.Run();
        }
    }
}
