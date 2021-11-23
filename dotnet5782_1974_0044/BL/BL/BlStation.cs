using IBL.BO;
using System;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL:IblStations
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
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
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
            if (name.Equals(default) && chargeSlots == -1)
                throw new ArgumentNullException("Update station -BL-:For updating at least one parameter must be initialized ");
            try
            {
                IDAL.DO.Station satationDl = dal.GetStation(id);
                if (chargeSlots < dal.CountFullChargeSlots(satationDl.Id))
                    throw new ArgumentOutOfRangeException("Update station -BL-:The number of charging slots is smaller than the number of slots used");
                dal.RemoveStation(satationDl);
                dal.AddStation(id, name.Equals(default) ? satationDl.Name : name, satationDl.Longitude, satationDl.Latitude, chargeSlots.Equals(default) ? satationDl.ChargeSlots : chargeSlots);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Update station -BL-"+ex.Message);
            }
            catch(IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Update station -BL-"+ex.Message );
            }
           
        }

        //-------------------------------------------------Return List-----------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the list of stations with empty charge slots  from the data and converts it to station to list
        /// </summary>
        /// <returns>A list of statin to print</returns>
        public IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots()
        {
            IEnumerable<IDAL.DO.Station> list = dal.GetSationsWithEmptyChargeSlots();
            List<StationToList> stations = new List<StationToList>();
            foreach (var item in list)
            {
                stations.Add(MapStationToList(item));
            }
            return stations;
        }

        /// <summary>
        /// Retrieves the list of stations from the data and converts it to station to list
        /// </summary>
        /// <returns>A list of statin to print</returns>
        public IEnumerable<StationToList> GetStations()
        {
            IEnumerable<IDAL.DO.Station> list = dal.GetStations();
            List<StationToList> stations = new List<StationToList>();
            foreach (var item in list)
            {
                stations.Add(MapStationToList(item));
            }
            return stations;
        }

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
                return MapStation(dal.GetStation(id));
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
        private BO.Station MapStation(IDAL.DO.Station station)
        {
            return new Station() {
                Id = station.Id,
                Name = station.Name,
                Location = new Location() { Latitude=station.Latitude,Longitude=station.Longitude },
                AvailableChargingPorts=station.ChargeSlots-dal.CountFullChargeSlots(station.Id),
                DroneInChargings=CreatListDroneInCharging(station.Id)
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
        private IDAL.DO.Station ClosetStationPossible(IEnumerable<IDAL.DO.Station> stations, Location droneToListLocation,double BatteryStatus, out double minDistance)
        {
            IDAL.DO.Station station = ClosetStation(stations, droneToListLocation);
            minDistance = Distance(new Location() { Longitude = station.Longitude, Latitude = station.Latitude }, droneToListLocation);
            return minDistance * available < BatteryStatus ? station : default(IDAL.DO.Station);
        }

        /// <summary>
        /// Calculates the station closest to a particular location 
        /// </summary>
        /// <param name="stations">The all stations</param>
        /// <param name="location">The  particular location</param>
        /// <returns>The station</returns>
        private IDAL.DO.Station ClosetStation(IEnumerable<IDAL.DO.Station> stations, Location location)
        {
            double minDistance = 0;
            double curDistance;
            IDAL.DO.Station station = default;
            foreach (var item in stations)
            {
                curDistance = Distance(location,
                    new Location() { Latitude = item.Latitude, Longitude = item.Longitude });
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    station = item;
                }
            }
            return station;
        }
    }
}
