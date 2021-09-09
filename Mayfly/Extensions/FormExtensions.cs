using AeroWizard;
using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Globalization;

namespace Mayfly.Extensions
{
    public static class FormExtensions
    {
        #region Placing form to the right and to the left of control 

        public static void SnapToRight(this Form form, Control control)
        {
            form.StartPosition = FormStartPosition.Manual;
            Point point = control.PointToScreen(new Point(control.Width, 0));
            Size screen = Screen.FromHandle(control.Handle).WorkingArea.Size;
            if (point.X + form.Width > screen.Width) point.X = screen.Width - form.Width - 5;
            if (point.Y + form.Height > screen.Height) point.Y = screen.Height - form.Height - 5;
            form.DesktopLocation = point;
        }

        public static void SnapToLeft(this Form form, Control control)
        {
            form.StartPosition = FormStartPosition.Manual;
            Point point = control.PointToScreen(new Point(-form.Width, 0));
            Size screen = Screen.FromHandle(control.Handle).WorkingArea.Size;
            if (point.X < 5) point.X = 5;
            if (point.Y + form.Height > screen.Height) point.Y = screen.Height - form.Height - 5;
            form.DesktopLocation = point;
        }

        #endregion

        #region Placing form above and to right to point on control

        public static void SetFriendlyDesktopLocation(this Form form, DataGridView dataGrid)
        {
            form.SetFriendlyDesktopLocation(dataGrid, new Point(dataGrid.Width, dataGrid.ColumnHeadersHeight));
        }

        public static void SetFriendlyDesktopLocation(this Form form, DataGridViewCell gridCell)
        {
            Rectangle rect = gridCell.DataGridView.GetCellDisplayRectangle(gridCell.ColumnIndex, gridCell.RowIndex, true);
            form.SetFriendlyDesktopLocation(gridCell.DataGridView, new Point(rect.Right, rect.Bottom));
        }

        public static void SetFriendlyDesktopLocation(this Form form, DataGridViewRow gridRow)
        {
            form.SetFriendlyDesktopLocation(gridRow.DataGridView,
                new Point(gridRow.DataGridView.RowHeadersWidth,
                    gridRow.DataGridView.GetRowDisplayRectangle(gridRow.Index, true).Bottom));
        }

        public static void SetFriendlyDesktopLocation(this Form form, DataGridViewColumn gridColumn)
        {
            form.SetFriendlyDesktopLocation(gridColumn.DataGridView,
                new Point(gridColumn.DataGridView.GetColumnDisplayRectangle(gridColumn.Index, true).Right,
                    gridColumn.DataGridView.ColumnHeadersHeight));
        }

        public static void SetFriendlyDesktopLocation(this Form form, ListViewItem li)
        {
            Rectangle rect = li.ListView.GetItemRect(li.Index, ItemBoundsPortion.Label);
            form.SetFriendlyDesktopLocation(li.ListView, new Point(rect.Right, rect.Bottom));
        }

        public static void SetFriendlyDesktopLocation(this Form form, Control control)
        {
            SetFriendlyDesktopLocation(form, control, FormLocation.AboveRight);
            //form.SetFriendlyDesktopLocation(control, new Point(control.Size));
        }

        public static void SetFriendlyDesktopLocation(this Form form, Control control, FormLocation formLocation)
        {
            switch (formLocation)
            {
                case FormLocation.NextToHost:
                    form.SnapToRight(control);
                    break;
                case FormLocation.AboveRight:
                    form.SetFriendlyDesktopLocation(control, new Point(control.Size));
                    break;
                case FormLocation.Centered:
                    form.SetCenteredLocation(control.FindForm());
                    break;
            }
        }

        public static void SetFriendlyDesktopLocation(this Form form, Control control, Point objectPositionOnControl)
        {
            form.SetAboveRightLocation(control.PointToScreen(objectPositionOnControl));
        }

        public static void SetFriendlyDesktopLocation(this Form form, ContextMenuStrip contextMenuStrip)
        {
            if (contextMenuStrip.Visible)
            {
                form.SetAboveRightLocation(contextMenuStrip.Bounds.Location);
            }
            else
            {
                form.SetFriendlyDesktopLocation(Form.MousePosition);
            }
        }

