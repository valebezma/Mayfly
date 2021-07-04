using System;
using System.Linq;
using Mayfly;
using System.Globalization;
using System.ComponentModel;

namespace Mayfly.Geographics
{
    ///// <summary>
    ///// Mode for displaying coordinates
    ///// </summary>
    //public enum CoordinateFormat
    //{
    //    Degrees,
    //    DegreesMinutes, 
    //    DegreesMinutesSeconds 
    //};

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
            get { return Mayfly.Service.ConvertDegreesToRadians(Degrees); }
        }

        /// <summary>
        /// Minutes value of coordinate (decimal)
        /// </summary>
        public double Minutes 
        {
            get; 
            set; 
        }
        
        /// <summary>
        /// Seconds value of coordinate (decimal)
        /// </summary>
        public double Seconds 
        {
            get;
            set;
        }

        public string Cardinal { get; private set; }



        public Coordinate(double value, bool islongitude)
        {
            Degrees = value;
            Cardinal = islongitude ? (value > 0 ? "E": "W") : (value > 0 ? "N": "S");
            Reset();
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
                while (stringValue.Length < 9)
                {
                    stringValue += "0";
                }
                switch (format)
                {
                    case "d":
                        Degrees = Math.Round(double.Parse(stringValue) / 1000000, 6);
                        Minutes = Math.Round((Degrees - Math.Floor(Degrees)) * 60, 4);
                        Seconds = Math.Round((Minutes - Math.Floor(Minutes)) * 60, 2);
                        break;
                    case "dm":
                        Minutes = Math.Round(double.Parse(stringValue.Substring(3, 6)) / 10000, 4);
                        Degrees = Math.Round(double.Parse(stringValue.Substring(0, 3)) + (Minutes / 60), 6);

                        Seconds = Math.Round((Minutes - Math.Floor(Minutes)) * 60, 2);
                        break;
                    case "dms":
                        Seconds = Math.Round(double.Parse(stringValue.Substring(5, 4)) / 100, 2);
                        Minutes = Math.Round(double.Parse(stringValue.Substring(3, 2)) + (Seconds / 60), 4);
                        Degrees = Math.Round(double.Parse(stringValue.Substring(0, 3)) + (Minutes / 60), 6);
                        break;
                }
            }

            if (isNegative)
            {
                Degrees = -Degrees;
            }

        }

        

        public void Reset()
        {
            Minutes = (Math.Abs(Degrees) - Math.Floor(Math.Abs(Degrees))) * 60;
            Seconds = (Math.Abs(Minutes) - Math.Floor(Math.Abs(Minutes))) * 60;
        }

        /// <summary>
        /// Returns coordinate textual presentation w/o direction mark
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToMaskedText(string format)
        {
            string result = string.Empty;

            switch (format)
            {
                case "d":
                    result = Math.Abs(Degrees).ToString("000.000000");
                    break;
                case "dm":
                    result = Math.Floor(Math.Abs(Degrees)).ToString("000");
                    result += Minutes.ToString("00.0000");
                    break;
                case "dms":
                    result = Math.Floor(Math.Abs(Degrees)).ToString("000");
                    result += Math.Floor(Minutes).ToString("00");
                    result += Seconds.ToString("00.00");
                    break;
                default:
                    result = Math.Abs(Degrees).ToString(format);
                    break;
            }

            while (result.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                result = result.Remove(result.IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), 1);
            }

            if (result.Length > 9)
            {
                result = result.Substring(0, 8);
            }

            while (result.Length < 9)
            {
                result += "0";
            }

            return result;
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
                    return GetMask(UserSettings.FormatCoordinate);
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

            string result = this.Cardinal;

            switch (format)
            {
                case "d":
                case "dm":
                case "dms":

                    string coordinate = ToMaskedText(format);
                    string mask = GetMask(format);
                    int index = 0;

                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (mask[i] == '0')
                        {
                            try
                            {
                                result += coordinate[index];
                            }
                            catch
                            {
                                result += "0";
                            }

                            index++;
                        }
                        else
                        {
                            result += mask[i];
                        }
                    }
                    //result = result.Trim(new char[] { '0' });
                    break;
                default:
                    result = Math.Abs(Degrees).ToString(format);
                    break;
            }

            //result += this.DirectionMark;
            return result.Replace(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator,
                ((CultureInfo)provider).NumberFormat.NumberDecimalSeparator);
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
