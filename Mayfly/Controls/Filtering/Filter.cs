using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Controls
{
    public partial class Filter : Form
    {
        #region Properties

        SpreadSheet SpreadSheet { get; set; }

        public FilterUnit[] SetFilterUnits
        {
            get
            {
                List<FilterUnit> result = new List<FilterUnit>();

                foreach (FilterUnit filterUnit in flowFilters.Controls)
                {
                    if (filterUnit.IsFilterSet) result.Add(filterUnit);
                }

                return result.ToArray();
            }
        }

        private FilterUnitCluster[] Clusters
        {
            set;
            get;
        }

        public bool IsFilterSet
        {
            get
            {
                foreach (FilterUnit filterUnit in flowFilters.Controls)
                {
                    if (filterUnit.IsFilterSet) return true;
                }

                return false;
            }
        }

        public event EventHandler Filtered;

        private FilterUnit EmptyTerm
        {
            get
            {
                foreach (FilterUnit filterUnit in flowFilters.Controls)
                {
                    if (filterUnit.Filterable == null)
                    {
                        return filterUnit;
                    }
                }
                return null;
            }
        }

        private bool ContainsEmptyTerm
        {
            get
            {
                foreach (FilterUnit filterUnit in flowFilters.Controls)
                {
                    if (filterUnit.Filterable == null)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        #endregion

        public Filter(SpreadSheet adapter)
        {
            InitializeComponent();
            SpreadSheet = adapter;
        }

        public Filter(SpreadSheet adapter, DataGridViewColumn gridColumn,
            object value) : this(adapter)
        {
            AddFilter(gridColumn, value);
        }

        public Filter(SpreadSheet adapter, DataGridViewColumn gridColumn,
            object value, bool negative) : this(adapter)
        {
            AddFilter(gridColumn, value, negative);
        }

        public Filter(SpreadSheet adapter, DataGridViewColumn gridColumn,
            double min, double max) : this(adapter)
        {
            AddFilter(gridColumn, min, max);
        }

        #region Methods

        public FilterUnit AddFilter()
        {
            if (ContainsEmptyTerm)
            {
                EmptyTerm.Focus();
                return EmptyTerm;
            }
            else
            {
                FilterUnit filterUnit = new FilterUnit(SpreadSheet);
                filterUnit.ValueChanged += filterUnit_ValueChanged;
                flowFilters.Controls.Add(filterUnit);

                return filterUnit;
            }
        }

        public FilterUnit AddFilter(DataGridViewColumn gridColumn)
        {
            FilterUnit filterUnit = AddFilter();
            filterUnit.Filterable = gridColumn;
            return filterUnit;
        }

        public FilterUnit AddFilter(DataGridViewColumn gridColumn, object value)
        {
            return AddFilter(gridColumn, value, false);
        }

        public FilterUnit AddFilter(DataGridViewColumn gridColumn, object value, bool negative)
        {
            if (gridColumn.ValueType.IsDoubleConvertible())
            {
                return AddFilter(gridColumn, value.ToDouble(), value.ToDouble());
            }
            else
            {
                return AddFilter(gridColumn, value.ToString());
            }
        }

        public FilterUnit AddFilter(DataGridViewColumn gridColumn, string value)
        {
            return AddFilter(gridColumn, value, false);
        }

        public FilterUnit AddFilter(DataGridViewColumn gridColumn, string value, bool negative)
        {
            foreach (FilterUnit unit in SetFilterUnits)
            {
                if (unit.Filterable == gridColumn)
                {
                    if (unit.Category == value)
                        return unit;
                }
            }

            FilterUnit filterUnit = AddFilter(gridColumn);
            filterUnit.IsNegative = negative;

            if (!string.IsNullOrEmpty(value))
            {
                filterUnit.comboBoxCategory.SelectedItem = value;
            }

            return filterUnit;
        }

        public FilterUnit AddFilter(DataGridViewColumn gridColumn, double min, double max)
        {
            return AddFilter(gridColumn, min, max, false);
        }

        public FilterUnit AddFilter(DataGridViewColumn gridColumn, double min, double max, bool negative)
        {
            foreach (FilterUnit unit in SetFilterUnits)
            {
                if (unit.Filterable == gridColumn)
                {
                    if (unit.From <= min && unit.To >= max)
                        return unit;
                }
            }

            FilterUnit filterUnit = AddFilter(gridColumn);
            filterUnit.IsNegative = negative;

            filterUnit.textBoxFrom.Text = min.ToString();
            filterUnit.textBoxTo.Text = max.ToString();

            return filterUnit;
        }

        public void Apply()
        {
            if (IsFilterSet && !Filterator.IsBusy)
            {
                SpreadSheet.StartProcessing(
                    SpreadSheet.IsBackgroundTableInitiated ? SpreadSheet.Background.Rows.Count : SpreadSheet.RowCount,
                    Resources.Interface.Filtering);

                foreach (Control control in Controls)
                {
                    control.Enabled = false;
                }

                UpdateClusters();
                SpreadSheet.IsFiltering = true;
                Filterator.RunWorkerAsync();
            }
            else
            {
                Drop();
            }

            pictureBoxChanges.Visible = false;
        }

        public void ClearEmpties()
        {
            foreach (FilterUnit filterUnit in flowFilters.Controls)
            {
                if (filterUnit.Filterable == null)
                {
                    flowFilters.Controls.Remove(filterUnit);
                }
            }
        }

        public void UpdateClusters()
        {
            List<DataGridViewColumn> gridColumns = new List<DataGridViewColumn>();

            foreach (FilterUnit filterUnit in SetFilterUnits)
            {
                if (!gridColumns.Contains(filterUnit.Filterable))
                    gridColumns.Add(filterUnit.Filterable);
            }

            List<FilterUnitCluster> clusters = new List<FilterUnitCluster>();

            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                List<FilterUnit> units = new List<FilterUnit>();

                foreach (FilterUnit filterUnit in SetFilterUnits)
                {
                    if (filterUnit.Filterable == gridColumn)
                    {
                        units.Add(filterUnit);
                    }
                }

                FilterUnitCluster cluster = new FilterUnitCluster(units);
                clusters.Add(cluster);
            }

            Clusters = clusters.ToArray();
        }

        public void Drop()
        {
            Drop(true);
        }

        public void Drop(bool cancelFiltered)
        {
            while (flowFilters.Controls.Count > 0)
            {
                flowFilters.Controls.RemoveAt(0);
            }

            if (cancelFiltered)
            {
                if (SpreadSheet.IsBackgroundTableInitiated)
                {
                    SpreadSheet.Rows.Clear();
                    foreach (DataRow dataRow in SpreadSheet.Background.Rows)
                    {
                        SpreadSheet.Add(dataRow);
                    }
                }
                else
                {
                    foreach (DataGridViewRow gridRow in SpreadSheet.Rows)
                    {
                        SpreadSheet.SetVisible(gridRow);
                    }
                }

                SpreadSheet.filterButton.Image = Filtering.Filtering.Funnel;

                if (SpreadSheet != null && Filtered != null)
                {
                    Filtered.Invoke(this, new EventArgs());
                }
            }

            AddFilter();
        }

        public void SignChanges()
        {
            pictureBoxChanges.Visible = true;
        }

        delegate void VisibleChangedEventHandler(DataGridViewRow gridRow, bool visible);

        private void SetRowVisibility(DataGridViewRow gridRow, bool visible)
        {
            if (gridRow.IsNewRow) return;

            if (gridRow.DataGridView.InvokeRequired)
            {
                VisibleChangedEventHandler visibilitySetter = new VisibleChangedEventHandler(SetRowVisibility);
                gridRow.DataGridView.Invoke(visibilitySetter, new object[] { gridRow, visible });
            }
            else
            {
                gridRow.Visible = visible;
            }
        }

        public bool DoesRowFit(DataGridViewRow gridRow)
        {
            bool result = true;

            foreach (FilterUnitCluster cluster in Clusters)
            {
                result &= cluster.DoesRowFit(gridRow);
            }

            return result;
        }

        public bool DoesRowFit(DataRow gridRow)
        {
            bool result = true;

            foreach (FilterUnitCluster cluster in Clusters)
            {
                result &= cluster.DoesRowFit(gridRow);
            }

            return result;
        }

        #endregion

        #region Interface

        private void Filterator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (SpreadSheet.Display == null || SpreadSheet.Display.ProgressBar == null) return;

            SpreadSheet.Display.SetProgress(e.ProgressPercentage);
        }

        private void Filterator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (SpreadSheet.IsBackgroundTableInitiated)
            {
                SpreadSheet.Rows.Clear();

                List<DataRow> result = e.Result as List<DataRow>;

                foreach (DataRow gridRow in result)
                {
                    SpreadSheet.Add(gridRow);
                }
            }
            else
            {
                List<DataGridViewRow> result = e.Result as List<DataGridViewRow>;

                if (SpreadSheet.AllowUserToHideRows)
                {
                    foreach (DataGridViewRow gridRow in SpreadSheet.Rows)
                    {
                        if (gridRow.IsNewRow) continue;

                        if (result.Contains(gridRow)) SpreadSheet.SetVisible(gridRow);
                        else SpreadSheet.SetHidden(gridRow);
                    }
                }
                else
                {
                    foreach (DataGridViewRow gridRow in SpreadSheet.Rows)
                    {
                        if (gridRow.IsNewRow) continue;

                        gridRow.Visible = (result.Contains(gridRow));
                    }
                }
            }

            SpreadSheet.StopProcessing();
            SpreadSheet.IsFiltering = false;
            SpreadSheet.UpdateStatus();

            foreach (Control control in Controls)
            {
                control.Enabled = true;
            }

            if (Filtered != null)
            {
                SpreadSheet.filterButton.Image = Filtering.Filtering.FunnelPencil;
                Filtered.Invoke(this, new EventArgs());
            }
        }

        private void Filterator_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SpreadSheet.IsBackgroundTableInitiated)
            {
                List<DataRow> result = new List<DataRow>();

                for (int i = 0; i < SpreadSheet.Background.Rows.Count; i++)
                {
                    (sender as BackgroundWorker).ReportProgress(i + 1);

                    if (DoesRowFit(SpreadSheet.Background.Rows[i]))
                    {
                        result.Add(SpreadSheet.Background.Rows[i]);
                    }
                }

                e.Result = result;
            }
            else
            {
                List<DataGridViewRow> result = new List<DataGridViewRow>();

                for (int i = 0; i < SpreadSheet.Rows.Count; i++)
                {
                    (sender as BackgroundWorker).ReportProgress(i + 1);

                    if (DoesRowFit(SpreadSheet.Rows[i]))
                    {
                        result.Add(SpreadSheet.Rows[i]);
                    }
                }

                e.Result = result;
            }
        }

        private void Filter_FormClosed(object sender, FormClosedEventArgs e)
        {
            Drop(DialogResult == DialogResult.Abort);
        }

        private void filterUnit_ValueChanged(object sender, EventArgs e)
        {
            ClearEmpties();
            buttonFilter.Enabled = IsFilterSet;
        }

        private void flowFilters_ControlAdded(object sender, ControlEventArgs e)
        {
            int unitHeight = e.Control.Height + e.Control.Margin.Bottom + e.Control.Margin.Top;

            Location = new Point(Math.Max(0, Location.X), Math.Max(0, Location.Y - unitHeight));
            Height += unitHeight;

            buttonFilter.Enabled = IsFilterSet;
        }

        private void flowFilters_ControlRemoved(object sender, ControlEventArgs e)
        {
            int unitHeight = e.Control.Height + e.Control.Margin.Bottom + e.Control.Margin.Top;

            Location = new Point(Math.Max(0, Location.X), Math.Max(0, Location.Y + unitHeight));
            Height -= unitHeight;

            buttonFilter.Enabled = IsFilterSet;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearEmpties();
            AddFilter();
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            ClearEmpties();
            Apply();
        }

        private void flowFilters_SizeChanged(object sender, EventArgs e)
        {
            foreach (FilterUnit filterUnit in flowFilters.Controls)
            {
                filterUnit.Width = flowFilters.Width;
            }
        }

        private void buttonDrop_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        #endregion
    }

    public class FilterUnitCluster : List<FilterUnit>
    {
        DataGridViewColumn Filterable { set; get; }

        /// <summary>
        /// Checks is there at least one negative filter unit
        /// </summary>
        public bool HasNegative
        {
            get
            {
                bool result = false;

                foreach (FilterUnit sibUnit in this)
                {
                    result |= sibUnit.IsNegative;
                }

                return result;
            }
        }

        public FilterUnitCluster(IEnumerable<FilterUnit> units)
            : base(units)
        {
            Filterable = this[0].Filterable;
        }

        public bool DoesRowFit(DataGridViewRow gridRow)
        {
            bool fits = false;

            foreach (FilterUnit filterUnit in this)
            {
                // Check if Row fits current unit
                bool currentFit = filterUnit.DoesRowFit(gridRow);

                //if (HasNegative) // If there is at least one negative
                //{
                //    fits &= currentFit; // than apply 'AND' rule
                //}
                //else
                //{
                    fits |= currentFit; // otherwise apply 'OR' rule
                //}
            }

            return fits;
        }

        public bool DoesRowFit(DataRow gridRow)
        {
            bool fits = false;

            foreach (FilterUnit filterUnit in this)
            {
                fits |= filterUnit.DoesRowFit(gridRow);
            }

            return fits;
        }
    }
}
