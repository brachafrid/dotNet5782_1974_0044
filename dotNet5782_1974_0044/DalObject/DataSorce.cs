using System;
using System.Collections.Generic;
using System.Linq;
using DO;


namespace Dal
{
    public class DataSorce
    {
        static readonly Random Rnd = new ();

        private const int DRONE_INIT = 5;
        private const int STATIONS_INIT = 2;
        private const int CUSTOMERS_INIT = 10;
        private const int PARCELS_INIT = 10;
        private const int RANGE_ENUM = 3;
        private const int PHONE_MIN = 100000000;
        private const int PHONE_MAX = 1000000000;
        private const int LATITUDE_MAX = 90;
        private const int LATITUDE_MIN = -90;
        private const int LONGITUDE_MAX = 90;
        private const int CHARGE_SLOTS_MAX = 100;
        private const int PARCELS_STATE = 4;


        public const string Administrator_Password = "";

        internal static List<Drone> Drones = new();
        internal static List<Station> Stations = new ();
        internal static List<Customer> Customers = new ();
        internal static List<Parcel> Parcels = new ();
        internal static List<DroneCharge> DroneCharges = new ();



        internal class Config
        {
            internal static int IdParcel = 0;
            internal static double Available = 0.001;
            internal static double LightWeightCarrier =0.002; 
            internal static double MediumWeightBearing = 0.003;
            internal static double CarriesHeavyWeight = 0.004;
            internal static double DroneLoadingRate=Rnd.NextDouble();
        }

        static internal void Initialize(DalObject dal)
        {
            for (int i = 1; i <= DRONE_INIT; ++i)
                RandomDrone(dal, i);
            for (int i = 1; i <= STATIONS_INIT; ++i)
                RandomStation(dal, i);
            for (int i = 1; i <= CUSTOMERS_INIT; ++i)
                RandomCustomer(dal, i);
            for (int i = 1; i <= PARCELS_INIT; ++i)
                RandParcel(i);
        }
        public static int AssignParcelDrone(WeightCategories weight)
        {
            Drone tmpDrone = Drones.FirstOrDefault(item => (weight <= item.MaxWeight));
            if (!tmpDrone.Equals(default(Drone)))
            {
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
            double latitude = Rnd.Next(LATITUDE_MIN,LATITUDE_MAX) + Rnd.NextDouble();
            double longitude = Rnd.Next(LONGITUDE_MAX) + Rnd.NextDouble();
            int chargeSlots = Rnd.Next(1,CHARGE_SLOTS_MAX);
            dal.AddStation(id, name, longitude, latitude, chargeSlots);
        }
        private static void RandomCustomer(DalObject dal, int id)
        {
            string name = $"Customer_ { id}_{id * Rnd.Next()}";
            string phone = $"0{Rnd.Next(PHONE_MIN, PHONE_MAX)}";
            double latitude = Rnd.Next(LATITUDE_MIN, LATITUDE_MAX) + Rnd.NextDouble();
            double longitude = Rnd.Next(LONGITUDE_MAX) + Rnd.NextDouble();
            dal.AddCustomer(id, phone, name, longitude, latitude);
        }
        private static void RandParcel( int id)
        {
            Parcel newParcel = new ();
            newParcel.Id = id;
            newParcel.SenderId = Customers[Rnd.Next(1, Customers.Count)].Id;
            do
            {
                newParcel.TargetId = Customers[Rnd.Next(1, Customers.Count)].Id;
            } while (newParcel.TargetId == newParcel.SenderId);
            newParcel.Weigth = (WeightCategories)Rnd.Next(RANGE_ENUM);
            newParcel.Priority = (Priorities)Rnd.Next(RANGE_ENUM);
            newParcel.Requested = DateTime.Now;;
            newParcel.Sceduled =default;
            newParcel.PickedUp = default;
            newParcel.Delivered = default;
            newParcel.DorneId = 0;
            newParcel.IsDeleted = false;
            int state = Rnd.Next(PARCELS_STATE);
            if (state!=0)
            {
                newParcel.DorneId = AssignParcelDrone(newParcel.Weigth);
                if (newParcel.DorneId != 0)
                {
                    Parcel tmp = (Parcels.FirstOrDefault(parcel => parcel.DorneId == newParcel.DorneId && parcel.Delivered == null));
                    if (tmp.DorneId==0)
                    {
                        newParcel.Sceduled = DateTime.Now;
                        if (state==2)
                        {
                            newParcel.PickedUp = DateTime.Now;
                        }
                           
                    }
                    if (state == 3)
                    {
                        newParcel.Sceduled = DateTime.Now;
                        newParcel.PickedUp = DateTime.Now;
                        newParcel.Delivered = DateTime.Now;
                    }
                        
                }

            }
            Parcels.Add(newParcel);
        }
    }

}

