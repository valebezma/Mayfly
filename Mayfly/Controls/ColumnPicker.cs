using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mayfly.Controls
{
    public partial class ColumnPicker
    {
        private List<DataGridViewColumn> columnCollection;

        public List<DataGridViewColumn> SelectedColumns
        {
            set; get;
        }

        public List<DataGridViewColumn> UserSelectedColumns
        {
            set
            {
                SelectorControl.SelectedItems.Clear();

                foreach (ListViewItem item in SelectorControl.Items)
                {
                    foreach (DataGridViewColumn gridColumn in value)
                    {
                        if (gridColumn.HeaderText == item.Text)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }

            get
            {
                List<DataGridViewColumn> result = new List<DataGridViewColumn>();

                foreach (ListViewItem item in SelectorControl.SelectedItems)
                {
                    foreach (DataGridViewColumn gridColumn in columnCollection)
                    {
                        if (gridColumn.Name == item.Name)
                        {
                            result.Add(gridColumn);
                            break;
                        }
                    }
                }

                return result;
            }
        }

        public List<DataGridViewColumn> ColumnCollection
        {
            get
            {
                return columnCollection;
            }

            set
            {
                PolluteControl(value);
            }
        }

        private ListView SelectorControl;

        public ColumnPicker(ListView listView)
        {
            columnCollection = new List<DataGridViewColumn>();
            SelectedColumns = new List<DataGridViewColumn>();
            SelectorControl = listView;
            SelectorControl.ItemSelectionChanged += SelectorControl_ItemSelectionChanged;
        }

        private void SelectorControl_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SelectedColumns = UserSelectedColumns;
        }

        public void SelectAll()
        {
            foreach (ListViewItem item in SelectorControl.Items)
            {
                item.Selected = true;
            }
        }

        public bool IsSelected(DataGridViewColumn gridColumn)
        {
            foreach (DataGridViewColumn selectedGridColumn in SelectedColumns)
            {
                if (gridColumn.HeaderText == selectedGridColumn.HeaderText) return true;
            }

            return false;
        }

        public void Add(DataGridViewColumn gridColumn)
        {
            columnCollection.Add(gridColumn);

            ListViewItem item = new ListViewItem();
            item.Name = gridColumn.Name;
            item.Text = gridColumn.HeaderText;

            SelectorControl.Items.Add(item);
        }

        public void AddRange(List<DataGridViewColumn> gridColumns)
        {
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                if (!gridColumn.Visible) continue;
                Add(gridColumn);
            }
        }

        public void PolluteControl(List<DataGridViewColumn> gridColumns)
        {
            SelectorControl.Items.Clear();
            AddRange(gridColumns);

        }

        public void PolluteControl(DataGridViewColumnCollection gridColumns)
        {
            SelectorControl.Items.Clear();
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                if (!gridColumn.Visible) continue;
                Add(gridColumn);
            }
        }
    }
}