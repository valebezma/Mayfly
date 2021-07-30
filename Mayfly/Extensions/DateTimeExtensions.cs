using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Mayfly.Extensions
{
    public static class DateTimeExtensions
    {

        public static string ToString(this DateTime dt, IFormatProvider formatProvider, string format)
        {
            if (format == null) return dt.ToString();

            string[] args = format.Split(' ');

            if (args.Length > 1 && args[0] == "collapse")
            {
                switch (args[1])
                {
                    case "daytime":
                        return dt.GetDayPeriod();

                    case "day":
                        switch (args[3])
                        {
                            case "week":
                                return dt.ToString("dddd");

                            case "month":
                                return dt.ToString("dd");

                            case "year":
                                return dt.ToString("M");

                            default:
                                return dt.Date.ToString();
                        }

                    case "week":
                        switch (args[3])
                        {
                            case "month":
                                return dt.GetWeekOfMonth().ToString();
                            case "year":
                                var cal = DateTimeFormatInfo.CurrentInfo.Calendar;
                                return cal.GetWeekOfYear(dt,
                                    CalendarWeekRule.FirstDay,
                                    DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek).ToString();
                            default:
                                return dt.Date.ToString();
                        }

                    case "month":
                        return dt.ToString("MMMM");

                    case "season":
                        return dt.GetSeason();

                    case "year":
                        return dt.ToString("yyyy");

                    case "decade":
                        return string.Format(Resources.DateTime.DecadeMask, (dt.Year / 10));

                    default:
                        return dt.ToString();
                }
            }
            else switch (format)
                {
                    case "a": // AGO
                        TimeSpan span = DateTime.Now - dt;
                        return span.ToString();

                    case "sec":
                        return dt.ToString("ss");

                    case "min":
                        return dt.ToString("mm");

                    case "hour":
                        return dt.ToString("HH");

                    case "time":
                        return dt.ToString("G");

                    case "d hour":
                        return string.Format(Constants.TwoWordsMask, dt.ToString("d"), dt.Hour);

                    case "daytime":
                        return string.Format(Constants.TwoWordsMask, dt.ToString("d"), dt.GetDayPeriod());

                    case "week":
                        return string.Format(Resources.DateTime.WeekOfYear,
                            dt,
                            DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFullWeek, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                            );

                    case "season":
                        return dt.GetExactSeason();

                    case "decade":
                        return string.Format(Resources.DateTime.DecadeMask, (dt.Year / 10));

                    case "friendly":
                        return dt.GetDatesDescription();

                    default:
                        return dt.ToString(format);
                }
        }

        public static string ToString(this DateTime dt, string format)
        {
            return ToString(dt, CultureInfo.CurrentCulture, format);
        }


        public static string GetFormat(PeriodType period, bool collapse, PeriodType option)
        {
            string result = string.Empty;

            if (collapse)
            {
                switch (period)
                {
                    case PeriodType.Seconds:
                        result += "sec";
                        break;
                    case PeriodType.Minutes:
                        result += "min";
                        break;
                    case PeriodType.Hours:
                        result += "hour";
                        break;
                    case PeriodType.Time:
                        result += "T";
                        break;
                    case PeriodType.DayPeriod:
                        result += "collapse daytime";
                        break;
                    case PeriodType.Days:
                        result += "collapse day";
                        break;
                    case PeriodType.Weeks:
                        result += "collapse week";
                        break;
                    case PeriodType.Months:
                        result += "collapse month";
                        break;
                    case PeriodType.Seasons:
                        result += "collapse season";
                        break;
                    case PeriodType.Years:
                        result += "yyyy";
                        break;
                    case PeriodType.Decades:
                        result += "collapse decade";
                        break;
                }

                switch (option)
                {
                    case PeriodType.Weeks:
                        result += " of week";
                        break;
                    case PeriodType.Months:
                        result += " of month";
                        break;
                    case PeriodType.Years:
                        result += " of year";
                        break;
                }
            }
            else switch (period)
                {
                    case PeriodType.Seconds:
                        result += "G";
                        break;
                    case PeriodType.Minutes:
                        result += "g";
                        break;
                    case PeriodType.Hours:
                        result += "d hour";
                        break;
                    case PeriodType.Time:
                        result += "time";
                        break;
                    case PeriodType.DayPeriod:
                        result += "daytime";
                        break;
                    case PeriodType.Days:
                        result += "d";
                        break;
                    case PeriodType.Weeks:
                        result += "week";
                        break;
                    case PeriodType.Months:
                        result += "y";
                        break;
                    case PeriodType.Seasons:
                        result += "season";
                        break;
                    case PeriodType.Years:
                        result += "yyyy";
                        break;
                    case PeriodType.Decades:
                        result += "decade";
                        break;
                    default:
                        result += "friendly";
                        break;
                }

            return result;
        }


        public static string GetSeason(this DateTime dateTime)
        {
            if (dateTime.Month <= 2 || dateTime.Month == 12) return Resources.DateTime.Season0;
            if (dateTime.Month <= 5) return Resources.DateTime.Season1;
            if (dateTime.Month <= 8) return Resources.DateTime.Season2;
            return Resources.DateTime.Season3;
        }

        public static string GetExactSeason(this DateTime dateTime)
        {
            int y = dateTime.Year;

            if (dateTime.Month <= 2) return string.Format(Constants.TwoWordsMask, Resources.DateTime.Season0, (y - 1) + Constants.Null + y);
            if (dateTime.Month == 12) return string.Format(Constants.TwoWordsMask, Resources.DateTime.Season0, y + Constants.Null + (y + 1));
            if (dateTime.Month <= 5) return string.Format(Constants.TwoWordsMask, Resources.DateTime.Season1, y);
            if (dateTime.Month <= 8) return string.Format(Constants.TwoWordsMask, Resources.DateTime.Season2, y);
            return string.Format(Constants.TwoWordsMask, Resources.DateTime.Season3, y);
        }

        public static string GetDayPeriod(this DateTime dateTime)
        {
            if (dateTime.Hour < 6) return Resources.DateTime.DayPeriod0;
            if (dateTime.Hour < 12) return Resources.DateTime.DayPeriod1;
            if (dateTime.Hour < 18) return Resources.DateTime.DayPeriod2;
            return Resources.DateTime.DayPeriod3;
        }

        public static int GetWeekOfMonth(this DateTime dateTime)
        {
            DateTime beginningOfMonth = new DateTime(dateTime.Year, dateTime.Month, 1);

            while (dateTime.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                dateTime = dateTime.AddDays(1);

            return (int)Math.Truncate((double)dateTime.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }
        

        public static string GetDatesDescription(this DateTime dt)
        {
            return dt.GetDatesDescription(DateTime.Today);
        }

        public static string GetDatesDescription(this DateTime dt, DateTime now)
        {
            TimeSpan span = now - dt;

            if (span.TotalSeconds < 0.0)
            {
                return dt.ToString("g");
            }
            else if (span < TimeSpan.FromDays(1.0))
            {
                return string.Format(Resources.DateTime.DescDay, dt);
            }
            else if (span < TimeSpan.FromDays(7.0))
            {
                return string.Format(Resources.DateTime.DescWeek, dt, dt);
            }
            else if (span < TimeSpan.FromDays(30))
            {
                return string.Format(Resources.DateTime.DescMonth, dt);
            }
            else if (span < TimeSpan.FromDays(365))
            {
                return string.Format(Resources.DateTime.DescYear, dt);
            }
            else
            {
                return dt.ToString("d");
            }
        }

        public static string GetDatesDescription(this IEnumerable<DateTime> datesList)
        {
            if (datesList.Count() == 0) return string.Empty;

            List<DateTime> dates = new List<DateTime>(datesList);
            dates.Sort();
            if (dates.Last() - dates.First() < TimeSpan.FromDays(365))
            {
                return dates.GetDatesDescription(dates.Last());
            }
            else
            {
                return dates.GetDatesDescription(DateTime.Today);
            }
        }

        public static string GetDatesDescription(this IEnumerable<DateTime> dates, DateTime now)
        {
            return dates.GetDatesDescription(now, 2);
        }

        public static string GetDatesDescription(this IEnumerable<DateTime> dates, DateTime now, int allowedGap)
        {
            List<DateTime> futureDates = new List<DateTime>();
            List<DateTime> lastYearDates = new List<DateTime>();
            List<DateTime> previousDates = new List<DateTime>();

            foreach (DateTime dt in dates)
            {
                if (dt > now) {
                    futureDates.Add(dt);
                } else if (now - dt <= TimeSpan.FromDays(365)) {
                    lastYearDates.Add(dt);
                } else {
                    previousDates.Add(dt);
                }
            }

            string result = string.Empty;

            if (previousDates.Count > 0)
            {
                int[] years = GetYears(previousDates);

                result += years[0];
                if (years.Length > 1) result += ", ";
                bool gapStarted = false;

                for (int i = 1; i < years.Length; i++)
                {
                    if (years[i] == years[i - 1] + 1)
                    {
                        if (result.Last() != '—') {
                            gapStarted = true;
                            result = result.TrimEnd(", ".ToCharArray());
                            result += "—";
                        }
                        continue;
                    }

                    if (gapStarted) {
                        gapStarted = false;
                        result += years[i - 1];
                        result += ", ";
                    }

                    result += years[i];
                    result += ", ";
                }

                if (gapStarted)
                {
                    result += years.Last();
                    result += ", ";
                }
                
                result += ", ";
            }
            
            if (lastYearDates.Count > 0)
            {
                foreach (DateTime[] bunch in lastYearDates.GetDatesBunches(allowedGap))
                {
                    result += bunch.GetDatesRangeDescription() + ", ";
                }
            }

            return result.TrimEnd(", ".ToCharArray());
        }

        public static List<DateTime[]> GetDatesBunches(this IEnumerable<DateTime> unsortedDates)
        {
            return unsortedDates.GetDatesBunches(2);
        }

        public static List<DateTime[]> GetDatesBunches(this IEnumerable<DateTime> unsortedDates, int allowedGap)
        {
            List<DateTime[]> result = new List<DateTime[]>();

            List<DateTime> dates = new List<DateTime>();
            dates.AddRange(unsortedDates);
            dates.Sort();

            List<DateTime> bunch = new List<DateTime>();

            // collect bunch of dates 
            for (int i = 0; i < dates.Count; i++)
            {
                if (bunch.Count == 0)
                {
                    bunch.Add(dates[i]);
                    continue;
                }

                if (dates[i].Date - dates[i - 1].Date <= TimeSpan.FromDays(allowedGap))
                {
                    bunch.Add(dates[i].Date);
                }
                else
                {
                    DateTime[] cc = new DateTime[bunch.Count];
                    bunch.CopyTo(cc);
                    result.Add(cc);
                    bunch.Clear();
                    i--;
                }
            }

            if (bunch.Count > 0)
            {
                DateTime[] cc = new DateTime[bunch.Count];
                bunch.CopyTo(cc);
                result.Add(cc);
            }

            return result;
        }

        public static string GetDatesRangeDescription(this IEnumerable<DateTime> dates)
        {
            List<DateTime> bunch = new List<DateTime>();
            bunch.AddRange(dates);
            bunch.Sort();

            DateTime startDate = bunch[0];
            DateTime endDate = bunch.Last();

            string format;
            bool dayFirst = DateTimeFormatInfo.CurrentInfo.MonthDayPattern.StartsWith("d");
            string dayPart = "dd";
            string monthPart = "MMM";
            string yearPart = "yyyy";

            if (bunch.Count == 1)
            {
                format = dayFirst ? "{0:"+ dayPart + " MMMM yyyy}" : "{0:MMMM " + dayPart + ", yyyy}";
                return string.Format(format, startDate);
            }
            else
            {
                if (startDate.Year == endDate.Year)
                {
                    if (startDate.Month == endDate.Month)
                    {
                        format = dayFirst ? "{0:" + dayPart + "}—{1:" + dayPart + " " + monthPart + " " + yearPart + "}" : "{0:" + monthPart + " " + dayPart + "}—{1:" + dayPart + ", " + yearPart + "}";
                    }
                    else
                    {
                        format = dayFirst ? "{0:" + dayPart + " " + monthPart + "} — {1:" + dayPart + " " + monthPart + " " + yearPart + "}" : "{0:" + monthPart + " " + dayPart + "} — {1:" + monthPart + " " + dayPart + ", " + yearPart + "}";
                    }
                }
                else
                {
                    format = dayFirst ? "{0:" + dayPart + " " + monthPart + " " + yearPart + "} — {1:" + dayPart + " " + monthPart + " " + yearPart + "}" : "{0:" + monthPart + " " + dayPart + ", " + yearPart + "} — {1:" + monthPart + " " + dayPart + ", " + yearPart + "}";
                }

                return string.Format(format, startDate, endDate);
            }
        }



        public static int[] GetYears(this IEnumerable<DateTime> datesList)
        {
            List<int> result = new List<int>();
            foreach (DateTime date in datesList)
            {
                if (result.Contains(date.Year)) continue;
                result.Add(date.Year);
            }
            result.Sort();
            return result.ToArray();
        }

        public static string ToString(this TimeSpan ts, string format)
        {
            if (format == null) return ts.ToString();

            switch (format)
            {
                case "HH:mm":
                    return (int)ts.TotalHours + ts.ToString(@"\:mm");
                default:
                    return ts.ToString(format);
            }
        }
    }

    public enum PeriodType
    {
        Seconds,
        Minutes,
        Hours,
        Time,
        DayPeriod,
        Days,
        Weeks,
        Months,
        Seasons,
        Years,
        Decades
    }
}
