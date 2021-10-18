using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace DalObject
{
    public class DataSorce
    {
        static Random rnd = new Random();

        public const int DRONE_INIT = 5;
        public const int STATIONS_INIT = 2;
        public const int CUSTOMERS_INIT = 10;
        public const int PARCELS_INIT = 10;
        public const int RANGE_ENUM = 3;
        public const int PHONE_MIN = 100000000;
        public const int PHONE_MAX = 1000000000;
        public const int LATITUDE_MAX = 180;
        public const int LONGITUDE_MAX = 90;
        public const int FULL_BATTERY = 100;

        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> stations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();

        
        internal class Config
        {
            internal static int idxDrones = 0;
            internal static int idxStations = 0;
            internal static int idxCustomers = 0;
            internal static int idxParcels = 0;

            static int IdParcel = 0;
        }

        static internal void Initialize()
        {
            for (int i = 0; i < DRONE_INIT; i++)
            {
                Drone tmp = new Drone();
                tmp.Id = i;
                tmp.Model = $"Model_Drone_ {'a' + i}_{i * rnd.Next()}";
                tmp.MaxWeight = (WeightCategories)rnd.Next(RANGE_ENUM);
                tmp.Status = (DroneStatuses)rnd.Next(RANGE_ENUM);
                tmp.Battery = rnd.Next(FULL_BATTERY) + rnd.NextDouble();
                drones.Add(tmp);
                ++Config.idxDrones;
            }

            for (int i = 0; i < STATIONS_INIT; i++)
            {
                Station tmp = new Station();
                tmp.Id = i;
                tmp.Name = $"station_{'a' + i}";
                tmp.Latitude = rnd.Next(LATITUDE_MAX) + rnd.NextDouble();
                tmp.Longitude = rnd.Next(LONGITUDE_MAX) + rnd.NextDouble();
                stations.Add(tmp);
                ++Config.idxStations;
            }

            for (int i = 0; i < CUSTOMERS_INIT; i++)
            {
                Customer tmp = new Customer();
                tmp.Id = i;
                tmp.Name = $"Customer_ {i + 1}_{customers[i].Id}";
                tmp.Phone = $"0{rnd.Next(PHONE_MIN, PHONE_MAX)}";
                tmp.Latitude = rnd.Next(LATITUDE_MAX) + rnd.NextDouble();
                tmp.Longitude = rnd.Next(LONGITUDE_MAX) + rnd.NextDouble();
                customers.Add(tmp);
                ++Config.idxCustomers;
            }

            for (int i = 0; i < PARCELS_INIT; i++)
            {
                Parcel tmp = new Parcel();
                tmp.Id = i;
                tmp.SenderId = rnd.Next();
                tmp.TargetId = rnd.Next(Config.idxStations);
                tmp.Weigth = (WeightCategories)rnd.Next(RANGE_ENUM);
                tmp.Priority = (Prioripies)rnd.Next(RANGE_ENUM);
                tmp.Requested = new DateTime();
                tmp.DorneId = rnd.Next(Config.idxDrones);
                tmp.Sceduled = new DateTime();
                tmp.PickedUp = new DateTime();
                tmp.Delivered = new DateTime();
                parcels.Add(tmp);
                ++Config.idxParcels;
            }
        }
    }
}

