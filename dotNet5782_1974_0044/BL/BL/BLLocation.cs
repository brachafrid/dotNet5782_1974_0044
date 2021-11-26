using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using System.Device.Location;

namespace IBL
{
    public partial class BL
    {
        /// <summary>
        /// Calculates the distance between two points on the earth
        /// </summary>
        /// <param name="sLocation">The source point</param>
        /// <param name="dLocation">The Destination point</param>
        /// <returns></returns>
        private static double Distance(Location sLocation, Location dLocation)
        {
            int R = 6371 * 1000; // metres
            double phi1 = sLocation.Latitude * Math.PI / 180; // φ, λ in radians
            double phi2 = dLocation.Latitude * Math.PI / 180;
            double deltaPhi = (dLocation.Latitude - sLocation.Latitude) * Math.PI / 180;
            double deltaLambda = (dLocation.Longitude - sLocation.Longitude) * Math.PI / 180;

            double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                       Math.Cos(phi1) * Math.Cos(phi2) *
                       Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c / 1000; // in kilometres
            return d;
        }
    }
}
