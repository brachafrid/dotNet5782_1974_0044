using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
   public interface IBL
    {
        public void AddStation(int id, string name, Location location, int chargeSlots);
        public void AddDrone(int id, BO.WeightCategories MaximumWeight, int stationId);
        public void AddCustomer(int id, string name, string phone);
        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, BO.WeightCategories Weight, BO.Priorities priority);
        public void UpdateDrone(int id, string name);
        public void UpdateStation(int id, string name, int chargeSlots);
        public void UpdateCusomer(int id, string name, string phone);
        public void SendDroneForCharg(int id);
        public void ReleaseDroneFromCharging(int id, float timeOfCharg);
        public void AssingParcellToDrone(int droneId);
        public void ParcelCollectionByDrone(int DroneId);
        public void DeliveryParcelByDrone(int droneId);
        public IDAL.DO.Station GetStation(int id);
        public IDAL.DO.Customer GetCustomer(int id);
        public IDAL.DO.Parcel GetParcel(int id);
        public IDAL.DO.Drone GetDrone(int id);
        public IEnumerable<IDAL.DO.Station> GetStations();
        public IEnumerable<IDAL.DO.Customer> GetCustomers();
        public IEnumerable<IDAL.DO.Parcel> GetParcels();
        public IEnumerable<IDAL.DO.Drone> GetDrones();
        public IEnumerable<IDAL.DO.Parcel> GetParcelsNotAssignedToDrone();
        public IEnumerable<IDAL.DO.Station> GetSationsWithEmptyChargeSlots();

    }
}
