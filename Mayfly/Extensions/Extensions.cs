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
using System.Security.Cryptography;
using System.Text;

namespace Mayfly.Extensions
{
    public static class Extensions
    {
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
            if (color.ToString() == "0") return Color.Transparent;
            return color.Darker(.2);
        }

        public static Color Darker(this Color color, double saturationdegree)
        {
            //int A = Math.Max(0, color.A - (int)(saturationdegree * 255));
            int R = Math.Max(0, color.R - (int)(saturationdegree * 255));
            int G = Math.Max(0, color.G - (int)(saturationdegree * 255));
            int B = Math.Max(0, color.B - (int)(saturationdegree * 255));
            return Color.FromArgb(color.A, R, G, B);
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

        public static string Merge(this IEnumerable array)
        {
            return Merge(array, Constants.ElementSeparator);
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
            return Merge(array, Constants.ElementSeparator, format, CultureInfo.CurrentCulture);
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

        public static string Format(this object value, string format)
        {
            if (value == null) return string.Empty;

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

        //public static long GetSize(this DirectoryInfo d)
        //{
        //    long size = 0;
        //    // Add file sizes.
        //    FileInfo[] fis = d.GetFiles();
        //    foreach (FileInfo fi in fis)
        //    {
        //        size += fi.Length;
        //    }
        //    // Add subdirectory sizes.
        //    DirectoryInfo[] dis = d.GetDirectories();
        //    foreach (DirectoryInfo di in dis)
        //    {
        //        size += di.GetSize();
        //    }
        //    return size;
        //}

        public static string GetHTMLReference(this Uri uri, string text)
        {
            return string.Format(@"<a target='_blank\' href='{0}'>{1}</a>", uri.OriginalString, text);
        }
    }
}
