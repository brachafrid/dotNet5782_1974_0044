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
        public const int Sations_INIT = 2;
        public const int CUSTOMERS_INIT = 10;
        public const int Parcels_INIT = 10;
        public const int RANGE_ENUM = 3;
        public const int PHONE_MIN = 100000000;
        public const int PHONE_MAX = 1000000000;
        public const int LATITUDE_MAX = 180;
        public const int LONGITUDE_MAX = 90;
        public const int FULL_BATTERY = 100;

        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Station> Sations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();


        internal class Config
        {
            internal static int IdParcel = 0;
        }

        static internal void Initialize(DalObject dal)
        {
          for(int i =1; i <= DRONE_INIT;++i)
                randomDrone(dal ,i);
            for ( int i =1; i <= Sations_INIT; ++i )
                randomStation(dal, i);
            for (int i =1;  i <= CUSTOMERS_INIT; ++i)
                randomCustomer(dal, i);
            for( int i =1; i <= Parcels_INIT; ++i)
                randParcel(dal,i);
        }
        public static int AssignParcelDrone(WeightCategories weight)
        {
            Drone tmpDrone = Drones.FirstOrDefault(item => (weight <= item.MaxWeight));
            if (!(tmpDrone.Equals(default(Drone))))
            {
                Drones.Remove(tmpDrone);
                Drones.Add(tmpDrone);
                return tmpDrone.Id;
            }
            return 0;

        }
        private static void randomDrone(DalObject dal,int id)
        {
            string model = $"Model_Drone_ {'a' + id}_{id * rnd.Next()}";
            WeightCategories maxWeight = (WeightCategories)rnd.Next(RANGE_ENUM);
            dal.addDrone( id, model, maxWeight);
        }
        private static void randomStation(DalObject dal, int id)
        {
            string name = $"station_{'a' + id}";
            double latitude = rnd.Next(LATITUDE_MAX) + rnd.NextDouble();
            double longitude = rnd.Next(LONGITUDE_MAX) + rnd.NextDouble();
            int chargeSlots = rnd.Next() + 1;
            dal.addStation(id,name, longitude, latitude, chargeSlots);
        }
        private static void randomCustomer(DalObject dal,int id)
        {
            string name = $"Customer_ { id}_{id* rnd.Next()}";
            string phone = $"0{rnd.Next(PHONE_MIN, PHONE_MAX)}";
            double latitude = rnd.Next(LATITUDE_MAX) + rnd.NextDouble();
            double longitude = rnd.Next(LONGITUDE_MAX) + rnd.NextDouble();
            dal.addCustomer(id,phone, name, longitude, latitude);
        }
        private static void randParcel(DalObject dal,int id)
        {
            Parcel newParcel = new Parcel();
            newParcel.Id = id;
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
            Parcels.Add(newParcel);
            
        }
    }

}

