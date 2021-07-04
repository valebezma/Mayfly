﻿using System;
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

namespace Mayfly
{
    public abstract class Service
    {
        #region

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int LoadString(IntPtr hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

        [DllImport("kernel32")]
        static extern IntPtr LoadLibrary(string lpFileName);


        public static void RunCalculator(string[] input)
        {
            Process.Start("calc");

            Thread.Sleep(250);
            IntPtr calculatorHandle = FindWindow("CalcFrame", "Калькулятор");

            if (calculatorHandle != IntPtr.Zero)
            {
                SetForegroundWindow(calculatorHandle);
                foreach (string inputString in input)
                {
                    SendKeys.SendWait(inputString);
                }
            }
        }

        public static string GetSeparator(CultureInfo ci)
        {
            if (ci.NumberFormat.NumberDecimalSeparator == ",")
                return "; ";
            else return ", ";
        }

        public static string GetValue(string path, uint index)
        {
            StringBuilder sb = new StringBuilder(4096);
            IntPtr user32 = LoadLibrary(path);
            LoadString(user32, index, sb, sb.Capacity);
            return sb.ToString().Replace("&", "");
        }

        public static Icon GetIcon(string resource, Size size)
        {
            try
            {
                string fileName = resource.Substring(0, Math.Max(0, resource.IndexOf(',')));
                int index = int.Parse(resource.Substring(resource.IndexOf(',') + 1));
                return GetIcon(fileName, index, size);
            }
            catch
            {
                return null;
            }
        }

        public static Icon GetIcon(string fileName, int index, Size size)
        {
            MultiIcon multiIcon = new MultiIcon();
            multiIcon.Load(fileName);
            SingleIcon si = multiIcon.ToArray()[index];

            if (size == Size.Empty) return si[si.Count - 1].Icon;

            for (int i = 0; i < si.Count; i++) {
                if (Size.Equals(si[i].Size, size)) return si[i].Icon;
            }

            return si.Icon;
        }
        


        public static void PlaySound(Stream stream)
        {
            SoundPlayer player = new SoundPlayer(stream);
            player.Play();
        }

        public static string Mask(int decimals)
        {
            return "N" + decimals.ToString();
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

        public static bool Available(Type type)
        {
            try
            {
                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type t in a.GetTypes())
                    {
                        if (t == type)
                            return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Converts Degrees to Radians
        /// </summary>
        /// <param name="degrees">Degrees</param>
        /// <returns></returns>
        public static double ConvertDegreesToRadians(float degrees)
        {
            return ConvertDegreesToRadians((double)degrees);
        }

        public static double ConvertDegreesToRadians(double degrees)
        {
            return ((Math.PI / (double)180) * degrees);
        }

        public static TimeSpan SpanFromHours(double hours)
        {
            return new TimeSpan((int)hours, (int) ((hours - (int)hours) * 60), 0);
        }

        public static string ArabicToRoman(int arabic)
        {
            StringBuilder retVal = new StringBuilder();
            if (arabic < 0)
                throw new ArgumentOutOfRangeException("arabic", arabic, "Argument should be positive");
            ProcessMagnitude(retVal, ref arabic, 1000, 'M');
            ProcessMagnitude(retVal, ref arabic, 500, 'D');
            ProcessMagnitude(retVal, ref arabic, 100, 'C');
            ProcessMagnitude(retVal, ref arabic, 50, 'L');
            ProcessMagnitude(retVal, ref arabic, 10, 'X');
            ProcessMagnitude(retVal, ref arabic, 5, 'V');
            ProcessMagnitude(retVal, ref arabic, 1, 'I');
            retVal.Replace("IIII", "IV");
            retVal.Replace("VIV", "IX");
            retVal.Replace("XXXX", "XL");
            retVal.Replace("LXL", "XC");
            retVal.Replace("CCCC", "CD");
            retVal.Replace("DCD", "CM");
            return retVal.ToString();
        }

        private static void ProcessMagnitude(StringBuilder retVal, ref int value, int magnitude, char letter)
        {
            while (value >= magnitude)
            {
                value -= magnitude;
                retVal.Append(letter);
            }
        }

        public static string GetLetter(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }

        #endregion



        public static void Delay(int ms, EventHandler f)
        {
            var tmp = new System.Windows.Forms.Timer { Interval = ms };
            tmp.Tick += new EventHandler((o, e) => { 
                tmp.Enabled = false;
                f.Invoke(o, e); 
            });
            tmp.Enabled = true;
        }



        public static Color Map(double value)
        {
            return Map(value, .5);
        }

        public static Color Map(double value, double edge)
        {
            if (value < edge)
                return Interpolate(Color.Crimson, Color.LemonChiffon, value / edge);
            return Interpolate(Color.LemonChiffon, Color.LimeGreen, (value - edge) / (1 - edge));
        }

        private static Color Interpolate(Color source, Color target, double fraction)
        {
            var r = (byte)(source.R + (target.R - source.R) * fraction);
            var g = (byte)(source.G + (target.G - source.G) * fraction);
            var b = (byte)(source.B + (target.B - source.B) * fraction);

            return Color.FromArgb(255, r, g, b);
        }

        public static Color GetAverageColor(Bitmap original)
        {
            Bitmap bmp = new Bitmap(1, 1);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, new Rectangle(0, 0, 1, 1));
            }

            return bmp.GetPixel(0, 0);
        }




        public static string GetFormat(string gridName, string columnName)
        {
            return GetFormat(gridName, columnName, string.Empty);
        }

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

        public static string GetPlaceableLink(string link, string text)
        {
            return string.Format(@"<a target='_blank\' href='{0}'>{1}</a>", link, text);
        }

        public static string GetLocalizedValue(string value)
        {
            return GetLocalizedValue(value, CultureInfo.CurrentCulture);
        }

        public static string GetLocalizedValue(string value, CultureInfo ci)
        {
            string result = string.Empty;

            CultureInfo currentci = CultureInfo.InvariantCulture;

            foreach (string line in value.Split(new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries))
            {
                string line1 = line;
                if (line.Length > 3 && line[2] == ':')
                {
                    try
                    {
                        currentci = new CultureInfo(line.Substring(0, 2));
                        line1 = line.Substring(3).Trim();
                    }
                    catch { }
                }

                if (currentci.Equals(ci))
                    result += line1;
            }

            if (string.IsNullOrWhiteSpace(result) && !ci.Equals(CultureInfo.InvariantCulture)) return GetLocalizedValue(value, CultureInfo.InvariantCulture);
            else return result;
        }


        #region Auto intervals and formats

        public static double GetAutoInterval(double range)
        {
            double degree = Math.Log10(range);
            //degree = degree < 0 ? -Math.Ceiling(Math.Abs(degree)) : Math.Round(degree);
            degree = Math.Floor(degree);
            double r = Math.Pow(10d, degree);
            if (range < 2 * r) return r / 5D;
            if (range > 5 * r) return r;
            return r / 2D;
        }

        public static string GetAutoFormat(double interval)
        {
            string format = "N3";
            if (Math.Round(Math.IEEERemainder(interval, .01), 3) == 0) { format = "N2"; }
            if (Math.Round(Math.IEEERemainder(interval, .1), 3) == 0) { format = "N1"; }
            if (Math.Round(Math.IEEERemainder(interval, 1), 3) == 0) { format = "N0"; }
            return format;
        }

        public static DateTimeIntervalType GetAutoIntervalType(double days)
        {
            //if (days > 365) { return DateTimeIntervalType.Years; }
            //if (days > 30) { return DateTimeIntervalType.Months; }
            //if (days > 7) { return DateTimeIntervalType.Weeks; }
            //if (days > 1) { return DateTimeIntervalType.Days; }
            //if (days > 1 / 24) { return DateTimeIntervalType.Hours; }
            //if (days > 1 / 24 / 60) { return DateTimeIntervalType.Minutes; }
            //if (days > 1 / 24 / 60 / 60) { return DateTimeIntervalType.Seconds; }
            //return DateTimeIntervalType.Auto;

            if (days >= 730) { return DateTimeIntervalType.Years; }
            if (days >= 60) { return DateTimeIntervalType.Months; }
            if (days >= 14) { return DateTimeIntervalType.Weeks; }
            if (days >= 2) { return DateTimeIntervalType.Days; }
            if (days >= 2.0 / 24) { return DateTimeIntervalType.Hours; }
            if (days >= 2.0 / 24 / 60) { return DateTimeIntervalType.Minutes; }
            if (days >= 2.0 / 34 / 60 / 60) { return DateTimeIntervalType.Seconds; }
            return DateTimeIntervalType.Auto;
        }

        public static DateTimeIntervalType GetAutoIntervalType(TimeSpan timespan) => GetAutoIntervalType(timespan.TotalDays);

        public static double GetAutoInterval(DateTimeIntervalType intervalType)
        {
            double result = 1;

            switch (intervalType)
            {
                case DateTimeIntervalType.Seconds:
                    result = 1.0 * 24 * 60 * 60;
                    break;
                case DateTimeIntervalType.Minutes:
                    result = 1.0 * 24 * 60;
                    break;
                case DateTimeIntervalType.Hours:
                    result = 1.0 * 24;
                    break;
                case DateTimeIntervalType.Days:
                    result = 1.0;
                    break;
                case DateTimeIntervalType.Weeks:
                    result = 1.0 / 7;
                    break;
                case DateTimeIntervalType.Months:
                    result = 1.0 / 30;
                    break;
                case DateTimeIntervalType.Years:
                    result = 1.0 / 365;
                    break;
            }

            return result;
        }

        public static string GetAutoFormat(DateTimeIntervalType intervalType)
        {
            string result = string.Empty;

            switch (intervalType)
            {
                case DateTimeIntervalType.Seconds:
                    result = "ss";
                    break;
                case DateTimeIntervalType.Minutes:
                    result = "T";
                    break;
                case DateTimeIntervalType.Hours:
                    result = "t";
                    break;
                case DateTimeIntervalType.Days:
                    result = "d";
                    break;
                case DateTimeIntervalType.Weeks:
                    result = "d";
                    break;
                case DateTimeIntervalType.Months:
                    result = "y";
                    break;
                case DateTimeIntervalType.Years:
                    result = "yyyy";
                    break;
            }

            return result;
        }

        #endregion


        public static string GetFriendlyBytes(long size)
        {
            if (size < 750) // Less than 750 bytes
            {
                return string.Format(Resources.FriendlyUnits.b, size);
            }
            else if (size < 768000)
            {
                return string.Format(Resources.FriendlyUnits.bKilo, (double)size / 1024);
            }
            else
            {
                return string.Format(Resources.FriendlyUnits.bMega, (double)size / 1024);
            }
        }


        public static void ResetUICulture()
        {
            CultureInfo ci = UserSettings.Language;
            if (ci.TwoLetterISOLanguageName == "en") ci = CultureInfo.InvariantCulture;

            Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture =
                CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture = ci;
        }


        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Log.Write(e.Exception);
            Error error = new Error(e.Exception);
            error.ShowDialog();
        }
    }
}
        