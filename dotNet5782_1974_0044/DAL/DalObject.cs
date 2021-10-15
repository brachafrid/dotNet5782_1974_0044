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
            if (DataSorce.Config.idxStations <= DataSorce.STATIONS_LENGTH - 1)
                throw new IndexOutOfRangeException("The array is full");
            if (latitude > 180 || latitude < 0)
                throw new ArgumentOutOfRangeException("invalid latitude");
            if (longitude < 0 || longitude > 90)
                throw new ArgumentOutOfRangeException("invalid longitude");
            DataSorce.stations[DataSorce.STATIONS_LENGTH].Id = id;
            DataSorce.stations[DataSorce.STATIONS_LENGTH].Name = name;
            DataSorce.stations[DataSorce.STATIONS_LENGTH].Latitude = latitude;
            DataSorce.stations[DataSorce.STATIONS_LENGTH].Longitude = longitude;
        }
        public void addCustomer(int id, string phone, string name, double longitude, double lattitude)
        {

        }
        public void addDrone(int id, string model, WeightCategories MaxWeight, DroneStatuses Status, double Battery)
        {

        }
        public void packageReception(int Id, int SenderId, int TargetId, WeightCategories Weigth, Prioripies Priority)
        {

        }
        private bool checkUniqId(int id, object[] arr)
        {
            foreach (var item in arr)
            {
                var property = item.GetType().GetProperty("Id");
                var id1= property.GetValue(item);
                if ((int)id1 == id)
                {
                    throw new KeyAlreadyExistsException();
                }
            }
            return true;
        }



    }
    public class KeyAlreadyExistsException: ArgumentException
    {
        public KeyAlreadyExistsException()
            :base("An element with the same key already exists in the collection")
        {

        }
    }
}
