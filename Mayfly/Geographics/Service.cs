using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Data;
using System.Xml.Linq;
using Mayfly.Extensions;
using System.Drawing;

namespace Mayfly.Geographics
{
    public class Service
    {
        public static Waypoint[] GetWaypoints(string filename)
        {
            FileInfo locationFile = new FileInfo(filename);
            switch (locationFile.Extension)
            {
                case ".gpx":
                    return GetWaypointsGpx(locationFile.FullName);
                case ".kml":
                    return GetWaypointsKml(locationFile.FullName);
                case ".wpt":
                    return GetWaypointsWpt(locationFile.FullName);
                case ".jpeg":
                case ".jpg":
                    return GetWaypointsJpg(locationFile.FullName);
                default:
                    return new Waypoint[0];
            }
        }

        public static Waypoint[] GetWaypoints(string[] filenames)
        {
            List<Waypoint> result = new List<Waypoint>();

            foreach (string filename in filenames)
            {
                result.AddRange(GetWaypoints(filename));
            }

            return result.ToArray();
        }

        public static Waypoint[] GetWaypointsWpt(string filename)
        {
            List<Waypoint> result = new List<Waypoint>();
            string[] WPTContent = File.ReadAllLines(filename, Encoding.Default);
            if (WPTContent.Length > 4)
            {
                for (int i = 4; i < WPTContent.Length; i++)
                {
                    try
                    {
                        string[] WPTValues = WPTContent[i].Split(',');

                        if (WPTValues[2].IsAcceptable() && WPTValues[3].IsAcceptable())
                        {
                            double lat = double.Parse(WPTValues[2].Trim(), CultureInfo.InvariantCulture);
                            double lng = double.Parse(WPTValues[3].Trim(), CultureInfo.InvariantCulture);
                            Waypoint resultWpt = new Waypoint(lat, lng);

                            if (WPTValues[1].Trim() != string.Empty) resultWpt.Name = WPTValues[1].Trim();

                            if (WPTValues[14].Trim() != string.Empty)
                            {
                                double Alt = double.Parse(WPTValues[14].Trim(), CultureInfo.InvariantCulture);
                                if (Alt != -777) resultWpt.Altitude = Alt;
                                if (Alt != 0) resultWpt.Altitude = Alt;
                            }

                            if (WPTValues[10].Trim() != string.Empty)
                            {
                                resultWpt.TimeMark = DateTime.Parse(WPTValues[10].Trim());
                            }

                            result.Add(resultWpt);
                        }
                    }
                    catch { }
                }
            }
            return result.ToArray();
        }

        public static Waypoint[] GetWaypointsKml(string filename)
        {
            List<Waypoint> result = new List<Waypoint>();

            DataSet dataSet = new DataSet();
            dataSet.ReadXml(filename);

            if (dataSet.Tables["Placemark"] != null)
            {
                foreach (DataRow dataRow in dataSet.Tables["Placemark"].Rows)
                {
                    DataRow[] pointRows = dataRow.GetChildRows("Placemark_Point");

                    foreach (DataRow pointRow in pointRows)
                    {
                        string[] wptValues = pointRow["coordinates"].ToString().Split(',');
                        try
                        {
                            double lng = double.Parse(wptValues[0].Trim(), CultureInfo.InvariantCulture);
                            double lat = double.Parse(wptValues[1].Trim(), CultureInfo.InvariantCulture);
                            Waypoint resultWpt = new Waypoint(lat, lng);
                            if (wptValues[2].Trim() != string.Empty)
                            {
                                double Alt = double.Parse(wptValues[2].Trim(), CultureInfo.InvariantCulture);
                                if (Alt != -777) resultWpt.Altitude = Alt;
                                if (Alt != 0) resultWpt.Altitude = Alt;
                            }

                            try
                            {
                                string placemarkName = dataRow["name"].ToString();
                                if (placemarkName != string.Empty) resultWpt.Name = placemarkName;
                            }
                            catch { }

                            try
                            {
                                string placemarkDesc = dataRow["description"].ToString();
                                if (placemarkDesc != string.Empty) resultWpt.Description = placemarkDesc;
                            }
                            catch { }

                            try
                            {
                                DataRow timeStampRow = dataRow.GetChildRows("Placemark_TimeStamp")[0];
                                string placemarkTimeStamp = timeStampRow["when"].ToString();
                                if (placemarkTimeStamp != string.Empty)
                                {
                                    resultWpt.TimeMark = DateTime.Parse(placemarkTimeStamp, CultureInfo.InvariantCulture);
                                }
                            }
                            catch { }

                            result.Add(resultWpt);
                        }
                        catch { }
                    }
                }
            }

            return result.ToArray();
        }

