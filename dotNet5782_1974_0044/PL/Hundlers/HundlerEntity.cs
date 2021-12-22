using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;
using BLApi;

namespace PL
{
    public static class HundlerEntity
    {
        static IBL ibal = BLFactory.GetBL();
        public static BO.Location ConvertBackLocation(Location location)
        {
            return new BO.Location()
            {
               Longitude=location.Longitude,
               Latitude=location.Latitude
            };
        }

        public static Location ConvertLocation(BO.Location location)
        {
            return new Location()
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }

    }
}
