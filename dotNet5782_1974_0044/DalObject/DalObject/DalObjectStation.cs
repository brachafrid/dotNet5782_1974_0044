using DLApi;
using System.Collections.Generic;
using System.Linq;
using System;
using DO;
using System.Runtime.CompilerServices;

namespace Dal

{
    public partial class DalObject:IDalStation
    {
        //-------------------------------------------------------Adding-------------------------------------------------
        /// <summary>
        ///  Gets parameters and create new station 
        /// </summary>
        /// <param name="name"> Station`s name</param>
        /// <param name="longitude">The position of the station in relation to the longitude </param>
        /// <param name="latitude">The position of the station in relation to the latitude</param>
        /// <param name="chargeSlots">Number of charging slots at the station</param>
         [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            if (IsExistsIDTaxCheck(DataSorce.Stations, id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(id);
            DalObjectService.AddEntity(new Station()
            {
                Id = id,
                Name = name,
                Latitude = latitude,
                Longitude = longitude,
                ChargeSlots = chargeSlots,
                IsNotActive = false
            });
        }

        //-------------------------------------------------Display-------------------------------------------------------------
        /// <summary>
        /// Find a station that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested station/param>
        /// <returns>A station for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id)
        {
            Station station = DalObjectService.GetEntities<Station>().FirstOrDefault(item => item.Id == id);
            if (station.Equals(default(Station)) )
                throw new KeyNotFoundException($"There is no suitable station in data , the station id: {id} ");
            return station;
        }

        /// <summary>
        ///  Prepares the list of Sations for display
        /// </summary>
        /// <returns>A list of stations</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations() => DalObjectService.GetEntities<Station>();

        /// <summary>
        /// Find the satation that have empty charging slots
        /// </summary>
        /// <param name="exsitEmpty">The predicate to screen out if the station have empty charge slots</param>
        /// <returns>A collection of the requested station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty) => DalObjectService.GetEntities<Station>().Where(item => exsitEmpty(item.ChargeSlots - CountFullChargeSlots(item.Id)) && !item.IsNotActive);

        //-------------------------------------------------Removing-------------------------------------------------------------
        /// <summary>
        /// Removing a station from the list
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(Station station,string name,int chargeSlots)
        {
            DalObjectService.RemoveEntity(station);
            if (!name.Equals(string.Empty))
                station.Name = name;
            if (chargeSlots != 0)
                station.ChargeSlots = chargeSlots;
            DalObjectService.AddEntity(station);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(int id)
        {
            Station station = DataSorce.Stations.FirstOrDefault(item => item.Id == id);
            if (station.Equals(default(Station)))
                throw new KeyNotFoundException($"the station {id} not exsits in data");
            DalObjectService.RemoveEntity(station);
            station.IsNotActive = true;
            DalObjectService.AddEntity(station);
        }
    }
}
