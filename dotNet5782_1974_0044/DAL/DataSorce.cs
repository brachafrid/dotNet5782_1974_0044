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
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();


        internal class Config
        {
            internal static int idxDrones = 0;
            internal static int idxStations = 0;
            internal static int idxCustomers = 0;
            internal static int idxParcels = 0;

            internal static int IdParcel = 0;
        }
        static internal void Initialize()
        {
            for (; Config.idxDrones < DRONE_INIT; Config.idxDrones++)
                randomDrone();
            for (; Config.idxStations < STATIONS_INIT; Config.idxStations++)
                randomStation();
            for (; Config.idxCustomers < CUSTOMERS_INIT; Config.idxCustomers++)
                randomCustomer();
            for (; Config.idxParcels < PARCELS_INIT; Config.idxParcels++)
                randParcel();
        }
        public static int AssignParcelDrone(WeightCategories weight)
        {
            Drone tmpDrone = DataSorce.drones.First(item => (weight <= item.MaxWeight && item.Status == DroneStatuses.AVAILABLE));
            DataSorce.drones.Remove(tmpDrone);
            tmpDrone.Status = DroneStatuses.DELIVERY;
            DataSorce.drones.Add(tmpDrone);
            return tmpDrone.Id;
        }
        private static void randomDrone()
        {
            Drone newDrone = new Drone();
            newDrone.Id = Config.idxDrones;
            newDrone.Model = $"Model_Drone_ {'a' + Config.idxDrones}_{Config.idxDrones++ * rnd.Next()}";
            newDrone.MaxWeight = (WeightCategories)rnd.Next(RANGE_ENUM);
            newDrone.Status = DroneStatuses.AVAILABLE;
            newDrone.Battery = rnd.Next(FULL_BATTERY) + rnd.NextDouble();
            drones.Add(newDrone);
        }
        private static void randomStation()
        {
            Station newStation = new Station();
            newStation.Id = Config.idxStations;
            newStation.Name = $"station_{'a' + Config.idxStations}";
            newStation.Latitude = rnd.Next(LATITUDE_MAX) + rnd.NextDouble();
            newStation.Longitude = rnd.Next(LONGITUDE_MAX) + rnd.NextDouble();
            stations.Add(newStation);
        }
        private static void randomCustomer()
        {
            Customer newCustomer = new Customer();
            newCustomer.Id = Config.idxCustomers;
            newCustomer.Name = $"Customer_ { Config.idxCustomers + 1}_{newCustomer.Id}";
            newCustomer.Phone = $"0{rnd.Next(PHONE_MIN, PHONE_MAX)}";
            newCustomer.Latitude = rnd.Next(LATITUDE_MAX) + rnd.NextDouble();
            newCustomer.Longitude = rnd.Next(LONGITUDE_MAX) + rnd.NextDouble();
            customers.Add(newCustomer);
        }
        private static void randParcel()
        {
            Parcel newParcel = new Parcel();
            newParcel.Id = Config.idxParcels;
            newParcel.SenderId = customers[rnd.Next(Config.idxCustomers)].Id;
            do
            {
                newParcel.TargetId = customers[rnd.Next(Config.idxCustomers)].Id;
            } while (newParcel.TargetId == newParcel.SenderId);
            newParcel.Weigth = (WeightCategories)rnd.Next(RANGE_ENUM);
            newParcel.Priority = (Prioripies)rnd.Next(RANGE_ENUM);
            newParcel.DorneId = AssignParcelDrone(newParcel.Weigth);
            newParcel.Requested = DateTime.Now;
            newParcel.Sceduled = DateTime.Now;
            newParcel.PickedUp = new DateTime();
            newParcel.Delivered = new DateTime();
            parcels.Add(newParcel);
            ++Config.IdParcel;
        }
    }

}

