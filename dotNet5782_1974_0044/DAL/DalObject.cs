using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSorce.Initialize();
        }
        public void addStation(int id, string name, double longitude, double latitude)
        {
            if (DataSorce.Config.idxStations >= DataSorce.STATIONS_LENGTH - 1)
                throw new IndexOutOfRangeException("The array is full");
            checkLongitudeAndLatitude(longitude,latitude);
            checkUniqueID(id, DataSorce.stations);

            Station tmp = new Station();
            tmp.Id = id;
            tmp.Name = name;
            tmp.Latitude = latitude;
            tmp.Longitude = longitude;
            DataSorce.stations.Add(tmp);
            ++DataSorce.Config.idxStations;
            //DataSorce.stations[DataSorce.Config.idxStations].Id = id;
            //DataSorce.stations[DataSorce.Config.idxStations].Name = name;
            //DataSorce.stations[DataSorce.Config.idxStations].Latitude = latitude;
            //DataSorce.stations[DataSorce.Config.idxStations].Longitude = longitude;
        }
        public void addCustomer(int id, string phone, string name, double longitude, double lattitude)
        {
            checkUniqueID(id, DataSorce.customers);
            checkLongitudeAndLatitude(longitude, lattitude);
            Customer tmp = new Customer();
            tmp.Id = id;
            tmp.Name = name;
            tmp.Phone = phone;
            tmp.Latitude = lattitude;
            tmp.Longitude = longitude;
            DataSorce.customers.Add(tmp);
            ++DataSorce.Config.idxCustomers;
            //DataSorce.customers[DataSorce.Config.idxCustomers].Id = id;
            //DataSorce.customers[DataSorce.Config.idxCustomers].Name = name;
            //DataSorce.customers[DataSorce.Config.idxCustomers].Latitude = lattitude;
            //DataSorce.customers[DataSorce.Config.idxCustomers].Longitude = Longitude;
        }
        public void addDrone(int id, string model, WeightCategories MaxWeight, DroneStatuses Status, double Battery)
        {
            checkUniqueID(id, DataSorce.drones);
            if (Battery < 0)
                throw new ArgumentOutOfRangeException("negative battery");
            Drone tmp = new Drone();
            tmp.Id = id;
            tmp.Model = model;
            tmp.MaxWeight = MaxWeight;
            tmp.Status = Status;
            tmp.Battery = Battery;
            DataSorce.drones.Add(tmp);
            ++DataSorce.Config.idxDrones;
            //DataSorce.drones[DataSorce.Config.idxDrones].Id = id;
            //DataSorce.drones[DataSorce.Config.idxDrones].Model = model;
            //DataSorce.drones[DataSorce.Config.idxDrones].MaxWeight = MaxWeight;
            //DataSorce.drones[DataSorce.Config.idxDrones].Status = Status;
            //DataSorce.drones[DataSorce.Config.idxDrones].Battery = Battery;
        }
        public void packageReception(int id, int SenderId, int TargetId, WeightCategories Weigth, Prioripies Priority)
        {
            checkUniqueID(id, DataSorce.parcels, "id");

        }
      private void checkLongitudeAndLatitude(double longitude, double latitude)
        {
            if (latitude > 180 || latitude < 0)
                throw new ArgumentOutOfRangeException("invalid latitude");
            if (longitude < 0 || longitude > 90)
                throw new ArgumentOutOfRangeException("invalid longitude");
        }
        private void checkUniqueID(int id, object[] arr)
        {
            foreach (var item in arr)
            {
                var property = item.GetType().GetProperty("id");
                var id1= property.GetValue(item);
                if ((int)id1 == id)
                {
                    throw new KeyAlreadyExistsException();
                }
            }
        }
    }
  
}
