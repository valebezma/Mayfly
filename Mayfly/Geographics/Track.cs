using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mayfly.Geographics
{
    public class Track : IComparable<Track>
    {
        public string Name { get; set; }

        public Waypoint[] Points { get; set; }

        public string Description { get; set; }

        public bool IsDescriptionNull { get { return Description == null || Description == string.Empty; } }



        public Track(string name, Waypoint[] points)
        {
            Name = name;
            Points = points;
        }

        public Track(Waypoint[] points)
            : this(string.Empty, points)
        { }



        public bool IsNameNull { get { return Name == null || Name == string.Empty; } }

        public int WaypointsCount
        {
            get
            {
                return Points.Count();
            }
        }

        public double Length
        {
            get
            {
                double result = 0;

                for (int i = 1; i < Points.Length; i++)
                {
                    double r = Waypoint.Distance(Points[i - 1], Points[i]);
                    result += r;
                }

                return result;
            }
        }

        public DateTime Started
        {
            get
            {
                return Points[0].TimeMark;
            }
        }

        public DateTime Ended
        {
            get
            {
                return Points.Last().TimeMark;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return Ended - Started;
            }
        }

        /// <summary>
        /// Returns average track velocity in meters per second
        /// </summary>
        public double Velocity
        {
            get
            {
                try
                {
                    return this.Length / Duration.TotalSeconds;
                }
                catch
                {
                    return double.NaN;
                }
            }
        }

        public double Kmph
        {
            get
            {
                return Velocity * 3.6;
            }
        }

        public Waypoint Middle
        {
            get
            {
                // Get segment containing middle
                double half = Length * 0.5;
                double passed = 0;
                int i = 0;
                double seg = 0;
                while (passed < half)
                {
                    seg = Waypoint.Distance(Points[i], Points[i + 1]);
                    passed += seg;
                }
                // Now i equals number of Point starting segment we are looking for
                // And passed equals length of path from start to end of segment

                // Now we should find what part of segment will get to the first half
                double resultingpart = 1 - ((passed - half) / seg);
                return Waypoint.GetSegmentPoint(Points[i], Points[i + 1], resultingpart);
            }
        }



        public int CompareTo(Track track)
        {
            if (track == null) return 1;
            else return this.Started.CompareTo(track.Started);
        }
        


        public static Waypoint[] GetWaypoints(Track[] tracks)
        {
            List<Track> list = new List<Geographics.Track>(tracks);
            list.Sort();

            List<Waypoint> result = new List<Geographics.Waypoint>();
            foreach (Track track in list)
            {
                result.AddRange(track.Points);
            }

            return result.ToArray();
        }

        public static double TotalLength(Track[] tracks)
        {
            double s = 0;

            foreach (Track track in tracks)
            {
                s += track.Length;
            }

            return s;
        }

        public static TimeSpan TotalDuration(Track[] tracks)
        {
            TimeSpan s = TimeSpan.Zero;

            foreach (Track track in tracks)
            {
                s += track.Duration;
            }

            return s;
        }

        public static double AverageVelocity(Track[] tracks)
        {
            return TotalLength(tracks) / TotalDuration(tracks).TotalSeconds;
        }

        public static double AverageKmph(Track[] tracks)
        {
            return AverageVelocity(tracks) * 3.6;
        }
    }
}
