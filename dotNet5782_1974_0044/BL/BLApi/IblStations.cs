
using System.Collections.Generic;
using System;
using BO;

namespace BLApi
{
    public interface IBlStations
    {
        /// <summary>
        /// Add a station to the list of stations
        /// </summary>
        /// <param name="stationBL">The station for Adding</param>
        public void AddStation(BO.Station station);

        /// <summary>
        /// Update a station in the Stations list
        /// </summary>
        /// <param name="id">The id of the station</param>
        /// <param name="name">The new name</param>
        /// <param name="chargeSlots">A nwe number for charging slots</param>
        public void UpdateStation(int id, string name, int chargeSlots);

        /// <summary>
        /// Retrieves the requested station from the data and converts it to BL station
        /// </summary>
        /// <param name="id">The requested station id</param>
        /// <returns>A Bl satation to print</returns>
        public Station GetStation(int id);

        /// <summary>
        /// Retrieves the list of stations from the data and converts it to station to list
        /// </summary>
        /// <returns>A list of statin to print</returns>
        public IEnumerable<StationToList> GetAllStations();

        /// <summary>
        /// Get active stations
        /// </summary>
        /// <returns>A list of active stations</returns>
        public IEnumerable<StationToList> GetActiveStations();

        /// <summary>
        /// Retrieves the list of stations with empty charge slots  from the data and converts it to station to list
        /// </summary>
        /// <param name="exsitEmpty">the predicate to screen out if the station have empty charge slots</param>
        /// <returns>A list of statin to print</returns>
        public IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots(Predicate<int> exsitEmpty);

        /// <summary>
        /// Delete station according to id
        /// </summary>
        /// <param name="id">id of station</param>
        public void DeleteStation(int id);

        /// <summary>
        /// Check if station is not active
        /// </summary>
        /// <param name="id">id of stations</param>
        /// <returns>if station is not active</returns>
        public bool IsNotActiveStation(int id);

    }
}

