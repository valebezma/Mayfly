using Mayfly.Extensions;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Mayfly.Wild
{
    public enum MassDegree
    {
        Microgramm = -6,
        Milligramm = -3,
        Gramm = 0,
        Kilogramm = 3,
        Ton = 6
    }

    public class MassConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
            return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            return new Mass(value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
            if (destinationType == typeof(string))
                return value.ToString();
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(MassConverter))]
    public class Mass : IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Returns mass value in gramms
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Returns mass value in gramms as Double
        /// </summary>
        /// <returns></returns>
        public double ToDouble() {

            return Value;
        }

        /// <summary>
        /// Returns mass value degree for string representation
        /// </summary>
        protected MassDegree Degree = ReaderSettings.IndividualMassDegree;

        /// <summary>
        /// Mass units for string representation
        /// </summary>
        public string Units {

            get {
                return Resources.Interface.Mass.ResourceManager.GetString(((int)Degree).ToString());
            }
        }

        /// <summary>
        /// Value according to Degree
        /// </summary>
        public double DisplayValue {

            get {
                return Value * Math.Pow(10d, -(int)Degree);
            }
        }


        public Mass(double d, MassDegree degree) {

            Degree = degree;
            Value = d * Math.Pow(10, (int)Degree);
        }

        public Mass(string value, MassDegree degree) : this(value.ToDouble(), degree) { }

        public Mass(object value, MassDegree degree) : this(value?.ToString(), degree) { }

        public Mass(double d) : this(d, MassDegree.Gramm) { }

        public Mass(string value) : this(value, MassDegree.Gramm) { }

        public Mass(object value) : this(value?.ToString()) { }

        public Mass() : this(0) { }


        #region Cast

        public static implicit operator Mass(string value) {
            return new Mass(value);
        }

        public static implicit operator Mass(double value) {
            return new Mass(value);
        }

        public static implicit operator string(Mass mass) {
            return mass.ToString();
        }

        public static explicit operator double(Mass mass) {
            return mass.Value;
        }

        #endregion

        #region Operators

        public static Mass operator +(Mass a) => new Mass(a == null ? 0 : a.Value, a.Degree);

        public static Mass operator -(Mass a) => new Mass(-(a == null ? 0 : a.Value), a.Degree);

        public static Mass operator +(Mass a, Mass b) => new Mass((a == null ? 0 : a.Value) + (b == null ? 0 : b.Value)) { Degree = (MassDegree)Math.Max((int)a.Degree, (int)b.Degree) };

        public static Mass operator -(Mass a, Mass b) => new Mass((a == null ? 0 : a.Value) - (b == null ? 0 : b.Value)) { Degree = (MassDegree)Math.Max((int)a.Degree, (int)b.Degree) };

        public static Mass operator *(Mass a, double b) => new Mass(a == null ? 0 : a.Value * b) { Degree = a.Degree };

        public static Mass operator /(Mass a, double b) {
            if (b == 0) {
                throw new DivideByZeroException();
            }
            return new Mass(a == null ? 0 : a.Value / b, a.Degree);
        }

        public static bool operator >(Mass a, double b) => a.Value > b;

        public static bool operator <(Mass a, double b) => a.Value < b;

        public static bool operator >(Mass a, int b) => a.Value > b;

        public static bool operator <(Mass a, int b) => a.Value < b;

        #endregion

        #region IComparable

        public static bool operator ==(Mass a, Mass b) {
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

        public static bool operator !=(Mass a, Mass b) {
            return !(a == b);
        }

        int IComparable.CompareTo(object obj) {
            return Compare(this, (Mass)obj);
        }

        public static int Compare(Mass value1, Mass value2) {
            return (int)(value1.Value) - (int)(value2.Value);
        }

        public override bool Equals(System.Object obj) {
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast return false.
            Mass p = obj as Mass;
            if ((System.Object)p == null) {
                return false;
            }

            // Return true if the fields match:
            return (Value == p.Value);
        }

        public bool Equals(Mass p) {
            // If parameter is null return false:
            if ((object)p == null) {
                return false;
            }

            // Return true if the fields match:
            return (p.Value == Value);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        #endregion

        #region IConvertible

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
            return (int)Value;
        }

        long IConvertible.ToInt64(IFormatProvider provider) {
            return (int)Value;
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
            return Convert.ToUInt16(Value);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider) {
            return Convert.ToUInt32(Value);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider) {
            return Convert.ToUInt64(Value);
        }

        #endregion

        #region IFormattable

        public string ToString(string format, IFormatProvider provider) {

            if (double.IsNaN(Value)) return Constants.Null;

            if (string.IsNullOrEmpty(format)) return DisplayValue.ToString(string.Empty, provider);

            format = format.ToLowerInvariant();

            switch (format[0]) {

                case 'a': // Auto
                    Mass m = new Mass(Value) {
                        Degree =
                        Value < 1.25 ? MassDegree.Milligramm :
                        Value < 1250 ? MassDegree.Gramm :
                        Value < 1250000 ? MassDegree.Kilogramm : MassDegree.Ton
                    };
                    return m.ToString(format.Replace("a", "u"), provider);

                case 'g': // Gramms
                    return Value.ToString(format.Replace("g", "n"), provider);

                case 'u': // Selected units
                    return DisplayValue.ToString(format.Replace("u", "n")) + " " + Units;

                default:
                    return DisplayValue.ToString(format, provider);
            }
        }

        public string ToString(string format) {

            return ToString(format, CultureInfo.CurrentCulture);
        }

        public override string ToString() {

            return ToString(string.Empty);
        }

        #endregion
    }

    public class SampleMassConverter : MassConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            return new SampleMass(value);
        }
    }

    [TypeConverter(typeof(SampleMassConverter))]
    public class SampleMass : Mass {

        public SampleMass() : base(0, ReaderSettings.LogMassDegree) { }

        public SampleMass(object value) : base(value, ReaderSettings.LogMassDegree) { }
    }
}
