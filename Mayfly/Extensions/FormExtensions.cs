using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;
using AeroWizard;

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

        public static void ResetText(this Form form, string fileName, string applicationName)
        {
            form.Text = String.Format("{0} — {1}", System.IO.Path.GetFileNameWithoutExtension(fileName), applicationName);
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

        public static void EnsureSelected(this WizardControl ctrl, WizardPage page)
        {
            if (ctrl.SelectedPage == null) return;

            if (ctrl.SelectedPage == page) return;

            while (ctrl.SelectedPage != ctrl.Pages[0]) {
                ctrl.PreviousPage();
            }

            while (ctrl.SelectedPage != page) {
                ctrl.NextPage();
            }
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
            Service.SaveCheckState(GetForm(checkBox).Name, checkBox.Name, checkBox.CheckState);
        }

        public static void RestoreCheckState(this CheckBox checkBox)
        {
            checkBox.CheckState = Service.GetCheckState(GetForm(checkBox).Name, checkBox.Name);
        }
    }
}
