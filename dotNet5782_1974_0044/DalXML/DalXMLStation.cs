using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DLApi;
using System.Runtime.CompilerServices;

namespace Dal
{
    public sealed partial class DalXml:IDalStation
    {
        const string STATION_PATH = @"XmlStation.xml";

        /// <summary>
        /// Update name and number of charge slots of station
        /// </summary>
        /// <param name="station">station</param>
        /// <param name="name">station's name</param>
        /// <param name="chargeSlots">station's charge slots</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(Station station, string name, int chargeSlots)
        {
            try
            {
                List<Station> stations = DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH);
                stations.Remove(station);
                if (!name.Equals(string.Empty))
                    station.Name = name;
                if (chargeSlots != 0)
                    station.ChargeSlots = chargeSlots;
                stations.Add(station);
                DalXmlService.SaveListToXMLSerializer(stations, STATION_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Get sations with empty charge slots
        /// </summary>
        /// <param name="exsitEmpty">Predicate type of int : exsitEmpty</param>
        /// <returns>Sations with empty charge slots</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty)
        {
            try { 
                return DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH).FindAll(item => exsitEmpty(item.ChargeSlots - CountFullChargeSlots(item.Id)) && !item.IsNotActive);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Get station
        /// </summary>
        /// <param name="id">station's id</param>
        /// <returns>station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id)
        {
            try { 
                Station station = DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH).FirstOrDefault(item => item.Id == id);
                if (station.Equals(default(Station)) )
                    throw new KeyNotFoundException("There is no suitable station in data");
                return station;
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Get the list of the stations
        /// </summary>
        /// <returns>list of stations</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations()
        {
            try { 
                return DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Delete station according to id
        /// </summary>
        /// <param name="id">station's id</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(int id)
        {
            try { 
                List<Station> stations = DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH);
                Station station = stations.FirstOrDefault(item => item.Id == id);
                if (station.Equals(default(Station)))
                    throw new KeyNotFoundException($"The station id {id}  not exsits in dta");
                stations.Remove(station);
                station.IsNotActive = true;
                stations.Add(station);
                DalXmlService.SaveListToXMLSerializer(stations, STATION_PATH);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Add new station
        /// </summary>
        /// <param name="id">new station's id</param>
        /// <param name="name">new station's name</param>
        /// <param name="longitude">new station's longitude</param>
        /// <param name="latitude">new station's latitude</param>
        /// <param name="chargeSlots">new station's charge slots</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            try { 
                List<Station> stations = DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH);
                if (ExistsIDTaxCheck(stations, id))
                    throw new ThereIsAnObjectWithTheSameKeyInTheListException(id);
                stations.Add(new()
                {
                    Id = id,
                    Name = name,
                    Latitude = latitude,
                    Longitude = longitude,
                    ChargeSlots = chargeSlots,
                    IsNotActive = false
                }); 
                DalXmlService.SaveListToXMLSerializer(stations, STATION_PATH);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
    }
}
