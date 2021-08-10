using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using Mayfly.Extensions;

namespace Mayfly.Geographics
{
    public class Waypoint : IFormattable, IComparable
    {
        public string Name { get; set; }

        public Coordinate Latitude { get; set; }

        public Coordinate Longitude { get; set; }

        public double Altitude { get; set; }

        public DateTime TimeMark { get; set; }

        public string Description { get; set; }

        public bool IsNameNull { get { return Name == null || Name == string.Empty; } }

        public bool IsDescriptionNull { get { return Description == null || Description == string.Empty; } }

        public bool IsLatitudeNull { get { return Latitude == null; } }

        public bool IsLongitudeNull { get { return Longitude == null; } }

        public bool IsAltitudeNull { get { return double.IsNaN(Altitude); } }

        public bool IsTimeMarkNull { get { return TimeMark == DateTime.MinValue; } }

        public bool IsEmpty { get { return (Latitude.Degrees + Longitude.Degrees) == 0; } }



        public Waypoint(Coordinate lat, Coordinate lng, double alt, DateTime date)
        {
            Latitude = lat;
            Longitude = lng;
            Altitude = alt;
            TimeMark = date;
        }

        public Waypoint(Coordinate lat, Coordinate lng, double alt) : this(lat, lng, alt, DateTime.Today) { }

        public Waypoint(Coordinate lat, Coordinate lng, DateTime date) : this(lat, lng, double.NaN, date) { }

        public Waypoint(Coordinate lat, Coordinate lng) : this(lat, lng, double.NaN, DateTime.Today) { }

        public Waypoint(double lat, double lng, double alt, DateTime date) : this(new Coordinate(lat, false), new Coordinate(lng, true), alt, date) { }

        public Waypoint(double lat, double lng, double alt) : this(lat, lng, alt, DateTime.MinValue) { }

        public Waypoint(double lat, double lng, DateTime date) : this(lat, lng, double.NaN, date) { }

        public Waypoint(double lat, double lng) : this(lat, lng, double.NaN, DateTime.MinValue) { }

        public Waypoint(string value)
        {
            string[] values = value.Split(',');

            Latitude = new Coordinate(values[0], false);
            Longitude = new Coordinate(values[1], true);
            TimeMark = DateTime.Today;
            Altitude = Convert.ToDouble(values[2], CultureInfo.InvariantCulture);
        }

        

