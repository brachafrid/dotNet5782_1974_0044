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
            newStation.Id = DataSorce.Config.idxStations++;
            newStation.Name = name;
            newStation.Latitude = latitude;
            newStation.Longitude = longitude;
            newStation.ChargeSlots = chargeSlots;
            DataSorce.stations.Add(newStation);
        }
        public  void addCustomer(string phone, string name, double longitude, double lattitude)
        {
            Customer newCustomer = new Customer();
            newCustomer.Id = DataSorce.Config.idxCustomers++;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = lattitude;
            newCustomer.Longitude = longitude;
            DataSorce.customers.Add(newCustomer);
        }
        public  void addDrone(string model, WeightCategories MaxWeight)
        {
            Drone newDrone = new Drone();
            newDrone.Id = DataSorce.Config.idxDrones++;
            newDrone.Model = model;
            newDrone.MaxWeight = MaxWeight;
            newDrone.Status = DroneStatuses.AVAILABLE;
            newDrone.Battery = 100;
            DataSorce.drones.Add(newDrone);
        }
        public  void parcelsReception(int SenderId, int TargetId, WeightCategories Weigth, Prioripies Priority)
        {
            Parcel newParcel = new Parcel();
            newParcel.Id = DataSorce.Config.idxParcels++;
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
