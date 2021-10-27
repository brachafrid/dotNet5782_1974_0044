using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// help array to convert for sexgesimal base
        /// </summary>
        private char[] helpSexagesimal = new char[]{'0','1','2','3','4','5','6','7','8','9',
                    'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                     'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x'};
        /// <summary>
        ///  Gets parameters and create new station 
        /// </summary>
        /// <param name="name"> Station`s name</param>
        /// <param name="longitude">The position of the station in relation to the longitude </param>
        /// <param name="latitude">The position of the station in relation to the latitude</param>
        /// <param name="chargeSlots">Number of charging slots at the station</param>
        public void addStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            uniqueIDTaxCheck<Station>(DataSorce.stations, id);
            Station newStation = new Station();
            newStation.Id = id;
            newStation.Name = name;
            newStation.Latitude = latitude;
            newStation.Longitude = longitude;
            newStation.ChargeSlots = chargeSlots;
            newStation.latitudeSexagesimal = DoubleToString(newStation.Latitude, helpSexagesimal);
            newStation.longitudeSexagesimal = DoubleToString(newStation.Longitude, helpSexagesimal);
            DataSorce.stations.Add(newStation);
        }
        /// <summary>
        /// Gets parameters and create new customer 
        /// </summary>
        /// <param name="phone">The customer`s number phone</param>
        /// <param name="name">The customer`s name</param>
        /// <param name="longitude">>The position of the customer in relation to the longitude</param>
        /// <param name="latitude">>The position of the customer in relation to the latitude</param>
        public void addCustomer(int id,string phone, string name, double longitude, double latitude)
        {
            uniqueIDTaxCheck<Customer>(DataSorce.customers, id);
            Customer newCustomer = new Customer();
            newCustomer.Id = ++DataSorce.Config.idxCustomers;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = latitude;
            newCustomer.Longitude = longitude;
            newCustomer.latitudeSexagesimal = DoubleToString(newCustomer.Latitude, helpSexagesimal);
            newCustomer.longitudeSexagesimal = DoubleToString(newCustomer.Longitude, helpSexagesimal);
            DataSorce.customers.Add(newCustomer);
        }
        /// <summary>
        ///  Gets parameters and create new drone 
        /// </summary>
        /// <param name="model"> Grone's model</param>
        /// <param name="MaxWeight"> The max weight that the drone can swipe (light- 0,medium - 1,heavy - 2)</param>
        public void addDrone(int id, string model, WeightCategories MaxWeight)
        {
            uniqueIDTaxCheck<Drone>(DataSorce.drones, id);
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
        public void parcelsReception(int id, int SenderId, int TargetId, WeightCategories Weigth, Prioripies Priority)
        {
            uniqueIDTaxCheck<Parcel>(DataSorce.parcels, id);
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
        /// <summary>
        /// convert a double number to any base in accordance the array of signs parameter
        /// </summary>
        /// <param name="value">The value thar needed to convert</param><param>
        /// <param name="baseChars">The arrray of signs in the needed base</param>
        /// <returns>A string that it is the value in the other base</returns>
        public static string DoubleToString(double value, char[] baseChars)
        {
            string result1 = string.Empty;
            string result2 = string.Empty;
            int targetBase = baseChars.Length;
            int valueInt = (int)value;
            double valueDouble = value - (int)value;

            do
            {
                result1 = baseChars[valueInt % targetBase] + result1;
                valueInt = valueInt / targetBase;
            }
            while (valueInt > 0);
            do
            {
                result2 += baseChars[(int)valueDouble % targetBase];
                valueDouble = (valueDouble % 1) * targetBase;
            } while (valueDouble > 0);
            return result1 + '.' + result2;
        }
        /// <summary>
        /// Checks if id is uniqe
        /// </summary>
        /// <typeparam name="T">object that has property - "id"</typeparam>
        /// <param name="lst"> list</param>
        /// <param name="id"> int </param>
        private void uniqueIDTaxCheck<T>(List<T> lst, int id)
        {
            foreach (var item in lst)
            {
                Type t = item.GetType();
                if ((int)t.GetProperty("id").GetValue(item) == id)
                    throw new ArgumentException(" An element with the same key already exists in the list");
            }

        }
    }

}