        public string Protocol
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2}", new object[] { 
                    Latitude.Degrees, Longitude.Degrees, Altitude 
                });
            }
        }

        public static Waypoint Parse(string value)
        {
            try
            {
                foreach (string element in new string[] { ", ", " ", "°", "'", "`", "\"" })
                {
                    value = value.Replace(element, ";");
                }

                value = value.ToLowerInvariant().Replace(",", ".");

                string[] elements = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                //Coordinate lat = new Coordinate(0, false);
                //Coordinate lng = new Coordinate(0, true);

                string format = "d";
                string lt = string.Empty;
                string ln = string.Empty;

                switch (elements.Length)
                {
                    case 0:
                        return Waypoint.Empty;

                    case 2:
                        lt = "0" + elements[0];
                        ln = elements[1];
                        if (ln.Substring(0, ln.IndexOf('.')).StripNonNumbers().Length == 2) ln = "0" + ln;
                        break;

                    case 4:
                        format = "dm";
                        lt = "0" + elements[0] + elements[1];
                        if (elements[2].StripNonNumbers().Length == 2) elements[2] = "0" + elements[2];
                        ln = elements[2] + elements[3];
                        break;

                    case 6:
                        format = "dms";
                        lt = "0" + elements[0] + elements[1] + elements[2];
                        if (elements[3].StripNonNumbers().Length == 2) elements[3] = "0" + elements[3];
                        ln = elements[3] + elements[4] + elements[5];
                        break;

                    case 8:
                        format = "dms";
                        lt = elements[0] + "0" + elements[1] + elements[2] + elements[3];
                        if (elements[4].StripNonNumbers().Length == 2) elements[4] = "0" + elements[4];
                        ln = elements[4] + "0" + elements[5] + elements[6] + elements[7];
                        break;
                }

                Coordinate lat = new Coordinate(lt.StripNonNumbers(), false, format, lt.Contains('-') | lt.Contains('s'));
                Coordinate lng = new Coordinate(ln.StripNonNumbers(), true, format, ln.Contains('-') | ln.Contains('w'));

                return new Waypoint(lat, lng);
            }
            catch { return null; }
        }

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
            if (Latitude.Degrees + Longitude.Degrees == 0) { return Constants.Null; }
            else { return string.Format("{0} {1}", this.Latitude.ToString(format, provider), this.Longitude.ToString(format, provider)); }
        }

        #endregion

        #region IComparable

        public static bool operator ==(Waypoint a, Waypoint b)
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
            return a.Latitude.Equals(b.Latitude) && a.Longitude.Equals(b.Longitude);
        }

        public static bool operator !=(Waypoint a, Waypoint b)
        {
            return !(a == b);
        }

        int IComparable.CompareTo(object obj)
        {
            return Waypoint.Compare(this, (Waypoint)obj);
        }

        int CompareTo(Waypoint wpt)
        {
            return Waypoint.Compare(this, wpt);
        }

        public static int Compare(Waypoint a, Waypoint b)
        {
            int tim = DateTime.Compare(a.TimeMark, b.TimeMark);

            if (tim != 0) return tim;

            int lon = Coordinate.Compare(a.Longitude, b.Longitude);

            if (lon != 0) return lon;

            int lat = Coordinate.Compare(a.Latitude, b.Latitude);
            
            return lat;
        }

        public override bool Equals(Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast return false.
            Waypoint p = obj as Waypoint;
            if (p == null) return false;

            return Equals(p);
        }

        public bool Equals(Waypoint p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this == p);
        }

        public override int GetHashCode()
        {
            return this.Latitude.GetHashCode() * this.Longitude.GetHashCode();
        }

        #endregion


        public static Waypoint Empty
        {
            get
            {
                return new Waypoint(0, 0);
            }
        }

        public static double Distance(Waypoint a, Waypoint b)
        {
            var R = 6371000; // metres

            var dLat = b.Latitude.Radians - a.Latitude.Radians;
            var dLng = b.Longitude.Radians - a.Longitude.Radians;

            var alpha = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(a.Latitude.Radians) * Math.Cos(b.Latitude.Radians) *
                    Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(alpha), Math.Sqrt(1 - alpha));

            return R * c;
        }

        public static Waypoint GetSegmentPoint(Waypoint start, Waypoint end, double percent)
        {
            double dx = (start.Longitude.Degrees - end.Longitude.Degrees) * percent;
            double dy = (start.Latitude.Degrees - end.Latitude.Degrees) * percent;
            return new Waypoint(start.Latitude.Degrees + dx, start.Longitude.Degrees + dy);
        }

        public static Waypoint GetWaypoint(Image img)
        {
            try
            {
                Coordinate lat = GetCoordinate(img.GetPropertyItem(1), img.GetPropertyItem(2), false);
                Coordinate lng = GetCoordinate(img.GetPropertyItem(3), img.GetPropertyItem(4), true);

                Waypoint wpt = new Waypoint(lat, lng);

                wpt.Altitude = (double)BitConverter.ToUInt32(img.GetPropertyItem(6).Value, 0);

                try { wpt.TimeMark = Convert.ToDateTime(BitConverter.ToUInt32(img.GetPropertyItem(7).Value, 0)); }
                catch { }

                if (wpt.IsTimeMarkNull)
                {
                    string dateTakenTag = System.Text.Encoding.ASCII.GetString(img.GetPropertyItem(36867).Value);
                    string[] parts = dateTakenTag.Split(':', ' ');
                    int year = int.Parse(parts[0]);
                    int month = int.Parse(parts[1]);
                    int day = int.Parse(parts[2]);
                    int hour = int.Parse(parts[3]);
                    int minute = int.Parse(parts[4]);
                    //int second = int.Parse(parts[5]);
                    wpt.TimeMark = new DateTime(year, month, day, hour, minute, 0);
                }

                return wpt;
            }
            catch
            {
                return null;
            }
        }

        private static Coordinate GetCoordinate(PropertyItem propItemRef,
            PropertyItem propItem, bool isLongitude)
        {
            char label = (char)((byte[])propItemRef.Value)[0];

            uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
            uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
            double degrees = degreesNumerator / (double)degreesDenominator;

            uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
            uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
            double minutes = minutesNumerator / (double)minutesDenominator;

            uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
            uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
            double seconds = secondsNumerator / (double)secondsDenominator;

            double coordinate = degrees + (minutes / 60f) + (seconds / 3600f);

            string gpsRef = System.Text.Encoding.ASCII.GetString(new byte[1] { propItemRef.Value[0] }); //N, S, E, or W
            if (gpsRef == "S" || gpsRef == "W")
                coordinate = 0 - coordinate;
            
            return new Coordinate(coordinate, isLongitude);
        }

        public static Waypoint GetCenterOf(IEnumerable<Waypoint> wpts)
        {
            double minLat = 90.0;
            double maxLat = -90.0;

            double minLng = 180.0;
            double maxLng = -180.0;

            foreach (Waypoint wpt in wpts)
            {
                minLat = Math.Min(minLat, wpt.Latitude.Degrees);
                maxLat = Math.Max(maxLat, wpt.Latitude.Degrees);

                minLng = Math.Min(minLng, wpt.Longitude.Degrees);
                maxLng = Math.Max(maxLng, wpt.Longitude.Degrees);
            }

            return new Waypoint(minLat + .5 * (maxLat - minLat), minLng + .5 * (maxLng - minLng));
        }

        public static Waypoint GetCenterOf(IEnumerable<Waypoint> wpts, ref double radius)
        {
            Waypoint center = GetCenterOf(wpts);
            double distance = 0.0;
            foreach (Waypoint wpt in wpts) {
                distance = Math.Max(distance, Distance(wpt, center));
            }
            radius = distance;
            return center;
        }



        public Uri GetLink()
        {
            if (IsEmpty) return null;
            else return new Uri(string.Format(CultureInfo.InvariantCulture,
                @"https://yandex.ru/maps/?ll={0},{1}&z=15&mode=whatshere&whatshere[point]={2},{3}&whatshere[zoom]=15",
                this.Longitude.Degrees, this.Latitude.Degrees,
                this.Longitude.Degrees, this.Latitude.Degrees));
        }

        public string GetHTMLReference(string format)
        {
            if (IsEmpty) return string.Empty;
            return GetLink().GetHTMLReference(ToString(format));
        }

        public string GetHTMLReference()
        {
            return GetHTMLReference(UserSettings.FormatCoordinate);
        }



        public static Uri GetLink(IEnumerable<Waypoint> locations)
        {
            if (locations.Count() == 1)
                return locations.ElementAt(0).GetLink();

            double radius = 0.0;
            Waypoint center = Waypoint.GetCenterOf(locations, ref radius);
            string path = string.Empty;

            foreach (Waypoint wpt in locations)
            {
                if (wpt.IsEmpty) continue;
                path += string.Format(CultureInfo.InvariantCulture, "{0},{1}~",
                    wpt.Latitude.Degrees, wpt.Longitude.Degrees);
            }

            path.TrimEnd('~');

            return new Uri(string.Format(CultureInfo.InvariantCulture,
                @"https://yandex.ru/maps/?ll={0},{1}&mode=routes&rtext={2}\\",
                center.Longitude.Degrees, center.Latitude.Degrees, path));
        }

        public static string GetHTMLReference(IEnumerable<Waypoint> locations, string format)
        {
            return GetLink(locations).GetHTMLReference(Waypoint.GetCenterOf(locations).ToString(format));
        }
    }
}
