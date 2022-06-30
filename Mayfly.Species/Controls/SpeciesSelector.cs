﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using Mayfly.Extensions;

namespace Mayfly.Species
{
    public partial class SpeciesSelector : Component
    {
        public SpeciesKey Index { get; internal set; }

        private DataGridView grid;
        private Button button;
        private DataGridViewTextBoxColumn column;
        private string columnName = string.Empty;
        private string handEntered = string.Empty;
        private string indexPath = string.Empty;
        private TextBox textBoxValue;
        private bool allowKey;
        private string valueBeforeEditing = string.Empty;

        #region Properties

        [Category("Behaviour"), DefaultValue(null)]
        public DataGridView Grid
        {
            get
            {
                return grid;
            }

            set
            {
                grid = value;

                if (grid != null)
                {
                    grid.EditingControlShowing += EditingControlShowing;
                    grid.Scroll += grid_Scroll;
                    grid.ColumnAdded += grid_CheckColumn;
                    grid.ColumnNameChanged += grid_CheckColumn;
                    
                    if (grid.Columns[ColumnName] != null)
                        grid_CheckColumn(grid, new DataGridViewColumnEventArgs(grid.Columns[ColumnName]));
                }                
            }
        }

        private void grid_Scroll(object sender, ScrollEventArgs e)
        {
            if (listSpc.Visible)
            {
                SetListPosition();
            }
        }

        private void grid_CheckColumn(object sender, DataGridViewColumnEventArgs e)
        {
            if (!string.IsNullOrEmpty(ColumnName) && Grid.Columns[ColumnName] is DataGridViewTextBoxColumn column1)
            {
                Column = column1;
            }
        }

        [Category("Behaviour"), DefaultValue(null)]
        public Button Button
        {
            get
            {
                return button;
            }

            set
            {
                button = value;

                if (Button != null)
                {
                    Button.Click += Button_Click;
                }                
            }
        }

        [Category("Behaviour"), DefaultValue("")]
        public string ColumnName
        {
            get
            {
                return columnName;
            }

            set
            {
                columnName = value;
                if (grid != null && grid.Columns[columnName] != null)
                    grid_CheckColumn(grid, new DataGridViewColumnEventArgs(grid.Columns[columnName]));
            }
        }

        [Category("Behaviour"), DefaultValue(true)]
        public bool AllowKey
        {
            get { return allowKey; }
            set
            {
                allowKey = value;
                toolStripMenuItemKey.Visible = toolStripSeparatorKey.Visible = value;
            }
        }

        /// <summary>
        /// Look for species in column when input complete
        /// </summary>
        [Category("Behaviour"), DefaultValue(true)]
        public bool CheckDuplicates
        {
            get;
            set;
        }

