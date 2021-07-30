using Mayfly.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using Mayfly.Geographics;

namespace Mayfly.Controls
{
    public partial class SpreadSheet : DataGridView, ISupportInitialize
    {
        private ContextMenuStrip contextCell;
        private ToolStripMenuItem itemFilterBy;
        private ToolStripMenuItem itemClear;
        private ToolStripMenuItem itemCopy;
        private ToolStripMenuItem itemPaste;
        private IContainer components;
        private ToolStripMenuItem itemSelectAll;
        private ToolStripMenuItem itemInverse;
        private ToolStripMenuItem itemHide;
        private ToolStripMenuItem itemShowHidden;
        private ContextMenuStrip contextRow;
        private ToolStripMenuItem itemShowAll;
        public ToolStripMenuItem ItemReport;
        public ToolStripMenuItem ItemCopyTable;
        private ContextMenuStrip contextTable;
        DataGridViewCellStyle alternatingRowsDefaultCellStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle columnHeadersDefaultCellStyle = new DataGridViewCellStyle();
        private CheckBox checkBoxHider;
        private ContextMenuStrip contextColumn;
        private ToolStripMenuItem itemColumnProperties;
        private ToolStripMenuItem itemRename;
        private ToolStripMenuItem itemDelete;
        private ToolStripMenuItem itemSetValue;
        private TaskDialogs.InputDialog inputDialog;
        DataGridViewCellStyle rowHeadersDefaultCellStyle = new DataGridViewCellStyle();
        internal bool IsBackgroundTableInitiated = false;
        public ToolStripMenuItem ItemSave;
        internal DataTable Background;

