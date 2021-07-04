using Mayfly.Extensions;
using Mayfly.Species;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using System.Windows.Forms;
using Mayfly.Wild;

namespace Mayfly.Bacterioplankton.Explorer
{
    public partial class MainForm
    {
        private void UpdateLogTotals()
        {
            labelLogCount.UpdateStatus(spreadSheetLog.VisibleRowCount);
        }

        private void LoadLog()
        {
            IsBusy = true;
            spreadSheetLog.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.SpeciesProcessing);
            spreadSheetLog.Rows.Clear();

            loaderLog.RunWorkerAsync();
        }

        

        private DataGridViewRow GetLine(Data.CardRow cardRow, Data.SpeciesRow speciesRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetLog);

            result.Cells[columnLogSpc.Index].Value = speciesRow.Species;
            result.Cells[columnLogID.Index].Value = cardRow.ID;

            Data.LogRow logRow = data.Log.FindByCardIDSpcID(cardRow.ID, speciesRow.ID);

            if (logRow == null)
            {
                result.Cells[columnLogAbundance.Index].Value = 0D;
                result.Cells[columnLogBiomass.Index].Value = 0D;

                result.Cells[columnLogAbundance.Index].Style.Format =
                result.Cells[columnLogBiomass.Index].Style.Format = string.Empty;
            }
            else
            {
                if (!logRow.IsQuantityNull()) {
                    result.Cells[columnLogAbundance.Index].Value = logRow.GetAbundance();
                }

                result.Cells[columnLogBiomass.Index].Value = logRow.GetBiomass();

            }

            SetCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

            return result;
        }
        
        private Data.LogRow LogRow(DataGridViewRow gridRow)
        {
            int id = (int)gridRow.Cells[columnLogID.Index].Value;

            string species = (string)gridRow.Cells[columnLogSpc.Index].Value;
            return data.Log.FindByCardIDSpcID(id, data.Species.FindBySpecies(species).ID);
        }
    }
}
