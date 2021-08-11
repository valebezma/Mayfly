using Mayfly.Extensions;
using Mayfly.Wild;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        public bool stratifiedShown;

        private Data.LogRow[] GetStratified(IList rows)
        {
            spreadSheetInd.EndEdit();
            List<Data.LogRow> result = new List<Data.LogRow>();

            foreach (DataGridViewRow gridRow in rows)
            {
                if (!gridRow.Visible) continue;
                if (gridRow.IsNewRow) continue;
                Data.LogRow logRow = LogRowStratified(gridRow);
                if (logRow == null) continue;

                result.Add(logRow);
            }

            return result.ToArray();
        }



        private void LoadStratifiedSamples()
        {
            IsBusy = true;
            spreadSheetStratified.StartProcessing(data.Stratified.Quantity, Wild.Resources.Interface.Process.StratifiedProcessing);
            spreadSheetStratified.Rows.Clear();
            loaderStratified.RunWorkerAsync();
        }

        private void ShowStratified()
        {
            if (!stratifiedShown)
            {
                stratifiedSample.Visible = true;
                spreadSheetStratified.Height -= (stratifiedSample.Height + stratifiedSample.Margin.Top);
                stratifiedShown = true;
            }
        }

        private void HideStratified()
        {
            if (stratifiedShown)
            {
                stratifiedSample.Visible = false;
                spreadSheetStratified.Height += (stratifiedSample.Height + stratifiedSample.Margin.Top);
                stratifiedShown = false;
                stratifiedSample.Reset();
            }
        }



        private DataGridViewRow StratifiedSampleRow(Data.LogRow logRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetStratified);

            result.Cells[columnStratifiedID.Index].Value = logRow.ID;
            result.Cells[columnStratifiedSpc.Index].Value = logRow.SpeciesRow.Species;
            result.Cells[columnStratifiedCount.Index].Value = logRow.QuantityStratified;
            result.Cells[columnStratifiedMass.Index].Value = logRow.MassStratified / 1000;
            result.Cells[columnStratifiedInterval.Index].Value = logRow.Interval;
            result.Cells[columnStratifiedFrom.Index].Value = logRow.MinStrate.LeftEndpoint;
            result.Cells[columnStratifiedTo.Index].Value = logRow.MaxStrate.LeftEndpoint;

            foreach (DataGridViewColumn gridColumn in spreadSheetStratified.GetInsertedColumns())
            {
                setCardValue(logRow.CardRow, result, gridColumn);
            }

            return result;
        }
    }
}