        [Category("Behaviour"), DefaultValue(5)]
        public int RecentListCount { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewTextBoxColumn Column
        {
            get
            {
                return column;
            }

            set
            {
                column = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string IndexPath
        {
            get
            {
                return indexPath;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) || !File.Exists(value))
                {
                    if (Button != null) Button.Enabled = false;
                    return;
                }

                indexPath = value;

                Index = new SpeciesKey();
                Index.Read(IndexPath);
                UserSettings.Interface.OpenDialog.InitialDirectory = Path.GetDirectoryName(IndexPath);
                toolStripMenuItemAll.Visible = Index.AllSpeciesCount <= UserSettings.AllowableSpeciesListLength;
                CreateList();
                GetSpeciesList();
            }
        }

        public bool IsButtonEnabled
        {
            get
            {
                return !string.IsNullOrWhiteSpace(IndexPath) && File.Exists(IndexPath);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool AllowSuggest { get; set; }

        #endregion

        public event SpeciesSelectEventHandler SpeciesSelected;

        public event DuplicateFoundEventHandler DuplicateFound;



        public SpeciesSelector()
        {
            InitializeComponent();

            AllowKey = true;
            AllowSuggest = true;

            toolStripMenuItemKey.Click += ToolStripMenuItemKey_Click;
        }

        public SpeciesSelector(IContainer container) : this()
        {
            container.Add(this);        
        }



        #region Methods

        public void CreateList()
        {
            Grid.FindForm().Controls.Add(listSpc);
            listSpc.BringToFront();
            listSpc.Shine(); 
        }

        public void InsertSpeciesHere(SpeciesKey.TaxonRow speciesRow)
        {
            if (Grid.CurrentCell.ColumnIndex == Column.Index)
            {
                if (speciesRow.MajorSynonym != null) {
                    speciesRow = speciesRow.MajorSynonym;
                }

                Grid.CurrentCell.Value = speciesRow;
            }
            
            listSpc.Visible = false;
            RunSelected(speciesRow.Name);
        }

        public DataGridViewRow InsertSpecies(SpeciesKey.TaxonRow typedSpecies)
        {
            int rowIndex = -1;

            //typedSpecies = typedSpecies.ValidSpeciesRow;

            // Try to find species in the list
            foreach (DataGridViewRow gridRow in Grid.Rows)
            {
                if (gridRow.Cells[ColumnName].Value == null) continue;

                if (object.Equals(gridRow.Cells[ColumnName].Value, typedSpecies))
                {
                    rowIndex = gridRow.Index;
                    break;
                }
            }

            // If there is no such species - insert the new row
            if (rowIndex == -1)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(Grid);
                rowIndex = Grid.Rows.Add(gridRow);

                Grid[ColumnName, rowIndex].Value = typedSpecies;
            }

            // Select the new row and its first cell
            Grid.ClearSelection();
            Grid.Rows[rowIndex].Selected = true;
            Grid.CurrentCell = Grid[ColumnName, rowIndex];
            RunSelected(typedSpecies.Name);

            return Grid.Rows[rowIndex];
        }

        private void SetListPosition()
        {
            Point p = Grid.GetPositionInForm();
            p.Offset(Grid.GetCurrentCellRectangle(true).Location);
            p.Offset(0, Grid.GetCurrentCellRectangle(true).Height);
            //Point boxLocation = Grid.FindForm().PointToClient(textBoxValue.Parent.PointToScreen(textBoxValue.Location));
            //Point point = new Point(boxLocation.X - 3, boxLocation.Y + textBoxValue.Height + 3);
            listSpc.Location = p;
            listSpc.Visible = new Rectangle(Grid.FindForm().PointToClient(
                Grid.Parent.PointToScreen(Grid.Location)), Grid.Size).Contains(p);
        }

        private void EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Grid.CurrentCell.OwningColumn == Column)
            {
                if (textBoxValue == null)
                {
                    textBoxValue = e.Control as TextBox;
                    textBoxValue.PreviewKeyDown += textBoxValue_PreviewKeyDown;
                    textBoxValue.TextChanged += textBoxValue_TextChanged;
                    textBoxValue.Leave += textBoxValue_Leave;
                }
                else
                {
                    textBoxValue = e.Control as TextBox;
                }

                //Cell = Grid.CurrentCell;
                valueBeforeEditing = textBoxValue.Text;
            }
        }

        private void GetSpeciesList()
        {
            while (contextMenuStripSpecies.Items.Count > 4)
            {
                contextMenuStripSpecies.Items.RemoveAt(1);
            }

            UpdateRecent();

            toolStripMenuItemAll.DropDownItems.Clear();
            toolStripMenuItemAll.Visible = Index.AllSpeciesCount <= UserSettings.AllowableSpeciesListLength;
            if (Index.AllSpeciesCount <= UserSettings.AllowableSpeciesListLength)
            {
                foreach (SpeciesKey.TaxonRow speciesRow in Index.GetSpeciesRows())
                {
                    ToolStripItem speciesItem = new ToolStripMenuItem
                    {
                        Tag = speciesRow,
                        Text = speciesRow.CommonName
                    };
                    speciesItem.Click += new EventHandler(speciesItem_Click);
                    toolStripMenuItemAll.DropDownItems.Add(speciesItem);
                }

                toolStripMenuItemAll.SortItems();
            }

            foreach (SpeciesKey.TaxonRow taxonRow in Index.GetRootTaxonRows())
            {
                if (taxonRow.IsHigher && !taxonRow.HasChildren) continue;
                contextMenuStripSpecies.Items.Add(getTaxonMenuItem(taxonRow));
                if (contextMenuStripSpecies.Items.Count == UserSettings.AllowableSpeciesListLength) break;
            }
        }