        #region Inherited properties

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellStyle AlternatingRowsDefaultCellStyle { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellStyle ColumnHeadersDefaultCellStyle { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellStyle DefaultCellStyle { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellStyle RowHeadersDefaultCellStyle { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellStyle RowsDefaultCellStyle { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AllowDrop { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AllowUserToOrderColumns { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AllowUserToResizeRows { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackgroundColor { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellBorderStyle CellBorderStyle { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewClipboardCopyMode ClipboardCopyMode { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewHeaderBorderStyle RowHeadersBorderStyle { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int ColumnHeadersHeight { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ColumnHeadersVisible { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Windows.Forms.Cursor Cursor { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool EnableHeadersVisualStyles { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color GridColor { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool RightToLeft { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ShowCellErrors { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ShowCellToolTips { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ShowEditingIcon { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ShowRowErrors { get; set; }

        [DefaultValue(false)]
        public new bool AllowUserToAddRows
        {
            get { return base.AllowUserToAddRows; }
            set { base.AllowUserToAddRows = value; }
        }

        [DefaultValue(false)]
        public new bool AllowUserToDeleteRows
        {
            get { return base.AllowUserToDeleteRows; }
            set { base.AllowUserToDeleteRows = value; }
        }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseWaitCursor { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ContextMenuStrip ContextMenuStrip { get; set; }

        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                if (filterButton != null) filterButton.Enabled = value;
                if (sheetButton != null) sheetButton.Enabled = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int InsertedColumnCount
        {
            get
            {
                return GetInsertedColumns().Count;
            }
        }

        #endregion

        #region Overriden handlers

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (CurrentCell != null && CurrentCell.OwningColumn.ReadOnly)
            {
                if (InputFailed != null)
                {
                    InputFailed.Invoke(this, new DataGridViewCellEventArgs(CurrentCell.ColumnIndex, CurrentCell.RowIndex));
                }
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (IsCurrentCellInEditMode)
            {
                return;
            }

            switch (e.KeyData)
            {
                case Keys.Back:
                    foreach (DataGridViewCell selectedCell in SelectedCells)
                    {
                        ClearCellValue(selectedCell);
                    }

                    if (AutoClearEmptyRows)
                    {
                        ClearEmptyRows();
                    }
                    break;
                case Keys.Delete:
                    if (SelectedRows.Count > 0) return;
                    foreach (DataGridViewCell selectedCell in SelectedCells)
                    {
                        ClearCellValue(selectedCell);
                    }

                    if (AutoClearEmptyRows)
                    {
                        ClearEmptyRows();
                    }
                    break;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Button == MouseButtons.Right)
            {
                if (CurrentCell != null && CurrentCell.IsInEditMode)
                {
                    EndEdit();
                }

                DataGridView.HitTestInfo hitTestInfo = HitTest(e.X, e.Y);

                switch (hitTestInfo.Type)
                {
                    case DataGridViewHitTestType.ColumnHeader:
                        // TODO: If not sortable and Selection Mode == ColumnSelection
                        // then do selection operations

                        ClickedColumn = Columns[hitTestInfo.ColumnIndex];

                        if (ColumnMenu == null)
                        {
                            contextColumn.Show(PointToScreen(e.Location), ToolStripDropDownDirection.BelowRight);
                        }
                        else
                        {
                            ColumnMenu.Show(PointToScreen(e.Location), ToolStripDropDownDirection.BelowRight);
                        }
                        break;

                    case DataGridViewHitTestType.RowHeader:
                        if (SelectionMode == DataGridViewSelectionMode.FullRowSelect ||
                            SelectionMode == DataGridViewSelectionMode.RowHeaderSelect ||
                            SelectionMode == DataGridViewSelectionMode.CellSelect)
                        {
                            if (!Rows[hitTestInfo.RowIndex].Selected)
                            {
                                if (!ModifierKeys.HasFlag(Keys.Control))
                                {
                                    foreach (DataGridViewRow gridRow in Rows)
                                    {
                                        gridRow.Selected = false;
                                    }
                                }

                                Rows[hitTestInfo.RowIndex].Selected = true;
                            }

                            if (!Rows[hitTestInfo.RowIndex].IsNewRow)
                            {
                                if (RowMenu == null)
                                {
                                    if (IsLog) contextRow.Show(PointToScreen(e.Location));
                                }
                                else
                                {
                                    RowMenu.Show(PointToScreen(e.Location));
                                }
                            }
                        }

                        break;

                    case DataGridViewHitTestType.Cell:

                        switch (SelectionMode)
                        {
                            case DataGridViewSelectionMode.FullRowSelect:

                                if (!Rows[hitTestInfo.RowIndex].Selected)
                                {
                                    if (!ModifierKeys.HasFlag(Keys.Control))
                                    {
                                        ClearSelection();
                                    }

                                    Rows[hitTestInfo.RowIndex].Selected = true;
                                }

                                if (!Rows[hitTestInfo.RowIndex].IsNewRow)
                                {
                                    if (RowMenu == null)
                                    {
                                        if (IsLog) contextRow.Show(PointToScreen(e.Location));
                                    }
                                    else
                                    {
                                        RowMenu.Show(PointToScreen(e.Location));
                                    }
                                }

                                break;

                            default:

                                if (!this[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex].Selected)
                                {
                                    if (!ModifierKeys.HasFlag(Keys.Control))
                                    {
                                        ClearSelection();
                                    }

                                    this.CurrentCell = this[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];
                                    this[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex].Selected = true;
                                }

                                if (CellMenu == null)
                                {
                                    contextCell.Show(this, e.X, e.Y);
                                }
                                else
                                {
                                    CellMenu.Show(this, e.X, e.Y);
                                }

                                break;
                        }

                        break;
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hitTestInfo = HitTest(e.X, e.Y);

                switch (hitTestInfo.Type)
                {
                    case DataGridViewHitTestType.ColumnHeader:
                        if (AllowColumnRenaming && !Columns[hitTestInfo.ColumnIndex].ReadOnly)
                        {
                            SetRenaming(Columns[hitTestInfo.ColumnIndex]);
                        }
                        break;

                    case DataGridViewHitTestType.Cell:
                        if (CellMenuLaunchableItemIndex != -1 && CellMenu.Items[CellMenuLaunchableItemIndex].Enabled)
                        {
                            CellMenu.Items[CellMenuLaunchableItemIndex].PerformClick();
                        }
                        break;

                    case DataGridViewHitTestType.RowHeader:
                        if (RowMenuLaunchableItemIndex != -1 && RowMenu.Items[RowMenuLaunchableItemIndex].Enabled)
                        {
                            RowMenu.Items[RowMenuLaunchableItemIndex].PerformClick();
                        }
                        break;
                }
            }
        }

        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            base.OnEditingControlShowing(e);

            if (e.Control is TextBox) 
            {
                TextBox editBox = e.Control as TextBox;
                editBox.KeyPress += editingControl_KeyPress;

                if (AllowStringSuggection != AutoCompleteMode.None &&
                    CurrentCell.OwningColumn.ValueType == typeof(string))
                {
                    editBox.AutoCompleteMode = AllowStringSuggection;
                    editBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection variants = new AutoCompleteStringCollection();

                    if (StringVariants == null)
                    {
                        variants.AddRange(CurrentCell.OwningColumn.GetStrings(true, false).ToArray());
                    }
                    else
                    {
                        variants.AddRange(StringVariants);
                    }

                    editBox.AutoCompleteCustomSource = variants;
                }
                else
                {
                    editBox.AutoCompleteMode = AutoCompleteMode.None;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ResetButtonPositions();
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            ResetButtonPositions();
        }

        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            base.OnCellEndEdit(e);

            if (AutoClearEmptyRows)
            {
                ClearEmptyRows();
            }

            //if (Columns[e.ColumnIndex].ValueType == typeof(DateTime))
            //{
            //    CurrentCell.Value = dtpAction.Value;
            //    dtpAction.Visible = false;
            //}
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (allowUserToHideRows && e.KeyCode == RowVisibilityKey)
            {
                RowVisibilityColumn.Visible = true;

                checkBoxHider.Visible = true;
                Rectangle rect = this.GetColumnDisplayRectangle(RowVisibilityColumn.Index, true);
                checkBoxHider.Location = new Point(rect.Location.X + rect.Width / 2 - checkBoxHider.Width / 2, 
                    rect.Location.Y + base.ColumnHeadersHeight /2 - checkBoxHider.Height / 2);   
                checkBoxHider.BringToFront();

                foreach (DataGridViewRow gridRow in Rows)
                {
                    if (gridRow.IsNewRow) continue;

                    gridRow.Visible = true;
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (allowUserToHideRows && e.KeyCode == RowVisibilityKey)
            {
                OffRowSelection();
            }
        }

        protected override void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            base.OnCellContentClick(e);

            if (e.ColumnIndex == -1) return;

            if (e.RowIndex == -1) return;

            if (AllowUserToHideRows)
            {
                if (e.ColumnIndex != RowVisibilityColumn.Index) return;

                CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (UserChangedRowVisibility != null)
                {
                    UserChangedRowVisibility.Invoke(this, new DataGridViewRowEventArgs(Rows[e.RowIndex]));
                }

                UpdateHiderCheck();
            }
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);

            UpdateButtonAvailablity();

            if (AllowUserToHideRows)
            {
                for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
                {
                    if (this[RowVisibilityColumn.Index, index].Value == null)
                        this[RowVisibilityColumn.Index, index].Value = true;
                }

                UpdateHiderCheck();
            }
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            if (Columns.Contains(RowVisibilityColumn) && e.Column.Frozen)
            {
                RowVisibilityColumn.Frozen = true;
            }

            base.OnColumnAdded(e);
        }

        protected override void OnColumnNameChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnNameChanged(e);

            e.Column.RestoreFormat();
        }

        protected override void OnColumnRemoved(DataGridViewColumnEventArgs e)
        {
            insertedColumns.Remove(e.Column);

            base.OnColumnRemoved(e);
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            base.OnRowsRemoved(e);

            if (RowCount == 0 && Filter != null)
            {
                Filter.Drop(false);
            }

            if (AllowUserToHideRows)
            {
                UpdateHiderCheck();
            }

            UpdateButtonAvailablity();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (IsLog)
            {
                HandleButton(IsLog, ref filterButton, Filtering.Filtering.Funnel, buttonFilter_Click);
                HandleButton(IsLog, ref sheetButton, Resources.Icons.Table, buttonSheet_Click);
                HandleStatus(ref statusLabel);
            }
        }

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) { e.FormattingApplied = false; return; }

            if (Columns[e.ColumnIndex].ValueType == typeof(DateTime))
            {
                try
                {
                    e.Value = DateTimeExtensions.ToString(Convert.ToDateTime(e.Value), e.CellStyle.Format);
                    e.FormattingApplied = true;
                }
                catch
                {
                    e.FormattingApplied = false;
                }
            }
            else if (Columns[e.ColumnIndex].ValueType == typeof(TimeSpan))
            {
                try
                {
                    e.Value = DateTimeExtensions.ToString((TimeSpan)e.Value, e.CellStyle.Format);
                    e.FormattingApplied = true;
                }
                catch
                {
                    e.FormattingApplied = false;
                }
            }
            else if (Columns[e.ColumnIndex].ValueType == typeof(double))
            {
                    try
                    {
                        e.Value = Mayfly.Extensions.Extensions.ToSmallValueString((double)e.Value, e.CellStyle.Format);
                        e.FormattingApplied = true;
                    }
                    catch
                    {
                        e.FormattingApplied = false;
                    }
            }

            DataGridViewCell gridCell = this[e.ColumnIndex, e.RowIndex];

            bool showNormal = e.Value != null || (
                this.CurrentCell == gridCell &&
                this.IsCurrentCellInEditMode
                );

            e.CellStyle.Alignment = showNormal ?
                gridCell.InheritedStyle.Alignment :
                DataGridViewContentAlignment.MiddleCenter;

            e.CellStyle.ForeColor = showNormal ?
                gridCell.InheritedStyle.ForeColor : 
                Mayfly.Constants.InfantColor;

            base.OnCellFormatting(e);
        }

        protected override void OnSortCompare(DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.ValueType == typeof(string))
            {
                if (e.CellValue1 == null) return;
                if (e.CellValue2 == null) return;
                OmniSorter omniSorter = new OmniSorter();
                e.SortResult = omniSorter.Compare((string)e.CellValue1, (string)e.CellValue2);
                e.Handled = true;
            }
            else
            {
                base.OnSortCompare(e);
            }
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            SendKeys.Send("{ESC}");
        }

        //protected override void OnDragEnter(DragEventArgs drgevent)
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            Point hitPoint = this.PointToClient(new Point(drgevent.X, drgevent.Y));
            HitTestInfo hit = this.HitTest(hitPoint.X, hitPoint.Y);

            Log.Write(hit.ToString());

            if (drgevent.Data.GetDataPresent(DataFormats.FileDrop, false) &&
                hit.Type == DataGridViewHitTestType.Cell &&
                this.Columns[hit.ColumnIndex].ValueType == typeof(Waypoint))
            {
                drgevent.Effect = DragDropEffects.Link;
            }
            //else
            //{
            //    drgevent.Effect = DragDropEffects.None;
            //}
            
            base.OnDragOver(drgevent);
        }

        private DataGridViewCell dropCell;

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            Point hitPoint = this.PointToClient(new Point(drgevent.X, drgevent.Y));
            HitTestInfo hit = this.HitTest(hitPoint.X, hitPoint.Y);
            
            if (drgevent.Data.GetDataPresent(DataFormats.FileDrop, false) &&
                hit.Type == DataGridViewHitTestType.Cell &&
                this.Columns[hit.ColumnIndex].ValueType == typeof(Waypoint))
            {
                string[] filenames = FileSystem.MaskedNames((string[])drgevent.Data.GetData(DataFormats.FileDrop),
                    FileSystem.InterfaceLocation.OpenExtensions);

                if (filenames.Length > 0)
                {
                    drgevent.Effect = DragDropEffects.None;

                    ListLocation locationSelection = new ListLocation(filenames);
                    dropCell = this[hit.ColumnIndex, hit.RowIndex];
                    locationSelection.SetFriendlyDesktopLocation(this[hit.ColumnIndex, hit.RowIndex]);
                    locationSelection.LocationSelected += new LocationEventHandler(waypoints_WaypointSelected);
                    locationSelection.ShowDialog(this);
                    
                    //ListWaypoints waypoints = new ListWaypoints(filenames);
                    //waypoints.Tag = this[hit.ColumnIndex, hit.RowIndex];
                    //waypoints.SetFriendlyDesktopLocation(this[hit.ColumnIndex, hit.RowIndex]);
                    //waypoints.WaypointSelected += new LocationEventHandler(waypoints_WaypointSelected);
                    //waypoints.Show(this);
                }
            }
        }

        private void waypoints_WaypointSelected(object sender, LocationEventArgs e)
        {
            Waypoint waypoint = Waypoint.Empty;

            if (e.LocationObjects == null)
            {
                if (e.LocationObject is Waypoint)
                {
                    waypoint = (Waypoint)e.LocationObject;
                }
                else if (e.LocationObject is Polygon)
                {
                    waypoint = ((Polygon)e.LocationObject).Center;
                }
                else if (e.LocationObject is Track)
                {
                    waypoint = ((Track)e.LocationObject).Middle;
                }
            }
            else
            {
                if (e.LocationObjects is Waypoint[])
                {
                    waypoint = Waypoint.GetCenterOf((Waypoint[])e.LocationObjects);
                }
                else if (e.LocationObjects is Polygon[])
                {
                    List<Waypoint> res = new List<Waypoint>();
                    foreach (Polygon poly1 in e.LocationObjects) {
                        res.Add(poly1.Center);
                    }
                    waypoint = Waypoint.GetCenterOf(res);
                }
                else if (e.LocationObjects is Track[])
                {
                    List<Waypoint> res = new List<Waypoint>();
                    foreach (Track poly1 in e.LocationObjects) {
                        res.Add(poly1.Middle);
                    }
                    waypoint = Waypoint.GetCenterOf(res);
                }
            }

            if (dropCell.Selected)
            {
                foreach (DataGridViewCell selectedCell in this.SelectedCells)
                {
                    if (selectedCell.ColumnIndex == dropCell.ColumnIndex)
                    {
                        selectedCell.Value = waypoint;
                    }
                }
            }
            else
            {
                dropCell.Value = waypoint;
            }
        }

        #endregion

        #region New properties

        [Category("Menus"), DefaultValue(null)]
        public ContextMenuStrip RowMenu
        {
            get { return rowMenu; }

            set
            {
                rowMenu = value;

                if (value != null)
                {
                    value.Opening += rowMenu_Opening;
                }
            }
        }

        private void rowMenu_Opening(object sender, CancelEventArgs e)
        {
            if (IsLog && contextRow.Items.Count > 0)
            {
                ExtendMenu(contextRow, RowMenu, MenuInsertionMethod.Below);
            }
            
            contextRow_Opening(sender, e);
        }

        [Category("Menus"), DefaultValue(-1)]
        public int RowMenuLaunchableItemIndex
        {
            get
            {
                return rowMenuLaunchableItemIndex;
            }

            set
            {
                if (RowMenu != null)
                {
                    rowMenuLaunchableItemIndex = value;
                }
            }
        }

        [Category("Menus"), DefaultValue(null)]
        public ContextMenuStrip ColumnMenu
        {
            get { return colMenu; }
            set
            {
                colMenu = value;

                if (value != null)
                {
                    value.Opening += colMenu_Opening;
                }
            }
        }

        private void colMenu_Opening(object sender, CancelEventArgs e)
        {
            if (contextColumn.Items.Count > 0)
            {
                ExtendMenu(contextColumn, ColumnMenu, MenuInsertionMethod.Under);
            }

            contextColumn_Opening(sender, e);
        }

        [Category("Menus"), DefaultValue(null)]
        public ContextMenuStrip CellMenu
        {
            get;
            set;
        }

        [Category("Menus"), DefaultValue(-1)]
        public int CellMenuLaunchableItemIndex
        {
            get
            {
                return cellMenuLaunchableItemIndex;
            }

            set
            {
                if (CellMenu != null)
                {
                    cellMenuLaunchableItemIndex = value;
                }
            }
        }

        [Category("Appearance"), DefaultValue(1)]
        public int DefaultDecimalPlaces 
        {
            get { return defaultDecimalPlaces; } 
            set {
                defaultDecimalPlaces = value;
                base.DefaultCellStyle.Format = "N" + value.ToString(); 
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Padding), "0, 0, 15, 0")]
        public Padding CellPadding
        {
            get { return DefaultCellStyle.Padding; } 
            set 
            {
                DefaultCellStyle.Padding = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(string), "1;2|record;records"), Localizable(true)]
        public string StatusFormat { get; set; }

        [Category("Behavior"), DefaultValue(false)]
        public bool AutoClearEmptyRows { get; set; }

        [Category("Behavior"), DefaultValue(false)]
        public bool AllowColumnRenaming { get; set; }

        [Category("Behavior"), DefaultValue(false)]
        public bool AutoExpandFormOnColumnInsert { get; set; }

        [Category("Behavior"), DefaultValue(false)]
        public bool AllowUserToDeleteColumns { get; set; }

        [Category("Behavior"), DefaultValue(AutoCompleteMode.None)]
        public AutoCompleteMode AllowStringSuggection { get; set; }


        [Category("Behavior"), DefaultValue(false)]
        public bool IsLog
        {
            get { return isLog; }
            set
            {
                isLog = value;
                itemFilterBy.Visible = isLog;
                if (Parent != null) HandleButton(value, ref filterButton, Filtering.Filtering.Funnel, buttonFilter_Click);
                if (Parent != null) HandleButton(value, ref sheetButton, Resources.Icons.Table, buttonSheet_Click);
                if (Parent != null) HandleStatus(ref statusLabel);
            }
        }



        [Category("Behavior"), DefaultValue(null)]
        public ProcessDisplay Display
        {
            get;
            set;
        }

        [Category("Behavior"), DefaultValue(false)]
        public bool AllowUserToHideRows
        {
            set
            {
                allowUserToHideRows = value;

                if (DesignMode) return;

                if (value)
                {
                    if (RowVisibilityColumn == null)
                    {
                        RowVisibilityColumn = new DataGridViewCheckBoxColumn();
                        RowVisibilityColumn.Name = "Column" + Name + "RowVisibility";
                        RowVisibilityColumn.HeaderText = string.Empty;
                        RowVisibilityColumn.Width = 50;
                        RowVisibilityColumn.ValueType = typeof(bool);
                        RowVisibilityColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        RowVisibilityColumn.Resizable = DataGridViewTriState.False;
                        RowVisibilityColumn.Visible = false;
                        RowVisibilityColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //RowVisibilityColumn.Frozen = Columns[0].Frozen;
                    }

                    if (!Columns.Contains(RowVisibilityColumn))
                    {
                        Columns.Insert(0, RowVisibilityColumn);
                        RowVisibilityColumn.DefaultCellStyle.Padding = new Padding(2,0,2,0);
                    }

                    if (!Controls.Contains(checkBoxHider))
                    {
                        Controls.Add(checkBoxHider);
                        UpdateHiderCheck();                    
                    }
                }
                else
                {
                    if (Columns.Contains(RowVisibilityColumn))
                    {
                        Columns.Remove(RowVisibilityColumn);
                    }

                    if (Controls.Contains(checkBoxHider))
                    {
                        Controls.Remove(checkBoxHider);
                    }

                    RowVisibilityColumn.Dispose();
                    RowVisibilityColumn = null;
                }
            }

            get
            {
                return allowUserToHideRows;
            }
        }

        [Category("Behavior"), DefaultValue(Keys.F3)]
        public Keys RowVisibilityKey
        {
            set { rowVisibilityKey = value; }
            get { return rowVisibilityKey; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] StringVariants { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFiltering { get; set; }

        [Browsable(false)]
        public Filter Filter;

        [Browsable(false)]
        public int VisibleRowCount
        {
            get
            {
                int i = 0;
                foreach (DataGridViewRow gridRow in Rows)
                {
                    if (gridRow.IsNewRow) continue;
                    if (!IsHidden(gridRow)) i++;
                    //if (gridRow.Visible) i++;
                }
                return i;
            }
        }

        public new DataGridViewCell[] SelectedCells
        {
            get
            {
                List<DataGridViewCell> result = new List<DataGridViewCell>();

                foreach (DataGridViewCell gridCell in base.SelectedCells)
                {
                    if (gridCell.Visible)
                    {
                        result.Add(gridCell);
                    }
                }

                return result.ToArray();
            }
        }

        public int SelectedNonEmptyCellsCount
        {
            get
            {
                int result = 0;
                foreach (DataGridViewCell cell in SelectedCells)
                {
                    if (cell.Value != null)
                    {
                        result++;
                    }
                }
                return result;
            }
        }

        public DataGridViewColumn ClickedColumn { internal set; get; }

        private List<Button> buttons;

        private GridColumnRenameEventHandler columnRenamed { set; get; }

        private DataGridViewCellEventHandler cellValueDeleting { set; get; }

        private DataGridViewRowEventHandler rowRemoving { set; get; }

        private ContextMenuStrip rowMenu;
        private ContextMenuStrip colMenu;
        private int defaultDecimalPlaces;
        private bool isLog = false;
        internal Button filterButton;
        private Button sheetButton;
        private Label statusLabel;
        private int rowMenuLaunchableItemIndex = -1;
        private int cellMenuLaunchableItemIndex = -1;
        private Keys rowVisibilityKey;

        public bool allowUserToHideRows = false;
        private DataGridViewCheckBoxColumn RowVisibilityColumn;

        private List<DataGridViewColumn> insertedColumns;

        #endregion

        #region Events

        [Category("Mayfly Events")]
        public event GridColumnRenameEventHandler ColumnRenamed
        {
            add
            {
                columnRenamed += value;
            }

            remove
            {
                columnRenamed -= value;
            }
        }

        [Category("Mayfly Events")]
        public event DataGridViewCellEventHandler CellValueDeleting
        {
            add
            {
                cellValueDeleting += value;
            }

            remove
            {
                cellValueDeleting -= value;
            }
        }

        [Category("Mayfly Events")]
        public event DataGridViewRowEventHandler RowRemoving
        {
            add
            {
                rowRemoving += value;
            }

            remove
            {
                rowRemoving -= value;
            }
        }

        [Category("Mayfly Events")]
        public event EventHandler Filtered;

        [Category("Mayfly Events")]
        public event DataGridViewCellEventHandler InputFailed;

        [Category("Mayfly Events")]
        public event DataGridViewRowEventHandler UserChangedRowVisibility;

        #endregion



        public SpreadSheet()
        {
            InitializeComponent();

            alternatingRowsDefaultCellStyle.BackColor = System.Drawing.SystemColors.InactiveBorder;
            base.AlternatingRowsDefaultCellStyle = alternatingRowsDefaultCellStyle;

            DefaultCellStyle = new DataGridViewCellStyle();
            DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            DefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            base.DefaultCellStyle = DefaultCellStyle;

            columnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            columnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            columnHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            columnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            columnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            columnHeadersDefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            columnHeadersDefaultCellStyle.Padding = new Padding(0, 0, 0, 0);
            base.ColumnHeadersDefaultCellStyle = columnHeadersDefaultCellStyle;
            base.EnableHeadersVisualStyles = true;

            rowHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            rowHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            rowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            rowHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            rowHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            rowHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            rowHeadersDefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            base.RowHeadersDefaultCellStyle = rowHeadersDefaultCellStyle;

            base.AllowUserToAddRows = false;
            base.AllowUserToDeleteRows = false;
            base.AllowDrop = true;
            base.AllowUserToOrderColumns = true;
            base.AllowUserToResizeRows = false;
            base.BackgroundColor = System.Drawing.SystemColors.Window;
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;
            base.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            base.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            base.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            base.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            base.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            base.ShowEditingIcon = false;

            buttons = new List<Button>();

            DefaultDecimalPlaces = 1;
            RowVisibilityKey = Keys.F3;
            CellPadding = new Padding(0, 0, 15, 0);
            insertedColumns = new List<DataGridViewColumn>();

            itemDelete.Visible = AllowUserToDeleteColumns;

            // Table
            ItemReport.Click += ItemReport_Click;
            ItemCopyTable.Click += ItemCopyTable_Click;

            // Column
            itemColumnProperties.Click += itemColumnProperties_Click;
            //itemColumnDuplicate.Click += itemDuplicate_Click;
            itemRename.Click += itemRename_Click;
            itemDelete.Click += itemDelete_Click;

            // Row
            itemSelectAll.Click += itemSelectAll_Click;
            itemInverse.Click += itemInverse_Click;
            itemHide.Click += itemHide_Click;
            itemShowHidden.Click += itemShowHidden_Click;
            itemShowAll.Click += itemShowAll_Click;

            // Value
            itemFilterBy.Click += itemFilterBy_Click;
            itemClear.Click += itemClear_Click;
            itemCopy.Click += itemCopy_Click;
            itemPaste.Click += itemPaste_Click;
            itemSetValue.Click += itemSetValue_Click;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpreadSheet));
            this.contextCell = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemFilterBy = new System.Windows.Forms.ToolStripMenuItem();
            this.itemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.itemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSetValue = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.itemInverse = new System.Windows.Forms.ToolStripMenuItem();
            this.itemHide = new System.Windows.Forms.ToolStripMenuItem();
            this.itemShowHidden = new System.Windows.Forms.ToolStripMenuItem();
            this.itemShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemReport = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemCopyTable = new System.Windows.Forms.ToolStripMenuItem();
            this.contextRow = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxHider = new System.Windows.Forms.CheckBox();
            this.contextColumn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemColumnProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRename = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.inputDialog = new Mayfly.TaskDialogs.InputDialog(this.components);
            this.contextCell.SuspendLayout();
            this.contextRow.SuspendLayout();
            this.contextTable.SuspendLayout();
            this.contextColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // contextCell
            // 
            this.contextCell.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemFilterBy,
            this.itemClear,
            this.itemCopy,
            this.itemPaste,
            this.itemSetValue});
            this.contextCell.Name = "contextMenuStripTranslate";
            resources.ApplyResources(this.contextCell, "contextCell");
            this.contextCell.Opening += new System.ComponentModel.CancelEventHandler(this.ValueMenu_Opening);
            // 
            // itemFilterBy
            // 
            this.itemFilterBy.Image = global::Mayfly.Controls.Filtering.Filtering.Funnel;
            this.itemFilterBy.Name = "itemFilterBy";
            resources.ApplyResources(this.itemFilterBy, "itemFilterBy");
            // 
            // itemClear
            // 
            this.itemClear.Image = global::Mayfly.Resources.Icons.Delete;
            this.itemClear.Name = "itemClear";
            resources.ApplyResources(this.itemClear, "itemClear");
            // 
            // itemCopy
            // 
            this.itemCopy.Image = global::Mayfly.Resources.Icons.Copy;
            this.itemCopy.Name = "itemCopy";
            resources.ApplyResources(this.itemCopy, "itemCopy");
            // 
            // itemPaste
            // 
            this.itemPaste.Image = global::Mayfly.Resources.Icons.Paste;
            this.itemPaste.Name = "itemPaste";
            resources.ApplyResources(this.itemPaste, "itemPaste");
            // 
            // itemSetValue
            // 
            this.itemSetValue.Name = "itemSetValue";
            resources.ApplyResources(this.itemSetValue, "itemSetValue");
            // 
            // itemSelectAll
            // 
            this.itemSelectAll.Name = "itemSelectAll";
            resources.ApplyResources(this.itemSelectAll, "itemSelectAll");
            // 
            // itemInverse
            // 
            this.itemInverse.Name = "itemInverse";
            resources.ApplyResources(this.itemInverse, "itemInverse");
            // 
            // itemHide
            // 
            this.itemHide.Name = "itemHide";
            resources.ApplyResources(this.itemHide, "itemHide");
            // 
            // itemShowHidden
            // 
            this.itemShowHidden.Name = "itemShowHidden";
            resources.ApplyResources(this.itemShowHidden, "itemShowHidden");
            // 
            // itemShowAll
            // 
            this.itemShowAll.Name = "itemShowAll";
            resources.ApplyResources(this.itemShowAll, "itemShowAll");
            // 
            // ItemReport
            // 
            this.ItemReport.Image = global::Mayfly.Resources.Icons.Print;
            this.ItemReport.Name = "ItemReport";
            resources.ApplyResources(this.ItemReport, "ItemReport");
            // 
            // ItemCopyTable
            // 
            this.ItemCopyTable.Image = global::Mayfly.Resources.Icons.Copy;
            this.ItemCopyTable.Name = "ItemCopyTable";
            resources.ApplyResources(this.ItemCopyTable, "ItemCopyTable");
            // 
            // contextRow
            // 
            this.contextRow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemSelectAll,
            this.itemInverse,
            this.itemHide,
            this.itemShowHidden,
            this.itemShowAll});
            this.contextRow.Name = "contextRow";
            resources.ApplyResources(this.contextRow, "contextRow");
            this.contextRow.Opening += new System.ComponentModel.CancelEventHandler(this.contextRow_Opening);
            // 
            // contextTable
            // 
            this.contextTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemSave,
            this.ItemReport,
            this.ItemCopyTable});
            this.contextTable.Name = "contextTable";
            resources.ApplyResources(this.contextTable, "contextTable");
            // 
            // ItemSave
            // 
            this.ItemSave.Image = global::Mayfly.Resources.Icons.Save;
            this.ItemSave.Name = "ItemSave";
            resources.ApplyResources(this.ItemSave, "ItemSave");
            this.ItemSave.Click += new System.EventHandler(this.ItemSave_Click);
            // 
            // checkBoxHider
            // 
            resources.ApplyResources(this.checkBoxHider, "checkBoxHider");
            this.checkBoxHider.Name = "checkBoxHider";
            this.checkBoxHider.UseVisualStyleBackColor = true;
            this.checkBoxHider.CheckedChanged += new System.EventHandler(this.checkBoxHider_CheckedChanged);
            this.checkBoxHider.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBoxHider_KeyUp);
            // 
            // contextColumn
            // 
            this.contextColumn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemColumnProperties,
            this.itemRename,
            this.itemDelete});
            this.contextColumn.Name = "contextColumn";
            resources.ApplyResources(this.contextColumn, "contextColumn");
            this.contextColumn.Opening += new System.ComponentModel.CancelEventHandler(this.contextColumn_Opening);
            // 
            // itemColumnProperties
            // 
            resources.ApplyResources(this.itemColumnProperties, "itemColumnProperties");
            this.itemColumnProperties.Name = "itemColumnProperties";
            // 
            // itemRename
            // 
            resources.ApplyResources(this.itemRename, "itemRename");
            this.itemRename.Name = "itemRename";
            // 
            // itemDelete
            // 
            this.itemDelete.Name = "itemDelete";
            resources.ApplyResources(this.itemDelete, "itemDelete");
            // 
            // inputDialog
            // 
            resources.ApplyResources(this.inputDialog, "inputDialog");
            this.contextCell.ResumeLayout(false);
            this.contextRow.ResumeLayout(false);
            this.contextTable.ResumeLayout(false);
            this.contextColumn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }



        #region Methods

        private void EnsureVisible()
        {
            // TODO: Change tab selection on form!
            if (Parent is TabPage)
            {
                ((TabControl)((TabPage)Parent).Parent).SelectedTab = (TabPage)Parent;
            }
            Parent.Show();
        }

        private void UpdateHiderCheck()
        {
            bool allSelected = true;
            bool noneSelected = true;

            for (int i = 0; i < RowCount; i++)
            {
                allSelected &= (bool)this[RowVisibilityColumn.Index, i].FormattedValue;
                noneSelected &= !(bool)this[RowVisibilityColumn.Index, i].FormattedValue;
            }

            if (allSelected)
            {
                checkBoxHider.CheckState = CheckState.Checked;
            }
            else if (noneSelected)
            {
                checkBoxHider.CheckState = CheckState.Unchecked;
            }
            else
            {
                checkBoxHider.CheckState = CheckState.Indeterminate;
            }
        }

        private void OffRowSelection()
        {
            RowVisibilityColumn.Visible = false;

            checkBoxHider.Visible = false;

            foreach (DataGridViewRow gridRow in Rows)
            {
                if (gridRow.IsNewRow) continue;

                gridRow.Visible = ((bool)gridRow.Cells[RowVisibilityColumn.Index].Value);
            }
        }

        public void ExtendMenu(ContextMenuStrip sourceMenu, ContextMenuStrip destMenu,
            MenuInsertionMethod insertionMethod)
        {
            if (DesignMode) return;

            if (insertionMethod == MenuInsertionMethod.Below)
            {
                destMenu.Items.Add(new ToolStripSeparator());
            }

            while(sourceMenu.Items.Count > 0)
            {
                switch (insertionMethod)
                {
                    case MenuInsertionMethod.Under:
                        destMenu.Items.Insert(0, sourceMenu.Items[sourceMenu.Items.Count - 1]);
                        break;
                    case MenuInsertionMethod.Below:
                        destMenu.Items.Add(sourceMenu.Items[0]);
                        break;
                }
            }
        }

        #region Buttons handling

        public void HandleStatus(ref Label label)
        {
            if (DesignMode) return;

            if (label == null)
            {
                label = new Label();
                label.Margin = new Padding(0, 3, 3, 0);
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.ForeColor = SystemColors.ControlDark;

                if (Parent != null)
                {
                    Parent.Controls.Add(label);
                    ResetButtonPositions();
                }
            }
        }

        public void HandleButton(bool showButton, ref Button button, Image buttonImage, EventHandler buttonEventHandler)
        {
            if (DesignMode) return;

            if (showButton)
            {
                if (button == null)
                {
                    button = new Button();
                    button.Size = new Size(23, 23);
                    button.Margin = new Padding(0, 3, 3, 0);
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = SystemColors.Window;
                    button.FlatAppearance.BorderSize = 0;
                    button.UseVisualStyleBackColor = true;
                    button.Image = buttonImage;
                    button.Click += buttonEventHandler;

                    if (!buttons.Contains(button))
                    {
                        buttons.Add(button);
                    }

                    if (Parent != null)
                    {
                        Parent.Controls.Add(button);
                        ResetButtonPositions();
                    }
                }
            }
            else
            {
                if (button != null)
                {
                    Parent.Controls.Remove(button);
                    button = null;
                }
            }
        }


        private void ResetButtonPositions()
        {
            int x = this.Left;

            foreach(Button button in buttons)
            {
                button.Location = new Point(x + button.Margin.Left, this.Bottom + this.Margin.Bottom + button.Margin.Top);
                x += button.Width + button.Margin.Right + this.Margin.Bottom;
            }

            if (statusLabel != null) {
                statusLabel.Location = new Point(x + statusLabel.Margin.Left, this.Bottom + this.Margin.Bottom + statusLabel.Margin.Top);
                statusLabel.Size = new Size(this.Width - (statusLabel.Left - this.Left), 23);
            }
        }

        public void UpdateStatus(string status)
        {
            if (statusLabel == null) return;
            statusLabel.Text = status;
        }

        public void UpdateStatus(int n)
        {
            UpdateStatus(n.ToCorrectString(this.StatusFormat));
        }

        public void UpdateStatus()
        {
            UpdateStatus(this.VisibleRowCount);
        }

        private void UpdateButtonAvailablity()
        {
            if (IsLog)
            {
                filterButton.Enabled = RowCount > 0;
                sheetButton.Enabled = RowCount > 0;
            }
        }

        public void AddTableMenu(ContextMenuStrip menu)
        {
            ExtendMenu(menu, contextTable, MenuInsertionMethod.Under);
        }

        public ContextMenuStrip AddCellMenu(ContextMenuStrip menu)
        {
            ExtendMenu(menu, contextCell, MenuInsertionMethod.Below);
            return contextCell;
        }

        #endregion

        #region IO

        public DataTable GetData()
        {
            return GetData(Columns, false, Rows, false);
        }

        public DataTable GetData(IEnumerable gridColumns)
        {
            return GetData(gridColumns, false, Rows, false);
        }

        public DataTable GetData(IEnumerable gridColumns, bool includeInvisibleColumns,
            IEnumerable gridRows, bool includeInvisibleRows)
        {
            DataTable table = new DataTable();

            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                if (!(gridColumn.Visible || includeInvisibleColumns)) continue;

                DataColumn dataColumn = new DataColumn(gridColumn.Name);
                dataColumn.Caption = gridColumn.HeaderText;
                dataColumn.DataType = (gridColumn.ValueType == typeof(double) ||
                    gridColumn.ValueType == typeof(int)) ? gridColumn.ValueType : typeof(string);
                table.Columns.Add(dataColumn);
            }

            foreach (DataGridViewRow gridRow in gridRows)
            {
                if (gridRow.IsNewRow) continue;
                if (!(gridRow.Visible || includeInvisibleRows)) continue;
                DataRow dataRow = table.NewRow();

                //bool ok = true;
                foreach (DataGridViewColumn gridColumn in gridColumns)
                {
                    if (!gridColumn.Visible) continue;
                    //if (this[gridColumn.Name, gridRow.Index].Value == null)
                    //{
                    //    ok = false;
                    //    break;
                    //}
                    //dataRow[gridColumn.HeaderText] = (gridColumn.ValueType == typeof(double)) ?
                    //    this[gridColumn.Name, gridRow.Index].Value :
                    //    this[gridColumn.Name, gridRow.Index].FormattedValue;

                    if (this[gridColumn.Name, gridRow.Index].Value != null)
                    {
                        dataRow[gridColumn.Name] = Convert.ChangeType(
                            table.Columns[gridColumn.Name].DataType == typeof(string) ? 
                            this[gridColumn.Name, gridRow.Index].FormattedValue : this[gridColumn.Name, gridRow.Index].Value,
                            table.Columns[gridColumn.Name].DataType);
                    }
                }

                //if (ok)
                //{
                    table.Rows.Add(dataRow);
                //}
            }

            return table;
        }

        public void Load(DataTable table)
        {
            Columns.Clear();

            foreach (DataColumn dataColumn in table.Columns)
            {
                InsertColumn(dataColumn.ColumnName, dataColumn.Caption, 
                    dataColumn.DataType, ColumnCount);
            }

            foreach (DataRow dataRow in table.Rows)
            {
                DataGridViewRow newGridRow = new DataGridViewRow();
                newGridRow.CreateCells(this);

                foreach (DataGridViewColumn gridColumn in Columns)
                {
                    if (dataRow[gridColumn.Name] is DBNull) continue;

                    newGridRow.Cells[gridColumn.Index].Value = dataRow[gridColumn.Name];
                }

                Rows.Add(newGridRow);
            }

            foreach (DataGridViewColumn gridColumn in Columns)
            {
                gridColumn.Width = gridColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells,
                    true);

                if (gridColumn.ValueType == typeof(double))
                {
                    gridColumn.DefaultCellStyle.Format = gridColumn.GetDoubles().MeanFormat();
                }
            }
        }

        public void Load(SpreadSheet spreadSheetSource)
        {
            Columns.Clear();

            DefaultDecimalPlaces = spreadSheetSource.DefaultDecimalPlaces;

            foreach (DataGridViewColumn gridColumn in spreadSheetSource.Columns)
            {
                if (gridColumn.Visible)
                {
                    InsertColumn(gridColumn);
                }
            }

            foreach (DataGridViewRow gridRow in spreadSheetSource.Rows)
            {
                if (gridRow.Visible || spreadSheetSource.AllowUserToHideRows)
                {
                    DataGridViewRow newGridRow = new DataGridViewRow();
                    newGridRow.CreateCells(this);

                    foreach (DataGridViewColumn gridColumn in Columns)
                    {
                        newGridRow.Cells[gridColumn.Index].Value = gridRow.Cells[gridColumn.Name].Value;
                    }

                    Rows.Add(newGridRow);
                }
            }
        }

        //public string SeparatedValues(char separator)
        //{
        //    return SeparatedValues(separator, true, false, false);
        //}

        //public string SeparatedValues(char separator, bool formatted,
        //    bool includeInvisibleColumns, bool includeInvisibleRows)
        //{
        //    StringWriter result = new StringWriter();

        //    string headerText = string.Empty;

        //    for (int i = 0; i <= ColumnCount - 1; i++)
        //    {
        //        if (includeInvisibleColumns || Columns[i].Visible)
        //        {
        //            if (i > 0) headerText += separator;
        //            headerText += ValueToFile(Columns[i].HeaderText);
        //        }
        //    }

        //    result.WriteLine(headerText);

        //    foreach (DataGridViewRow gridRow in Rows)
        //    {
        //        if (gridRow.IsNewRow) continue;

        //        if (!(includeInvisibleRows || gridRow.Visible)) continue;

        //        string rowValues = string.Empty;

        //        for (int i = 0; i <= ColumnCount - 1; i++)
        //        {
        //            if (!(includeInvisibleColumns || Columns[i].Visible)) continue;

        //            if (i > 0) rowValues += separator;

        //            if (gridRow.Cells[i].Value == null) continue;

        //            rowValues += ValueToFile(formatted ? gridRow.Cells[i].FormattedValue : gridRow.Cells[i].Value);
        //        }

        //        result.WriteLine(rowValues);
        //    }

        //    return result.ToString();
        //}

        //public string PrintableFile()
        //{
        //    StringWriter result = new StringWriter();

        //    List<int> lengths = new List<int>();

        //    string headerText = string.Empty;

        //    for (int i = 0; i <= ColumnCount - 1; i++)
        //    {
        //        if (!Columns[i].Visible) continue;

        //        object[] values = Columns[i].GetValues(true, false).ToArray();

        //        int length = Columns[i].HeaderText.Length;

        //        foreach (object value in values)
        //        {
        //            length = Math.Max(length, value.ToString().Length);
        //        }

        //        lengths.Add(length);
        //    }

        //    for (int i = 0; i <= ColumnCount - 1; i++)
        //    {
        //        if (!Columns[i].Visible) continue;

        //        headerText += string.Format("{0,-" + lengths[i] + "}", Columns[i].HeaderText);
        //    }

        //    result.WriteLine(headerText);

        //    foreach (DataGridViewRow gridRow in Rows)
        //    {
        //        if (gridRow.IsNewRow) continue;

        //        if (!gridRow.Visible) continue;

        //        string rowValues = string.Empty;

        //        for (int i = 0; i <= ColumnCount - 1; i++)
        //        {
        //            if (!Columns[i].Visible) continue;

        //            rowValues += string.Format("{0,-" + lengths[i] + "}", gridRow.Cells[i].Value);
        //        }

        //        result.WriteLine(rowValues);
        //    }

        //    return result.ToString();
        //}

        //private string ValueToFile(object value)
        //{
        //    if (value == null)
        //    {
        //        return string.Empty; 
        //    }
        //    else 
        //    {
        //        return value.ToString();
        //    }
        //}

        public void PasteToGrid()
        {
            PasteToGrid(CurrentCell, Clipboard.GetText());
        }

        public void PasteToGrid(DataGridViewCell gridCell, string stringValue)
        {
            PasteToGrid(gridCell, stringValue.Trim().Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
        }

        public void PasteToGrid(DataGridViewCell gridCell, string[] lines)
        {
            PasteToGrid(gridCell, lines, '\t');
        }

        public void PasteToGrid(DataGridViewCell gridCell, string[] lines, char separator)
        {
            int rowIndex = gridCell.RowIndex;

            foreach (string line in lines)
            {
                string[] pastesCells = line.TrimEnd(Environment.NewLine.ToCharArray()).Split(separator);

                if (rowIndex == RowCount - 1)
                {
                    Rows.Add(); //.Insert(rowIndex, 1);
                }

                int columnIndex = 0;

                bool rowInserted = false;

                for (int i = this.GetUserOrderedColumns().IndexOf(gridCell.OwningColumn); //.ColumnIndex;
                    columnIndex < pastesCells.Length && i < this.ColumnCount;
                    i++)
                {
                    DataGridViewColumn col = this.GetUserOrderedColumns()[i];

                    if (!col.Visible) continue;

                    string value = pastesCells[columnIndex].Trim('\"');                    
                    object inserted = this[col.Index, rowIndex].EnterValue(value);

                    rowInserted |= inserted != null;
                    columnIndex++;
                }

                if (rowInserted)
                    rowIndex++;
            }

            if (AutoClearEmptyRows)
            {
                ClearEmptyRows();
            }
        }

        public Report Print(string title)
        {
            Report result = new Report(title);
            this.AddToReport(result);
            result.EndBranded();
            return result;
        }

        public void AddToReport(Report result)
        {
            result.AddTable(GetReport());
        }

        public Report.Table GetReport()
        {
            Report.Table result = new Report.Table(ReportTableClass.Big);

            result.StartRow();

            foreach (DataGridViewColumn gridColumn in GetVisibleColumns())
            {
                result.AddHeaderCell(gridColumn.HeaderText);
            }

            result.EndRow();

            foreach (DataGridViewRow gridRow in Rows)
            {
                if (gridRow.IsNewRow) continue;

                if (!gridRow.Visible) continue;

                result.StartRow();

                foreach (DataGridViewColumn gridColumn in GetVisibleColumns())
                {
                    object o = this[gridColumn.Index, gridRow.Index].FormattedValue;

                    o = o.ToString().Replace(Environment.NewLine, "<br>");

                    switch (gridColumn.DefaultCellStyle.Alignment)
                    {
                        case DataGridViewContentAlignment.BottomLeft:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.TopLeft:
                            result.AddCell(o);
                            break;

                        case DataGridViewContentAlignment.BottomRight:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.TopRight:
                            result.AddCellRight(o);
                            break;

                        default:
                            result.AddCellValue(o);
                            break;
                    }

                    //if (gridColumn.ValueType == typeof(double))
                    //{
                    //    result.AddCellRight(this[gridColumn.Index, gridRow.Index].FormattedValue);
                    //}
                    //else if (gridColumn.ValueType == typeof(string))
                    //{
                    //    result.AddCell(this[gridColumn.Index, gridRow.Index].FormattedValue);
                    //}
                    //else
                    //{
                    //    result.AddCellValue(this[gridColumn.Index, gridRow.Index].FormattedValue);
                    //}
                }

                result.EndRow();
            }

            return result;
        }

        #endregion

        #region Filtering

        private FilterEventArgs FilteringArgs;

        public void EnsureFilter(DataGridViewColumn dataGridViewColumn, double startValue, double endValue, BackgroundWorker backgroundWorker, EventHandler launchHandler)
        {
            EnsureFilter(new FilterEventArgs(new DataGridViewColumn[] { dataGridViewColumn }, new string[] { startValue.ToString(), endValue.ToString() }, backgroundWorker), launchHandler);
        }

        public void EnsureFilter(DataGridViewColumn[] dataGridViewColumns, string[] values, BackgroundWorker backgroundWorker, EventHandler launchHandler)
        {
            EnsureFilter(new FilterEventArgs(dataGridViewColumns, values, backgroundWorker), launchHandler);
        }

        public void EnsureFilter(DataGridViewColumn dataGridViewColumn, string value, BackgroundWorker backgroundWorker, EventHandler launchHandler)
        {
            EnsureFilter(new FilterEventArgs(dataGridViewColumn, value, backgroundWorker), launchHandler);
        }

        public void EnsureFilter(DataGridViewColumn dataGridViewColumn, int value, BackgroundWorker backgroundWorker, EventHandler launchHandler)
        {
            EnsureFilter(new FilterEventArgs(dataGridViewColumn, value, backgroundWorker), launchHandler);
        }

        private void EnsureFilter(FilterEventArgs e, EventHandler launchHandler)
        {
            //if (e.DataGridViewColumns.Length != e.Values.Length)
            //    throw new ArgumentException("Columns and Values should be the same length.");

            foreach (DataGridViewColumn gridColumn in e.DataGridViewColumns)
            {
                if (!Columns.Contains(gridColumn))
                    throw new ArgumentException(string.Format("Column '{0}' doesn't belong to this SpreadSheet", gridColumn.HeaderText));
            }

            FilteringArgs = e;

            if (RowCount > (AllowUserToAddRows ? 1 : 0))
            {
                Filterate(null, e);
            }
            else
            {
                FilteringArgs = e;
                e.BackgroundWorker.RunWorkerCompleted += Filterate;
                launchHandler.Invoke(null, new EventArgs());
            }
        }

        private void Filterate(object sender, EventArgs e)
        {
            EnsureVisible();
            OpenFilter(FilteringArgs.DataGridViewColumns, FilteringArgs.Values, true);
            FilteringArgs.BackgroundWorker.RunWorkerCompleted -= Filterate;
        }

        public void OpenFilter()
        {
            if (Filter == null || Filter.IsDisposed)
            {
                Filter = new Filter(this);
                if (Filtered != null) Filter.Filtered += Filtered;
                Filter.Text = string.Empty;
                Filter.FormBorderStyle = FormBorderStyle.Sizable;
                Filter.ControlBox = false;
                Filter.VisibleChanged += Filter_VisibleChanged;
                Filter.Deactivate += Filter_Deactivate;

                if (RowCount > VisibleRowCount)
                {
                    Filter.SignChanges();
                }
            }

            if (!Filter.Visible)
            {
                Filter.Show(FindForm());
            }

            if (Filter.WindowState == FormWindowState.Minimized)
            {
                Filter.WindowState = FormWindowState.Normal;
            }
            else
            {
                Filter.BringToFront();
            }

            if (!Filter.IsFilterSet)
            {
                Filter.AddFilter();
            }
        }

        public void OpenFilter(bool dropPrevious)
        {
            OpenFilter();

            if (dropPrevious)
            {
                Filter.Drop();
            }
        }

        public void OpenFilter(DataGridViewColumn gridColumn, object value, bool dropPrevious)
        {
            OpenFilter(gridColumn, value, false, dropPrevious);
            Filter.Apply();
        }

        public void OpenFilter(DataGridViewColumn gridColumn, object value, bool negative, bool dropPrevious)
        {
            OpenFilter(dropPrevious);
            Filter.AddFilter(gridColumn, value, negative);
            Filter.Apply();
        }

        public void OpenFilter(DataGridViewColumn gridColumn, double min, double max, bool dropPrevious)
        {
            OpenFilter(dropPrevious);
            Filter.AddFilter(gridColumn, min, max);
            Filter.Apply();
        }

        public void OpenFilter(DataGridViewColumn[] gridColumns, object[] values, bool dropPrevious)
        {
            if (gridColumns.Length == 1)
            {
                double min = double.MaxValue;
                double max = double.MinValue;

                foreach (object o in values)
                {
                    min = Math.Min(min, o.ToDouble());
                    max = Math.Max(max, o.ToDouble());
                }

                OpenFilter(gridColumns[0], min, max, dropPrevious);
            }
            else
            {
                OpenFilter(dropPrevious);
                for (int i = 0; i < gridColumns.Length; i++)
                {
                    Filter.AddFilter(gridColumns[i], values[i]);
                }
                Filter.Apply();
            }
        }

        public void OpenFilter(DataGridViewColumn[] gridColumns, object[] values)
        {
            OpenFilter(gridColumns, values, false);
        }

        private void Filter_VisibleChanged(object sender, EventArgs e)
        {
            if (((Filter)sender).Visible)
            {
                ((Filter)sender).DesktopLocation = filterButton.PointToScreen(new Point(0, -Filter.Height - SystemInformation.SizingBorderWidth));
            }
        }

        private void Filter_Deactivate(object sender, EventArgs e)
        {
            ((Filter)sender).Hide();
            if (((Filter)sender).Owner != null)
            {
                ((Filter)sender).Owner.BringToFront();
            }
        }

        public void InitiateBackgroundTable()
        {
            Background = GetData(Columns, true, Rows, true);
                        
            IsBackgroundTableInitiated = true;
        }

        internal void Add(DataRow dataRow)
        {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(this, dataRow.ItemArray);
            Rows.Add(gridRow);
        }

        #endregion

        #region Column methods

        //public void InitializeDateTimeColumn(DataGridViewColumn columnDateTime)
        //{
        //    if (columnDateTime.ValueType != typeof(DateTime))
        //        throw new ArgumentException();

        //    dtpAction = new DateTimePicker();
        //    dtpAction.Format = DateTimePickerFormat.Custom;
        //    dtpAction.CustomFormat = columnDateTime.DefaultCellStyle.Format;
        //    dtpAction.Visible = false;
        //    dtpAction.Width = columnDateTime.Width;

        //    dtpAction.ValueChanged += dtpAction_ValueChanged;

        //    Controls.Add(dtpAction);
        //}

        //void dtpAction_ValueChanged(object sender, EventArgs e)
        //{
        //    if (CurrentCell.OwningColumn.ValueType == typeof(DateTime))
        //    {
        //        CurrentCell.Value = dtpAction.Value;
        //        NotifyCurrentCellDirty(true);
        //    }
        //}

        public DataGridViewColumn GetColumn(string name)
        {
            foreach (DataGridViewColumn gridColumn in Columns)
            {
                if (gridColumn.Name == name)
                {
                    return gridColumn;
                }

                if (gridColumn.HeaderText == name)
                {
                    return gridColumn;
                }
            }

            return null;
        }

        public DataGridViewColumn[] GetColumns(string namePart)
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();

            foreach (DataGridViewColumn gridColumn in Columns)
            {
                if (gridColumn.Name.Contains(namePart))
                {
                    result.Add(gridColumn);
                }
            }

            return result.ToArray();
        }

        public List<DataGridViewColumn> GetInsertedColumns()
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();

            foreach (DataGridViewColumn gridColumn in GetUserOrderedColumns())
            {
                if (insertedColumns.Contains(gridColumn))
                {
                    result.Add(gridColumn);
                }
            }

            return result;
        }

        public List<DataGridViewColumn> GetUserOrderedColumns()
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();

            for (int i = 0; i < Columns.Count; i++)
            {
                foreach (DataGridViewColumn gridColumn in Columns)
                {
                    if (i == gridColumn.DisplayIndex)
                    {
                        result.Add(gridColumn);
                    }
                }
            }

            return result;
        }

        public List<DataGridViewColumn> GetVisibleColumns()
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();

            foreach (DataGridViewColumn gridColumn in GetUserOrderedColumns())
            {
                if (!gridColumn.Visible) continue;

                result.Add(gridColumn);
            }

            return result;
        }

        public List<DataGridViewColumn> GetNumericalColumns()
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();

            foreach (DataGridViewColumn gridColumn in GetVisibleColumns())
            {
                if (gridColumn.ValueType.IsDoubleConvertible())
                {
                    result.Add(gridColumn);
                }
            }

            return result;
        }

        public List<DataGridViewColumn> GetTextColumns()
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();

            foreach (DataGridViewColumn gridColumn in GetVisibleColumns())
            {
                if (gridColumn.ValueType == typeof(string))
                {
                    result.Add(gridColumn);
                }
            }

            return result;
        }

