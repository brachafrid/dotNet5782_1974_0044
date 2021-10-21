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
        public  void addStation(string name, double longitude, double latitude, int chargeSlots )
        {
            Station newStation = new Station();
            newStation.Id = ++DataSorce.Config.idxStations;
            newStation.Name = name;
            newStation.Latitude = latitude;
            newStation.Longitude = longitude;
            newStation.ChargeSlots = chargeSlots;
            DataSorce.stations.Add(newStation);
        }
        public  void addCustomer(string phone, string name, double longitude, double lattitude)
        {
            Customer newCustomer = new Customer();
            newCustomer.Id = ++DataSorce.Config.idxCustomers;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = lattitude;
            newCustomer.Longitude = longitude;
            DataSorce.customers.Add(newCustomer);
        }
        /// <summary>
        ///  gets parameters and create new drone 
        /// </summary>
        /// <param name="model"> drone's model</param>
        /// <param name="MaxWeight"> max weight that the drone can swipe (light- 0,medium - 1,heavy - 2)</param>
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
        /// gets parameters and create new parcel 
        /// </summary>
        /// <param name="SenderId"> id of sener</param>
        /// <param name="TargetId"> id of target</param>
        /// <param name="Weigth"> weigth of parcel (light- 0,medium - 1,heavy - 2)</param>
        /// <param name="Priority"> priority of send the parcel (regular - 0,fast - 1,emergency - 2)</param>
        public void parcelsReception(int SenderId, int TargetId, WeightCategories Weigth, Prioripies Priority)
        {
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