        public static void SetFriendlyDesktopLocation(this Form form, Point objectPositionOnScreen)
        {
            form.SetAboveRightLocation(objectPositionOnScreen);
        }

        public static void SetMaximumDesktopView(this Form form, int margin)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.DesktopLocation = new Point(margin, margin);
            form.Size = new Size(Screen.GetWorkingArea(form).Width - 2 * margin,
                Screen.GetWorkingArea(form).Height - 2 * margin);
        }

        #endregion

        public static void Expand(this Form form)
        {
            form.Expand(0);
        }

        public static void Expand(this Form form, int margin)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Left = form.Top = margin;
            form.Width = Screen.PrimaryScreen.WorkingArea.Width - 2 * margin;
            form.Height = Screen.PrimaryScreen.WorkingArea.Height - 2 * margin;
        }

        public static void SetFriendlyDesktopLocation(this Form form, Form launchForm, FormLocation formLocation)
        {
            switch (formLocation)
            {
                case FormLocation.NextToHost:
                    form.SetNextToHostLocation(launchForm);
                    break;
                case FormLocation.AboveRight:
                    form.SetAboveRightLocation(launchForm.PointToScreen(new Point(launchForm.Size)));
                    break;
                case FormLocation.Centered:
                    form.SetCenteredLocation(launchForm);
                    break;
            }
        }

        public static Point GetNextToHostLocation(this Form form, Form hostForm)
        {
            Point point = new Point(hostForm.DesktopLocation.X + hostForm.Width, hostForm.DesktopLocation.Y);
            Size screen = Screen.FromHandle(hostForm.Handle).WorkingArea.Size;
            if (point.X + form.Width > screen.Width) point.X = screen.Width - form.Width - 5;
            if (point.Y + form.Height > screen.Height) point.Y = screen.Height - form.Height - 5;
            return point;
        }

        public static Point GetCenteredLocation(this Form form, Form hostForm)
        {
            Point point = new Point(hostForm.DesktopLocation.X + hostForm.Width / 2 - form.Width / 2,
                hostForm.DesktopLocation.Y + hostForm.Height / 2 - form.Height / 2);
            Size screen = Screen.FromHandle(hostForm.Handle).WorkingArea.Size;
            if (point.X + form.Width > screen.Width) point.X = screen.Width - form.Width - 5;
            if (point.Y + form.Height > screen.Height) point.Y = screen.Height - form.Height - 5;
            return point;
        }

        public static Point GetAboveRightLocation(this Form form, Point objectLocationOnDesktop)
        {
            Point point = new Point(objectLocationOnDesktop.X + 15, objectLocationOnDesktop.Y + 15);
            Size screen = Screen.FromHandle(form.Handle).WorkingArea.Size;
            if (point.X + form.Width > screen.Width) point.X = screen.Width - form.Width - 5;
            if (point.Y + form.Height > screen.Height) point.Y = screen.Height - form.Height - 5;
            return point;
        }

        private static Point GetFreePosition(this Form form, Point point)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f == form) continue;

                if (f.DesktopLocation.Equals(point))
                {
                    Point freeLocation = form.GetNextToHostLocation(f);
                    if (freeLocation.Equals(point)) return form.GetAboveRightLocation(freeLocation);
                    else return freeLocation;
                }
            }

            return point;
        }

        public static void SetNextToHostLocation(this Form form, Form hostForm)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.DesktopLocation = form.GetFreePosition(form.GetNextToHostLocation(hostForm));
        }

        public static void SetCenteredLocation(this Form form, Form hostForm)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.DesktopLocation = form.GetFreePosition(form.GetCenteredLocation(hostForm));
        }

        public static void SetAboveRightLocation(this Form form, Point objectLocationOnDesktop)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.DesktopLocation = form.GetFreePosition(form.GetAboveRightLocation(objectLocationOnDesktop));
        }

        public static Point GetPositionInForm(this Control ctrl)
        {
            Point p = ctrl.Location;
            Control parent = ctrl.Parent;
            while (!(parent is Form))
            {
                p.Offset(parent.Location.X, parent.Location.Y);
                parent = parent.Parent;
            }
            return p;
        }



        public static void ResetText(this Form form, string title, bool icon)
        {
            form.Text = title;

            if (icon)
            {
                form.Icon = Service.GetIcon(Application.ExecutablePath, 0, new Size(16, 16));
            }
        }

        public static void ResetText(this Form form, string filename, string applicationName)
        {
            form.ResetText(string.Format("{0} — {1} ({2})", System.IO.Path.GetFileNameWithoutExtension(filename), applicationName, License.AllowedFeaturesLevel), true);
        }

        public static void ResetText(this Form form, string applicationName)
        {
            form.ResetText(string.Format("{0} ({1})", applicationName, License.AllowedFeaturesLevel), true);
        }


        public static void SetMenuIcons(this MenuStrip menuStrip)
        {
            foreach (ToolStripMenuItem j in menuStrip.Items)
            {
                foreach (ToolStripItem i in j.DropDownItems)
                {
                    if (i is ToolStripMenuItem m)
                    {
                        if (m.HasDropDownItems) continue;

                        switch (m.Name)
                        {
                            case "menuItemNew":
                                m.Image = Pictogram.New;
                                break;

                            case "menuItemOpen":
                                m.Image = Pictogram.Open;
                                break;

                            case "menuItemSave":
                                m.Image = Pictogram.Save;
                                break;

                            //case "menuItemSave":
                            //    m.Image = Pictogram.Save;
                            //    break;

                            case "menuItemPrint":
                                m.Image = Pictogram.Print;
                                break;

                            case "menuItemPreview":
                                m.Image = Pictogram.Preview;
                                break;

                            case "menuItemSettings":
                                m.Image = Pictogram.Settings;
                                break;
                        }
                    }
                }
            }
        }



        public static string GetLocalizedText(this Control ctrl)
        {
            ResourceManager resources = new ResourceManager(ctrl.FindForm().GetType());
            return resources.GetString(ctrl.Name + ".Text");
        }

        public static void Sync(this Form form, Form original)
        {
            form.WindowState = original.WindowState;

            if (original.Visible && 
                original.WindowState == FormWindowState.Normal) {
                form.DesktopLocation = original.DesktopLocation;
                form.StartPosition = FormStartPosition.Manual;
                form.Size = original.Size;
            }
        }

        public static void Replace(this Form form, Form original)
        {
            form.Sync(original);

            form.Resize += new EventHandler(
                (object sender, EventArgs e) => { original.Sync(form); });
            form.LocationChanged += new EventHandler(
                (object sender, EventArgs e) => { original.Sync(form); });
            //original.VisibleChanged += new EventHandler((object sender, EventArgs e) => { form.Visible = original.Visible; });


            foreach (Control c in form.Controls.OfType<WizardControl>())
            {
                foreach (Control d in original.Controls.OfType<WizardControl>())
                {
                    ((WizardControl)c).Title = ((WizardControl)d).Title;
                    break;
                }
                break;
            }

            form.Show();
            original.Hide();
        }

        public static Form GetForm(this Control ctrl)
        {
            object o = ctrl.Parent;
            return (o is Form) ? (Form)o : GetForm((Control)o);
        }

        //public static void EnsureSelected(this WizardControl ctrl, WizardPage page, bool forward)
        public static void EnsureSelected(this WizardControl ctrl, WizardPage page)
        {
            if (ctrl.SelectedPage == null) return;

            if (ctrl.SelectedPage == page) return;

            //else
            //{
                while (ctrl.SelectedPage != ctrl.Pages[0])
                {
                    ctrl.PreviousPage();
                    if (ctrl.SelectedPage == page) return;
                }
            //}

            //if (forward)
            //{
                while (ctrl.SelectedPage != ctrl.Pages[ctrl.Pages.Count() - 1])
                {
                    ctrl.NextPage();
                    if (ctrl.SelectedPage == page) return;
                }
            //}
        }



        public static void SaveAllCheckStates(this Form form)
        {
            SaveAllCheckStates(form.Controls);
        }

        public static void SaveAllCheckStates(this Control.ControlCollection collection)
        {
            foreach (Control ctrl in collection)
            {
                if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).SaveCheckState();
                }
                else if (ctrl is WizardControl)
                {
                    foreach (WizardPage wzrdPage in ((WizardControl)ctrl).Pages)
                    {
                        SaveAllCheckStates(wzrdPage.Controls);
                    }
                }
                else if (ctrl.Controls.Count > 0)
                {
                    SaveAllCheckStates(ctrl.Controls);
                }
            }
        }

        public static void RestoreAllCheckStates(this Form form)
        {
            RestoreAllCheckStates(form.Controls);
            form.FormClosing += Form_FormClosing;
        }

        private static void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Form)sender).SaveAllCheckStates();
        }

        public static void RestoreAllCheckStates(this Control.ControlCollection collection)
        {
            foreach (Control ctrl in collection)
            {
                if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).RestoreCheckState();
                }
                else if (ctrl is WizardControl)
                {
                    foreach (WizardPage wzrdPage in ((WizardControl)ctrl).Pages)
                    {
                        RestoreAllCheckStates(wzrdPage.Controls);
                    }
                }
                else if (ctrl.Controls.Count > 0)
                {
                    RestoreAllCheckStates(ctrl.Controls);
                }
            }
        }

        public static void SaveCheckState(this CheckBox checkBox)
        {
            UI.SaveCheckState(GetForm(checkBox).Name, checkBox.Name, checkBox.CheckState);
        }

        public static void RestoreCheckState(this CheckBox checkBox)
        {
            checkBox.CheckState = UI.GetCheckState(GetForm(checkBox).Name, checkBox.Name);
        }


        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int SetWindowTheme(IntPtr hWnd, string appName, string partList);

        public static void Shine(this Control control)
        {
            SetWindowTheme(control.Handle, "explorer", null);
        }

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        #region Control extensions

        public static void UpdateStatus(this Label label, int newStatus)
        {
            if (label.FindForm() == null) label.Text = newStatus.ToString();
            else label.Text = newStatus.ToCorrectString(
                new ResourceManager(label.FindForm().GetType()).GetString(label.Name + ".Text"));
        }

        public static void ResetFormatted(this ToolStripStatusLabel label, object value)
        {
            if (label.GetCurrentParent() == null || label.GetCurrentParent().FindForm() == null) label.Text = value.ToString();
            else label.Text = string.Format(
                new ResourceManager(label.GetCurrentParent().FindForm().GetType()).GetString(label.Name + ".Text"),
                value);
        }

        public static void ResetTitle(this AeroWizard.WizardControl wizard, object value)
        {
            if (wizard.FindForm() == null) wizard.Text = value.ToString();
            else wizard.Title = string.Format(
                new ResourceManager(wizard.FindForm().GetType()).GetString(wizard.Name + ".Title"),
                value);
        }

        public static void ResetFormatted(this Form form, object value)
        {
            form.Text = string.Format(new ResourceManager(form.GetType()).GetString("$this.Text"), value);
        }

        public static void ResetFormatted(this Control label, params object[] values)
        {
            if (label.FindForm() == null)
            {
                label.Text = values.Merge();
            }
            else
            {
                string resString = new ResourceManager(label.FindForm().GetType()).GetString(label.Name + ".Text");
                label.Text = resString == null ? values.Merge() : string.Format(resString, values);
            }
        }

        //public static void ResetFormatted(this Control label, object value1, object value2)
        //{
        //    if (label.FindForm() == null) label.Text = string.Empty;
        //    else label.Text = string.Format(
        //        new ResourceManager(label.FindForm().GetType()).GetString(label.Name + ".Text"),
        //        value1, value2);
        //}

        //public static void ResetFormatted(this Control label, object value1, object value2, object value3)
        //{
        //    if (label.FindForm() == null) label.Text = string.Empty;
        //    else label.Text = string.Format(
        //        new ResourceManager(label.FindForm().GetType()).GetString(label.Name + ".Text"),
        //        value1, value2, value3);
        //}

        public static void ResetFormatted(this DataGridViewColumn column, string value)
        {
            if (column.DataGridView.FindForm() == null) column.HeaderText = value;
            else column.HeaderText = string.Format(
                new ResourceManager(column.DataGridView.FindForm().GetType()).
                GetString(column.Name + ".HeaderText"), value);
        }

        //public static void ResetFormatted(this TaskDialogs.TaskDialog taskDialog, object value)
        //{
        //    taskDialog.WindowTitle = string.Format(new ResourceManager(((Form)taskDialog.Container).GetType()).GetString(taskDialog + ".WindowTitle"), value);
        //}

        public static void ScrollToEnd(this TextBox textBox)
        {
            const int EM_LINESCROLL = 0x00B6;
            //const int SB_HORZ = 0;
            const int SB_VERT = 1;

            SetScrollPos(textBox.Handle, SB_VERT, textBox.Lines.Length - 1, true);
            SendMessage(textBox.Handle, EM_LINESCROLL, 0, textBox.Lines.Length - 1);
        }

        public static void SortItems(this ToolStripMenuItem itemParent)
        {
            ArrayList itemList = new ArrayList();

            itemList.AddRange(itemParent.DropDownItems);
            IComparer myComparer = new ToolStripItemListArray.ToolStripItemComparer();
            itemList.Sort(myComparer);

            itemParent.DropDownItems.Clear();

            foreach (ToolStripItem item in itemList)
            {
                itemParent.DropDownItems.Add(item);
            }
        }

        #region ListView logics

        public static ListViewItem GetItem(this ListView listView, string name)
        {
            foreach (ListViewItem item in listView.Items)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }

        public static ListViewItem CreateItem(string itemName, string itemText)
        {
            ListViewItem result = new ListViewItem();
            result.Name = itemName;
            result.Text = itemText;
            return result;
        }

        public static ListViewItem CreateItem(this ListView listView, string itemName)
        {
            return listView.CreateItem(itemName, itemName);
        }

        public static ListViewItem CreateItem(this ListView listView, string itemName, string itemText)
        {
            ListViewItem result = listView.GetItem(itemName);

            if (result == null)
            {
                result = new ListViewItem();
                listView.Items.Add(result);
            }

            result.SubItems.Clear();
            result.Name = itemName;
            result.Text = itemText;

            return result;
        }

        //public static ListBoxItem CreateItem(this ListView listView, string itemName, string itemText)
        //{
        //    ListViewItem result = listView.GetItem(itemName);
        //    if (result == null)
        //    {
        //        result = new ListViewItem();
        //        listView.Items.Add(result);
        //    }
        //    result.SubItems.Clear();
        //    result.Name = itemName;
        //    result.Text = itemText;
        //    return result;
        //}

        public static ListViewItem CreateItem(this ListView listView, string itemName, string itemText, int imageIndex)
        {
            ListViewItem result = listView.CreateItem(itemName, itemText);
            result.ImageIndex = imageIndex;
            return result;
        }

        public static ListViewGroup GetGroup(this ListView listView, string name)
        {
            foreach (ListViewGroup group in listView.Groups)
            {
                if (group.Name == name)
                {
                    return group;
                }
            }
            return null;
        }

        public static ListViewGroup CreateGroup(this ListView listView, string name)
        {
            return listView.CreateGroup(name, name);
        }

        public static ListViewGroup CreateGroup(this ListView listView, string name, string header)
        {
            ListViewGroup result = listView.GetGroup(name);

            if (result == null)
            {
                result = new ListViewGroup();
                listView.Groups.Add(result);
            }

            result.Header = header;
            result.Name = name;
            return result;
        }

        public static void UpdateItem(this ListViewItem item, object[] values)
        {
            while (item.SubItems.Count > 1)
            {
                item.SubItems.RemoveAt(1);
            }

            foreach (object value in values)
            {
                item.SubItems.Add(value.ToString());
            }
        }

        public static ListViewItem GetHoveringItem(this ListView listView, int X, int Y)
        {
            return listView.HitTest(listView.PointToClient(new Point(X, Y))).Item;
        }

        public static int GetID(this ListViewItem item)
        {
            try
            {
                return int.Parse(item.Name);
            }
            catch
            {
                return 0;
            }
        }

        public static int GetID(this ListView listView)
        {
            if (listView.SelectedItems.Count > 0)
                return listView.SelectedItems[0].GetID();
            else return 0;
        }

        public static void MakeSortable(this ListView listView)
        {
            listView.MakeSortable(0, SortOrder.Ascending);
        }

        public static void MakeSortable(this ListView listView, int defaultColumn, SortOrder defaultOrder)
        {
            listView.ListViewItemSorter = new ListViewColumnSorter();
            ((ListViewColumnSorter)listView.ListViewItemSorter).SortColumn = defaultColumn;
            ((ListViewColumnSorter)listView.ListViewItemSorter).Order = defaultOrder;
            listView.ColumnClick += listView_ColumnClick;
        }

        static void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnSorter lvis = (ListViewColumnSorter)((ListView)sender).ListViewItemSorter;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvis.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvis.Order == SortOrder.Ascending)
                {
                    lvis.Order = SortOrder.Descending;
                }
                else
                {
                    lvis.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvis.SortColumn = e.Column;
                lvis.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            ((ListView)sender).Sort();
        }

        #endregion

        #region TreeView logics

        public static TreeNode GetNode(this TreeView treeView, string name)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                if (node.Name == name)
                {
                    return node;
                }

                TreeNode childNode = node.GetNode(name);

                if (childNode != null)
                {
                    return childNode;
                }
            }
            return null;
        }

        public static TreeNode GetNode(this TreeNode treeNode, string name)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Name == name)
                {
                    return node;
                }

                TreeNode childNode = node.GetNode(name);

                if (childNode != null)
                {
                    return childNode;
                }
            }
            return null;
        }

        public static TreeNode GetHoveringNode(this TreeView treeView, int X, int Y)
        {
            return treeView.HitTest(treeView.PointToClient(new Point(X, Y))).Node;
        }

        public static void AutoScroll(this TreeView treeView)
        {
            // Set a constant to define the autoscroll region
            const Single scrollRegion = 20;

            // See where the cursor is
            Point pt = treeView.PointToClient(Cursor.Position);

            // See if we need to scroll up or down
            if ((pt.Y + scrollRegion) > treeView.Height)
            {
                // Call the API to scroll down
                SendMessage(treeView.Handle, (int)277, (int)1, 0);
            }
            else if (pt.Y < (treeView.Top + scrollRegion))
            {
                // Call thje API to scroll up
                SendMessage(treeView.Handle, (int)277, (int)0, 0);
            }
        }

        #endregion

        public static void CenterWith(this Control control, Control bigger)
        {
            control.Left = bigger.Left + bigger.Width / 2 - control.Width / 2;
        }

        public static void SetNavigation(this WizardPage page, bool isFree)
        {
            page.AllowBack = page.AllowNext = isFree;

            //if (isFree)
            //{
            //    page.FindForm().Cursor = Cursors.Default;
            //}
            //else
            //{
            //    page.FindForm().Cursor = Cursors.WaitCursor;
            //}
        }

        public static Button GetFinishButton(this WizardControl wizardControl)
        {
            return (Button)wizardControl.Controls[2].Controls[1];
        }

        public static void SetNullValue(this DataGridViewCell gridCell, object nullValue)
        {
            if (gridCell.DataGridView == null || !gridCell.DataGridView.InvokeRequired)
            {
                gridCell.Style.NullValue = nullValue;
            }
            else
            {
                NullValueSetEventHandler ageSetter = new NullValueSetEventHandler(SetNullValue);
                gridCell.DataGridView.Invoke(ageSetter, new object[] { gridCell, nullValue });
            }
        }

        private delegate void NullValueSetEventHandler(DataGridViewCell gridCell, object nullValue);

        #endregion

        #region Handling input

        public static void HandleInput(this ComboBox comboBox, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Back)
            {
                comboBox.SelectedIndex = -1;
            }
        }

        public static void HandleInput(this Control control, KeyPressEventArgs e, Type valueType)
        {
            if (valueType == typeof(double) &&
                e.KeyChar.ToString() == CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)
            {
                e.KeyChar = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                //control.Text = control.Text.Replace('.', ',');
            }

            InputVariant inputVariant = control.AllowInput(e.KeyChar, valueType);

            if (inputVariant != InputVariant.Allow)
            {
                ToolTip toolTip = new ToolTip();
                string instruction = string.Empty;
                switch (inputVariant)
                {
                    case InputVariant.DecimalRepeat:
                        toolTip.ToolTipTitle = Mayfly.Resources.Interface.InputDecimalRepeat;
                        instruction = String.Format(Mayfly.Resources.Interface.InputDecimalRepeatInstruction, e.KeyChar);
                        break;
                    case InputVariant.NotNumber:
                        toolTip.ToolTipTitle = Mayfly.Resources.Interface.InputNotNumber;
                        instruction = String.Format(Mayfly.Resources.Interface.InputNotNumberInstruction, e.KeyChar);
                        break;
                    case InputVariant.Other:
                        toolTip.ToolTipTitle = Mayfly.Resources.Interface.InputOther;
                        instruction = String.Format(Mayfly.Resources.Interface.InputOtherInstruction, e.KeyChar);
                        break;
                }
                toolTip.Show(instruction, control, control.Width / 2, control.Height, 1500);
                Mayfly.Service.PlaySound(Resources.Sounds.StandardSound);
            }

            e.Handled = inputVariant != InputVariant.Allow;
        }

        public static InputVariant AllowInput(this Control control, char symbol, Type valueType)
        {
            if (symbol == (char)Keys.Back)
            {
                return InputVariant.Allow;
            }

            if (valueType == typeof(string))
            {
                if (Constants.Forbidden.Contains(symbol))
                {
                    return InputVariant.Other;
                }
                else
                {
                    return InputVariant.Allow;
                }
            }

            if (valueType == typeof(int))
            {
                if (Constants.Numbers.Contains(symbol))
                {
                    return InputVariant.Allow;
                }
                else
                {
                    return InputVariant.NotNumber;
                }
            }

            if (valueType == typeof(double))
            {
                // If symbol is decinal separator
                if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.Contains(symbol) ||
                    CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator.Contains(symbol))
                {
                    // If separator is already in value
                    if (control.Text.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) ||
                        control.Text.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator))
                    {
                        // If separator is in selected part
                        if (control is TextBoxBase && (((TextBoxBase)control).SelectedText.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) ||
                                ((TextBoxBase)control).SelectedText.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)))
                        {
                            return InputVariant.Allow;
                        }
                        else
                        {
                            return InputVariant.DecimalRepeat;
                        }
                    }
                    else // If it is first instance of separator
                    {
                        return InputVariant.Allow;
                    }
                }
                else // If symbol is digit
                {
                    if (Constants.Numbers.Contains(symbol))
                    {
                        return InputVariant.Allow;
                    }
                    else
                    {
                        return InputVariant.NotNumber;
                    }
                }
            }

            return InputVariant.Allow;
        }

        #endregion
    }

    public enum FormLocation
    {
        NextToHost,
        AboveRight,
        Centered
    }

    public enum InputVariant
    {
        Allow,
        NotNumber,
        DecimalRepeat,
        Other
    }

    public class ToolStripItemListArray
    {
        public class ToolStripItemComparer : IComparer
        {
            int IComparer.Compare(Object x, Object y)
            {
                return ((new CaseInsensitiveComparer()).Compare(
                    ((ToolStripItem)x).Text,
                    ((ToolStripItem)y).Text));
            }
        }
    }

    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int ColumnToSort;
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private OmniSorter ObjectCompare;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new OmniSorter();
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Compare the two items
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}
