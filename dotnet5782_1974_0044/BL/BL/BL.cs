using BLApi;
using BO;
using DLApi;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace BL
{
    public sealed partial class BL : Singletone<BL>, IBL
    {
        internal IDal dal { get; } = DLFactory.GetDL();
        private const int DRONESTATUSESLENGTH = 2;
        public const int MAXINITBATTARY = 20;
        public const int MININITBATTARY = 0;
        public const int FULLBATTRY = 100;
        internal static readonly Random rand = new();
        internal readonly List<DroneToList> drones;
        internal readonly double available;
        internal readonly double lightWeightCarrier;
        internal readonly double mediumWeightBearing;
        internal readonly double carriesHeavyWeight;
        internal readonly double droneLoadingRate;

        /// <summary>
        /// constructor init logig drone list
        /// </summary>
        BL()
        {

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
            try
            {
                Initialize();
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }

        }

        /// <summary>
        /// init logic drones list
        /// </summary>
        private void Initialize()
        {
            var tmpDrones = dal.GetDrones();
            var parcels = dal.GetParcels();
            // create list of stations' location
            var customersGotParcelLocation = GetLocationsCustomersGotParcels((int recivedparcels) => recivedparcels > 0);
            foreach (var drone in tmpDrones)
            {
                bool isAbleTakeParcel = true;
                var parcel = parcels.FirstOrDefault(parcel => parcel.DorneId == drone.Id && parcel.Delivered == null);
                double BatteryStatus;
                double tmpBatteryStatus = default;
                Location tmpLocaiton = default;
                Location Location;
                Location tmpDroneWithParcelLocation = default;
                DroneState state = default;
                var droneInCharging = dal.GetDronescharging().Where(item => item.Droneld == drone.Id);
                //set status
                // if the drone makes delivery
                if (parcel.DorneId != 0)
                {
                    state = DroneState.DELIVERY;
                    try
                    {
                        tmpBatteryStatus = MinBattary(parcel, ref isAbleTakeParcel, out tmpDroneWithParcelLocation);
                    }
                    catch (ThereIsNoNearbyBaseStationThatTheDroneCanReachException)
                    {
                        state = DroneState.RESCUE;
                    }
                    
                    if (!isAbleTakeParcel)
                    {
                        state = default;
                        parcel.DorneId = 0;
                        tmpDroneWithParcelLocation = default;
                    }
                }
                else if (droneInCharging.Any())
                {
                    state = DroneState.MAINTENANCE;
                }
                else if (state == default)
                {
                    state = (DroneState)rand.Next(0, DRONESTATUSESLENGTH);
                    if (customersGotParcelLocation.Count() <= 0 )
                        state = DroneState.MAINTENANCE;

                }
                // set location and battery
                switch (state)
                {
                    case DroneState.AVAILABLE:
                        Location = tmpLocaiton = customersGotParcelLocation.ElementAt(rand.Next(0, customersGotParcelLocation.Count()));
                        BatteryStatus = rand.Next((int)MinBatteryForAvailAble(tmpLocaiton) + 1, FULLBATTRY);
                        break;
                    case DroneState.MAINTENANCE:
                        if (droneInCharging.Count() <= 0)
                        {
                            var stationsToDroneCharge = from station in dal.GetSationsWithEmptyChargeSlots((int numOfEmpty) => numOfEmpty > 0)
                                                        let stationLocation = new Location() { Latitude = station.Latitude, Longitude = station.Longitude }
                                                        select new { Location = stationLocation, Id = station.Id };
                            var stationToDroneCharge = stationsToDroneCharge.ElementAt(rand.Next(0, stationsToDroneCharge.Count()));
                            Location = stationToDroneCharge.Location;
                            dal.AddDroneCharge(drone.Id, stationToDroneCharge.Id);
                        }
                        else
                        {
                            var stationToDroneCharge = dal.GetStation(droneInCharging.First().Stationld);
                            Location = new() { Latitude = stationToDroneCharge.Latitude, Longitude = stationToDroneCharge.Longitude };
                        }
                        BatteryStatus = rand.NextDouble() + rand.Next(MININITBATTARY, MAXINITBATTARY);
                        break;
                    case DroneState.DELIVERY:
                        Location = tmpDroneWithParcelLocation;
                        BatteryStatus = tmpBatteryStatus;
                        break;
                    default:
                        Location = null;
                        BatteryStatus = 0;
                        break;
                }

                // add the new drone to drones list
                drones.Add(new DroneToList()
                {
                    Id = drone.Id,
                    Weight = (WeightCategories)drone.MaxWeight,
                    DroneModel = drone.Model,
                    DroneState = state,
                    CurrentLocation = Location,
                    ParcelId = parcel.DorneId == 0 ? 0 : parcel.Id,
                    BatteryState = BatteryStatus,
                    IsNotActive = false
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
            return !temp.Equals(default(T));
        }

        /// <summary>
        /// creates list of locations of all the customers that recived at least one parcel
        /// </summary>
        /// <param name="exsitParcelRecived">The predicate to screen out if the customer have recived parcels</param>
        /// <returns>list of locations</returns>
        private IEnumerable<Location> GetLocationsCustomersGotParcels(Predicate<int> exsitParcelRecived)
        {
            return GetCustomers().Where(customer => exsitParcelRecived(customer.NumParcelReceived))
                     .Select(Customer => new Location()
                     {
                         Latitude = dal.GetCustomer(Customer.Id).Latitude,
                         Longitude = dal.GetCustomer(Customer.Id).Longitude
                     });
        }

        /// <summary>
        /// find the location for drone that has parcel
        /// </summary>
        /// <param name="drone">drone</param>
        /// <param name="parcel">drone's parcel</param>
        /// <returns>drone location</returns>
        private Location FindLocationDroneWithParcel(DO.Parcel parcel)
        {
            //get sender location
            Location locaiton = GetCustomer(parcel.SenderId).Location;
            // if the drone hasn't picked up the parcel
            if (parcel.Delivered == null && parcel.PickedUp != null)
                return locaiton;
            var station = ClosetStation(locaiton, (int chargeSlots) => chargeSlots > 0);
            if(station==null)
                station = ClosetStation(locaiton, (int chargeSlots) =>true);
            if (station == null)
                throw new ThereIsNoNearbyBaseStationThatTheDroneCanReachException();
            return station.Location;
        }

        /// <summary>
        /// Calculate electricity for drone to take spesipic parcel 
        /// </summary>
        /// <param name="parcel">the drone's parcel</param>
        /// <param name="drone">drone</param>
        /// <param name="canTakeParcel">ref boolian</param>
        /// <returns> min electricity</returns>
        private double MinBattary(DO.Parcel parcel, ref bool canTakeParcel, out Location location)
        {
            try
            {
                var customerSender = dal.GetCustomer(parcel.SenderId);
                var customerReciver = dal.GetCustomer(parcel.TargetId);
                Location senderLocation = new() { Latitude = customerSender.Latitude, Longitude = customerSender.Longitude };
                Location targetLocation = new() { Latitude = customerReciver.Latitude, Longitude = customerReciver.Longitude };
                // find drone's location 
                location = FindLocationDroneWithParcel(parcel);
                double electrity = CalculateElectricity(location, null, senderLocation, targetLocation, (WeightCategories)parcel.Weigth, out _);
                // if the drone need more electricity 
                if (electrity > FULLBATTRY)
                {
                    DO.Parcel newParcel = parcel;
                    newParcel.Id = 0;
                    newParcel.PickedUp = newParcel.Sceduled = default;
                    dal.UpdateParcel(parcel, newParcel);
                    canTakeParcel = false;
                    return 0;
                }
                return rand.NextDouble() + rand.Next((int)electrity + 1, FULLBATTRY);
            }
            catch (ThereIsNoNearbyBaseStationThatTheDroneCanReachException)
            {
                throw new ThereIsNoNearbyBaseStationThatTheDroneCanReachException();
            }
           
        }

        /// <summary>
        /// Calculate minimum amount of electricity for drone for arraiving to the closet statoin  
        /// </summary>
        /// <param name="location">drose's location</param>
        /// <returns> min electricity</returns>
        private double MinBatteryForAvailAble(Location location)
        {

            var station = ClosetStation(location, (int chargeSlots) => chargeSlots > 0);
            if (station == null)
                return MININITBATTARY;
            double electricity = Distance(location, station.Location) * available;
            return electricity > FULLBATTRY ? MININITBATTARY : electricity;
        }

        /// <summary>
        /// Get administor passsword
        /// </summary>
        /// <returns>administor passsword</returns>
        public string GetAdministorPasssword()
        {
            try
            {
                return dal.GetAdministorPasssword();
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }

    }
}
