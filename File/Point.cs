using System;
using System.Collections.Generic;
using System.Text;

namespace File
{
    class Point
    {
        public int Ind { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Ele { get; set; }
        public double AveEle { get; set; }

        public double Asc { get; set; }
        public double AveAsc { get; set; }

        public double Section { get; set; }

        const double PIx = 3.141592653589793;
        const double RADIUS = 6378.16;

        /// <summary>
        /// Convert degrees to Radians
        /// </summary>
        /// <param name="x">Degrees</param>
        /// <returns>The equivalent in radians</returns>
        public static double Radians(double x)
        {
            return x * PIx / 180;
        }

        /// <summary>
        /// Calculate the distance between two places.
        /// </summary>
        /// <param name="lon1"></param>
        /// <param name="lat1"></param>
        /// <param name="lon2"></param>
        /// <param name="lat2"></param>
        /// <returns></returns>
        public static double Distance(Point p1, Point p2)
        {
            double dlon = Radians(p2.Lon - p1.Lon);
            double dlat = Radians(p2.Lat - p1.Lat);

            double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(p1.Lat)) * Math.Cos(Radians(p2.Lat)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
            double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return angle * RADIUS;
        }
    }
}
