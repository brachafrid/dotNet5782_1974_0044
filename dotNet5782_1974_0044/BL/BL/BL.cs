﻿using System;
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
        public double Available { get; set; }
        public double LightWeightCarrier{ get; set; }
        public double MediumWeightBearing { get; set; }
        public double CarriesHeavyWeight { get; set; }
        public double DroneLoadingRate { get; set; }
        public BL()
        {
            dal = new DalObject.DalObject();
            // set electricty variablses
            drones = new List<DroneToList>();
            (
                Available,
                LightWeightCarrier,
                MediumWeightBearing,
                CarriesHeavyWeight,
                DroneLoadingRate
            ) = dal.GetElectricity();
            // set the drones
            Initialize();
        }
        //private void Initialize()
        //{
        //    foreach (var drone in dal.GetDrones())
        //    {
        //        DroneToList tmpDrone = new()
        //        {
        //            Id = drone.Id,
        //            Weight = (BO.WeightCategories)drone.MaxWeight,
        //            DroneModel = drone.Model,
        //            CurrentLocation=default(Location)
        //        };
        //        var parcels = dal.GetParcels().ToList();
        //        var locations = dal.GetStations().Select(station => new Location()
        //        {
        //            Latitude = station.Latitude,
        //            Longitude = station.Longitude
        //        });
        //        foreach (var parcel in parcels)
        //        {
        //            if (parcel.DorneId == drone.Id && parcel.Delivered.Equals(default))
        //            {
        //                tmpDrone.DroneStatus = DroneStatuses.DELIVERY;
        //                tmpDrone.ParcelId = parcel.Id;
        //                if (parcel.PickedUp.Equals(default))
        //                {
        //                    var tmpStation = ClosetStation(dal.GetStations(), new() {Longitude = dal.GetCustomer(parcel.SenderId).Longitude, Latitude = dal.GetCustomer(parcel.SenderId).Latitude });
        //                    tmpDrone.CurrentLocation = new Location() { 
        //                        Longitude = tmpStation.Longitude,
        //                        Latitude = tmpStation.Latitude
        //                    };
        //                }   
        //                else
        //                    tmpDrone.CurrentLocation = new() { Longitude = dal.GetCustomer(parcel.SenderId).Longitude, Latitude = dal.GetCustomer(parcel.SenderId).Latitude };
        //                double minDistance;
        //                IDAL.DO.Customer customerSender = dal.GetCustomer(parcel.SenderId);
        //                IDAL.DO.Customer customerReciver = dal.GetCustomer(parcel.TargetId);
        //                double electrity = calculateElectricity(tmpDrone, new() { Latitude = customerSender.Latitude, Longitude = customerSender.Longitude }, new() { Latitude = customerReciver.Latitude, Longitude = customerReciver.Longitude }, (BO.WeightCategories)parcel.Weigth, out minDistance);
        //                if(electrity>100)
        //                {
        //                    dal.RemoveParcel(parcel);
        //                    dal.ParcelsReception(parcel.SenderId, parcel.TargetId,parcel.Weigth,parcel.Priority,parcel.Id);
        //                    tmpDrone.ParcelId = null;
        //                }    
        //                tmpDrone.BatteryStatus = rand.NextDouble() + rand.Next(electrity>100?0:(int)electrity + 1, 100);
        //            }  
        //        }
        //        if (tmpDrone.ParcelId.Equals(default))
        //        {
        //            tmpDrone.DroneStatus = (DroneStatuses)rand.Next(0, 2);
        //            int count=0;
        //            if (tmpDrone.DroneStatus == DroneStatuses.AVAILABLE)
        //            {
        //                List<Location> tmp = GetCustomers().Where(customer => customer.NumParcelReceived > 0).
        //                    Select(Customer => new Location() { Latitude = dal.GetCustomer(Customer.Id).Latitude, Longitude = dal.GetCustomer(Customer.Id).Longitude }).ToList();
        //                count = tmp.Count;
        //                if (tmp.Count > 0)
        //                    tmpDrone.CurrentLocation = tmp[rand.Next(0, tmp.Count)];
        //            }
        //            if ( count == 0)
        //            {
        //                tmpDrone.BatteryStatus = rand.Next(0, 20) + rand.NextDouble();
        //                IDAL.DO.Station station = dal.GetStations().ToList()[rand.Next(0, dal.GetStations().ToList().Count)];
        //                tmpDrone.CurrentLocation = new Location()
        //                {
        //                    Latitude = station.Latitude,
        //                    Longitude = station.Longitude
        //                };
        //            }
        //        }
        //        drones.Add(tmpDrone);
        //    }
        //}

        /// <summary>
        /// init drones list
        /// </summary>
        private void Initialize()
        {
            var tmpDrones = dal.GetDrones();
            var parcels = dal.GetParcels();
            // create list of stations' location
            var locationOfStation = dal.GetStations().Select(Station => new Location() { Latitude = Station.Latitude, Longitude = Station.Longitude }).ToList();
            var customersGotParcelLocation = GetLocationsCustomersGotParcels();
            foreach (var drone in tmpDrones)
            {
                bool canTakeParcel = true;
                var parcel = parcels.FirstOrDefault(parcel => parcel.DorneId == drone.Id && !parcel.Delivered.Equals(default));
                double BatteryStatus ;
                double tmpBatteryStatus = default;
                Location tmpLocaiton = default;
                Location Location;
                DroneStatuses statuse = default;
                //set status
                // if the drone makes delivery
                if (!parcel.Equals(default))
                {
                    statuse = DroneStatuses.DELIVERY;
                    tmpBatteryStatus = minBattary(parcel, drone, ref canTakeParcel);
                    if (!canTakeParcel)
                        statuse = default;
                }
                else if(statuse == default)
                {
                    if (customersGotParcelLocation.Count > 0)
                        statuse = (DroneStatuses)rand.Next(0, 2);
                    else
                        statuse = DroneStatuses.MAINTENANCE;

                }
                // set location and battery
                (Location,BatteryStatus) = statuse switch
                {
                    DroneStatuses.AVAILABLE => (tmpLocaiton = customersGotParcelLocation[rand.Next(0, customersGotParcelLocation.Count)],MinBatteryForAvailAble(tmpLocaiton)
                    ),
                    DroneStatuses.MAINTENANCE => (locationOfStation[rand.Next(0, locationOfStation.Count)],
                    rand.NextDouble() + rand.Next(0,20)),
                    DroneStatuses.DELIVERY => (FindLocationDroneWithParcel(drone, parcel), tmpBatteryStatus)
                }; 
                // add the new drone to drones list
                drones.Add(new DroneToList()
                {
                    Id = drone.Id,
                    Weight = (WeightCategories)drone.MaxWeight,
                    DroneModel = drone.Model,
                    DroneStatus = statuse,
                    CurrentLocation = Location,
                    ParcelId = parcel.Id,
                    BatteryStatus = BatteryStatus
                });

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
            // no item in the list
            if (!lst.Any())
                return false;
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item, null) == id);
            return !(temp.Equals(default(T)));
        }
        /// <summary>
        /// creates list of locations of all the customers that recived at least one parcel
        /// </summary>
        /// <returns>list of locations</returns>
        private List<Location> GetLocationsCustomersGotParcels()
        {
            return GetCustomers().Where(customer => customer.NumParcelReceived > 0)
                     .Select(Customer => new Location()
                     {
                         Latitude = dal.GetCustomer(Customer.Id).Latitude,
                         Longitude = dal.GetCustomer(Customer.Id).Longitude
                     })
                     .ToList();
        }
        /// <summary>
        /// find the location for drone that has parcel
        /// </summary>
        /// <param name="drone">drone</param>
        /// <param name="parcel">drone's parcel</param>
        /// <returns>drone location</returns>
        private Location FindLocationDroneWithParcel(IDAL.DO.Drone drone, IDAL.DO.Parcel parcel)
        {
            //get sender location
            Location locaiton = GetCustomer(parcel.SenderId).Location;
            // if the drone has
            if (parcel.Delivered == default && parcel.PickedUp != default)
                return locaiton;
            var station = ClosetStation(dal.GetStations(), locaiton);
            return new()
            {
                Latitude = station.Latitude,
                Longitude = station.Longitude
            };
        }
        /// <summary>
        /// Calculate minimum amount of electricity for drone to take spesipic parcel 
        /// </summary>
        /// <param name="parcel">the drone's parcel</param>
        /// <param name="drone">drone</param>
        /// <param name="canTakeParcel">ref boolian</param>
        /// <returns> min electricity</returns>
        private double minBattary(IDAL.DO.Parcel parcel, IDAL.DO.Drone drone,ref bool canTakeParcel)
        {
            double minDistance;
            IDAL.DO.Customer customerSender = dal.GetCustomer(parcel.SenderId);
            IDAL.DO.Customer customerReciver = dal.GetCustomer(parcel.TargetId);
            Location senderLocation = new() { Latitude = customerSender.Latitude, Longitude = customerSender.Longitude };
            Location targetLocation = new() { Latitude = customerReciver.Latitude, Longitude = customerReciver.Longitude };
            var location = FindLocationDroneWithParcel(drone, parcel);
            double electrity = calculateElectricity(location,null,senderLocation ,targetLocation, (BO.WeightCategories)parcel.Weigth, out minDistance); 
            if (electrity > 100)
            {
                dal.RemoveParcel(parcel);
                dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id,parcel.DorneId);
                canTakeParcel = false;
            }
            return  rand.NextDouble() + rand.Next((int)electrity + 1, 100);
        }
        /// <summary>
        /// Calculate minimum amount of electricity for drone for arraiving to the closet statoin  
        /// </summary>
        /// <param name="location">drose's location</param>
        /// <returns> min electricity</returns>
        private double MinBatteryForAvailAble(Location location)
        {
            var station = ClosetStation(dal.GetStations(), location);

           return Distance(location, new() { Latitude = station.Latitude, Longitude = station.Longitude }) * dal.GetElectricityUse()[1];
        }
    }
}
