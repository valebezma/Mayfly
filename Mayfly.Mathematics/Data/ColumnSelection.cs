using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mayfly.Controls;
using Mayfly.Extensions;

namespace Mayfly.Mathematics
{
    public partial class ColumnSelection : Form
    {
        #region Properties

        private SpreadSheet adapter;

        public SpreadSheet Adapter
        {
            get
            {
                return adapter;
            }

            set
            {
                adapter = value;

                comboBoxValues.DataSource = adapter.GetNumericalColumns();

                foreach (DataGridViewColumn gridColumn in Adapter.GetVisibleColumns())
                {
                    if (!gridColumn.Visible) continue;
                    ListViewItem item = new ListViewItem();
                    item.Name = gridColumn.Name;
                    item.Text = gridColumn.HeaderText;
                    listViewGroupers.Items.Add(item);
                }
            }
        }

        public DataGridViewColumn ValuesColumn
        {
            get
            {
                return comboBoxValues.SelectedItem as DataGridViewColumn;
            }

            set
            {
                comboBoxValues.SelectedItem = value;
            }
        }

        public List<DataGridViewColumn> Groupers
        {
            get
            {
                List<DataGridViewColumn> result = new List<DataGridViewColumn>();
                foreach (ListViewItem item in listViewGroupers.Items)
                {
                    if (item.Selected)
                    {
                        result.Add(Adapter.GetColumn(((ListViewItem)item).Name));
                    }
                }
                return result;
            }

            set
            {
                foreach (DataGridViewColumn gridColumn in value)
                {
                    foreach (ListViewItem item in listViewGroupers.Items)
                    {
                        if (item.Name == gridColumn.Name)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        private ListViewItem dragableItem = null;

        #endregion

        #region Constructors

        public ColumnSelection(SpreadSheet adapter)
        {
            InitializeComponent();
            listViewGroupers.Shine();
            Adapter = adapter;
        }

        #endregion

        #region Interface logics

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void comboBoxArgument_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = comboBoxValues.SelectedIndex != -1;
        }

        private void listViewGroupers_MouseDown(object sender, MouseEventArgs e)
        {
            dragableItem = listViewGroupers.GetItemAt(e.X, e.Y);
            // if the LV is still empty, no item will be found anyway, so we don't have to consider this case
        }

        private void listViewGroupers_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragableItem == null)
                return;

            // Show the user that a drag operation is happening
            Cursor = Cursors.Hand;

            // calculate the bottom of the last item in the LV so that you don't have to stop your drag at the last item
            int lastItemBottom = Math.Min(e.Y, listViewGroupers.Items[listViewGroupers.Items.Count - 1].GetBounds(ItemBoundsPortion.Entire).Bottom - 1);

            // use 0 instead of e.X so that you don't have to keep inside the columns while dragging
            ListViewItem itemOver = listViewGroupers.GetItemAt(0, lastItemBottom);

            if (itemOver == null)
                return;

            Rectangle rectangle = itemOver.GetBounds(ItemBoundsPortion.Entire);
            if (e.Y < rectangle.Top + (rectangle.Height / 2))
            {
                listViewGroupers.LineBefore = itemOver.Index;
                listViewGroupers.LineAfter = -1;
            }
            else
            {
                listViewGroupers.LineBefore = -1;
                listViewGroupers.LineAfter = itemOver.Index;
            }

            // invalidate the LV so that the insertion line is shown
            listViewGroupers.Invalidate();
        }

        private void listViewGroupers_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragableItem == null)
                return;

            try
            {
                // calculate the bottom of the last item in the LV so that you don't have to stop your drag at the last item
                int lastItemBottom = Math.Min(e.Y, listViewGroupers.Items[listViewGroupers.Items.Count - 1].GetBounds(ItemBoundsPortion.Entire).Bottom - 1);

                // use 0 instead of e.X so that you don't have to keep inside the columns while dragging
                ListViewItem itemOver = listViewGroupers.GetItemAt(0, lastItemBottom);

                if (itemOver == null)
                    return;

                Rectangle rectangle = itemOver.GetBounds(ItemBoundsPortion.Entire);

                // find out if we insert before or after the item the mouse is over
                bool insertBefore;
                if (e.Y < rectangle.Top + (rectangle.Height / 2))
                {
                    insertBefore = true;
                }
                else
                {
                    insertBefore = false;
                }

                if (dragableItem != itemOver) // if we dropped the item on itself, nothing is to be done
                {
                    if (insertBefore)
                    {
                        listViewGroupers.Items.Remove(dragableItem);
                        listViewGroupers.Items.Insert(itemOver.Index, dragableItem);
                    }
                    else
                    {
                        listViewGroupers.Items.Remove(dragableItem);
                        listViewGroupers.Items.Insert(itemOver.Index + 1, dragableItem);
                    }
                }

                // clear the insertion line
                listViewGroupers.LineAfter =
                listViewGroupers.LineBefore = -1;

                listViewGroupers.Invalidate();

            }
            finally
            {
                // finish drag&drop operation
                dragableItem = null;
                Cursor = Cursors.Default;
            }
        }

        #endregion
    }
}
