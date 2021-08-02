using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Mayfly.Wild
{
    public partial class Sex : IComparable, IFormattable
    {
        #region Properties

        public int Value = 0;

        #endregion

        #region Constructors

        public Sex(int value)
        {
            Value = value;
        }

        public Sex(string value)
        {
            switch (value)
            {
                case "2":
                case "f":
                case "F":
                case "fem":
                case "female":
                    Value = 2;
                    break;
                case "1":
                case "m":
                case "M":
                case "mal":
                case "male":
                    Value = 1;
                    break;
                case "0":
                case "j":
                case "J":
                case "juv.":
                    Value = 0;
                    break;
            }
        }

        #endregion

        #region Static values

        public static Sex Juvenile
        {
            get
            {
                return new Sex(0);
            }
        }

        public static Sex Male
        {
            get
            {
                return new Sex(1);
            }
        }

        public static Sex Female
        {
            get
            {
                return new Sex(2);
            }
        }

        #endregion

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
            switch (format.ToLower())
            {
                case "l":
                    switch (Value)
                    {
                        case 2:
                            return "F";
                        case 1:
                            return "M";
                        case 0:
                            return "J";
                        default:
                            return string.Empty;
                    }

                case "c":
                    switch (Value)
                    {
                        case 2:
                            return "♀";
                        case 1:
                            return "♂";
                        case 0:
                            return "juv.";
                        default:
                            return string.Empty;
                    }

                default:
                    switch (Value)
                    {
                        case 2:
                            return Resources.Interface.Sex.ResourceManager.GetString("Female", provider as CultureInfo);
                        case 1:
                            return Resources.Interface.Sex.ResourceManager.GetString("Male", provider as CultureInfo);
                        case 0:
                            return Resources.Interface.Sex.ResourceManager.GetString("Juvenile", provider as CultureInfo);
                        default:
                            return string.Empty;
                    }
            }
        }

        public static Sex Parse(string value)
        {
            return new Sex(value);
        }

        #region Cast implementations

        public static implicit operator Sex(string value)
        {
            return new Sex(value);
        }

        public static implicit operator string(Sex value)
        {
            return value.ToString();
        }

        public static implicit operator Sex(int value)
        {
            return new Sex(value);
        }

        public static implicit operator int(Sex value)
        {
            return value.Value;
        }

        #endregion

        #region IComparable implementations

        public static bool operator ==(Sex a, Sex b)
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
            return a.Value == b.Value;
        }

        public static bool operator !=(Sex a, Sex b)
        {
            return !(a == b);
        }

        int IComparable.CompareTo(object obj)
        {
            return Compare(this, (Sex)obj);
        }

        public static int Compare(Sex value1, Sex value2)
        {
            return value1.Value - value2.Value;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast return false.
            Sex p = obj as Sex;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Value == p.Value);
        }

        public bool Equals(Sex p)
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
            return (int)(Value * 100);
        }

        #endregion
    }
}
