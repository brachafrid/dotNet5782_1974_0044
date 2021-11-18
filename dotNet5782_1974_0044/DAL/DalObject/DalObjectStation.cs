using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace  DalObject

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
        public void addStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            uniqueIDTaxCheck<Station>(DataSorce.Stations, id);
            Station newStation = new Station();
            newStation.Id = id;
            newStation.Name = name;
            newStation.Latitude = latitude;
            newStation.Longitude = longitude;
            newStation.ChargeSlots = chargeSlots;
            DataSorce.Stations.Add(newStation);
        }

        //-------------------------------------------------Display-------------------------------------------------------------
        /// <summary>
        /// Find a satation that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested station/param>
        /// <returns>A station for display</returns>
        public Station GetStation(int id)=>DataSorce.Stations.First(item => item.Id == id);
       
        /// <summary>
        ///  Prepares the list of Sations for display
        /// </summary>
        /// <returns>A list of stations</returns>
        public IEnumerable<Station> GetStations() => DataSorce.Stations;

        /// <summary>
        /// Find the satation that have empty charging slots
        /// </summary>
        /// <returns>A list of the requested station</returns>
        /// /// <summary>
        /// Checks which base Sations are available for charging
        /// </summary>
        /// <returns>A list of avaiable satations</returns>
        private List<Station> getAvailbleStations() => (DataSorce.Stations.FindAll(item => item.ChargeSlots > CountFullChargeSlots(item.Id)));
        public IEnumerable<Station> GetSationsWithEmptyChargeSlots() => getAvailbleStations().ToList();

        //-------------------------------------------------Removing-------------------------------------------------------------
        /// <summary>
        /// Removing a station from the list
        /// </summary>
        /// <param name="station"></param>
        public void RemoveStation(Station station)
        {
            DataSorce.Stations.Remove(station);
        }


        
    }
}
