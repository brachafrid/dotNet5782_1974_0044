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
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude);
        public void AddDrone(int id, string model, WeightCategories MaxWeight);
        public void AddParcel( int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority,int id=0);
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public void RemoveCustomer(Customer customer);
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
        public void RemoveStation(Station station);
        public int CountFullChargeSlots(int id);
        public List<int> GetDronechargingInStation(int id);
        public void RemoveDrone(Drone drone);
        public void AddDRoneCharge(int droneId, int stationId);
        public void RemoveDroneCharge(int droneId);
        public double[] GetElectricityUse();
        public void RemoveParcel(Parcel parcel);

    }
}
