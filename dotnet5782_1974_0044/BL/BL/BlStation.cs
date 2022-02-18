using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public partial class BL : IBlStations
    {
        #region ADD
        // [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station stationBL)
        {
            try
            {
                lock (dal)
                    dal.AddStation(stationBL.Id, stationBL.Name, stationBL.Location.Longitude, stationBL.Location.Longitude, stationBL.AvailableChargingPorts);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex.Id);
            }

        }
        #endregion

        #region Return

        // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots(Predicate<int> exsitEmpty)
        {
            try
            {
                IEnumerable<DO.Station> list;
                lock (dal)
                    list = dal.GetSationsWithEmptyChargeSlots(exsitEmpty);
                return list.Select(item => MapStationToList(item));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }

        // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetAllStations()
        {
            try
            {
                lock (dal)
                    return dal.GetStations().Select(item => MapStationToList(item));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {

                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }


        // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetActiveStations()
        {
            try
            {
                lock (dal)
                    return dal.GetStations().Where(item => !item.IsNotActive).Select(item => MapStationToList(item));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {

                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }


        // [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id)
        {
            try
            {
                lock (dal)
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
        // [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            if (name.Equals(string.Empty) && chargeSlots == 0)
                throw new ArgumentNullException("For updating at least one parameter must be initialized ");
            try
            {
                DO.Station stationDl;
                lock (dal)
                    stationDl = dal.GetStation(id);
                lock (dal)
                    if (chargeSlots != 0 && chargeSlots < dal.CountFullChargeSlots(stationDl.Id))
                        throw new ArgumentOutOfRangeException("The number of charging slots is smaller than the number of slots used");
                lock (dal)
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


        // [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(int id)
        {
            try
            {
                lock (dal)
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
                lock (dal)
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
        internal Station ClosetStationPossible(Location droneToListLocation, Predicate<int> emptyChargeslots, double BatteryStatus, out double minDistance)
        {
            Station station = ClosetStation(droneToListLocation, emptyChargeslots);
            if (station == null)
            {
                minDistance = 0;
                return null;
            }
            minDistance = Distance(droneToListLocation, station.Location);
            return minDistance * available <= BatteryStatus ? station : null;
        }

        /// <summary>
        /// Calculates the station closest to a particular location 
        /// </summary>
        /// <param name="stations">The all stations</param>
        /// <param name="location">The  particular location</param>
        /// <returns>The station</returns>
        private Station ClosetStation(Location location, Predicate<int> emptyChargeslots)
        {
            double minDistance = int.MaxValue;
            double curDistance;
            Station station = null;
            lock (dal)
                foreach (var item in dal.GetSationsWithEmptyChargeSlots(emptyChargeslots))
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
