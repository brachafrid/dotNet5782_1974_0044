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
        private static readonly Random rand = new();
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
                DroneToList tmpDrone = new ()
                {
                    Id = drone.Id,
                    Weight = (BO.WeightCategories)drone.MaxWeight,
                    DroneModel = drone.Model
                };
                foreach (var parcel in dal.GetParcels())
                {
                    if (parcel.DorneId == drone.Id && parcel.Delivered.Equals(default))
                    {
                        tmpDrone.DroneStatus = DroneStatuses.DELIVERY;
                        if (parcel.PickedUp.Equals(default))
                        {
                            var tmpStation = ClosetStation(dal.GetStations(), MapCustomer(dal.GetCustomer(parcel.SenderId)).Location);
                            tmpDrone.CurrentLocation = new Location() { 
                                Longitude = tmpStation.Longitude,
                                Latitude = tmpStation.Latitude
                            };
                        }   
                        else
                            tmpDrone.CurrentLocation = MapCustomer(dal.GetCustomer(parcel.SenderId)).Location;
                        double minDistance;
                        tmpDrone.BatteryStatus = rand.NextDouble() + rand.Next((int)calculateElectricity(tmpDrone, mapParcelToList(parcel),out minDistance)+1, 100);
                    }
                    else
                    {
                        tmpDrone.DroneStatus = (DroneStatuses)rand.Next(0, 2);
                        if (tmpDrone.DroneStatus == DroneStatuses.AVAILABLE)
                        {
                            var tmp = GetCustomers().Where(customer => customer.NumParcelReceived > 0).
                                Select(Customer => GetCustomer(Customer.Id)).ToList();
                            tmpDrone.CurrentLocation = tmp[rand.Next(0, tmp.Count)].Location;
                        }
                        else
                        {
                            tmpDrone.BatteryStatus = rand.Next(0,20) + rand.NextDouble();
                            IDAL.DO.Station station = dal.GetStations().ToList()[rand.Next(0, dal.GetStations().ToList().Count())];
                            tmpDrone.CurrentLocation = new Location()
                            {
                                Latitude = station.Latitude,
                                Longitude = station.Longitude
                            };      
                        }
                    }
                }
                drones.Add(tmpDrone);
            }
        }
        bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("id").GetValue(item, null) == id);
            return !(temp.GetType().Equals(default));
        }
        private double Distance(Location sLocation, Location tLocation)
        {
            var sCoord = new GeoCoordinate(sLocation.Latitude, sLocation.Longitude);
            var tCoord = new GeoCoordinate(tLocation.Latitude, tLocation.Longitude);
            return sCoord.GetDistanceTo(tCoord);
        }
    }
}