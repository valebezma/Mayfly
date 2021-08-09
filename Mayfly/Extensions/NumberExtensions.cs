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
    public static class NumberExtensions
    {
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

        public static string ToLetter(this int index)
        {
            //CultureInfo.CurrentCulture.TextInfo.ListSeparator

            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }

        static void processMagnitude(StringBuilder retVal, ref int value, int magnitude, char letter)
        {
            while (value >= magnitude)
            {
                value -= magnitude;
                retVal.Append(letter);
            }
        }

        public static string ToRoman(this int arabic)
        {
            StringBuilder retVal = new StringBuilder();
            if (arabic < 0)
                throw new ArgumentOutOfRangeException("arabic", arabic, "Argument should be positive");
            processMagnitude(retVal, ref arabic, 1000, 'M');
            processMagnitude(retVal, ref arabic, 500, 'D');
            processMagnitude(retVal, ref arabic, 100, 'C');
            processMagnitude(retVal, ref arabic, 50, 'L');
            processMagnitude(retVal, ref arabic, 10, 'X');
            processMagnitude(retVal, ref arabic, 5, 'V');
            processMagnitude(retVal, ref arabic, 1, 'I');
            retVal.Replace("IIII", "IV");
            retVal.Replace("VIV", "IX");
            retVal.Replace("XXXX", "XL");
            retVal.Replace("LXL", "XC");
            retVal.Replace("CCCC", "CD");
            retVal.Replace("DCD", "CM");

            return retVal.ToString();
        }

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

            //if (value is int)
            //{
            //    return (double)(int)value;
            //}

            try
            {
                return (double)value;
            }
            catch (InvalidCastException)
            {
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
                return Convert.ToDouble(value);
            }
        }

        public static string ToCorrectString(this int value, string formattedMask)
        {
            if (formattedMask == null) return value.ToString();

            if (!formattedMask.Contains('|')) return value.ToString(formattedMask);

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
                return value.ToString(formattedMask);
            }
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

        public static string MeanFormat(this IEnumerable<double> values)
        {
            return "N" + values.Precision();
        }
    }
}
