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
    }
}