        public void UpdateRecent()
        {
            toolStripMenuItemRecent.DropDownItems.Clear();

            ToolStripDropDownItem[] recentItems = MostUsedRecent();
            //toolStripSeparatorKey.Visible =
            toolStripMenuItemRecent.Visible = recentItems.Length > 0;
            toolStripMenuItemRecent.DropDownItems.AddRange(recentItems);
        }

        public void FindInKey(DataGridViewRow gridRow)
        {
            if (!(gridRow.Cells[ColumnName].Value is string)) return;

            if (Form.ModifierKeys.HasFlag(Keys.Control))
            {
                FindInKey(IndexPath, gridRow);
            }
            else
            {
                if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    FindInKey(UserSettings.Interface.OpenDialog.FileName, gridRow);
                }
            }
        }

        private void FindInKey(string filename, DataGridViewRow gridRow)
        {
            //Display speciesKey = new Display(filename, true);

            //if (gridRow.Cells[ColumnName].Value == null)
            //{
            //    if (speciesKey.ShowDialog() == DialogResult.OK)
            //    {
            //        gridRow.Cells[ColumnName].Value = speciesKey.SelectedSpeciesRow.Species;
            //    }
            //}
            //else
            //{
            //    string species = (string)gridRow.Cells[ColumnName].Value;
            //    speciesKey.ToSpecies(species);

            //    if (speciesKey.SelectedSpeciesRow == null)
            //    {
            //        speciesKey.Visible = false;
            //        if (speciesKey.ShowDialog() == DialogResult.OK)
            //        {
            //            gridRow.Cells[ColumnName].Value = speciesKey.SelectedSpeciesRow.Species;
            //        }
            //    }
            //}
        }

        private void InsertFromKey(string filename)
        {
            //Display speciesKey = new Display(filename, true);

            //if (speciesKey.ShowDialog() == DialogResult.OK)
            //{
            //    InsertSpecies(speciesKey.SelectedSpeciesRow);
            //}
        }

        private ToolStripMenuItem getTaxonMenuItem(SpeciesKey.TaxonRow taxonRow)
        {
            ToolStripMenuItem taxonItem = new ToolStripMenuItem
            {
                Tag = taxonRow,
                Text = taxonRow.CommonName
            };

            taxonItem.Click += new EventHandler(speciesItem_Click);

            foreach (SpeciesKey.TaxonRow derRow in taxonRow.GetTaxonRows())
            {
                if (derRow.IsHigher && !derRow.HasChildren) continue;
                taxonItem.DropDownItems.Add(getTaxonMenuItem(derRow));
            }

            return taxonItem;
        }

