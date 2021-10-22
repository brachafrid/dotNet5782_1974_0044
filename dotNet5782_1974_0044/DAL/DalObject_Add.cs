using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
  public  partial class DalObject
    {
         char[] helpSexagesimal = new char[] { '0','1','2','3','4','5','6','7','8','9',
                     'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                     'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x'};
        /// <summary>
        ///  Gets parameters and create new station 
        /// </summary>
        /// <param name="name"> Station`s name</param>
        /// <param name="longitude">The position of the station in relation to the longitude </param>
        /// <param name="latitude">The position of the station in relation to the latitude</param>
        /// <param name="chargeSlots">Number of charging slots at the station</param>
        public void addStation(string name, double longitude, double latitude, int chargeSlots )
        {
            Station newStation = new Station();
            newStation.Id = ++DataSorce.Config.idxStations;
            newStation.Name = name;
            newStation.Latitude = latitude;
            newStation.Longitude = longitude;
            newStation.ChargeSlots = chargeSlots;
            newStation.latitudeSexagesimal = IntToString((int)newStation.Latitude, helpSexagesimal);
            newStation.longitudeSexagesimal = IntToString((int)newStation.Latitude, helpSexagesimal);
            DataSorce.stations.Add(newStation);
        }
        /// <summary>
        /// Gets parameters and create new customer 
        /// </summary>
        /// <param name="phone">The customer`s number phone</param>
        /// <param name="name">The customer`s name</param>
        /// <param name="longitude">>The position of the customer in relation to the longitude</param>
        /// <param name="latitude">>The position of the customer in relation to the latitude</param>
        public void addCustomer(string phone, string name, double longitude, double latitude)
        {
            Customer newCustomer = new Customer();
            newCustomer.Id = ++DataSorce.Config.idxCustomers;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = latitude;
            newCustomer.Longitude = longitude;
            newCustomer.latitudeSexagesimal = IntToString((int)newCustomer.Latitude, helpSexagesimal);
            newCustomer.longitudeSexagesimal = IntToString((int)newCustomer.Latitude, helpSexagesimal);
            DataSorce.customers.Add(newCustomer);
        }
        /// <summary>
        ///  Gets parameters and create new drone 
        /// </summary>
        /// <param name="model"> Grone's model</param>
        /// <param name="MaxWeight"> The max weight that the drone can swipe (light- 0,medium - 1,heavy - 2)</param>
        public void addDrone(string model, WeightCategories MaxWeight)
        {
            Drone newDrone = new Drone();
            newDrone.Id = ++DataSorce.Config.idxDrones;
            newDrone.Model = model;
            newDrone.MaxWeight = MaxWeight;
            newDrone.Status = DroneStatuses.AVAILABLE;
            newDrone.Battery = 100;
            DataSorce.drones.Add(newDrone);
        }
        /// <summary>
        /// Gets parameters and create new parcel 
        /// </summary>
        /// <param name="SenderId"> Id of sener</param>
        /// <param name="TargetId"> Id of target</param>
        /// <param name="Weigth"> The weigth of parcel (light- 0,medium - 1,heavy - 2)</param>
        /// <param name="Priority"> The priority of send the parcel (regular - 0,fast - 1,emergency - 2)</param>
        public void parcelsReception(int SenderId, int TargetId, WeightCategories Weigth, Prioripies Priority)
        {
            DataSorce.customers.First(item => item.Id == SenderId);
            DataSorce.customers.First(item => item.Id == TargetId);
            Parcel newParcel = new Parcel();
            newParcel.Id = ++DataSorce.Config.idxParcels;
            newParcel.SenderId = SenderId;
            newParcel.TargetId = TargetId;
            newParcel.Weigth = Weigth;
            newParcel.Priority = Priority;
            newParcel.Requested = DateTime.Now;
            newParcel.DorneId = 0;
            DataSorce.parcels.Add(newParcel);
        }

        public static string IntToString(int value, char[] baseChars)
        {
            string result = string.Empty;
            int targetBase = baseChars.Length;

            do
            {
                result = baseChars[value % targetBase] + result;
                value = value / targetBase;
            }
            while (value > 0);

            return result;
        }
    }
}
