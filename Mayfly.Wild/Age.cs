using Mayfly.Extensions;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Mayfly.Wild
{
    [TypeConverter(typeof(AgeConverter))]
    public partial class Age : IComparable, IConvertible, IFormattable
    {
        public int Years {
            get;
            set;
        }

        public bool Gain {
            get;
            set;
        }

        public string Group {
            get {
                return string.Format("{0}—{0}+", Years);
            }
        }

        public double Value { get { return Years + (Gain ? .5 : .0); } }


        public Age(int y, bool g) { Years = y; Gain = g; }

        public Age(int y) : this(y, false) { }

        public Age() : this(0, false) { }

        public Age(string value) : this() {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidCastException("Age can not be obtained from empty string.");

            if (value.IsDoubleConvertible()) // Number string is given
            {
                Years = (int)double.Parse(value);
                Gain = double.Parse(value) - Years > 0;
            } else {
                if (value.Contains('–')) // Group presentation is given
                {
                    Years = (int)double.Parse(value.Substring(0, value.IndexOf('–')));
                } else // Age formatted presentation is given
                  {
                    Years = (int)double.Parse(value.TrimEnd('+'));
                    Gain = true;
                }
            }
        }

        public Age(object value) : this(value.ToString()) { }



        public static Age Parse(string value) {
            try {
                return new Age(value);
            } catch (InvalidCastException) {
                return null;
            }
        }

        public static Age FromDouble(double value) {
            return new Age(value);
        }

        public double ToDouble() {
            return this.Value;
        }

        public bool Contains(double a) {
            return ((a >= this.Years) && (a < (this.Years + 1)));
        }



        public static Age ThisYearYangster {
            get {
                return new Age(0.5);
            }
        }

        public static Age OneYearYangster {
            get {
                return new Age(1);
            }
        }

        public static Age TwoSummerYangster {
            get {
                return new Age(1.5);
            }
        }

        public static Age TwoYearYangster {
            get {
                return new Age(2);
            }
        }



        public static implicit operator Age(string value) {
            return new Age(value);
        }

        public static implicit operator string(Age age) {
            return age.ToString();
        }

        public static explicit operator Age(double value) {
            return new Age(value);
        }

        public static implicit operator double(Age age) {
            return age.Value;
        }



        public static bool operator ==(Age a, Age b) {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) {
                return false;
            }

            // Return true if the fields match:
            return a.Value == b.Value;
        }

        public static bool operator !=(Age a, Age b) {
            return !(a == b);
        }

        int IComparable.CompareTo(object obj) {
            return Compare(this, (Age)obj);
        }

        public static int Compare(Age value1, Age value2) {
            return (int)(value1.Value * 10) - (int)(value2.Value * 10);
        }

        public override bool Equals(System.Object obj) {
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast return false.
            Age p = obj as Age;
            if ((System.Object)p == null) {
                return false;
            }

            // Return true if the fields match:
            return (Value == p.Value);
        }

        public bool Equals(Age p) {
            // If parameter is null return false:
            if ((object)p == null) {
                return false;
            }

            // Return true if the fields match:
            return (p.Value == Value);
        }

        public override int GetHashCode() {
            return (int)(Value * 100);
        }



        public TypeCode GetTypeCode() {
            return TypeCode.Double;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider) {
            return double.IsNaN(Value);
        }

        byte IConvertible.ToByte(IFormatProvider provider) {
            return Convert.ToByte(Value);
        }

        char IConvertible.ToChar(IFormatProvider provider) {
            return Convert.ToChar(Value);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider) {
            return Convert.ToDateTime(Value);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider) {
            return Convert.ToDecimal(Value);
        }

        double IConvertible.ToDouble(IFormatProvider provider) {
            return Value;
        }

        short IConvertible.ToInt16(IFormatProvider provider) {
            return Convert.ToInt16(Value);
        }

        int IConvertible.ToInt32(IFormatProvider provider) {
            return Years;
        }

        long IConvertible.ToInt64(IFormatProvider provider) {
            return Years;
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider) {
            return Convert.ToSByte(Value);
        }

        float IConvertible.ToSingle(IFormatProvider provider) {
            return Convert.ToSingle(Value);
        }

        string IConvertible.ToString(IFormatProvider provider) {
            return ToString();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider) {
            return Convert.ChangeType(Value, conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider) {
            return Convert.ToUInt16(Years);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider) {
            return Convert.ToUInt32(Years);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider) {
            return Convert.ToUInt64(Years);
        }



        public string ToString(string format, IFormatProvider provider) {
            switch (format) {
                case "0": // return only year
                    return Years.ToString("0");
                case "g": // return age group
                    return Group;
                default: // return year and + if needed
                    return Years.ToString(Gain ? "0+" : "0");
            }
        }

        public string ToString(string format) {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public override string ToString() {
            if (Gain) {
                return this.ToString("0+");
            } else {
                return this.ToString("0");
            }
        }
    }

    public class AgeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
            return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            return new Age(value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
            if (destinationType == typeof(string))
                return value.ToString();
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
