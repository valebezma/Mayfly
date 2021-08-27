using Mayfly.Extensions;
using Mayfly.Species;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using System.Windows.Forms;
using Mayfly.Wild;
using Mayfly.Controls;
using System;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        SpeciesKey.BaseRow baseLog;

        SpeciesKey.TaxaRow[] taxaLog;



        private void loadLog()
        {
            IsBusy = true;
            spreadSheetLog.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.SpeciesProcessing);
            spreadSheetLog.Rows.Clear();

            columnLogDiversityA.Visible = (baseSpc != null);
            columnLogDiversityB.Visible = (baseSpc != null);

            loaderLog.RunWorkerAsync();
        }

        private Data.LogRow findLogRow(DataGridViewRow gridRow)
        {
            return baseSpc == null ? data.Log.FindByID((int)gridRow.Cells[columnLogID.Index].Value) : null;
        }

        private void updateLogRow(DataGridViewRow gridRow)
        {
            Data.LogRow logRow = findLogRow(gridRow);

            gridRow.Cells[columnLogSpc.Index].Value = logRow.SpeciesRow;

            if (!logRow.IsQuantityNull())
            {
                gridRow.Cells[columnLogQuantity.Index].Value = logRow.Quantity;
                gridRow.Cells[columnLogAbundance.Index].Value = logRow.GetAbundance();
                gridRow.Cells[columnLogAbundance.Index].Style.Format = columnLogAbundance.ExtendFormat(logRow.CardRow.GetAbundanceUnits());
            }

            if (!logRow.IsMassNull())
            {
                gridRow.Cells[columnLogMass.Index].Value = logRow.Mass;
                gridRow.Cells[columnLogBiomass.Index].Value = logRow.GetBiomass();
                gridRow.Cells[columnLogBiomass.Index].Style.Format = columnLogBiomass.ExtendFormat(logRow.CardRow.GetBiomassUnits());
            }

            setCardValue(logRow.CardRow, gridRow, spreadSheetLog.GetInsertedColumns());

            updateLogArtefacts(gridRow);
        }

        private void updateLogArtefacts(DataGridViewRow gridRow)
        {
            LogArtefact artefact = findLogRow(gridRow).GetFacts();

            if (artefact.OddMassCriticality > ArtefactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnLogMass.Index]).Image = Artefact.GetImage(artefact.OddMassCriticality);
                gridRow.Cells[columnLogMass.Index].ToolTipText = artefact.GetNoticeOddMass();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnLogMass.Index]).Image = null;
                gridRow.Cells[columnLogMass.Index].ToolTipText = string.Empty;

            }

            if (artefact.UnmeasurementsCriticality > ArtefactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnLogQuantity.Index]).Image = Artefact.GetImage(artefact.UnmeasurementsCriticality);
                gridRow.Cells[columnLogQuantity.Index].ToolTipText = artefact.GetNoticeUnmeasured();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnLogQuantity.Index]).Image = null;
                gridRow.Cells[columnLogQuantity.Index].ToolTipText = string.Empty;
            }
        }

        private void saveLogRow(DataGridViewRow gridRow)
        {
            if (baseSpc != null) return;
            
            Data.LogRow logRow = findLogRow(gridRow);

            if (logRow == null) return;

            object qty = gridRow.Cells[columnLogQuantity.Name].Value;
            if (qty == null) logRow.SetQuantityNull();
            else logRow.Quantity = (int)qty;

            object mass = gridRow.Cells[columnLogMass.Name].Value;
            if (mass == null) logRow.SetMassNull();
            else logRow.Mass = (double)mass;

            rememberChanged(logRow.CardRow);

            updateLogRow(gridRow);

            updateQty(FullStack.Quantity());
            updateMass(FullStack.Mass());

            updateLogArtefacts(gridRow);
            updateCardArtefacts(spreadSheetCard.Find(columnCardID, logRow.CardID));
        }



        private Data.LogRow LogRowStratified(DataGridViewRow gridRow)
        {
            int ID = (int)gridRow.Cells[columnStratifiedID.Index].Value;
            return data.Log.FindByID(ID);
        }

        private DataGridViewRow GetLine(Data.CardRow cardRow, SpeciesKey.TaxaRow taxaRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetLog);

            result.Cells[columnLogID.Index].Value = cardRow.ID;

            int Q = 0;
            double W = 0.0;
            double A = 0.0;
            double B = 0.0;
            List<double> abundances = new List<double>();
            List<double> biomasses = new List<double>();

            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                if (double.IsNaN(cardRow.GetEffort())) continue;

                if (!taxaRow.Includes(logRow.SpeciesRow.Species)) continue;

                if (!logRow.IsQuantityNull())
                {
                    Q += logRow.Quantity;
                    A += logRow.GetAbundance();
                    abundances.Add(logRow.GetAbundance());
                }

                if (!logRow.IsMassNull())
                {
                    W += logRow.Mass;
                    B += logRow.GetBiomass();
                    biomasses.Add(logRow.GetBiomass());
                }
            }

            setCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

            result.Cells[columnLogSpc.Index].Value = taxaRow.TaxonName;

            result.Cells[columnLogQuantity.Index].Value = Q;
            result.Cells[columnLogMass.Index].Value = W;
            result.Cells[columnLogAbundance.Index].Value = A;
            result.Cells[columnLogBiomass.Index].Value = B;
            result.Cells[columnLogDiversityA.Index].Value = new Sample(abundances).Diversity();
            result.Cells[columnLogDiversityB.Index].Value = new Sample(biomasses).Diversity();

            return result;
        }
    }
}