        public List<DataGridViewColumn> GetCategorialColumns()
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();

            foreach (DataGridViewColumn gridColumn in GetVisibleColumns())
            {
                //if (gridColumn.ValueType == typeof(string))
                //{
                //    result.Add(gridColumn);
                //}

                //if (gridColumn.ValueType == typeof(DateTime))
                //{
                //    result.Add(gridColumn);
                //}

                //if (gridColumn.ValueType == typeof(int))
                //{
                //    result.Add(gridColumn);
                //}

                if (gridColumn.ValueType == typeof(double))
                {
                    if ( gridColumn.GetStrings(true).Count <= 10)
                    {
                        result.Add(gridColumn);
                    }
                }
                else
                {
                    result.Add(gridColumn);
                }
            }

            return result;
        }

        public DataGridViewColumn InsertColumn(string name, string header, Type type)
        {
            return InsertColumn(name, header, type, ColumnCount);
        }

        public DataGridViewColumn InsertColumn(string name, Type type, string format)
        {
            DataGridViewColumn result = InsertColumn(name, name, type);
            result.DefaultCellStyle.Format = format;
            return result;
        }

        public DataGridViewColumn InsertColumn(string name, string header, Type type, int index)
        {
            DataGridViewColumn result = GetColumn(name);

            if (result == null)
            {
                result = new DataGridViewTextBoxColumn();
                result.Name = name;
                result.HeaderText = header;
                result.ValueType = type;
                if (type == typeof(string)) { result.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; }
                else if (type == typeof(double)) { result.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; }
                else if (type == typeof(DateTime)) {
                    result.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    result.DefaultCellStyle.Format = "D";
                }
                else if (type == typeof(int)) {
                    result.DefaultCellStyle.Format = "0";
                    result.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else { 
                    result.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                
                Columns.Insert(index, result);

                insertedColumns.Add(result);

                result.RestoreFormat();
                //ExpandForm(result);
            }

            return result;
        }

        public DataGridViewColumn InsertColumn(string name, string header, Type type, int index, int width)
        {
            DataGridViewColumn result = InsertColumn(name, header, type, index);
            result.Width = width;
            return result;
        }

        public DataGridViewColumn InsertColumn(string name, string header, Type type, string format)
        {
            DataGridViewColumn result = InsertColumn(name, header, type);
            result.DefaultCellStyle.Format = format;
            return result;
        }

        public DataGridViewColumn InsertColumn(string name, string header, Type type, int index, string format)
        {
            DataGridViewColumn result = InsertColumn(name, header, type, index);
            result.DefaultCellStyle.Format = format;
            return result;
        }

        public DataGridViewColumn InsertColumn(string name, string header, Type type, int index, int width, string format)
        {
            DataGridViewColumn result = InsertColumn(name, header, type, index);
            result.Width = width;
            result.DefaultCellStyle.Format = format;
            return result;
        }

        public DataGridViewColumn InsertColumn(string name, Type valueType)
        {
            DataGridViewColumn result = InsertColumn(name, name, valueType, ColumnCount, 100);

            result.Visible = true;

            //if (Width / (Columns.Count + 1) < 90)
            //{
            //    FindForm().Width += 100;
            //}

            return result;
        }

        public DataGridViewColumn InsertColumn(string varName)
        {
            DataGridViewColumn result = InsertColumn(varName, varName, typeof(double), ColumnCount, 100);

            result.Visible = true;

            //if (Width / (Columns.Count + 1) < 90)
            //{
            //    FindForm().Width += 100;
            //}

            return result;
        }

        public DataGridViewColumn InsertColumn(string varName, int index)
        {
            DataGridViewColumn result = InsertColumn(varName, varName, typeof(double), index, 100);

            result.Visible = true;


            if (AutoExpandFormOnColumnInsert && Width / (Columns.Count + 1) < 90)
            {
                FindForm().Width += 100;
            }

            return result;
        }

        public DataGridViewColumn InsertColumn(DataGridViewColumn originalColumn)
        {
            return InsertColumn(originalColumn.Name, originalColumn.HeaderText,
                originalColumn.ValueType, ColumnCount,
                originalColumn.Width, originalColumn.DefaultCellStyle.Format);
        }

        public DataGridViewColumn InsertColumn(DataGridViewColumn originalColumn, int index)
        {
            return InsertColumn(Columns.Contains(originalColumn) ? originalColumn.Name + "_1" : originalColumn.Name, 
                originalColumn.HeaderText,
                originalColumn.ValueType, index,
                originalColumn.Width,
                originalColumn.DefaultCellStyle.Format);
        }

        public DataGridViewColumn InsertColumn(DataGridViewColumn originalColumn, int index, string name)
        {
            return InsertColumn(name, originalColumn.HeaderText,
                originalColumn.ValueType, index,
                originalColumn.Width, originalColumn.DefaultCellStyle.Format);
        }

        public void InsertColumn()
        {
            DataGridViewColumn newGridColumn = InsertColumn(string.Empty);
            SetRenaming(newGridColumn);
        }

        public void InsertColumn(int index)
        {
            DataGridViewColumn newGridColumn = InsertColumn(string.Empty, string.Empty, typeof(double), index, 100);
            SetRenaming(newGridColumn);
        }

        public SpreadSheetIconTextBoxColumn InsertIconColumn(string name, string header, Type type)
        {
            SpreadSheetIconTextBoxColumn result = new SpreadSheetIconTextBoxColumn();
            result = new SpreadSheetIconTextBoxColumn();
            result.Name = name;
            result.HeaderText = header;
            result.ValueType = type;
            if (type == typeof(string)) { result.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; }
            else if (type == typeof(double) || type == typeof(int)) { result.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; }
            else if (type == typeof(DateTime)) { result.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            else { result.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }

            if (type == typeof(int)) { result.DefaultCellStyle.Format = "N0"; }
            result.DefaultCellStyle.Padding = this.DefaultCellStyle.Padding;

            Columns.Insert(ColumnCount, result);
            insertedColumns.Add(result);
            return result;
        }

        public void ClearInsertedColumns()
        {
            while (insertedColumns.Count > 0)
            {
                Columns.Remove(insertedColumns[0]);
            }
        }

        //public DataGridViewColumn[] GetColumnsExcept(DataGridViewColumn[] exceptColumns)
        //{
        //    List<DataGridViewColumn> result = new List<DataGridViewColumn>();

        //    foreach (DataGridViewColumn gridColumn in Columns)
        //    {
        //        if (!gridColumn.Visible) continue;

        //        if (!(exceptColumns).Contains(gridColumn))
        //        {
        //            result.Add(gridColumn);
        //        }
        //    }

        //    return result.ToArray();
        //}

        //public DataGridViewColumn[] GetColumnsExcept(DataGridViewColumn exceptColumn)
        //{
        //    return GetColumnsExcept(new DataGridViewColumn[] { exceptColumn });
        //}

        public void SetColumnsVisibility(DataGridViewColumn[] gridColumns, bool exceptEmpty)
        {
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                if (exceptEmpty)
                {
                    gridColumn.Visible = !IsEmpty(gridColumn);
                }
                else
                {
                    gridColumn.Visible = true;
                }
            }
        }

        public void SetColumnsVisibility(DataGridViewColumn[] gridColumns)
        {
            SetColumnsVisibility(gridColumns, false);
        }

        public void HideEmptyColumns()
        {
            foreach (DataGridViewColumn gridColumn in Columns)
            {
                gridColumn.Visible = (gridColumn.Visible && !IsEmpty(gridColumn));
            }
        }

        public void ResetColumnsVisibility()
        {
            foreach (DataGridViewColumn gridColumn in Columns)
            {
                gridColumn.Visible = !IsEmpty(gridColumn);
            }
        }

        public void EnsureVisible(DataGridViewColumn gridColumn)
        {
            if (!gridColumn.Visible)
            {
                gridColumn.Visible = true;
                ExpandForm(gridColumn);
            }
            if (CurrentRow != null) CurrentCell = this[gridColumn.Index, CurrentRow.Index];
            Focus();
        }

        private void ExpandForm(DataGridViewColumn gridColumn)
        {
            int possibleWidth = FindForm().Width + gridColumn.Width;
            int space = Screen.FromHandle(FindForm().Handle).WorkingArea.Width - FindForm().Left;
            if (possibleWidth > space)
            {
                FindForm().Width = space;
            }
            else
            {
                FindForm().Width = possibleWidth;
            }
        }

        public void SetRenaming(DataGridViewColumn gridColumn)
        {
            ClickedColumn = gridColumn;

            TextBox textBox = new TextBox();

            textBox.AutoSize = false;
            textBox.KeyPress += textBoxCaption_KeyPress;
            textBox.Leave += textBoxCaption_Leave;

            Rectangle rect = GetColumnDisplayRectangle(gridColumn.Index, true);
            textBox.Bounds = new Rectangle(Location.X + rect.X, Location.Y, rect.Width, base.ColumnHeadersHeight);
            textBox.Text = gridColumn.HeaderText;
            textBox.Tag = gridColumn;

            Parent.Controls.Add(textBox);
            textBox.BringToFront();

            textBox.Focus();
        }

        //public void Duplicate(DataGridViewColumn gridColumn)
        //{
        //    DataGridViewColumn clone = InsertColumn(gridColumn, gridColumn.Index + 1);

        //    foreach (DataGridViewRow gridRow in Rows)
        //    {
        //        this[clone.Index, gridRow.Index].Value = this[gridColumn.Index, gridRow.Index].Value;
        //    }

        //    SetRenaming(clone);
        //}

        public double Sum(DataGridViewColumn gridColumn)
        {
            if (!gridColumn.ValueType.IsDoubleConvertible())
                throw new FormatException("Wrong value type");

            double sum = .0;

            if (gridColumn.DataGridView == null) return sum;

            foreach (DataGridViewRow gridRow in gridColumn.DataGridView.Rows)
            {
                if (gridRow.IsNewRow) continue;

                if (IsHidden(gridRow)) continue;
                
                if (gridRow.Cells[gridColumn.Index].Value == null) continue;
                
                sum += gridRow.Cells[gridColumn.Index].Value.ToDouble();
            }

            return sum;
        }

        public string[] StringValues(DataGridViewColumn gridColumn)
        {
            List<string> values = new List<string>();

            if (gridColumn.DataGridView == null) return values.ToArray();

            foreach (DataGridViewRow gridRow in gridColumn.DataGridView.Rows)
            {
                if (gridRow.IsNewRow) continue;

                if (IsHidden(gridRow)) continue;

                if (gridRow.Cells[gridColumn.Index].Value == null) continue;

                values.Add(gridRow.Cells[gridColumn.Index].Value.ToString());
            }

            return values.ToArray();
        }

        #endregion

        #region Row methods

        public List<DataGridViewRow> GetVisibleRows()
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();
            foreach (DataGridViewRow gridRow in Rows)
            {
                if (gridRow.IsNewRow) continue;
                if (IsHidden(gridRow)) continue;
                result.Add(gridRow);
            }
            return result;
        }

        public void ClearEmptyRows()
        {
            this.CancelEdit();
            
            for (int i = 0; i < RowCount; i++)
            {
                if (Rows[i].IsNewRow) continue;

                if (IsEmpty(Rows[i]))
                {
                    if (rowRemoving == null)
                    {
                        //this.BeginInvoke(new MethodInvoker(() => { Rows.RemoveAt(i); }));
                        //this.BeginInvoke(new Action(() => { Rows.RemoveAt(i); }));
                        Rows.RemoveAt(i);
                    }
                    else
                    {
                        rowRemoving.Invoke(Rows[i], new DataGridViewRowEventArgs(Rows[i]));
                    }
                    i--;
                }
            }
        }

        public bool IsHidden(int i)
        {
            return IsHidden(Rows[i]);
        }

        public bool IsHidden(DataGridViewRow gridRow)
        {
            if (gridRow.IsNewRow) return false;

            if (AllowUserToHideRows)
            {
                return !(bool)gridRow.Cells[RowVisibilityColumn.Index].Value;
            }
            else
            {
                return !gridRow.Visible;
            }
        }

        public void SetHidden(int i)
        {
            SetHidden(Rows[i]);
        }

        public void SetHidden(DataGridViewRow gridRow)
        {
            if (gridRow.IsNewRow) return;

            if (AllowUserToHideRows)
            {
                gridRow.Cells[RowVisibilityColumn.Index].Value = false;

                if (UserChangedRowVisibility != null)
                {
                    UserChangedRowVisibility.Invoke(this, new DataGridViewRowEventArgs(gridRow));
                }
            }

            gridRow.Visible = false;
        }

        public void SetVisible(int i)
        {
            SetVisible(Rows[i]);
        }

        public void SetVisible(DataGridViewRow gridRow)
        { 
            if (AllowUserToHideRows)
            {
                gridRow.Cells[RowVisibilityColumn.Index].Value = true;

                if (UserChangedRowVisibility != null)
                {
                    UserChangedRowVisibility.Invoke(this, new DataGridViewRowEventArgs(gridRow));
                }
            }

            gridRow.Visible = true;
        }

        #endregion

        #region Cell methods

        private void ClearCellValue(DataGridViewCell gridCell)
        {
            if (gridCell.OwningColumn.ReadOnly) return;

            if (cellValueDeleting != null)
            {
                cellValueDeleting.Invoke(gridCell,
                    new DataGridViewCellEventArgs(gridCell.ColumnIndex,
                        gridCell.RowIndex));
            }
            else
            {
                gridCell.Value = null;
            }
        }

        public DataGridViewCell FirstClearCell()
        {
            foreach (DataGridViewRow gridRow in Rows)
            {
                foreach (DataGridViewColumn gridColumn in Columns)
                {
                    if (!gridColumn.Visible) continue;
                    if (gridRow.Cells[gridColumn.Index].Value != null) continue;
                    return gridRow.Cells[gridColumn.Index];
                }
            }
            return Rows[RowCount - 1].Cells[-1];
        }

        public DataGridViewCell FirstClearCell(DataGridViewColumn dataColumn)
        {
            foreach (DataGridViewRow gridRow in Rows)
                if (gridRow.Cells[dataColumn.Index].Value == null) return gridRow.Cells[dataColumn.Index];
            return Rows[dataColumn.DataGridView.RowCount - 1].Cells[dataColumn.Index];
        }

        #endregion

        #region Finding empty cells

        public bool IsEmpty(DataGridViewRow gridRow)
        {
            foreach (DataGridViewColumn gridColumn in Columns)
            {
                if (gridColumn.Visible && gridRow.Cells[gridColumn.Index].Value != null)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsEmpty(DataGridViewColumn gridColumn)
        {
            foreach (DataGridViewRow gridRow in Rows)
            {
                if (gridColumn.DataGridView[gridColumn.Index, gridRow.Index].Value != null)
                {
                    return false;
                }
            }

            return true;
        }

        public bool ContainsEmptyCells(DataGridViewColumn gridColumn)
        {
            foreach (DataGridViewRow gridRow in Rows)
            {
                if (gridColumn.DataGridView[gridColumn.Index, gridRow.Index].Value == null)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        private void ResetDisplay()
        {
            Display = null;
        }

        public void StartProcessing(int max, string process)
        {
            if (Display != null) Display.StartProcessing(max, process);
            Enabled = false;
        }

        public void StopProcessing()
        {
            if (Display != null) Display.StopProcessing();
            Enabled = true;
        }

        #endregion



        #region Interface logics

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            OpenFilter();
        }

        private void buttonSheet_Click(object sender, EventArgs e)
        {
            contextTable.Show((Control)sender, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void checkBoxHider_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHider.ContainsFocus)
            {
                foreach (DataGridViewRow gridRow in Rows)
                {
                    switch (checkBoxHider.CheckState)
                    {
                        case CheckState.Checked:
                            gridRow.Cells[RowVisibilityColumn.Index].Value = true;
                            break;
                        case CheckState.Unchecked:
                            gridRow.Cells[RowVisibilityColumn.Index].Value = false;
                            break;
                    }
                }
            }
        }

        private void checkBoxHider_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        #region Column menu

        private void contextColumn_Opening(object sender, CancelEventArgs e)
        {
            itemRename.Enabled = (AllowColumnRenaming && !ClickedColumn.ReadOnly);
            // If this column is inserted
            if (InsertedColumnCount > 0)
            {
                itemRename.Enabled &= GetInsertedColumns().Contains(ClickedColumn);
                itemDelete.Enabled = GetInsertedColumns().Contains(ClickedColumn);
            }
        }

        private void itemColumnProperties_Click(object sender, EventArgs e)
        {
            ColumnProperties properties = new ColumnProperties(ClickedColumn);
            properties.SetFriendlyDesktopLocation(ClickedColumn);
            properties.ShowDialog();
        }

        private void itemRename_Click(object sender, EventArgs e)
        {
            SetRenaming(ClickedColumn);
        }

        //private void itemDuplicate_Click(object sender, EventArgs e)
        //{
        //    Duplicate(ClickedColumn);
        //}

        private void itemDelete_Click(object sender, EventArgs e)
        {
            Columns.Remove(ClickedColumn);
        }

        #endregion

        private void contextRow_Opening(object sender, CancelEventArgs e)
        {
            itemHide.Enabled = SelectedRows.Count > 0;
            itemSelectAll.Enabled = itemInverse.Enabled = MultiSelect;
        }

        #region Table Menu

        public void ItemSave_Click(object sender, EventArgs e)
        {
            if (FileSystem.InterfaceSheets.SaveDialog.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                this.SaveToFile(FileSystem.InterfaceSheets.SaveDialog.FileName);
            }
        }

        public void ItemReport_Click(object sender, EventArgs e)
        {
            Print(string.Empty).Run();
        }

        public void ItemCopyTable_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.SeparatedValues(Constants.Tab, ModifierKeys.HasFlag(Keys.Shift),
                ModifierKeys.HasFlag(Keys.Control), ModifierKeys.HasFlag(Keys.Alt)));
        }



        private void itemSelectAll_Click(object sender, EventArgs e)
        {
            ClearSelection();

            foreach (DataGridViewRow gridRow in Rows)
            {
                gridRow.Selected = gridRow.Visible;
            }
        }

        private void itemInverse_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in Rows)
            {
                gridRow.Selected = !gridRow.Selected;
            }
        }

