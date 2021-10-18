﻿using System;
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
                throw new ArgumentException("The array is full");
            if (latitude > 180 || latitude < 0) 
                throw new ArgumentException("invalid latitude");
            

            //checkUniqueID(id, DataSorce.stations);
            DataSorce.stations[DataSorce.Config.idxStations].Id = id;
            DataSorce.stations[DataSorce.Config.idxStations].Name = name;
            DataSorce.stations[DataSorce.Config.idxStations].Latitude = latitude;
            DataSorce.stations[DataSorce.Config.idxStations].Longitude = longitude;
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
        private bool checkUniqueID(int id, object[] arr)
        {
            foreach (var item in arr)
            {
                var property = item.GetType().GetProperty("Id");
                var id1 = property.GetValue(item);
                if ((int)id1 == id)
                {
                    throw new ArgumentException();
                }
            }
            return true;
        }
    }
  
}
