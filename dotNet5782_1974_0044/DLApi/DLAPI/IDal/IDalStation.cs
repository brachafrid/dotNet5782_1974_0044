using DO;
using System;
using System.Collections.Generic;

namespace DLApi
{
    public interface IDalStation
    {
        /// <summary>
        /// Add new station
        /// </summary>
        /// <param name="id">new station's id</param>
        /// <param name="name">new station's name</param>
        /// <param name="longitude">new station's longitude</param>
        /// <param name="latitude">new station's latitude</param>
        /// <param name="chargeSlots">new station's charge slots</param>
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);


        /// <summary>
        /// Get station
        /// </summary>
        /// <param name="id">station's id</param>
        /// <returns>station</returns>
        public Station GetStation(int id);

        /// <summary>
        /// Get the list of the stations
        /// </summary>
        /// <returns>list of stations</returns>
        public IEnumerable<Station> GetStations();


        /// <summary>
        /// Get sations with empty charge slots
        /// </summary>
        /// <param name="exsitEmpty">Predicate type of int : exsitEmpty return true if exsits empty charge slors</param>
        /// <returns>Sations with empty charge slots</returns>
        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty);

        /// <summary>
        /// Update name or number of charge slots of station
        /// </summary>
        /// <param name="station">station</param>
        /// <param name="name">station's name</param>
        /// <param name="chargeSlots">station's charge slots</param>
        public void UpdateStation(Station station, string name, int chargeSlots);

        /// <summary>
        /// Delete station according to id
        /// </summary>
        /// <param name="id">station's id</param>
        public void DeleteStation(int id);
    }
}
