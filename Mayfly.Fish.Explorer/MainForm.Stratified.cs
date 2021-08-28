using Mayfly.Wild;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        public bool stratifiedShown;



        private void loadStratifiedSamples(Data.LogRow[] logRows)
        {
            IsBusy = true;
            spreadSheetStratified.StartProcessing(data.Stratified.Quantity, Wild.Resources.Interface.Process.StratifiedProcessing);
            spreadSheetStratified.Rows.Clear();
            loaderStratified.RunWorkerAsync(logRows);
        }

        private void loadStratifiedSamples(CardStack stack)
        {
            loadStratifiedSamples(stack.GetLogRows());
        }

        private void loadStratifiedSamples()
        {
            loadStratifiedSamples(FullStack);
        }

        private void loadStratifiedSamples(Data.SpeciesRow[] spcRows, CardStack stack)
        {
            List<Data.LogRow> logRows = new List<Data.LogRow>();

            foreach (Data.SpeciesRow spcRow in spcRows)
            {
                logRows.AddRange(stack.GetLogRows(spcRow));
            }

            loadStratifiedSamples(logRows.ToArray());
        }

        private void loadStratifiedSamples(Data.SpeciesRow[] spcRows)
        {
            loadStratifiedSamples(spcRows, FullStack);
        }



        private Data.LogRow findLogRowStratified(DataGridViewRow gridRow)
        {
            return data.Log.FindByID((int)gridRow.Cells[columnStratifiedID.Index].Value);
        }



        private Data.LogRow[] getLogRowsStratified(IList rows)
        {
            spreadSheetStratified.EndEdit();
            List<Data.LogRow> result = new List<Data.LogRow>();

            foreach (DataGridViewRow gridRow in rows)
            {
                if (!gridRow.Visible) continue;
                if (gridRow.IsNewRow) continue;
                Data.LogRow logRow = findLogRowStratified(gridRow);
                if (logRow == null) continue;

                result.Add(logRow);
            }

            return result.ToArray();
        }



        private void showStratified()
        {
            if (!stratifiedShown)
            {
                stratifiedSample.Visible = true;
                spreadSheetStratified.Height -= (stratifiedSample.Height + stratifiedSample.Margin.Top);
                stratifiedShown = true;
            }
        }

        private void hideStratified()
        {
            if (stratifiedShown)
            {
                stratifiedSample.Visible = false;
                spreadSheetStratified.Height += (stratifiedSample.Height + stratifiedSample.Margin.Top);
                stratifiedShown = false;
                stratifiedSample.Reset();
            }
        }
    }
}
