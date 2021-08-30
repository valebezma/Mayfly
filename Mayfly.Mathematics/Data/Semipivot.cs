using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;

namespace Mayfly.Mathematics
{
    public partial class Semipivot : Form
    {
        public MathAdapter Adapter { set; get; }

        public event EventHandler ParameterChanged;

        public DataGridViewColumn RowGrouper;

        public ColumnPicker Picker;



        public Semipivot(MathAdapter adapter)
        {
            InitializeComponent();
            listViewColumn.Shine();

            Adapter = adapter;


            Picker = new ColumnPicker(listViewColumn);
            Picker.ColumnCollection = Adapter.Sheet.GetNumericalColumns();
            Picker.SelectAll();
            this.SnapToLeft(Adapter.Sheet);
            Height = Adapter.Sheet.Height;
            comboBoxRow.DataSource = Adapter.Sheet.GetCategorialColumns();

            Adapter.Sheet.FindForm().ResizeEnd += Sheet_BoundsChanged;
            Adapter.Sheet.FindForm().LocationChanged += Sheet_BoundsChanged;

            if (ParameterChanged != null)
            {
                ParameterChanged.Invoke(this, new EventArgs());
            }
        }



        private void group_Changed(object sender, EventArgs e)
        {
            RowGrouper = (DataGridViewColumn)comboBoxRow.SelectedItem;

            if (ParameterChanged != null)
            {
                ParameterChanged.Invoke(this, new EventArgs());
            }

            if (RowGrouper == null) return;

            if (Adapter.OperatingSheet == null)
            {
                Adapter.InitiateOperating(RowGrouper);
            }
            else
            {
                Adapter.ClearOperating();
                Adapter.OperatingSheet.Columns[0].HeaderText =
                    RowGrouper.HeaderText;
            }

            foreach (DataGridViewColumn gridColumn in Picker.SelectedColumns)
            {
                Adapter.OperatingSheet.InsertColumn(
                    gridColumn.HeaderText, typeof(Sample), "S").Frozen = true;
            }

            string[] rowGroups = RowGrouper.GetStrings(true).ToArray();

            comboBoxRow.Enabled = listViewColumn.Enabled = false;

            Adapter.OperatingSheet.StartProcessing(rowGroups.Length, Resources.Interface.SemipivotProgress);
            if (!calculator.IsBusy) calculator.RunWorkerAsync(rowGroups);
        }

        private void calculator_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            int i = 0;

            foreach (string value in (string[])e.Argument)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(Adapter.OperatingSheet);
                gridRow.Cells[0].Value = value;
                gridRow.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //gridRow.MinimumHeight = 35;

                List<DataGridViewRow> valueRows = RowGrouper.GetRows(value, false);

                foreach (DataGridViewColumn gridColumn in Adapter.OperatingSheet.GetInsertedColumns())
                {
                    if (gridColumn.Index == 0) continue;

                    Sample sample = Adapter.Sheet.GetColumn(gridColumn.Name).GetSample(valueRows);
                    SampleDisplay samdis = new SampleDisplay(sample);

                    if (sample.Count > 0)
                    {
                        sample.Name = string.Format("{0} = {1}", RowGrouper.HeaderText, value);
                        gridRow.Cells[gridColumn.Index].ToolTipText = samdis.ToLongString();
                        gridRow.Cells[gridColumn.Index].Value = samdis;
                    }
                    else
                    {
                        gridRow.Cells[gridColumn.Index].ToolTipText = string.Empty;
                        gridRow.Cells[gridColumn.Index].Value = null;
                    }
                }

                i++;
                (sender as BackgroundWorker).ReportProgress(i);

                result.Add(gridRow);
            }

            e.Result = result.ToArray();
        }

        private void calculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                Adapter.OperatingSheet.Rows.Clear();
                Adapter.OperatingSheet.Rows.AddRange((DataGridViewRow[])e.Result);
                Adapter.OperatingSheet.Refresh();
                Adapter.OperatingSheet.StopProcessing();

                comboBoxRow.Enabled = listViewColumn.Enabled = true;
            }
        }

        private void Semipivot_FormClosing(object sender, FormClosingEventArgs e)
        {
            calculator.CancelAsync();

            if (Adapter.OperatingSheet != null)
            {
                Adapter.OperatingSheet.StopProcessing();
                Adapter.OperatingSheet.Dispose();
                Adapter.OperatingSheet = null;
                //Adapter.DisplayToolBox.Close();
            }
        }

        private void Sheet_BoundsChanged(object sender, EventArgs e)
        {
            if (Adapter.Sheet != null)
            {
                this.SnapToLeft(Adapter.Sheet);
            }
        }
    }
}