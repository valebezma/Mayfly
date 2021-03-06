using System;
using System.Linq;
using Mayfly;
using System.Globalization;
using System.ComponentModel;
using Mayfly.Extensions;

namespace Mayfly.Geographics
{
    public class Coordinate : IComparable, IFormattable
    {
        /// <summary>
        /// Degree value of coordinate (decimal)
        /// </summary>
        public double Degrees 
        {
            get;
            set;
        }

        /// <summary>
        /// Degree value of coordinate (decimal)
        /// </summary>
        public double Radians 
        {
            get { return Mayfly.Service.DegreesToRadians(Degrees); }
        }

        /// <summary>
        /// Minutes value of coordinate (decimal)
        /// </summary>
        public double Minutes
        {
            get { return Math.Round((Math.Abs(Degrees) % 1) * 60, 4); }
        }
        
        /// <summary>
        /// Seconds value of coordinate (decimal)
        /// </summary>
        public double Seconds 
        {
            get { return Math.Round((Minutes % 1) * 60, 2); }
        }

        public string Cardinal { get; private set; }



        public Coordinate(double value, bool islongitude)
        {
            Degrees = value;
            Cardinal = islongitude ? (value > 0 ? "E": "W") : (value > 0 ? "N": "S");
        }

        public Coordinate(object value, bool islongitude)  : this(Convert.ToDouble(value, CultureInfo.InvariantCulture), islongitude) {  }

        public Coordinate(string stringValue, bool islongitude, string format, bool isNegative)
        {
            while (stringValue.Contains(' '))
            {
                stringValue = stringValue.Replace(' ', '0');
            }

            if (stringValue != string.Empty)
            {
                if (stringValue.Length > 9)
                {
                    stringValue = stringValue.Substring(0, 9);
                }

                while (stringValue.Length < 9)
                {
                    stringValue += "0";
                }

                switch (format)
                {
                    case "d":
                        Degrees = double.Parse(stringValue) / 1000000;
                        break;
                    case "dm":
                        Degrees = double.Parse(stringValue.Substring(0, 3)) + (double.Parse(stringValue.Substring(3, 6)) / 10000) / 60;
                        break;
                    case "dms":
                        double sec = double.Parse(stringValue.Substring(5, 4)) / 100;
                        double min = double.Parse(stringValue.Substring(3, 2)) + sec / 60;
                        Degrees = double.Parse(stringValue.Substring(0, 3)) + min / 60;
                        break;
                }
            }

            if (isNegative)
            {
                Degrees = -Degrees;
            }

            Cardinal = islongitude ? (Degrees > 0 ? "E" : "W") : (Degrees > 0 ? "N" : "S");
        }


        /// <summary>
        /// Returns coordinate textual presentation w/o direction mark
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToMaskedText(string format)
        {
            return ToString(format).StripNonNumbers();

            //string result;

            //switch (format)
            //{
            //    case "d":
            //        result = Math.Abs(Degrees).ToString("000.000000");
            //        break;
            //    case "dm":
            //        result = Math.Floor(Math.Abs(Degrees)).ToString("000");
            //        result += Minutes.ToString("00.0000");
            //        break;
            //    case "dms":
            //        result = Math.Floor(Math.Abs(Degrees)).ToString("000");
            //        result += Math.Floor(Minutes).ToString("00");
            //        result += Seconds.ToString("00.00");
            //        break;
            //    default:
            //        result = Math.Abs(Degrees).ToString(format);
            //        break;
            //}

            //while (result.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            //{
            //    result = result.Remove(result.IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), 1);
            //}

            //if (result.Length > 9)
            //{
            //    result = result.Substring(0, 8);
            //}

            //while (result.Length < 9)
            //{
            //    result += "0";
            //}

            //return result;
        }

        public static string ReverseCardinal(string cardinal)
        {
            switch (cardinal.Trim())
            {
                case "E": return "W";
                case "W": return "E";
                case "S": return "N";
                case "N": return "S";
            }

            return "W";
        }

        /// <summary>
        /// Mask for output
        /// </summary>
        public static string GetMask(string format)
        {
            switch (format)
            {
                case "d":
                    return "000.000000°";
                case "dm":
                    return "000° 00.0000'";
                case "dms":
                    return "000° 00' 00.00\"";
                default:
                    return GetMask(UI.FormatCoordinate);
            }
        }



        #region IComparable implementations

        public static bool operator ==(Coordinate a, Coordinate b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Degrees == b.Degrees;
        }

        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return !(a == b);
        }

        int IComparable.CompareTo(object obj)
        {
            return Compare(this, (Coordinate)obj);
        }

        public static int Compare(Coordinate value1, Coordinate value2)
        {
            return (int)(value1.Degrees * 1000000) - (int)(value2.Degrees * 1000000);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast return false.
            Coordinate p = obj as Coordinate;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Degrees == p.Degrees);
        }

        public bool Equals(Coordinate p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (p.Degrees == Degrees);
        }

        public bool Equals(Double p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (p == Degrees);
        }

        public bool Equals(int p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (p == (int)Degrees);
        }

        public override int GetHashCode()
        {
            return (int)(Degrees * 1000000);
        }

        #endregion


        #region IFormattable

        public override string ToString()
        {
            return ToString(string.Empty);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            format = format.ToLowerInvariant();

            switch (format)
            {
                case "d":
                    return string.Format("{0}{1:000.000000}°", Cardinal, Math.Abs(Degrees));

                case "dm":
                    return string.Format("{0}{1:000}° {2:00.0000}'", Cardinal, Math.Floor(Math.Abs(Degrees)), Minutes);

                case "dms":
                    return string.Format("{0}{1:000}° {2:00}' {3:00.00}\"", Cardinal, Math.Floor(Math.Abs(Degrees)), Math.Floor(Minutes), Seconds);

                default:
                    return string.Format("{0:" + format + "}°", Degrees);
            }
        }

        #endregion
    }

    public class CoordinateConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string)) ? true : base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return new Coordinate(value, true);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return value.ToString();
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
