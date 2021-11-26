using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;


namespace IBL
{
    public partial class BL : IBL
    {
        private static readonly Random rand = new();
        private readonly List<DroneToList> drones;
        private readonly IDAL.IDal dal;
        private readonly double available;
        private readonly double lightWeightCarrier;
        private readonly double mediumWeightBearing;
        private readonly double carriesHeavyWeight;
        private readonly double droneLoadingRate;
        public BL()
        {
            dal = new DalObject.DalObject();
            // set electricty variablses
            drones = new List<DroneToList>();
            (
                available,
                lightWeightCarrier,
                mediumWeightBearing,
                carriesHeavyWeight,
                droneLoadingRate
            ) = dal.GetElectricity();
            // set the drones
            Initialize();
        }
        
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
                var parcel = parcels.FirstOrDefault(parcel => parcel.DorneId == drone.Id && parcel.Delivered==default);
                double BatteryStatus;
                double tmpBatteryStatus = default;
                Location tmpLocaiton = default;
                Location Location;
                DroneStatuses statuse = default;
                //set status
                // if the drone makes delivery
                if (parcel.DorneId != 0 )
                {
                    statuse = DroneStatuses.DELIVERY;
                    tmpBatteryStatus = MinBattary(parcel,ref canTakeParcel);
                    if (!canTakeParcel)
                    {
                        statuse = default;
                        parcel.DorneId = 0;
                    }
                        
                }
                else if (statuse == default)
                {
                    if (customersGotParcelLocation.Count > 0)
                        statuse = (DroneStatuses)rand.Next(0, 2);
                    else
                        statuse = DroneStatuses.MAINTENANCE;

                }
                // set location and battery
                (Location, BatteryStatus) = statuse switch
                {
                    DroneStatuses.AVAILABLE => (tmpLocaiton = customersGotParcelLocation[rand.Next(0, customersGotParcelLocation.Count)], rand.Next((int)MinBatteryForAvailAble(tmpLocaiton) +1,100)
                    ),
                    DroneStatuses.MAINTENANCE => (locationOfStation[rand.Next(0, locationOfStation.Count)],
                    rand.NextDouble() + rand.Next(0,20)),
                    DroneStatuses.DELIVERY => (FindLocationDroneWithParcel( parcel), tmpBatteryStatus)
                }; 
                // add the new drone to drones list
                drones.Add(new DroneToList()
                {
                    Id = drone.Id,
                    Weight = (WeightCategories)drone.MaxWeight,
                    DroneModel = drone.Model,
                    DroneStatus = statuse,
                    CurrentLocation = Location,
                    ParcelId =parcel.DorneId == 0? 0: parcel.Id,
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
        private Location FindLocationDroneWithParcel( IDAL.DO.Parcel parcel)
        {
            //get sender location
            Location locaiton = GetCustomer(parcel.SenderId).Location;
            // if the drone hasn't picked up the parcel
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
        /// Calculate electricity for drone to take spesipic parcel 
        /// </summary>
        /// <param name="parcel">the drone's parcel</param>
        /// <param name="drone">drone</param>
        /// <param name="canTakeParcel">ref boolian</param>
        /// <returns> min electricity</returns>
        private double MinBattary(IDAL.DO.Parcel parcel,ref bool canTakeParcel)
        {
            var customerSender = dal.GetCustomer(parcel.SenderId);
            var customerReciver = dal.GetCustomer(parcel.TargetId);
            Location senderLocation = new() { Latitude = customerSender.Latitude, Longitude = customerSender.Longitude };
            Location targetLocation = new() { Latitude = customerReciver.Latitude, Longitude = customerReciver.Longitude };
            // find drone's location 
            var location = FindLocationDroneWithParcel(parcel);
            double electrity = CalculateElectricity(location,null,senderLocation ,targetLocation, (BO.WeightCategories)parcel.Weigth, out _); 
            // if the drone need more electricity 
            if (electrity > 100)
            {
                dal.RemoveParcel(parcel);
                dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id, 0,parcel.Requested,parcel.Sceduled,parcel.PickedUp,parcel.Delivered);
                canTakeParcel = false;
                return 0;
            }
            return rand.NextDouble() + rand.Next((int)electrity + 1, 100);
        }
        /// <summary>
        /// Calculate minimum amount of electricity for drone for arraiving to the closet statoin  
        /// </summary>
        /// <param name="location">drose's location</param>
        /// <returns> min electricity</returns>
        private double MinBatteryForAvailAble(Location location)
        {
            var station = ClosetStation(dal.GetStations(), location);
            double electricity = Distance(location, new() { Latitude = station.Latitude, Longitude = station.Longitude }) * available;
            return electricity > 100 ? 0 : electricity;
        }
    }
}
