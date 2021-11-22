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
            var sCoord = new GeoCoordinate(sLocation.Latitude, sLocation.Longitude);
            var tCoord = new GeoCoordinate(dLocation.Latitude, dLocation.Longitude);
            return sCoord.GetDistanceTo(tCoord)/1000; 
        }
    }
}
