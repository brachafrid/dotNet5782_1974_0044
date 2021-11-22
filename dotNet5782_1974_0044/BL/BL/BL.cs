using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;


namespace IBL
{
    public partial class BL : IBL
    {
        private static readonly Random rand = new();
        private List<DroneToList> drones;
        private readonly IDAL.IDal dal;
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
                DroneToList tmpDrone = new()
                {
                    Id = drone.Id,
                    Weight = (BO.WeightCategories)drone.MaxWeight,
                    DroneModel = drone.Model,
                    CurrentLocation=default(Location)
                };
                var parcels = dal.GetParcels().ToList();
                foreach (var parcel in parcels)
                {
                    if (parcel.DorneId == drone.Id && parcel.Delivered.Equals(default))
                    {
                        tmpDrone.DroneStatus = DroneStatuses.DELIVERY;
                        tmpDrone.ParcelId = parcel.Id;
                        if (parcel.PickedUp.Equals(default))
                        {
                            var tmpStation = ClosetStation(dal.GetStations(), new() {Longitude = dal.GetCustomer(parcel.SenderId).Longitude, Latitude = dal.GetCustomer(parcel.SenderId).Latitude });
                            tmpDrone.CurrentLocation = new Location() { 
                                Longitude = tmpStation.Longitude,
                                Latitude = tmpStation.Latitude
                            };
                        }   
                        else
                            tmpDrone.CurrentLocation = new() { Longitude = dal.GetCustomer(parcel.SenderId).Longitude, Latitude = dal.GetCustomer(parcel.SenderId).Latitude };
                        double minDistance;
                        IDAL.DO.Customer customerSender = dal.GetCustomer(parcel.SenderId);
                        IDAL.DO.Customer customerReciver = dal.GetCustomer(parcel.TargetId);
                        double electrity = calculateElectricity(tmpDrone, new() { Latitude = customerSender.Latitude, Longitude = customerSender.Longitude }, new() { Latitude = customerReciver.Latitude, Longitude = customerReciver.Longitude }, (BO.WeightCategories)parcel.Weigth, out minDistance);
                        if(electrity>100)
                        {
                            dal.RemoveParcel(parcel);
                            dal.AddParcel(parcel.SenderId, parcel.TargetId,parcel.Weigth,parcel.Priority,parcel.Id);
                            tmpDrone.ParcelId = null;
                        }    
                        tmpDrone.BatteryStatus = rand.NextDouble() + rand.Next(electrity>100?0:(int)electrity+1, 100);
                    }  
                }
                if (tmpDrone.ParcelId.Equals(default))
                {
                    tmpDrone.DroneStatus = (DroneStatuses)rand.Next(0, 2);
                    int count=0;
                    if (tmpDrone.DroneStatus == DroneStatuses.AVAILABLE)
                    {
                        List<Location> tmp = GetCustomers().Where(customer => customer.NumParcelReceived > 0).
                            Select(Customer => new Location() { Latitude = dal.GetCustomer(Customer.Id).Latitude, Longitude = dal.GetCustomer(Customer.Id).Longitude }).ToList();
                        count = tmp.Count;
                        if (tmp.Count > 0)
                            tmpDrone.CurrentLocation = tmp[rand.Next(0, tmp.Count)];
                    }
                    if ( count == 0)
                    {
                        tmpDrone.BatteryStatus = rand.Next(0, 20) + rand.NextDouble();
                        IDAL.DO.Station station = dal.GetStations().ToList()[rand.Next(0, dal.GetStations().ToList().Count)];
                        tmpDrone.CurrentLocation = new Location()
                        {
                            Latitude = station.Latitude,
                            Longitude = station.Longitude
                        };
                    }
                }
                drones.Add(tmpDrone);
            }
        }

        ///  <summary>
        /// Find if the id is unique in a spesific list
        /// </summary>
        /// <typeparam name="T">the type of list</typeparam>
        /// <param name="lst">the spesific list </param>
        /// <param name="id">the id to check</param>
        private static bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
            if (!lst.Any())
                return false;
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item, null) == id);
            return !(temp.Equals(default(T)));
        }

    }
}