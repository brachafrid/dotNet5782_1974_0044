using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
 

namespace DalObject
{
    class DataSorce
    {
        static Random rnd = new Random();
        const int DRONE_LENGTH = 10;
        const int STATIONS_LENGTH = 5;
        const int CUSTOMERS_LENGTH = 100;
        const int PARCELS_LENGTH = 1000;
        internal static Drone[] drones = new Drone[DRONE_LENGTH];
        internal static Station[] stations = new Station[STATIONS_LENGTH];
        internal static Customer[] customers = new Customer[CUSTOMERS_LENGTH];
        internal static Parcel[] parcels = new Parcel[PARCELS_LENGTH];

        internal class Config
        {
            internal static int idxDrones = 0;
            internal static int idxStations=0;
            internal static int idxCustomers=0;
            internal static int idxParcels=0;

            static int IdParcel =0;
        }

        static internal void Initialize()
        {
            for (int i = 0; i < 2; i++)
            {
                stations[i].Id = (i + 1) * 10;
                stations[i].Name = $"station_{'a'+i}";
                stations[i].Latitude = rnd.Next(180) + rnd.NextDouble();
                stations[i].Longitude = rnd.Next(90) + rnd.NextDouble();
            }

            for (int i = 0; i < 5; i++)
            {
                drones[i].Id = (i + 1) * 10;
                drones[i].Model = $"Model_Drone_ {'a' + i}_{i*rnd.Next()}";
                drones[i].MaxWeight = (WeightCategories)rnd.Next(3);
                drones[i].Status = (DroneStatuses)rnd.Next(3);
                drones[i].Battery = rnd.Next(100) + rnd.NextDouble(); ;   
            }

            for (int i = 0; i < 10; i++)
            {
                customers[i].Id = (i + 1) *10;
                customers[i].Name = $"Customer_ {i+1}_{customers[i].Id}";
                customers[i].Phone = (WeightCategories)rnd.Next(3);
    
            }
        }
    }

}