        private void itemHide_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in SelectedRows)
            {
                SetHidden(gridRow);
            }

            ClearSelection();

            if (Filter != null)
            {
                Filter.SignChanges();
            }

            this.UpdateStatus();

            if (Filtered != null)
            {
                Filtered.Invoke(sender, e);
            }
        }

        private void itemShowHidden_Click(object sender, EventArgs e)
        {
            if (Filter == null)
            {
                itemShowAll_Click(sender, e);
            }
            else
            {
                Filter.Apply();

                if (Filtered != null)
                {
                    Filtered.Invoke(sender, e);
                }
            }
        }

        public void itemShowAll_Click(object sender, EventArgs e)
        {
            if (Filter == null)
            {
                foreach (DataGridViewRow gridRow in Rows)
                {
                    SetVisible(gridRow);
                }
            }
            else
            {
                Filter.Drop();
                Filter.Close();
            }

            this.UpdateStatus();

            if (Filtered != null)
            {
                Filtered.Invoke(sender, e);
            }
        }

        #endregion

        #region Value Menu

        private void ValueMenu_Opening(object sender, CancelEventArgs e)
        {
            bool containsNotReadOnly = false;

            foreach (DataGridViewCell selectedCell in SelectedCells)
            {
                if (selectedCell.ReadOnly) continue;
                containsNotReadOnly = true;
                break;
            }
            
            itemFilterBy.Visible = IsLog;

            itemPaste.Enabled = 
                itemClear.Enabled = 
                itemSetValue.Enabled =
                containsNotReadOnly;
        }

        private void itemFilterBy_Click(object sender, EventArgs e)
        {
            OpenFilter();

            List<FilterUnit> units = new List<FilterUnit>(Filter.SetFilterUnits);
            List<FilterUnit> newUnits = new List<FilterUnit>();

            foreach (DataGridViewCell gridCell in SelectedCells)
            {
                FilterUnit newUnit = Filter.AddFilter(gridCell.OwningColumn, gridCell.Value);
                newUnits.Add(newUnit);
            }

            foreach (FilterUnit unit in newUnits)
            {
                if (!units.Contains(unit))
                {
                    Filter.Apply();
                    break;
                }
            }
        }

        private void itemClear_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell selectedCell in SelectedCells)
            {
                if (selectedCell.OwningColumn.ReadOnly) continue;
                selectedCell.Value = null;
            }

            if (AutoClearEmptyRows)
            {
                ClearEmptyRows();
            }
        }

        private void itemCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(GetClipboardContent());
        }

        private void itemPaste_Click(object sender, EventArgs e)
        {
            PasteToGrid();
        }

        private void itemSetValue_Click(object sender, EventArgs e)
        {
            string input = string.Empty;

            foreach (DataGridViewCell gridCell in SelectedCells)
            {
                string value = gridCell.FormattedValue.ToString();

                if (value == string.Empty) continue;

                if (input == string.Empty)
                {
                    // setting first not null value
                    input = value;
                    continue;
                }

                // if next value is not the same
                if (input != value)
                {
                    // dropping input and stopping cycle
                    input = string.Empty;
                    break;
                }
            }

            inputDialog.Input = input;
            inputDialog.ShowDialog(FindForm());

            foreach (DataGridViewCell gridCell in SelectedCells)
            {
                gridCell.EnterValue(inputDialog.Input);
            }
        }

        #endregion

        private void textBoxCaption_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((TextBox)sender).HandleInput(e, typeof(string));

            if (e.KeyChar == (char)Keys.Enter)
            {
                CurrentCell = this[ClickedColumn.Index, CurrentRow.Index];
                Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                ((TextBox)sender).Text = (((TextBox)sender).Tag as DataGridViewColumn).HeaderText;
                Focus();
            }
        }

        private void textBoxCaption_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).Visible = false;

            DataGridViewColumn gridColumn = ((TextBox)sender).Tag as DataGridViewColumn;

            if (gridColumn != null)
            {
                string prevCaption = gridColumn.HeaderText;

                if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                {
                    if (IsEmpty(gridColumn))
                    {
                        Columns.Remove(gridColumn);
                    }
                    else
                    {
                        ((TextBox)sender).Visible = true;
                        ((TextBox)sender).Focus();
                        return;
                    }
                }
                else
                {
                    gridColumn.HeaderText = ((TextBox)sender).Text;

                    if (columnRenamed != null)
                    {
                        columnRenamed.Invoke(gridColumn, new GridColumnRenameEventArgs(gridColumn, prevCaption));
                    }
                }
            }

            ((TextBox)sender).Dispose();
        }

        private void editingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((TextBox)sender).HandleInput(e, this.CurrentCell.OwningColumn.ValueType);
        }

        #endregion
    }

    public delegate void GridColumnRenameEventHandler(object sender, GridColumnRenameEventArgs e);

    public class GridColumnRenameEventArgs : EventArgs
    {
        public string PreviousCaption { get; set; }

        public DataGridViewColumn Column { get; set; }

        public GridColumnRenameEventArgs() { }

        public GridColumnRenameEventArgs(DataGridViewColumn dataGridViewColumn, string previousCaption)
        {
            Column = dataGridViewColumn;
            PreviousCaption = previousCaption;
        }
    }

    public class FilterEventArgs : EventArgs
    {
        public DataGridViewColumn[] DataGridViewColumns;
        public object[] Values;
        public BackgroundWorker BackgroundWorker;

        public FilterEventArgs(DataGridViewColumn[] dataGridViewColumns, string[] values,
            BackgroundWorker backgroundWorker)
        {
            DataGridViewColumns = dataGridViewColumns;
            Values = values;
            BackgroundWorker = backgroundWorker;
        }

        public FilterEventArgs(DataGridViewColumn dataGridViewColumn, string value,
            BackgroundWorker backgroundWorker)
        {
            DataGridViewColumns = new DataGridViewColumn[] { dataGridViewColumn };
            Values = new string[] { value };
            BackgroundWorker = backgroundWorker;
        }

        public FilterEventArgs(DataGridViewColumn dataGridViewColumn, int value,
            BackgroundWorker backgroundWorker)
        {
            DataGridViewColumns = new DataGridViewColumn[] { dataGridViewColumn };
            Values = new object[] { value };
            BackgroundWorker = backgroundWorker;
        }
    }

    public enum MenuInsertionMethod
    {
        Below,
        Under
    }
}