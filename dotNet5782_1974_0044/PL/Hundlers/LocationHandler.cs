using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;
using BLApi;

namespace PL
{
    public static class LocationHandler
    {
        static IBL ibal = BLFactory.GetBL();
        public static BO.Location ConvertBackLocation(Location location)
        {
            return new BO.Location()
            {
               Longitude=(double)location.Longitude,
               Latitude=(double)location.Latitude
            };
        }

        public static Location ConvertLocation(BO.Location location)
        {
            return new Location()
            {
                Longitude = (double)location.Longitude,
                Latitude = (double)location.Latitude
            };
        }

    }
}
