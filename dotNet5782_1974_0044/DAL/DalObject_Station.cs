using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace  DalObjec

{
    public partial class DalObjec
    {
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

        /// <summary>
        /// Find a satation that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested station/param>
        /// <returns>A station for display</returns>
        public Station GetStation(int id)
        {
            return DataSorce.Stations.First(item => item.Id == id);
        }
        /// <summary>
        /// Find a drone that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested drone</param>
        /// <returns>A drone for display</returns>
    }
}
