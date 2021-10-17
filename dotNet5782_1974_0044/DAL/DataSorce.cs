﻿using System;
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
        public const int DRONE_LENGTH = 10;
        public const int STATIONS_LENGTH = 5;
        public const int CUSTOMERS_LENGTH = 100;
        public const int PARCELS_LENGTH = 1000;
        public const int DRONE_INIT = 5;
        public const int STATIONS_INIT = 2;
        public const int CUSTOMERS_INIT = 10;
        public const int PARCELS_INIT = 10;
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

            static int IdParcel = 0;
        }

        static internal void Initialize()
        {
            for (int i = 0; i < STATIONS_INIT; i++)
            {
                stations[i].Id = (i + 1) * 10;
                stations[i].Name = $"station_{'a'+i}";
                stations[i].Latitude = rnd.Next(180) + rnd.NextDouble();
                stations[i].Longitude = rnd.Next(90) + rnd.NextDouble();
                ++Config.idxStations;
            }

            for (int i = 0; i < DRONE_INIT; i++)
            {
                drones[i].Id = (i + 1) * 10;
                drones[i].Model = $"Model_Drone_ {'a' + i}_{i*rnd.Next()}";
                drones[i].MaxWeight = (WeightCategories)rnd.Next(3);
                drones[i].Status = (DroneStatuses)rnd.Next(3);
                drones[i].Battery = rnd.Next(100) + rnd.NextDouble();
                ++Config.idxDrones;
            }

            for (int i = 0; i < CUSTOMERS_INIT; i++)
            {
                customers[i].Id = (i + 1) *10;
                customers[i].Name = $"Customer_ {i+1}_{customers[i].Id}";
                customers[i].Phone = $"0{rnd.Next(1000000000)}";
                customers[i].Latitude = rnd.Next(180) + rnd.NextDouble();
                customers[i].Longitude = rnd.Next(90) + rnd.NextDouble();
                ++Config.idxCustomers;
            }

            for (int i = 0; i < PARCELS_INIT; i++)
            {
            
                parcels[i].Id = (i + 1) * 10;
                parcels[i].SenderId = rnd.Next();
                parcels[i].TargetId = rnd.Next(Config.idxStations*10);
                parcels[i].Weigth = (WeightCategories)rnd.Next(3);
                parcels[i].Priority = (Prioripies)rnd.Next(3);
                parcels[i].Requested = new DateTime(2021,12,25);
                parcels[i].DorneId = rnd.Next(Config.idxDrones * 10);
                parcels[i].Sceduled = new DateTime(2020,12,03);
                parcels[i].PickedUp = new DateTime(2020,11,25);
                parcels[i].Delivered = new DateTime(2019,04,02);
                ++Config.idxParcels;
            }
        }
    }c

}
