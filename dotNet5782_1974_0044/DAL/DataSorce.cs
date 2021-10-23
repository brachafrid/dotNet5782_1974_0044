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
        static internal void Initialize(DalObject dal)
        {
             //= new DalObject();

            for (; Config.idxDrones < DRONE_INIT; Config.idxDrones++)
                randomDrone(dal);
            for (; Config.idxStations < STATIONS_INIT; Config.idxStations++)
                randomStation(dal);
            for (; Config.idxCustomers < CUSTOMERS_INIT; Config.idxCustomers++)
                randomCustomer(dal);
            for (; Config.idxParcels < PARCELS_INIT; Config.idxParcels++)
                randParcel(dal);
        }
        public static int AssignParcelDrone(WeightCategories weight)
        {
            Drone tmpDrone = drones.FirstOrDefault(item => (weight <= item.MaxWeight && item.Status == DroneStatuses.AVAILABLE));
            if (!(tmpDrone.Equals(default(Drone))))
            {
                drones.Remove(tmpDrone);
                tmpDrone.Status = DroneStatuses.DELIVERY;
                drones.Add(tmpDrone);
                return tmpDrone.Id;
            }
            return 0;

        }
        private static void randomDrone(DalObject dal)
        {
            string model = $"Model_Drone_ {'a' + Config.idxDrones}_{Config.idxDrones + 1 * rnd.Next()}";
            WeightCategories maxWeight = (WeightCategories)rnd.Next(RANGE_ENUM);
            dal.addDrone(model, maxWeight);
        }
        private static void randomStation(DalObject dal)
        {
            string name = $"station_{'a' + Config.idxStations + 1}";
            double latitude = rnd.Next(LATITUDE_MAX) + rnd.NextDouble();
            double longitude = rnd.Next(LONGITUDE_MAX) + rnd.NextDouble();
            int chargeSlots = rnd.Next() + 1;
            dal.addStation(name, longitude, latitude, chargeSlots);
        }
        private static void randomCustomer(DalObject dal)
        {
            string name = $"Customer_ { Config.idxCustomers + 1}_{Config.idxCustomers + 1}";
            string phone = $"0{rnd.Next(PHONE_MIN, PHONE_MAX)}";
            double latitude = rnd.Next(LATITUDE_MAX) + rnd.NextDouble();
            double longitude = rnd.Next(LONGITUDE_MAX) + rnd.NextDouble();
            dal.addCustomer(phone, name, longitude, latitude);
        }
        private static void randParcel(DalObject dal)
        {
            Parcel newParcel = new Parcel();
            newParcel.Id = Config.idxParcels + 1;
            newParcel.SenderId = customers[rnd.Next(0, customers.Count())].Id;
            do
            {
                newParcel.TargetId = customers[rnd.Next(0, customers.Count())].Id;
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

