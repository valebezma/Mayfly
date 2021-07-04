using AeroWizard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mayfly.Extensions
{
    public static class Extensions
    {
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
            if (label.FindForm() == null) label.Text = values.Merge();
            else label.Text = string.Format(
                new ResourceManager(label.FindForm().GetType()).GetString(label.Name + ".Text"),
                values);
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
            try {
                return int.Parse(item.Name); }
            catch {
                return 0; }
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

        public static Image Resize(this Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        public static Image Fit(this Image img, Size frame)
        {
            return Resize(img, FitToFrame(img.Size, frame));
        }

        private static Size FitToFrame(this Size original, Size frame)
        {
            double widthScale = 0, heightScale = 0;
            if (original.Width != 0)
                widthScale = (double)frame.Width / (double)original.Width;
            if (original.Height != 0)
                heightScale = (double)frame.Height / (double)original.Height;

            double scale = Math.Min(widthScale, heightScale);

            Size result = new Size((int)(original.Width * scale),
                                (int)(original.Height * scale));
            return result;
        }

        public static byte[] ToByteArray(this Image image)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();
        }

        public static Image GetImage(this byte[] array)
        {
            MemoryStream stream = new MemoryStream(array);
            return Image.FromStream(stream);
        }

        public static int ColorSaturationIncrement = 35;

        public static Color Darker(this Color color)
        {
            return color.Darker(.2);
        }

        public static Color Darker(this Color color, double saturationdegree)
        {
            int A = Math.Max(0, color.A - (int)(saturationdegree * 255));
            int R = Math.Max(0, color.R - (int)(saturationdegree * 255));
            int G = Math.Max(0, color.G - (int)(saturationdegree * 255));
            int B = Math.Max(0, color.B - (int)(saturationdegree * 255));
            return Color.FromArgb(A, R, G, B);
        }

        public static Color Lighter(this Color color)
        {
            return color.Lighter(.1);
        }

        public static Color Lighter(this Color color, double saturationdegree)
        {
            return color.GetDesaturatedColor(saturationdegree);
        }

        public static Color GetDesaturatedColor(this Color color, double kValue)
        {
            double greyLevel = (color.R * 0.299) + (color.G * 0.587) + (color.B * 0.144);
            int r = (int)(greyLevel * kValue + (color.R) * (1 - kValue));
            int g = (int)(greyLevel * kValue + (color.G) * (1 - kValue));
            int b = (int)(greyLevel * kValue + (color.B) * (1 - kValue));
            return Color.FromArgb(kValue * 255 < 0 ? 0 : kValue > 255 ? 255 : (int) (kValue * 255), r, g, b);
        }

        public static string[] GetLongWords(this string text, int minLength)
        {
            string[] words = text.Split(' ');
            List<string> result = new List<string>();
            foreach (string word in words)
            {
                if (word.Length >= minLength)
                {
                    result.Add(word);
                }
            }
            return result.ToArray();
        }

        public static string InsertBreaks(this string comments, int rowLength)
        {
            if (comments.Length > rowLength)
            {
                string start = comments.Substring(0, rowLength);
                int breakIndex = start.LastIndexOf(' ');
                start = comments.Substring(0, breakIndex);
                string end = comments.Substring(breakIndex);
                comments = start.Trim() + Constants.Break + InsertBreaks(end.Trim(), rowLength);
            }
            return comments;
        }

        public static string Merge(this IEnumerable array)
        {
            return Merge(array, Constants.StdSeparator);
        }

        public static string Merge(this IEnumerable array, string space)
        {
            return Merge(array, space, string.Empty);
        }

        public static string Merge(this IEnumerable array, string space, string format)
        {
            return Merge(array, space, format, CultureInfo.CurrentCulture);
        }

        public static string Merge(this IEnumerable array, string format, IFormatProvider provider)
        {
            return Merge(array, Service.GetSeparator((CultureInfo)provider),
                format, CultureInfo.CurrentCulture);
        }

        public static string Merge(this IEnumerable array, string space, string format, IFormatProvider provider)
        {
            string result = string.Empty;

            if (array == null) return result;

            foreach (object k in array)
            {
                if (k is IFormattable)
                {
                    result += ((IFormattable)k).ToString(format, provider) + space;
                }
                else
                {
                    result += k.ToString() + space;
                }
            }

            return result.TrimEnd(space.ToCharArray());
        }

        public static int Precision(this IEnumerable<double> values)
        {
            int result = 0;
            foreach (double value in values)
            {
                result = Math.Max(result, value.Precision());
            }
            return result;
        }

        public static int Precision(this double value)
        {
            if (double.IsNaN(value)) return 0;

            decimal separate = Math.Abs((decimal)value) - Math.Floor(Math.Abs((decimal)value));

            if (separate == 0)
            {
                return 0;
            }
            else
            {
                return Math.Min(5, Math.Abs((int)Math.Ceiling(-Math.Log10((double)separate))));
            }
        }

        public static string MeanFormat(this IEnumerable<double> values)
        {
            return Mayfly.Service.Mask(values.Precision());
        }

        public static string[] Split(this string text, int firstlinelength, int alllineslength)
        {
            List<string> result = new List<string>();

            if (text.Length > firstlinelength)
            {
                string start = text.Substring(0, firstlinelength);
                int breakIndex = start.LastIndexOfAny(new char[] { ' ', '-' }) + 1;

                if (breakIndex == -1)
                {
                    result.Add(string.Empty);
                    result.AddRange(text.Split(alllineslength));
                }
                else
                {
                    start = text.Substring(0, breakIndex);
                    result.Add(start.Trim());
                    string end = text.Substring(breakIndex);
                    result.AddRange(end.Trim().Split(alllineslength));
                }

                return result.ToArray();
            }

            return new string[] { text };
        }

        public static string[] Split(this string text, int linelength)
        {
            return text.Split(linelength, linelength);
        }

        public static bool Contains(this string value, char c)
        {
            return new List<char>(value.ToCharArray()).Contains(c);
        }

        public static char GetInitial(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return ' ';
            if ((Constants.Forbidden + "\"").Contains(value[0])) return ((value.Length > 1) ? (GetInitial(value.Substring(1))) : ' ');
            else return value[0];
        }

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

        #region Numbers logic

        public static string ToSmallValueString(this double value, string format)
        {
            string result = value.ToString(format);

            double zero = 0;

            if (value != 0 && result == zero.ToString(format))
            {
                int indexOfLastZero = result.LastIndexOf('0');

                result = "<" + result.Substring(0, indexOfLastZero) + "1" +
                    result.Substring(indexOfLastZero + 1);
            }

            return result;
        }

        public static string ToDots(this int value, char symbol, int maxlength, string _break)
        {
            string result = string.Empty;
            int currlength = 0;

            for (int i = 0; i < value; i++)
            {
                currlength++;
                result += symbol;
                if (currlength == maxlength)
                {
                    result += _break;
                    currlength = 0;
                }
            }

            return result;
        }

        public static string ToDots(this int value, int maxlength, string _break)
        {
            return ToDots(value, '●', maxlength, _break);
        }

        public static string ToDots(this int value, int maxlength)
        {
            return ToDots(value, '●', maxlength, Environment.NewLine);
        }

        public static string ToDots(this int value, char symbol)
        {
            return ToDots(value, symbol, 250, Environment.NewLine);
        }

        public static string ToDots(this int value)
        {
            return ToDots(value, '●');// '•');
        }

        public static bool IsDoubleConvertible(this Type type)
        {
            if (type == null) return false;
            if (type == typeof(int)) return true;
            if (type == typeof(decimal)) return true;
            if (type == typeof(double)) return true;
            if (type == typeof(DateTime)) return true;
            return type.GetMethod("ToDouble") != null;
        }

        public static bool IsDoubleConvertible(this object value)
        {
            if (value == null) return false;
            if (value is DBNull) return false;

            if (value is double)
            {
                if (double.IsNaN((double)value)) return false;
                if (double.IsInfinity((double)value)) return false;
                return true;
            }

            if (value is string)
            {
                double d = 0;
                DateTime dt = DateTime.Now;
                return (double.TryParse((string)value, out d) || DateTime.TryParse((string)value, out dt));
            }
            
            return IsDoubleConvertible(value.GetType());
        }

        public static bool IsDoubleConvertible(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            double g = 0;
            return double.TryParse(text, out g);
        }

        public static bool IsAcceptable(this string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        public static double ToDouble(this object value)
        {
            if (value is DateTime)
            {
                return ((DateTime)value).ToOADate();
            }

            if (value is int)
            {
                return (double)(int)value;
            }

            if (value is string)
            {
                try
                {
                    return Convert.ToDouble(value);
                }
                catch (FormatException)
                {
                    return (Convert.ToDateTime(value)).ToOADate();
                }
            }

            try
            {
                return (double)value;
            }
            catch (InvalidCastException)
            {
                return Convert.ToDouble(value);
            }
        }

        public static string ToCorrectString(this int value, string formattedMask)
        {
            if (formattedMask == null) return value.ToString();

            try
            {
                string[] parameters = formattedMask.Split('|');
                string[] numbers = parameters[0].Split(';');
                string[] masks = parameters[1].Split(';');

                string mask = masks.Last();
                int residual = value;

                if (Math.IEEERemainder(value, 100) < 10 || Math.IEEERemainder(value, 100) > 20)
                {
                    residual = (int)Math.IEEERemainder(value, 10);
                    if (residual < 0) residual = 10 + residual;
                }

                for (int i = 1; i <= residual; i++)
                {
                    for (int j = 0; j < masks.Length; j++)
                    {
                        if (numbers[j] == i.ToString())
                        {
                            mask = masks[j];
                            break;
                        }
                    }
                }

                return value.ToString("0 " + mask);
            }
            catch
            {
                return value.ToString();
            }
        }

        #endregion

        public static string Format(this object value, string format)
        {
            string formatted = value.ToString();

            if (value is DateTime)
            {
                formatted = DateTimeExtensions.ToString((DateTime)value, CultureInfo.CurrentCulture, format);
            }
            else if (value is IFormattable)
            {
                formatted = ((IFormattable)value).ToString(format, CultureInfo.CurrentCulture);
            }

            return formatted;
        }

        public static long GetSize(this DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += di.GetSize();
            }
            return size;
        }
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
