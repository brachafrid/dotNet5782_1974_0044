using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : IBlStations
    {
        #region ADD
        /// <summary>
        /// Add a station to the list of stations
        /// </summary>
        /// <param name="stationBL">The station for Adding</param>
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station stationBL)
        {
            try
            {
                dal.AddStation(stationBL.Id, stationBL.Name, stationBL.Location.Longitude, stationBL.Location.Longitude, stationBL.AvailableChargingPorts);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex.Id);
            }

        }
        #endregion

        #region Return
        /// <summary>
        /// Retrieves the list of stations with empty charge slots  from the data and converts it to station to list
        /// </summary>
        /// <param name="exsitEmpty">the predicate to screen out if the station have empty charge slots</param>
        /// <returns>A list of statin to print</returns>
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots(Predicate<int> exsitEmpty)
        {
            try
            {
                IEnumerable<DO.Station> list = dal.GetSationsWithEmptyChargeSlots(exsitEmpty);
                return list.Select(item => MapStationToList(item));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// Retrieves the list of stations from the data and converts it to station to list
        /// </summary>
        /// <returns>A list of statin to print</returns>
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStations()
        {
            try
            {
               return dal.GetStations().Select(item => MapStationToList(item));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {

                throw new XMLFileLoadCreateException(ex.FilePath,ex.Message,ex.InnerException);
            }
            
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetActiveStations()
        {
            try
            {
                return dal.GetStations().Where(item => !item.IsNotActive).Select(item => MapStationToList(item));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {

                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
            
        }

        /// <summary>
        /// Retrieves the requested station from the data and converts it to BL station
        /// </summary>
        /// <param name="id">The requested station id</param>
        /// <returns>A Bl satation to print</returns>
        //[MethodImpl(MethodImplOptions.Synchronized)]
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
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }


        }
        #endregion

        #region Update
        /// <summary>
        /// Update a station in the Stations list
        /// </summary>
        /// <param name="id">The id of the station</param>
        /// <param name="name">The new name</param>
        /// <param name="chargeSlots">A nwe number for charging slots</param>
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            if (name.Equals(string.Empty) && chargeSlots == 0)
                throw new ArgumentNullException("For updating at least one parameter must be initialized ");
            try
            {
                DO.Station stationDl = dal.GetStation(id);
                if (chargeSlots != 0 && chargeSlots < dal.CountFullChargeSlots(stationDl.Id))
                    throw new ArgumentOutOfRangeException("The number of charging slots is smaller than the number of slots used");
                dal.UpdateStation(stationDl, name, chargeSlots);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }
        #endregion

        #region Delete
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(int id)
        {
            try
            {
                dal.DeleteStation(id);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }

        }
        #endregion
        public bool IsNotActiveStation(int id)
        {
            try
            {
                return dal.GetStations().Any(station => station.Id == id && station.IsNotActive);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// Calculate what is the nearest station that it possible for the drone to reach
        /// call to other function that returenthe nearest station
        /// </summary>
        /// <param name="stations">The all station</param>
        /// <param name="droneToList">The drone</param>
        /// <param name="minDistance">The distance the drone need to travel</param>
        /// <returns></returns>
        internal Station ClosetStationPossible(Location droneToListLocation, double BatteryStatus, out double minDistance)
        {
            Station station = ClosetStation(droneToListLocation);
            minDistance = Distance(droneToListLocation, station.Location);
            return minDistance * available <= BatteryStatus ? station : null;
        }

        /// <summary>
        /// Calculates the station closest to a particular location 
        /// </summary>
        /// <param name="stations">The all stations</param>
        /// <param name="location">The  particular location</param>
        /// <returns>The station</returns>
        private Station ClosetStation(Location location)
        {
            double minDistance = int.MaxValue;
            double curDistance;
            Station station = null;
            foreach (var item in dal.GetStations())
            {
                curDistance = Distance(location, new Location() { Latitude = item.Latitude, Longitude = item.Longitude });
                if (curDistance < minDistance && !item.IsNotActive)
                {
                    minDistance = curDistance;
                    station = ConvertStation(item);
                }
            }
            return station;
        }
    }
}