        public ListViewItem[] SpeciesItems(string pattern)
        {
            List<ListViewItem> result = new List<ListViewItem>();
    
            SpeciesKey.TaxonRow[] speciesRows = Index.GetSpeciesNameContaining(pattern);

            foreach (SpeciesKey.TaxonRow speciesRow in speciesRows)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = speciesRow.ToString(Column.DefaultCellStyle.Format, null),
                    Tag = speciesRow,
                    ToolTipText = speciesRow.ToolTip.Merge(Constants.Break)
                };
                result.Add(item);
            }

            List<string> genera = new List<string>();

            foreach (SpeciesKey.TaxonRow speciesRow in speciesRows)
            {
                string genus = SpeciesKey.Genus(speciesRow.Name);
                if (genus == null) continue;
                if (genera.Contains(genus)) continue;
                genera.Add(genus);
            }

            foreach (string genus in genera)
            {
                if (!genus.ToUpper().Contains(pattern.ToUpper())) continue;

                ListViewItem item = new ListViewItem
                {
                    Text = string.Format("{0} sp.", genus),
                    ToolTipText = string.Format(Resources.Interface.GenusToolTip, genus)
                };
                result.Add(item);
            }

            result.Sort(new SearchResultSorter(pattern));

            return result.ToArray();
        }

        private ToolStripDropDownItem[] MostUsedRecent()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(UserSettings.Path).OpenSubKey(Path.GetFileNameWithoutExtension(IndexPath));

            if (key == null) return new ToolStripDropDownItem[0];

            List<string> recentSpecies = new List<string>();

            while (recentSpecies.Count < Math.Min(key.ValueCount, RecentListCount))
            {
                int maxUsed = 0;
                string maxUsedSpecies = string.Empty;

                foreach (string value in key.GetValueNames())
                {
                    if (recentSpecies.Contains(value)) continue;
                    int used = (int)key.GetValue(value);
                    if (used > maxUsed)
                    {
                        maxUsedSpecies = value;
                        maxUsed = used;
                    }
                }

                recentSpecies.Add(maxUsedSpecies);
            }

            List<ToolStripDropDownItem> recentSpeciesItems = new List<ToolStripDropDownItem>();

            foreach (string recent in recentSpecies)
            {
                SpeciesKey.TaxonRow speciesRow = Index.FindBySpecies(recent);

                if (speciesRow == null) continue;

                ToolStripDropDownItem speciesItem = new ToolStripMenuItem
                {
                    Tag = speciesRow,
                    Text = speciesRow.CommonName
                };
                speciesItem.Click += new EventHandler(speciesItem_Click);
                recentSpeciesItems.Add(speciesItem);
            }

            return recentSpeciesItems.ToArray();
        }

        public SpeciesKey.TaxonRow Find(string species)
        {
            return Index.FindBySpecies(species);
        }

        private void RunSelected(string species)
        {
            object used = UserSetting.GetValue(UserSettings.Path, Path.GetFileNameWithoutExtension(IndexPath), species, null);

            if (used == null) {
                UserSetting.SetValue(UserSettings.Path, Path.GetFileNameWithoutExtension(IndexPath), species, 1);
            } else {
                UserSetting.SetValue(UserSettings.Path, Path.GetFileNameWithoutExtension(IndexPath), species, (int)used + 1);
            }

            DataGridViewCell gridCell = Grid.CurrentCell;

            if (gridCell.Value == null)
            {
                gridCell.Value = Resources.Interface.UnidentifiedTitle;
            }

            for (int i = 0; i < Grid.RowCount; i++)
            {
                DataGridViewRow gridRow = Grid.Rows[i];

                if (gridRow.IsNewRow)
                {
                    continue;
                }

                if (gridRow == gridCell.OwningRow)
                {
                    continue;
                }

                if (object.Equals(gridCell.Value, gridRow.Cells[Column.Index].Value))
                {
                    if (DuplicateFound != null)
                    {
                        DuplicateFound.Invoke(this, new DuplicateFoundEventArgs(gridCell.OwningRow, gridRow));
                    }

                    break;
                }
            }

            if (species != valueBeforeEditing && SpeciesSelected != null)
            {
                Grid.BeginInvoke(new MethodInvoker(() =>
                {
                    SpeciesSelected.Invoke(this, new SpeciesSelectEventArgs(valueBeforeEditing,
                        species, gridCell));
                }));
            }
        }

        //public AutoExpandResult UpdateIndex(SpeciesKey localIndex, string defaultName, bool inspect)
        //{
        //    if (localIndex == null) return AutoExpandResult.None;

        //    if (Wild.Service.GetReferencePath(this.IndexPath) == null) // File does not exist
        //    {
        //        // Save local reference to specified path

        //        UserSettings.Interface.SaveDialog.FileName = defaultName; // Path.Combine(Mayfly.FileSystem.AppFolder, autoname);

        //        if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            // Setting newly created index as reference for this
        //            localIndex.SaveToFile(UserSettings.Interface.SaveDialog.FileName);
        //            this.IndexPath = UserSettings.Interface.SaveDialog.FileName;
                    
        //            // Return true - reference was created
        //            return AutoExpandResult.Created;
        //        }
        //        else
        //        {
        //            return AutoExpandResult.None;
        //        }
        //    }
        //    else // File already exists
        //    {
        //        // Imports local reference to global with specified visual inspection option

        //        localIndex.ImportTo(this.Index, inspect);
        //        this.Index.SaveToFile(this.IndexPath);
        //        return AutoExpandResult.Expanded;
        //    }
        //}

        public void UpdateIndex(SpeciesKey localIndex, bool inspect)
        {
            localIndex.ImportTo(this.Index, inspect);
            this.Index.SaveToFile(this.IndexPath);
        }

        public void Remove(string species)
        {
            DataGridViewRow gridRow = FindLine(species);
            if (gridRow == null) return;
            Grid.Rows.Remove(gridRow);
        }

        public DataGridViewRow FindLine(string species)
        {
            // Try to find species in the list
            foreach (DataGridViewRow gridRow in Grid.Rows)
            {
                if (gridRow.Cells[ColumnName].Value == null) continue;

                if (object.Equals(gridRow.Cells[ColumnName].Value, species))
                {
                    return gridRow;
                }
            }

            return null;
        }

        #endregion



        #region Entering value logics

        private void textBoxValue_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Down:
                    if (listSpc.Visible && listSpc.Items.Count > 0)
                    {
                        listSpc.Focus();
                        listSpc.Items[0].Selected = true;
                        //listSpc.Items[0].Focused = true;
                        //listSpc.Items[0].EnsureVisible();
                    }
                    break;
                case Keys.Up:
                    if (listSpc.Visible && listSpc.Items.Count > 0)
                    {
                        listSpc.Focus();
                        listSpc.Items[listSpc.Items.Count - 1].Selected = true;
                        listSpc.Items[listSpc.Items.Count - 1].Focused = true;
                        listSpc.Items[listSpc.Items.Count - 1].EnsureVisible();
                    }
                    break;
            }
        }

        private void textBoxValue_Leave(object sender, EventArgs e)
        {
            if (Grid.CurrentCell.OwningColumn != Column) return;
            if (Grid.CurrentCell.OwningRow.Index == -1) return;
            if (Grid.CurrentCell.Value == null) return;

            if (Grid.FindForm().ActiveControl != listSpc)
            {
                listSpc.Visible = false;

                if (Grid.CurrentCell.Value.ToString() != valueBeforeEditing)
                {
                    RunSelected(textBoxValue.Text);
                }
            }
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            if (Index == null) return;

            if (Grid.CurrentCell.ColumnIndex == Column.Index)
            {
                if (!textBoxValue.Text.IsAcceptable())
                {
                    Grid.CurrentCell.Value = null;
                }

                if (AllowSuggest && textBoxValue.Text.Length > 1)
                {
                    AllowSuggest = false;
                    listLoader.RunWorkerAsync(textBoxValue.Text);
                }
            }
        }

        #endregion



        #region Species list logics

        private void listLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SpeciesItems((string)e.Argument);
        }

        private void listLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ListViewItem[] result = e.Result as ListViewItem[];
            if (result.Length > 0)
            {
                foreach (ListViewItem item in listSpc.Items)
                {
                    if (!result.Contains(item))
                    {
                        listSpc.Items.Remove(item);
                    }
                }

                foreach (ListViewItem item in result)
                {
                    if (!listSpc.Items.Contains(item))
                    {
                        listSpc.Items.Add(item);
                    }
                }

                if (!listSpc.Visible)
                {
                    SetListPosition();
                    listSpc.Visible = true;
                }

                listSpc.Width = Column.Width;
                if (result.Length > 4)
                {
                    listSpc.Columns[0].Width = listSpc.Width - 2 -
                         SystemInformation.VerticalScrollBarWidth;
                }
                else
                {
                    listSpc.Columns[0].Width = listSpc.Width - 2;
                }

            }
            else
            {
                listSpc_Leave(listSpc, new EventArgs());
            }
            AllowSuggest = true;
        }

        private void listSpc_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                listSpc.Visible = false;
            }

            if (e.KeyCode == Keys.Escape)
            {
                listSpc.Visible = false;
                Grid.CurrentCell.Value = valueBeforeEditing;
            }
        }

        private void listSpc_ItemActivate(object sender, EventArgs e)
        {
            if (listSpc.SelectedItems.Count == 0) return;

            SpeciesKey.TaxonRow selectedRow = listSpc.SelectedItems[0].Tag as SpeciesKey.TaxonRow;

            if (selectedRow == null)
            {
                selectedRow = Index.Taxon.AddTaxonRow(91, 0, listSpc.SelectedItems[0].Text, null, listSpc.SelectedItems[0].Text, null, null) as SpeciesKey.TaxonRow;
            }

            if (this.CheckDuplicates)
            {
                int rowIndex = -1;

                // Try to find species in the list
                foreach (DataGridViewRow gridRow in Grid.Rows)
                {
                    if (gridRow.Cells[ColumnName].Value == null) continue;

                    if (gridRow.Cells[ColumnName] == Grid.CurrentCell) continue;

                    if (object.Equals(gridRow.Cells[ColumnName].Value, selectedRow))
                    {
                        rowIndex = gridRow.Index;
                        break;
                    }
                }

                if (rowIndex == -1)
                {
                    InsertSpeciesHere(selectedRow);
                }
                else
                {
                    // First - clear current cell
                    listSpc.Visible = false;
                    Grid.CurrentCell.Value = null;
                    Grid.Rows.RemoveAt(Grid.CurrentCell.RowIndex);

                    // Second - select already in table row
                    Grid.ClearSelection();
                    Grid.Rows[rowIndex].Selected = true;
                    Grid.CurrentCell = Grid[ColumnName, rowIndex];
                    RunSelected(selectedRow.Name);
                }
            }
            else
            {
                InsertSpeciesHere(selectedRow);
            }
        }

        private void listSpc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSpc.SelectedItems.Count > 0)
            {
                AllowSuggest = false;
                Grid.CurrentCell.Value = listSpc.SelectedItems[0].Tag as SpeciesKey.TaxonRow;
                AllowSuggest = true;
            }
        }

        private void listSpc_VisibleChanged(object sender, EventArgs e)
        {
            if (listSpc.Visible)
            {
            }
            else
            {
                AllowSuggest = true;
                if (Grid.FindForm().ActiveControl == textBoxValue) { }
                else
                {
                    Grid.Focus();
                }
            }
        }

        private void listSpc_Enter(object sender, EventArgs e)
        {
            if (Grid.CurrentCell.Value == null)
            {
                handEntered = string.Empty;
            }
            else
            {
                handEntered = Grid.CurrentCell.Value.ToString();
            }
        }

        private void listSpc_Leave(object sender, EventArgs e)
        {
            listSpc.Visible = false;
        }

        #endregion



        #region Button and menu logics

        private void Button_Click(object sender, EventArgs e)
        {
            contextMenuStripSpecies.Show(Button, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void speciesItem_Click(object sender, EventArgs e)
        {
            InsertSpecies((SpeciesKey.TaxonRow)((ToolStripMenuItem)sender).Tag);
        }

        private void ToolStripMenuItemKey_Click(object sender, EventArgs e)
        {
            if (Form.ModifierKeys.HasFlag(Keys.Control))
            {
                InsertFromKey(IndexPath);
            }
            else
            {
                if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    InsertFromKey(UserSettings.Interface.OpenDialog.FileName);
                }
            }
        }

        #endregion
    }

    public class SpeciesSelectEventArgs : EventArgs
    {
        public string OriginalValue { get; set; }

        public string SpeciesName { get; set; }

        public DataGridViewColumn Column { get; set; }

        public DataGridViewRow Row { get; set; }

        public SpeciesSelectEventArgs(string originalSpc, string spc, DataGridViewCell gridCell)
        {
            OriginalValue = originalSpc;
            SpeciesName = spc;
            Column = gridCell.OwningColumn;
            Row = gridCell.OwningRow;
        }

        public DataGridViewCell GetCell()
        {
            return Column.DataGridView[Column.Index, Row.Index];
        }
    }

    public delegate void SpeciesSelectEventHandler(object sender, SpeciesSelectEventArgs e);

    public class DuplicateFoundEventArgs : EventArgs
    {
        public DataGridViewRow EditedRow { get; set; }

        public DataGridViewRow DuplicateRow { get; set; }

        public DuplicateFoundEventArgs(DataGridViewRow editedRow, DataGridViewRow duplicateRow)
        {
            EditedRow = editedRow;
            DuplicateRow = duplicateRow;
        }
    }

    public delegate void DuplicateFoundEventHandler(object sender, DuplicateFoundEventArgs e);

    public enum AutoExpandResult
    {
        Created,
        Expanded,
        None
    }
}
