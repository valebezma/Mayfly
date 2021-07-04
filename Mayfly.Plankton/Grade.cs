using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Mayfly.Plankton
{
    [TypeConverter(typeof(GradeConverter))]
    public partial class Grade : IConvertible, IComparable
    {
        public Grade(int value)
        {
            Value = value;
        }

        public Grade(string value)
        {
            try
            {
                Value = Convert.ToInt32(value);
            }
            catch
            {
                if (new string[] { Resources.Grade.CacoonR.ToLowerInvariant(), 
                    Resources.Grade.Cacoon.ToLowerInvariant() }.Contains(value.ToLowerInvariant()))
                {
                    Value = 0;
                }
                else if (new string[] { Resources.Grade.LarvaR.ToLowerInvariant(), 
                    Resources.Grade.Larva.ToLowerInvariant() }.Contains(value.ToLowerInvariant())) 
                {
                    Value = 1;
                }
                else if (new string[] { Resources.Grade.SubimagoR.ToLowerInvariant(), 
                    Resources.Grade.Subimago.ToLowerInvariant() }.Contains(value.ToLowerInvariant())) 
                {
                    Value = 5;
                }
                else if (new string[] { Resources.Grade.ImagoR.ToLowerInvariant(), 
                    Resources.Grade.Imago.ToLowerInvariant() }.Contains(value.ToLowerInvariant())) 
                {
                    Value = 6;
                }
                else switch (value)
                {
                    case "e":
                    case "egg":
                        Value = 0;
                        break;
                    case "larva":
                    case "l":
                        Value = 1;
                        break;
                    case "subimago":
                    case "si":
                    case "s":
                        Value = 5;
                        break;
                    case "adult":
                    case "ad":
                    case "a":
                    case "imago":
                    case "i":
                        Value = 6;
                        break;
                }
            }
        }

        public Grade(object value) : this(value.ToString())
        { }

        public int Value = int.MinValue;



        public override string ToString()
        {
            switch (Value)
            {
                case 0:
                    return Resources.Grade.Cacoon;
                case 1:
                    return Resources.Grade.Larva;
                case 5:
                    return Resources.Grade.Subimago;
                case 6:
                    return Resources.Grade.Imago;
            }

            return string.Empty;
        }

        public static Grade Parse(string value)
        {
            return new Grade(value);
        }

        public string ToShortString()
        {
            switch (Value)
            {
                case 0:
                    return Resources.Grade.CacoonR;
                case 1:
                    return Resources.Grade.LarvaR;
                case 5:
                    return Resources.Grade.SubimagoR;
                case 6:
                    return Resources.Grade.ImagoR;
            }

            return string.Empty;
        }



        #region Operators

        public static explicit operator Grade(string value)
        {
            return new Grade(value);
        }

        public static explicit operator Grade(double value)
        {
            return new Grade(value);
        }

        public static explicit operator Grade(int value)
        {
            return new Grade(value);
        }

        public static explicit operator int(Grade grade)
        {
            return grade.Value;
        }

        public static explicit operator double(Grade grade)
        {
            return (double)grade.Value;
        }

        public static explicit operator string(Grade grade)
        {
            return grade.ToString();
        }

        #endregion

        #region IComparable

        int IComparable.CompareTo(object obj)
        {
            return Compare(this, (Grade)obj);
        }

        public static int Compare(Grade Value1, Grade Value2)
        {
            return Value1.Value - Value2.Value;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast return false.
            Grade p = obj as Grade;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Value == p.Value);
        }

        public bool Equals(Grade p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (p.Value == Value);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        #endregion

        #region IConvertible

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Value == int.MinValue;
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
            return Value;
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Value);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Value);
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

    public class GradeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string)) ? true : base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return new Grade(value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return value.ToString();
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