        public static Waypoint[] GetWaypointsGpx(string filename)
        {
            XDocument gpxDoc = XDocument.Load(filename);
            XNamespace gpx = XNamespace.Get("http://www.topografix.com/GPX/1/1");

            var waypoints = from waypoint in gpxDoc.Descendants(gpx + "wpt")

            select new
            {
                Latitude = waypoint.Attribute("lat").Value,
                Longitude = waypoint.Attribute("lon").Value,
                Elevation = waypoint.Element(gpx + "ele") != null ?
                    waypoint.Element(gpx + "ele").Value : null,
                Name = waypoint.Element(gpx + "name") != null ?
                    waypoint.Element(gpx + "name").Value : null,
                Dt = waypoint.Element(gpx + "cmt") != null ?
                    waypoint.Element(gpx + "cmt").Value : null,
                Desc = waypoint.Element(gpx + "desc") != null ?
                    waypoint.Element(gpx + "desc").Value : null
            };

            List<Waypoint> result = new List<Waypoint>();

            foreach (var wpt in waypoints)
            {
                double lat = double.Parse(wpt.Latitude, CultureInfo.InvariantCulture);
                double lng = double.Parse(wpt.Longitude, CultureInfo.InvariantCulture);
                Waypoint resultWpt = new Waypoint(lat, lng);

                try
                {
                    if (wpt.Name != string.Empty)
                    {
                        resultWpt.Name = wpt.Name;
                    }
                }
                catch { }

                try
                {
                    if (wpt.Desc != string.Empty)
                    {
                        resultWpt.Description = wpt.Desc;
                    }
                }
                catch { }

                try
                {
                    if (wpt.Elevation != string.Empty)
                    {
                        resultWpt.Altitude = double.Parse(wpt.Elevation);
                    }
                }
                catch { }

                try
                {
                    if (wpt.Dt != string.Empty)
                    {
                        resultWpt.TimeMark = DateTime.Parse(wpt.Dt, CultureInfo.InvariantCulture);
                    }
                }
                catch { }

                result.Add(resultWpt);
            }

            return result.ToArray();
        }

        public static Waypoint[] GetWaypointsJpg(string filename)
        {
            Waypoint wpt = GetWaypointJpg(filename);
            if (wpt == null) return new Waypoint[] { };
            return new Waypoint[] { wpt };
        }

        public static Waypoint GetWaypointJpg(string filename)
        {
            Waypoint wpt = Waypoint.GetWaypoint(Image.FromFile(filename));

            if (wpt == null) return null;

            wpt.Name = Path.GetFileNameWithoutExtension(filename);
            return wpt;
        }


        public static Track[] GetTracks(string filename)
        {
            FileInfo locationFile = new FileInfo(filename);

            switch (locationFile.Extension)
            {
                case ".gpx":
                    return GetTracksGpx(locationFile.FullName);
                case ".kml":
                    return GetTracksKml(locationFile.FullName);
                default:
                    return new Track[0];
            }
        }

        public static Track[] GetTracks(string[] filenames)
        {
            List<Track> result = new List<Track>();

            foreach (string filename in filenames)
            {
                result.AddRange(GetTracks(filename));
            }

            return result.ToArray();
        }

        public static Track[] GetTracksKml(string filename)
        {
            List<Track> result = new List<Track>();

            DataSet dataSet = new DataSet();
            dataSet.ReadXml(filename);

            if (dataSet.Tables["LineString"] != null)
            {
                foreach (DataRow dataRow in dataSet.Tables["LineString"].Rows)
                {
                    List<Waypoint> points = new List<Waypoint>();
                    string[] wpts = dataRow["coordinates"].ToString().Split(' ');

                    foreach (string wpt in wpts)
                    {
                        string[] wptValues = wpt.Split(',');
                        try
                        {
                            double lng = double.Parse(wptValues[0].Trim(), CultureInfo.InvariantCulture);
                            double lat = double.Parse(wptValues[1].Trim(), CultureInfo.InvariantCulture);
                            Waypoint resultWpt = new Waypoint(lat, lng);
                            if (wptValues[2].Trim() != string.Empty)
                            {
                                double alt = double.Parse(wptValues[2].Trim(), CultureInfo.InvariantCulture);
                                if (alt != -777) resultWpt.Altitude = alt;
                                if (alt != 0) resultWpt.Altitude = alt;
                            }

                            points.Add(resultWpt);
                        }
                        catch { }
                    }

                    Track resultTrack = new Track(string.Empty, points.ToArray());

                    try
                    {
                        string placemarkName = dataRow.
                            GetParentRow("Placemark_LineString")["name"].ToString();
                        if (placemarkName != string.Empty) resultTrack.Name = placemarkName;
                    }
                    catch { }

                    try
                    {
                        string placemarkDesc = dataRow.
                            GetParentRow("Placemark_LineString")["description"].ToString();
                        if (placemarkDesc != string.Empty) resultTrack.Description = placemarkDesc;
                    }
                    catch { }

                    result.Add(resultTrack);
                }
            }

            return result.ToArray();
        }

