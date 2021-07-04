using Mayfly.Controls;
using Mayfly.Extensions;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Mathematics
{
    public partial class Pivot : Form
    {
        public MathAdapter Adapter { set; get; }

        public event EventHandler ParameterChanged;

        public List<DataGridViewColumn> RowGroupers
        {
            get;
            set;
        }

        public List<DataGridViewColumn> ColumnGroupers
        {
            get;
            set;
        }

        public DataGridViewColumn ValueColumn;

        public DataGridViewColumn AvgColumn;

        public DataGridViewColumn GnlColumn;

        public ColumnPicker Picker;

        public bool ShowGeneralized { get { return checkBoxGeneralized.Checked; } set { checkBoxGeneralized.Checked = value; } }

        public bool ShowAveraged { get { return checkBoxAveraged.Checked; } set { checkBoxAveraged.Checked = value; } }

        public string DisplayFormat
        {
            get { return comboBoxFormat.SelectedValue == null ? comboBoxFormat.Text : (string)comboBoxFormat.SelectedValue; }

            set { comboBoxFormat.Text = value; }
        }



        private Pivot()
        {
            InitializeComponent();

            listViewValue.Shine();
            sortableRowGroups.Shine();
            sortableColumnGroups.Shine();

            List<ComboBoxItem> items = new List<ComboBoxItem>();

            foreach (string format in new string[] { "E","G","Q","S" }) {
                ComboBoxItem item = new ComboBoxItem();

                item.Text = Resources.FormatNotice.ResourceManager.GetString(format);
                item.Value = format;

                items.Add(item);
            }

            comboBoxFormat.DataSource = items;
        }


        public Pivot(MathAdapter adapter) : this()
        {
            Adapter = adapter;
            adapter.Sheet.VisibleChanged += Sheet_VisibleChanged;

            Picker = new ColumnPicker(listViewValue);
            Picker.ColumnCollection = Adapter.Sheet.GetNumericalColumns();

            this.SnapToRight(Adapter.Sheet);
            Height = Adapter.Sheet.Height;

            foreach (DataGridViewColumn gridColumn in Adapter.Sheet.GetVisibleColumns())
            {
                if (!gridColumn.Visible) continue;

                ListViewItem itemR = new ListViewItem();
                itemR.Name = gridColumn.Name;
                itemR.Text = gridColumn.HeaderText;

                sortableRowGroups.Items.Add(itemR);

                ListViewItem itemC = new ListViewItem();
                itemC.Name = gridColumn.Name;
                itemC.Text = gridColumn.HeaderText;

                sortableColumnGroups.Items.Add(itemC);
            }


            Adapter.Sheet.FindForm().ResizeEnd += Sheet_BoundsChanged;
            Adapter.Sheet.FindForm().LocationChanged += Sheet_BoundsChanged;

            if (ParameterChanged != null)
            {
                ParameterChanged.Invoke(this, new EventArgs());
            }
        }

        private void Sheet_VisibleChanged(object sender, EventArgs e)
        {
            if (IsDisposed) return;
            
            if (((SpreadSheet)sender).Visible)
            {
                Show();
                BringToFront();
            }
            else
            {
                Hide();
            }
        }

        public void SetRowGroupers(List<DataGridViewColumn> value)
        {
            foreach (DataGridViewColumn gridColumn in value)
            {
                foreach (ListViewItem item in sortableRowGroups.Items)
                {
                    if (item.Name == gridColumn.Name)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }

        private List<DataGridViewColumn> GetRowGroupers()
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();
            foreach (ListViewItem item in sortableRowGroups.Items)
            {
                if (item.Selected)
                {
                    result.Add(Adapter.Sheet.GetColumn(((ListViewItem)item).Name));
                }
            }

            return result;
        }

        public void SetColumnGroupers(List<DataGridViewColumn> value)
        {
            foreach (DataGridViewColumn gridColumn in value)
            {
                foreach (ListViewItem item in sortableColumnGroups.Items)
                {
                    if (item.Name == gridColumn.Name)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }

        private List<DataGridViewColumn> GetColumnGroupers()
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();
            foreach (ListViewItem item in sortableColumnGroups.Items)
            {
                if (item.Selected)
                {
                    result.Add(Adapter.Sheet.GetColumn(((ListViewItem)item).Name));
                }
            }
            return result;
        }


        
        private void pivot_Changed(object sender, EventArgs e)
        {
            if (Picker.UserSelectedColumns.Count > 0)
                ValueColumn = (DataGridViewColumn)Picker.UserSelectedColumns[0];
            else ValueColumn = null;

            if (ParameterChanged != null)
            {
                ParameterChanged.Invoke(this, new EventArgs());
            }

            if (RowGroupers == null) return;
            if (RowGroupers.Count == 0) return;
            if (ColumnGroupers == null) return;
            if (ColumnGroupers.Count == 0) return;
            if (ValueColumn == null) return;

            if (Adapter.OperatingSheet == null)
            {
                Adapter.InitiateOperating(RowGroupers[0]);
            }
            else
            {
                Adapter.ClearOperating();

                if (Adapter.OperatingSheet.ColumnCount == 0)
                {
                    Adapter.OperatingSheet.InsertColumn(
                        RowGroupers[0].HeaderText, typeof(Sample),
                        string.Empty).Frozen = true;
                }

                Adapter.OperatingSheet.Columns[0].HeaderText =
                    RowGroupers[0].HeaderText;
            }

            //foreach (DataGridViewColumn colGrouper in ColumnGroupers)
            //{
            string[] columnGroups = ColumnGroupers[0].GetStrings(true).ToArray();

            foreach (string value in columnGroups)
            {
                Adapter.OperatingSheet.InsertColumn(value, typeof(Sample), DisplayFormat);
            }

            GnlColumn = Adapter.OperatingSheet.InsertColumn(Resources.Interface.ColGeneralized, typeof(Sample), DisplayFormat);
            GnlColumn.Visible = false;

            AvgColumn = Adapter.OperatingSheet.InsertColumn(Resources.Interface.ColAveraged, typeof(Sample), DisplayFormat);
            AvgColumn.Visible = false;

            string[] rowGroups = RowGroupers[0].GetStrings(true).ToArray();

            sortableRowGroups.Enabled = sortableColumnGroups.Enabled = listViewValue.Enabled = false;

            Adapter.OperatingSheet.StartProcessing(columnGroups.Length * rowGroups.Length, Resources.Interface.PivotProgress);
            if (!calculator.IsBusy) calculator.RunWorkerAsync(rowGroups);
        }

        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Adapter == null) return;

            foreach (DataGridViewColumn gridColumn in Adapter.OperatingSheet.GetInsertedColumns())
            {
                if (gridColumn.Frozen) continue;
                gridColumn.DefaultCellStyle.Format = DisplayFormat;
            }
        }

        private void comboBoxFormat_TextChanged(object sender, EventArgs e)
        {
            if (Adapter == null) return;

            foreach (DataGridViewColumn gridColumn in Adapter.OperatingSheet.GetInsertedColumns())
            {
                if (gridColumn.Frozen) continue;
                gridColumn.DefaultCellStyle.Format = DisplayFormat;
            }
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

                List<DataGridViewRow> valueRows = RowGroupers[0].GetRows(value);

                Sample generalized = new Sample();
                Sample averaged = new Sample();

                foreach (DataGridViewColumn gridColumn in Adapter.OperatingSheet.GetInsertedColumns())
                {
                    if (gridColumn.Index == 0) continue;

                    i++;
                    (sender as BackgroundWorker).ReportProgress(i);

                    Sample sample = ValueColumn.GetSample(ColumnGroupers[0].GetRows(gridColumn.Name, valueRows));

                    if (sample.Count > 0)
                    {
                        sample.Name = string.Format("{0} = {1}; {2} = {3}", RowGroupers[0].HeaderText, value, ColumnGroupers[0].HeaderText, gridColumn.Name);

                        SampleDisplay samdis = new SampleDisplay(sample);
                        gridRow.Cells[gridColumn.Index].ToolTipText = samdis.ToLongString();
                        gridRow.Cells[gridColumn.Index].Value = samdis;

                        generalized.Add(sample);
                        averaged.Add(sample.Mean);
                    }
                    else
                    {
                        gridRow.Cells[gridColumn.Index].ToolTipText = string.Empty;
                        gridRow.Cells[gridColumn.Index].Value = null;
                        averaged.Add(0);
                    }
                }

                gridRow.Cells[AvgColumn.Index].Value = averaged.Count > 0 ? new SampleDisplay(averaged) : null;
                gridRow.Cells[GnlColumn.Index].Value = generalized.Count > 0 ? new SampleDisplay(generalized) : null;

                result.Add(gridRow);
            }

            e.Result = result.ToArray();
        }

        private void calculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Adapter.OperatingSheet.Rows.Clear();
            Adapter.OperatingSheet.Rows.AddRange((DataGridViewRow[])e.Result);

            //Adapter.OperatingSheet.Columns[0].Width = DataGridView
            Adapter.OperatingSheet.Refresh();
            Adapter.OperatingSheet.StopProcessing();

            AvgColumn.Visible = ShowAveraged;
            GnlColumn.Visible = ShowGeneralized;

            sortableRowGroups.Enabled = sortableColumnGroups.Enabled = listViewValue.Enabled = true;
        }


        private void Pivot_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                this.SnapToRight(Adapter.Sheet);
            }
        }

        private void sortableRowGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            RowGroupers = GetRowGroupers();
            pivot_Changed(sender, e);
        }

        private void sortableColumnGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColumnGroupers = GetColumnGroupers();
            pivot_Changed(sender, e);
        }
    }
}
