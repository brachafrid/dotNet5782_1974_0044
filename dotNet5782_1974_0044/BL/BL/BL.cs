using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;
using System.Device.Location;
namespace IBL
{
    public partial class BL : IBL
    {
        private List<DroneToList> drones;
        private IDAL.IDal dal = new DalObject.DalObject();
        bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
            T temp=lst.FirstOrDefault(item => (int)item.GetType().GetProperty("id").GetValue(item, null) == id);
            return !(temp.GetType().Equals(default(T)));
        }
        private double Distance(BO.Location sLocation, BO.Location tLocation)
        {
            var sCoord = new GeoCoordinate(sLocation.Latitude, sLocation.Longitude);
            var tCoord = new GeoCoordinate(tLocation.Latitude, tLocation.Longitude);

            return sCoord.GetDistanceTo(tCoord);
        }
    }
}

