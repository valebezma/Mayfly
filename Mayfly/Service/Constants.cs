using System.Drawing;
using System.Globalization;
using Mayfly.Extensions;

namespace Mayfly
{
    public static class Constants
    {
        public static string Tab
        { get { return "\t"; } }

        public static string Return
        { get { return "\r"; } }

        public static string Break
        { get { return "\n"; } }

        public static string Numbers
        {
            get
            {
                return CultureInfo.CurrentCulture.NumberFormat.NativeDigits.Merge(string.Empty) + CultureInfo.CurrentCulture.NumberFormat.NegativeSign + "%";
            }
        }

        public static string Forbidden
        { get { return @"@#^&_+<>/:{}[]"; } }

        public static string Separators
        { get { return ",;:.|/_"; } }

        public static string Null
        { get { return "—"; } }

        public static string ElementSeparator
        {
            get
            {
                // CultureInfo.CurrentCulture.TextInfo.ListSeparator
                return (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",") ? "; " : ", ";
            }
        }

        public static string Check
        { get { return "✓"; } }

        public static string Negative
        { get { return "X"; } }

        public static string Asterisk
        { get { return "*"; } }

        public static string RepeatedValue
        { get { return "-\"-"; } }

        public static string TwoWordsMask
        { get { return "{0} {1}"; } }

        public static string Fraction
        { get { return "{0:N3}"; } }

        public static string ValueWithFraction
        { get { return "{0} ({1:N3})"; } }

        public static string ValueOutOfTotalWithFraction
        { get { return "{0} / {1} ({2:N3})"; } }

        public static string TotalOrMean
        { get { return string.Format("{0} / {1}", Resources.Interface.Total, Resources.Interface.Mean); } }

        public static Color InfantColor
        {
            get
            {
                return Color.LightGray;
            }
        }

        public static Color MainAccent
        {
            get
            {
                return Color.RoyalBlue;
            }
        }
    }
}
