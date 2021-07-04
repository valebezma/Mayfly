using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Software
{
    public partial class ActionLogs : Form
    {
        public ActionLogs()
        {
            InitializeComponent();

            ColumnWhen.ValueType = typeof(DateTime);
            ColumnEvent.ValueType = typeof(string);

            dateTimePickerFrom.MaxDate =
                dateTimePickerTo.MaxDate = DateTime.Today;

            dateTimePickerFrom.MinDate =
                dateTimePickerTo.MinDate = DateTime.Today.AddDays(-UserSettings.LogSpan);

            dateTimePickerFrom.Value = DateTime.Today.AddDays(-UserSettings.LogSpan);
            dateTimePickerTo.Value = DateTime.Today;
            comboBoxEventType.SelectedIndex = 0;

            statusUser.ResetFormatted(UserSettings.Username);
            statusPC.ResetFormatted(Environment.MachineName);
        }

        private void term_ValueChanged(object sender, EventArgs e)
        {
            if (!this.Visible) return;

            spreadSheetEvents.Rows.Clear();
            Cursor = Cursors.WaitCursor;

            if (backLoader.IsBusy)
            {
                backLoader.CancelAsync();
            }

            backLoader.RunWorkerAsync(new UserActionEventArgsSearchTerms(
                dateTimePickerFrom.Value, dateTimePickerTo.Value.AddHours(24),
                (EventType)comboBoxEventType.SelectedIndex));
        }

        private void backLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Log.GetEventsLog((UserActionEventArgsSearchTerms)e.Argument);
        }

        private void backLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                UserActionEventArgs[] log = (UserActionEventArgs[])e.Result;

                foreach (UserActionEventArgs logevent in log)
                {
                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.CreateCells(spreadSheetEvents, logevent.EventTime, logevent.Feature, logevent.EventDescription);
                    spreadSheetEvents.Rows.Add(gridRow);
                }
                statusEvents.ResetFormatted(log.Length);
            }

            Cursor = Cursors.Default;
        }

        private void ActionLogs_Load(object sender, EventArgs e)
        {
            term_ValueChanged(this, new EventArgs());
        }
    }
}
