﻿using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : IBlStations
    {
        //-----------------------------------------------------------Adding------------------------------------------------------------------------
        /// <summary>
        /// Add a station to the list of stations
        /// </summary>
        /// <param name="stationBL">The station for Adding</param>
        public void AddStation(Station stationBL)
        {
            try
            {
                dal.AddStation(stationBL.Id, stationBL.Name, stationBL.Location.Longitude, stationBL.Location.Longitude, stationBL.AvailableChargingPorts);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }

        }

        //-------------------------------------------------------Updating-----------------------------------------------------------------------------
        /// <summary>
        /// Update a station in the Stations list
        /// </summary>
        /// <param name="id">The id of the station</param>
        /// <param name="name">The new name</param>
        /// <param name="chargeSlots">A nwe number for charging slots</param>
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            if (name.Equals(string.Empty) && chargeSlots == 0)
                throw new ArgumentNullException("For updating at least one parameter must be initialized ");
            try
            {
                DO.Station stationDl = dal.GetStation(id);
                if (chargeSlots != 0 && chargeSlots < dal.CountFullChargeSlots(stationDl.Id))
                    throw new ArgumentOutOfRangeException("The number of charging slots is smaller than the number of slots used");
                dal.UpdateStation(stationDl,name,chargeSlots);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }

        }

        public void DeleteStation(int id)
        {
            dal.DeleteStation(id);
        }
        public bool IsNotActiveStation(int id) => dal.GetStations().Any(station => station.Id == id && station.IsNotActive);

        //-------------------------------------------------Return List-----------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the list of stations with empty charge slots  from the data and converts it to station to list
        /// </summary>
        /// <param name="exsitEmpty">the predicate to screen out if the station have empty charge slots</param>
        /// <returns>A list of statin to print</returns>
        public IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots(Predicate<int> exsitEmpty)
        {
            IEnumerable<DO.Station> list = dal.GetSationsWithEmptyChargeSlots(exsitEmpty);
            return list.Select(item => MapStationToList(item));
        }

        /// <summary>
        /// Retrieves the list of stations from the data and converts it to station to list
        /// </summary>
        /// <returns>A list of statin to print</returns>
        public IEnumerable<StationToList> GetStations() => dal.GetStations().Select(item => MapStationToList(item));
        public IEnumerable<StationToList> GetActiveStations() => dal.GetStations().Where(item => !item.IsNotActive).Select(item => MapStationToList(item));

        //--------------------------------------------------Return-----------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the requested station from the data and converts it to BL station
        /// </summary>
        /// <param name="id">The requested station id</param>
        /// <returns>A Bl satation to print</returns>
        public Station GetStation(int id)
        {
            try
            {
                return ConvertStation(dal.GetStation(id));
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }

        }

        //-----------------------------------------------Help function-----------------------------------------------------------------------------------
        /// <summary>
        /// Convert a DAL station to BL satation
        /// </summary>
        /// <param name="station">The sation to convert</param>
        /// <returns>The converted station</returns>
        private BO.Station ConvertStation(DO.Station station)
        {
            return new Station()
            {
                Id = station.Id,
                Name = station.Name,
                Location = new Location() { Latitude = station.Latitude, Longitude = station.Longitude },
                AvailableChargingPorts = station.ChargeSlots - dal.CountFullChargeSlots(station.Id),
                DroneInChargings = CreatListDroneInCharging(station.Id)
            };
        }

        /// <summary>
        /// Calculate what is the nearest station that it possible for the drone to reach
        /// call to other function that returenthe nearest station
        /// </summary>
        /// <param name="stations">The all station</param>
        /// <param name="droneToList">The drone</param>
        /// <param name="minDistance">The distance the drone need to travel</param>
        /// <returns></returns>
        private DO.Station ClosetStationPossible(IEnumerable<DO.Station> stations, Location droneToListLocation, double BatteryStatus, out double minDistance)
        {
            DO.Station station = ClosetStation(stations, droneToListLocation);
            minDistance = Distance(droneToListLocation, new Location() { Longitude = station.Longitude, Latitude = station.Latitude });
            return minDistance * available <= BatteryStatus ? station : default(DO.Station);
        }

        /// <summary>
        /// Calculates the station closest to a particular location 
        /// </summary>
        /// <param name="stations">The all stations</param>
        /// <param name="location">The  particular location</param>
        /// <returns>The station</returns>
        private DO.Station ClosetStation(IEnumerable<DO.Station> stations, Location location)
        {
            double minDistance = double.MaxValue;
            double curDistance;
            DO.Station station = default;
            foreach (var item in stations)
            {
                curDistance = Distance(location, new Location() { Latitude = item.Latitude, Longitude = item.Longitude });
                if (curDistance < minDistance && !item.IsNotActive)
                {
                    minDistance = curDistance;
                    station = item;
                }
            }
            return station;
        }
    }
}
