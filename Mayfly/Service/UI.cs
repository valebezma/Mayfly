using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using Mayfly.Software;
using System.Drawing.IconLib;
using System.Text.RegularExpressions;

namespace Mayfly
{
    public abstract class UI
    {
        public static string GetFormat(string gridName, string columnName, string ifnull)
        {
            object result = UserSetting.GetValue(UserSettingPaths.KeyUI,
                new string[] { UserSettingPaths.FormatColumn, gridName }, columnName);

            if (result == null)
            {
                return ifnull;
            }
            else
            {
                return result.ToString();
            }
        }

        public static void SaveFormat(string gridName, string columnName, string format)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                UserSetting.Remove(UserSettingPaths.KeyUI,
                    new string[] { UserSettingPaths.FormatColumn, gridName }, 
                    columnName);
            }
            else
            {
                UserSetting.SetValue(UserSettingPaths.KeyUI,
                    new string[] { UserSettingPaths.FormatColumn, gridName }, 
                    columnName, format);
            }
        }


        public static CheckState GetCheckState(string formName, string checkBoxName)
        {
            return GetCheckState(formName, checkBoxName, CheckState.Checked);
        }

        public static CheckState GetCheckState(string formName, string checkBoxName, CheckState defaultState)
        {
            object result = UserSetting.GetValue(UserSettingPaths.KeyUI,
                new string[] { UserSettingPaths.CheckState, formName }, checkBoxName);

            if (result == null)
            {
                return defaultState;
            }
            else
            {
                return (CheckState)Convert.ToInt32(result);
            }
        }

        public static void SaveCheckState(string formName, string checkBoxName, CheckState state)
        {
            UserSetting.SetValue(UserSettingPaths.KeyUI,
                new string[] { UserSettingPaths.CheckState, formName },
                checkBoxName, (int)state);
        }




        public static void SetMenuAvailability(bool available, params ToolStripItem[] items)
        {
            foreach (ToolStripItem item in items)
            {
                item.Visible = available;
            }
        }

        public static void SetMenuClickability(bool available, params ToolStripItem[] items)
        {
            foreach (ToolStripItem item in items)
            {
                item.Enabled = available;
            }
        }

        public static void SetControlAvailability(bool available, params Control[] controls)
        {
            foreach (Control control in controls)
            {
                control.Visible = available;
            }
        }

        public static void SetControlClickability(bool available, Control control)
        {
            if (control is DataGridView)
            {
                foreach (DataGridViewRow r in ((DataGridView)control).Rows)
                {
                    r.ReadOnly = !available;
                }
            }
            else if (control is TextBox)
            {
                ((TextBox)control).ReadOnly = !available;
            }
            else if (control is MaskedTextBox)
            {
                ((MaskedTextBox)control).ReadOnly = !available;
            }
            else if (control is Panel)
            {
                SetControlClickability(available, ((Panel)control).Controls);
            }
            else if (control is Geographics.WaypointControl)
            {
                ((Geographics.WaypointControl)control).ReadOnly = !available;
            }
            else
            {
                control.Enabled = available;
            }
        }

        public static void SetControlClickability(bool available, Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                SetControlClickability(available, control);
            }
        }

        public static void SetControlClickability(bool available, params Control[] controls)
        {
            foreach (Control control in controls)
            {
                SetControlClickability(available, control);
            }
        }

        public static void SetControlClickability(bool available, params TabPage[] tabs)
        {
            foreach (TabPage tab in tabs)
            {
                SetControlClickability(available, tab.Controls);
            }
        }



        public static void SetTabsAvailability(bool available, params TabPage[] tabs)
        {
            foreach (TabPage tab in tabs)
            {
                if (!available) tab.Parent = null;
            }
        }



        private static void DrawDropBox(Control control, Rectangle rectangle, string message)
        {
            Graphics DropBox = control.CreateGraphics();

            Color Back = Color.FromArgb(220, Color.White);
            DropBox.FillRectangle(new SolidBrush(Back), rectangle);

            using (Pen Border = new Pen(Color.Gray, 2))
            {
                Border.DashStyle = DashStyle.Dash;
                Border.Alignment = PenAlignment.Inset;
                DropBox.DrawRectangle(Border, rectangle);
            }

            StringFormat SF = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            DropBox.DrawString(message,
                SystemInformation.MenuFont,
                Brushes.Gray,
                rectangle,
                SF);
        }
    }
}
        