using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mayfly.Geographics
{
    public class Polygon : Track
    {
        public Polygon(string name, Waypoint[] points) : base(name, points)
        { }

        public Polygon(Track track)
            : base(track.Name, track.Points)
        { }

        public double Perimeter
        {
            get
            {
                double result = Length;
                result += Waypoint.Distance(Points.Last(), Points.First());
                return result;
            }
        }

        public double Area
        {
            get
            {
                double sum = 0;
                double prevcolat = 0;
                double prevaz = 0;
                double colat0 = 0;
                double az0 = 0;
                for (int i = 0; i < Points.Count(); i++)
                {
                    double lat = Points[i].Latitude.Degrees;
                    double lng = Points[i].Longitude.Degrees;

                    double colat = 2 * Math.Atan2(Math.Sqrt(Math.Pow(Math.Sin(lat * Math.PI / 180 / 2), 2) + 
                        Math.Cos(lat * Math.PI / 180) * Math.Pow(Math.Sin(lng * Math.PI / 180 / 2), 2)), Math.Sqrt(1 - Math.Pow(Math.Sin(lat * Math.PI / 180 / 2), 2) - 
                        Math.Cos(lat * Math.PI / 180) * Math.Pow(Math.Sin(lng * Math.PI / 180 / 2), 2)));
                    double az = 0;
                    if (lat >= 90d)
                    {
                        az = 0;
                    }
                    else if (lat <= -90)
                    {
                        az = Math.PI;
                    }
                    else
                    {
                        az = Math.Atan2(Math.Cos(lat * Math.PI / 180) * Math.Sin(lng * Math.PI / 180), Math.Sin(lat * Math.PI / 180)) % (2 * Math.PI);
                    }
                    if (i == 0)
                    {
                        colat0 = colat;
                        az0 = az;
                    }
                    if (i > 0 && i < Points.Count())
                    {
                        sum = sum + (1 - Math.Cos(prevcolat + (colat - prevcolat) / 2)) * Math.PI * ((Math.Abs(az - prevaz) / Math.PI) -
                            2 * Math.Ceiling(((Math.Abs(az - prevaz) / Math.PI) - 1) / 2)) * Math.Sign(az - prevaz);
                    }
                    prevcolat = colat;
                    prevaz = az;
                }
                sum = sum + (1 - Math.Cos(prevcolat + (colat0 - prevcolat) / 2)) * (az0 - prevaz);

                double s = 5.10072E14 * Math.Min(Math.Abs(sum) / 4 / Math.PI, 1 - Math.Abs(sum) / 4 / Math.PI);

                return s;
            }
        }

        public double Width
        {
            get
            {
                double result = 0;

                for (int i = 0; i < Points.Length; i++)
                {
                    for (int j = 1; j < Points.Length; j++)
                    {
                        double r = Waypoint.Distance(Points[i], Points[j]);
                        result = Math.Max(r, result);
                    }
                }

                return result;

            }
        }

        public Waypoint Center
        {
            get
            {
                return Waypoint.GetCenterOf(Points);
            }
        }
    }
}
