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
        private static Random rand = new Random();
        private List<DroneToList> drones;
        private IDAL.IDal dal;
        public BL()
        {
            dal = new DalObject.DalObject();
            drones = new List<DroneToList>();
            Initialize();
        }
        private void Initialize()
        {
            foreach (IDAL.DO.Drone drone in dal.GetDrones())
            {
                DroneToList tmpDrone = new DroneToList()
                {
                    Id = drone.Id,
                    Weight = (BO.WeightCategories)drone.MaxWeight,
                    DroneModel = drone.Model
                };
                foreach (var parcel in dal.GetParcels())
                {
                    if (parcel.DorneId == drone.Id && parcel.Delivered.Equals(default(DateTime)))
                    {
                        tmpDrone.DroneStatus = DroneStatuses.DELIVERY;

                        if (parcel.PickedUp.Equals(default(DateTime)))
                        {
                            double help;
                            tmpDrone.CurrentLocation = ClosetStation(dal.GetStations(), MapCustomer(dal.GetCustomer(parcel.SenderId)).Location,out help);
                        }
                    }
                    else
                    {
                        tmpDrone.DroneStatus = (DroneStatuses)rand.Next(0, 1);
                        if (tmpDrone.DroneStatus == DroneStatuses.AVAILABLE)
                        {
                            var tmp = GetCustomers().Where(customer => customer.NumParcelReceived > 0).
                                Select(Customer => GetCustomer(Customer.Id)).ToList();
                            tmpDrone.CurrentLocation = tmp[rand.Next(0, tmp.Count)].Location;

                        }
                    }


                }

            }


        }
        bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("id").GetValue(item, null) == id);
            return !(temp.GetType().Equals(default(T)));
        }
        private double Distance(Location sLocation, Location tLocation)
        {
            var sCoord = new GeoCoordinate(sLocation.Latitude, sLocation.Longitude);
            var tCoord = new GeoCoordinate(tLocation.Latitude, tLocation.Longitude);
            return sCoord.GetDistanceTo(tCoord);
        }

    }
}

