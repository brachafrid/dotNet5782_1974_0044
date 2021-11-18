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
        static readonly Random rnd = new ();

        public const int DRONE_INIT = 5;
        public const int Sations_INIT = 2;
        public const int CUSTOMERS_INIT = 10;
        public const int Parcels_INIT = 10;
        public const int RANGE_ENUM = 3;
        public const int PHONE_MIN = 100000000;
        public const int PHONE_MAX = 1000000000;
        public const int LATITUDE_MAX = 180; 
        public const int LONGITUDE_MAX = 90;
        public const int PERCENTAGE = 100;

        internal static List<Drone> Drones = new();
        internal static List<Station> Stations = new ();
        internal static List<Customer> Customers = new ();
        internal static List<Parcel> Parcels = new ();
        internal static List<DroneCharge> DroneCharges = new ();

        public static Random Rnd { get => rnd; set => rnd = value; }

        internal class Config
        {
            internal static int IdParcel = 0;
            internal static double Available = Rnd.NextDouble();
            internal static double LightWeightCarrier =Rnd.Next((int)Available* PERCENTAGE, PERCENTAGE) / PERCENTAGE; 
            internal static double MediumWeightBearing = Rnd.Next((int)LightWeightCarrier* PERCENTAGE, PERCENTAGE) / PERCENTAGE;
            internal static double CarriesHeavyWeight = Rnd.Next((int)MediumWeightBearing* PERCENTAGE, PERCENTAGE) / PERCENTAGE;
            internal static double DroneLoadingRate=Rnd.NextDouble();
        }

        static internal void Initialize(DalObject dal)
        {
            for (int i = 1; i <= DRONE_INIT; ++i)
                RandomDrone(dal, i);
            for (int i = 1; i <= Sations_INIT; ++i)
                RandomStation(dal, i);
            for (int i = 1; i <= CUSTOMERS_INIT; ++i)
                RandomCustomer(dal, i);
            for (int i = 1; i <= Parcels_INIT; ++i)
                randParcel(dal, i);
        }
        public static int AssignParcelDrone(WeightCategories weight)
        {
            Drone tmpDrone = Drones.FirstOrDefault(item => (weight <= item.MaxWeight));
            if (!(tmpDrone.Equals(default)))
            {
                Drones.Remove(tmpDrone);
                Drones.Add(tmpDrone);
                return tmpDrone.Id;
            }
            return 0;

        }
        private static void RandomDrone(DalObject dal, int id)
        {
            string model = $"Model_Drone_ {'a' + id}_{id * Rnd.Next()}";
            WeightCategories maxWeight = (WeightCategories)Rnd.Next(RANGE_ENUM);
            dal.AddDrone(id, model, maxWeight);
        }
        private static void RandomStation(DalObject dal, int id)
        {
            string name = $"station_{'a' + id}";
            double latitude = Rnd.Next(LATITUDE_MAX) + Rnd.NextDouble();
            double longitude = Rnd.Next(LONGITUDE_MAX) + Rnd.NextDouble();
            int chargeSlots = Rnd.Next() + 1;
            dal.AddStation(id, name, longitude, latitude, chargeSlots);
        }
        private static void RandomCustomer(DalObject dal, int id)
        {
            string name = $"Customer_ { id}_{id * Rnd.Next()}";
            string phone = $"0{Rnd.Next(PHONE_MIN, PHONE_MAX)}";
            double latitude = Rnd.Next(LATITUDE_MAX) + Rnd.NextDouble();
            double longitude = Rnd.Next(LONGITUDE_MAX) + Rnd.NextDouble();
            dal.AddCustomer(id, phone, name, longitude, latitude);
        }
        private static void RandParcel( int id)
        {
            Parcel newParcel = new ();
            newParcel.Id = id;
            newParcel.SenderId = Customers[Rnd.Next(0, Customers.Count)].Id;
            do
            {
                newParcel.TargetId = Customers[Rnd.Next(0, Customers.Count)].Id;
            } while (newParcel.TargetId == newParcel.SenderId);
            newParcel.Weigth = (WeightCategories)Rnd.Next(RANGE_ENUM);
            newParcel.Priority = (Priorities)Rnd.Next(RANGE_ENUM);
            newParcel.DorneId = AssignParcelDrone(newParcel.Weigth);
            newParcel.Requested = DateTime.Now;
            newParcel.Sceduled = DateTime.Now;
            newParcel.PickedUp = new DateTime();
            newParcel.Delivered = new DateTime();
            Parcels.Add(newParcel);
        }
    }

}