        public static Track[] GetTracksGpx(string filename)
        {
            XDocument gpxDoc = XDocument.Load(filename);
            XNamespace gpx = XNamespace.Get("http://www.topografix.com/GPX/1/1");

            var tracks = from track in gpxDoc.Descendants(gpx + "trk")
            select new
            {
                Name = track.Element(gpx + "name") != null ?
                track.Element(gpx + "name").Value : null,
                Segs = (
                    from trackpoint in track.Descendants(gpx + "trkpt")
                    select new
                    {
                        Latitude = trackpoint.Attribute("lat").Value,
                        Longitude = trackpoint.Attribute("lon").Value,
                        Elevation = trackpoint.Element(gpx + "ele") != null ?
                        trackpoint.Element(gpx + "ele").Value : null,
                        Time = trackpoint.Element(gpx + "time") != null ?
                        trackpoint.Element(gpx + "time").Value : null
                    }
                )
            };

            List<Track> result = new List<Track>();

            foreach (var trk in tracks)
            {
                List<Waypoint> wpts = new List<Waypoint>();

                // Populate track data objects. 
                foreach (var trkSeg in trk.Segs)
                {

                    double lat = double.Parse(trkSeg.Latitude, CultureInfo.InvariantCulture);
                    double lng = double.Parse(trkSeg.Longitude, CultureInfo.InvariantCulture);

                    Waypoint wpt = new Waypoint(lat, lng);

                    try
                    {
                        if (!string.IsNullOrEmpty(trkSeg.Elevation))
                        {
                            wpt.Altitude = double.Parse(trkSeg.Elevation);
                        }
                    }
                    catch { }

                    try
                    {
                        if (trk.Name != string.Empty)
                        {
                            wpt.Name = trk.Name;
                        }
                    }
                    catch { }

                    try
                    {
                        if (!string.IsNullOrEmpty(trkSeg.Time))
                        {
                            wpt.TimeMark = DateTime.Parse(trkSeg.Time, CultureInfo.InvariantCulture);
                        }
                    }
                    catch { }

                    wpts.Add(wpt);
                }

                Track resultTrk = new Track(wpts.ToArray());

                try
                {
                    if (trk.Name != string.Empty)
                    {
                        resultTrk.Name = trk.Name;
                    }
                }
                catch { }

                result.Add(resultTrk);
            }

            return result.ToArray();
        }



        public static Polygon[] GetPolygons(string filename)
        {
            FileInfo locationFile = new FileInfo(filename);
            switch (locationFile.Extension)
            {
                //case ".gpx":
                //    return GetPolygonsGpx(locationFile.FullName);
                case ".kml":
                    return GetPolygonsKml(locationFile.FullName);
                default:
                    return new Polygon[0];
            }
        }

        public static Polygon[] GetPolygons(string[] filenames)
        {
            List<Polygon> result = new List<Polygon>();

            foreach (string filename in filenames)
            {
                result.AddRange(GetPolygons(filename));
            }

            return result.ToArray();
        }

        public static Polygon[] GetPolygonsKml(string filename)
        {
            List<Polygon> result = new List<Polygon>();

            DataSet dataSet = new DataSet();
            dataSet.ReadXml(filename);

            if (dataSet.Tables["LinearRing"] != null)
            {
                foreach (DataRow dataRow in dataSet.Tables["LinearRing"].Rows)
                {
                    List<Waypoint> points = new List<Waypoint>();
                    string[] wpts = dataRow["coordinates"].ToString().Split(' ');

                    foreach (string wpt in wpts)
                    {
                        string[] wptValues = wpt.Split(',');
                        try
                        {
                            double lng = double.Parse(wptValues[0].Trim(), CultureInfo.InvariantCulture);
                            double lat = double.Parse(wptValues[1].Trim(), CultureInfo.InvariantCulture);
                            Waypoint resultWpt = new Waypoint(lat, lng);
                            if (wptValues[2].Trim() != string.Empty)
                            {
                                double alt = double.Parse(wptValues[2].Trim(), CultureInfo.InvariantCulture);
                                if (alt != -777) resultWpt.Altitude = alt;
                                if (alt != 0) resultWpt.Altitude = alt;
                            }

                            points.Add(resultWpt);
                        }
                        catch { }
                    }

                    Polygon resultPoly = new Polygon(string.Empty, points.ToArray());

                    try
                    {
                        string placemarkName = dataRow.GetParentRow("outerBoundaryIs_LinearRing").
                            GetParentRow("Polygon_outerBoundaryIs").
                            GetParentRow("Placemark_Polygon")["name"].ToString();
                        if (placemarkName != string.Empty) resultPoly.Name = placemarkName;
                    }
                    catch { }

                    result.Add(resultPoly);
                }
            }

            return result.ToArray();
        }


        public static void SaveWaypoints(Waypoint[] points)
        {
            if (IO.InterfaceLocation.SaveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = IO.InterfaceLocation.SaveDialog.FileName;

                throw new NotImplementedException();
            }
        }
    }
}
