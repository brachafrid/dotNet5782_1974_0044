using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        public void addCustomer(int id, string phone, string name, double longitude, double latitude);
        public void addDrone(int id, string model, WeightCategories MaxWeight);
        public void ParcelsReception(int id, int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority);
        public void addStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public void AssignParcelDrone(int parcelId);
        public void CollectParcel(int parcelId);
        public void SupplyParcel(int parcelId);
        public void SendDroneCharg(int droneId);
        public void ReleasDroneCharg(int droneId);
        public Station GetStation(int id);
        public Drone GetDrone(int id);
        public Customer GetCustomer(int id);
        public Parcel GetParcel(int id);
        public IEnumerable<Station> GetStations();
        public IEnumerable<Drone> GetDrones();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone();
        public IEnumerable<Station> GetSationsWithEmptyChargeSlots();
        public int countFullChargeSlots(int id);
        public List<int> GetDronechargingInStation(int id);
    }
}
