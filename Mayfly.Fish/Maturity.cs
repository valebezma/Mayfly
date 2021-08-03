using System.Linq;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Mayfly.Extensions;

namespace Mayfly.Fish
{
    [TypeConverter(typeof(MaturityConverter))]
    public partial class Maturity : IComparable, IConvertible
    {
        #region Properties

        public int Value
        {
            get; 
            set;
        }

        public bool IsIntermediate
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public Maturity()
        {
            Value = 2;
        }

        public Maturity(int value)
        {
            if (value < 7 && value > 0)
            {
                Value = value;
            }
        }

        public Maturity(int value, bool isIntermediate) : this(value)
        {
            IsIntermediate = isIntermediate;
        }

        public Maturity(double value)
            : this((int)value)
        {
            if (value > Value)
            {
                IsIntermediate = true;
            }
        }

        public Maturity(string value)
        {
            try
            {
                if (value.Contains('+')) // like '2+'
                {
                    Value = int.Parse(value.TrimEnd('+'));
                    IsIntermediate = true;
                }
                else if (value.Contains('-')) // like '2-3'
                {
                    Value = int.Parse(value.Split('-')[0]);
                    IsIntermediate = true;
                }
                else
                {
                    int valueNum = int.Parse(value);

                    if (valueNum < 7 && valueNum > 0)
                    {
                        Value = valueNum;
                    }
                }
            }
            catch { 

            }
        }

        public Maturity(object value) : this(value.ToString())
        { }

        #endregion

        #region Methods

        public override string ToString()
        {
            if (IsIntermediate)
            {
                if (Value < 6)
                    return Value.ToRoman() + "–" + (Value + 1).ToRoman();
                else
                    return Value.ToRoman() + "–" + (2).ToRoman();
            }
            else
            {
                return Value.ToRoman();
            }
        }

        public static Maturity Parse(string value)
        {
            return new Maturity(value);
        }

        public static Maturity FromDouble(double value)
        {
            return new Maturity(value);
        }

        #endregion

        #region Cast implementations

        public static implicit operator Maturity(string value)
        {
            return new Maturity(value);
        }

        public static implicit operator string(Maturity stage)
        {
            return stage.ToString();
        }

        public static explicit operator Maturity(int value)
        {
            return new Maturity(value);
        }

        public static implicit operator int(Maturity stage)
        {
            return stage.Value;
        }

        #endregion

        #region IComparable implementations

        public static bool operator ==(Maturity a, Maturity b)
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
            return (a.Value == b.Value && a.IsIntermediate == b.IsIntermediate);
        }

        public static bool operator !=(Maturity a, Maturity b)
        {
            return !(a == b);
        }

        int IComparable.CompareTo(object obj)
        {
            return Compare(this, (Maturity)obj);
        }

        public static int Compare(Maturity value1, Maturity value2)
        {
            int result1 = value1.Value * 10;
            if (value1.IsIntermediate) result1 += 5;

            int result2 = value2.Value * 10;
            if (value2.IsIntermediate) result2 += 5;

            return value1.GetHashCode() - value2.GetHashCode();
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast return false.
            Maturity p = obj as Maturity;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Value == p.Value && IsIntermediate == p.IsIntermediate);
        }

        public bool Equals(Maturity p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (p.Value == Value && IsIntermediate == p.IsIntermediate);
        }

        public override int GetHashCode()
        {
            int result = Value * 10;
            if (IsIntermediate) result += 5;

            return result;
        }

        #endregion

        #region IConvertible implementations

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Value > 1 & Value < 6;
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Value);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(Value);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Value);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Value);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Value;
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Value;
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Value);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Value, conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Value);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Value);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Value);
        }                

        #endregion
    }

    public class MaturityConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string)) ? true : base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return new Maturity(value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return value.ToString();
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
