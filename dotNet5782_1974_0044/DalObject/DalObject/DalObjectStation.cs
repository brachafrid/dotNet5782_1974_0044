
using System.Collections.Generic;
using System.Linq;
using System;
using DO;

namespace Dal

{
    public partial class DalObject
    {
        //-------------------------------------------------------Adding-------------------------------------------------
        /// <summary>
        ///  Gets parameters and create new station 
        /// </summary>
        /// <param name="name"> Station`s name</param>
        /// <param name="longitude">The position of the station in relation to the longitude </param>
        /// <param name="latitude">The position of the station in relation to the latitude</param>
        /// <param name="chargeSlots">Number of charging slots at the station</param>
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            if (ExistsIDTaxCheck(DataSorce.Stations, id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException();
            Station newStation = new();
            newStation.Id = id;
            newStation.Name = name;
            newStation.Latitude = latitude;
            newStation.Longitude = longitude;
            newStation.ChargeSlots = chargeSlots;
            newStation.IsActive = false;
            AddEntity(newStation);
        }

        //-------------------------------------------------Display-------------------------------------------------------------
        /// <summary>
        /// Find a station that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested station/param>
        /// <returns>A station for display</returns>
        public Station GetStation(int id)
        {
            Station station = getEntities<Station>().FirstOrDefault(item => item.Id == id);
            if (station.Equals(default(Station)) || station.IsActive == true)
                throw new KeyNotFoundException("There is no suitable station in data");
            return station;
        }

        /// <summary>
        ///  Prepares the list of Sations for display
        /// </summary>
        /// <returns>A list of stations</returns>
        public IEnumerable<Station> GetStations() => getEntities<Station>();

        /// <summary>
        /// Find the satation that have empty charging slots
        /// </summary>
        /// <param name="exsitEmpty">The predicate to screen out if the station have empty charge slots</param>
        /// <returns>A collection of the requested station</returns>
        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty) => getEntities<Station>().Where(item => exsitEmpty(item.ChargeSlots - CountFullChargeSlots(item.Id)) && !item.IsActive);

        //-------------------------------------------------Removing-------------------------------------------------------------
        /// <summary>
        /// Removing a station from the list
        /// </summary>
        /// <param name="station"></param>
        public void RemoveStation(Station station)
        {
            RemoveEntity(station);
        }

        public void DeleteStation(int id)
        {
            Station station = DataSorce.Stations.FirstOrDefault(item => item.Id == id);
            RemoveEntity(station);
            station.IsActive = true;
            AddEntity(station);
        }
    }
}
