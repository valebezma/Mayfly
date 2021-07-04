using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mayfly.Fish
{
    public partial class Gender
    {
        public Gender(int value)
        {
            Value = value;
        }

        public Gender(string value)
        {
            switch (value)
            {
                case "♀": Value = 2; break;
                case "♂": Value = 1; break;
                case "juv.": Value = 0; break;
                default:
                    if (AppProperties.SignFemale.Contains(value))
                    {
                        Value = 2;
                    }

                    if (AppProperties.SignMale.Contains(value))
                    {
                        Value = 1;
                    }

                    if (AppProperties.SignJuv.Contains(value))
                    {
                        Value = 0;
                    }

                    break;
            }
        }

        public int Value = int.MinValue;

        public override string ToString()
        {
            switch (Value)
            {
                case 2:
                    return Resources.Sex.Female;
                case 1:
                    return Resources.Sex.Male;
                case 0:
                    return Resources.Sex.Juvenile;
            }

            return string.Empty;
        }

        public string ToSymbol()
        {
            switch (Value)
            {
                case 2:
                    return "♀";
                case 1:
                    return "♂";
                case 0:
                    return "juv.";
            }
            return string.Empty;
        }

        public static Gender Juvenile
        {
            get
            {
                return new Gender(0);
            }
        }

        public static Gender Male
        {
            get
            {
                return new Gender(1);
            }
        }

        public static Gender Female
        {
            get
            {
                return new Gender(2);
            }
        }
    }
}
