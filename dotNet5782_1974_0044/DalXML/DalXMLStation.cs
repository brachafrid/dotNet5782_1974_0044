using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DLApi;

namespace Dal
{
    public sealed partial class DalXml:IDalStation
    {
        const string STATION_PATH = @"XmlStation.xml"; 
        public void RemoveStation(Station station)
        {
            try
            {
                List<Station> stations = DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH);
                stations.Remove(station);
                DalXmlService.SaveListToXMLSerializer<Station>(stations, STATION_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty)
        {
            try { 
                return DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH).FindAll(item => exsitEmpty(item.ChargeSlots - CountFullChargeSlots(item.Id)) && !item.IsNotActive);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

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
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        public IEnumerable<Station> GetStations()
        {
            try { 
                return DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        public void DeleteStation(int id)
        {
            try { 
                List<Station> stations = DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH);
                Station station = stations.FirstOrDefault(item => item.Id == id);
                stations.Remove(station);
                station.IsNotActive = true;
                stations.Add(station);
                DalXmlService.SaveListToXMLSerializer<Station>(stations, STATION_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            try { 
                List<Station> stations = DalXmlService.LoadListFromXMLSerializer<Station>(STATION_PATH);
                if (ExistsIDTaxCheck(stations, id))
                    throw new ThereIsAnObjectWithTheSameKeyInTheListException();
                Station newStation = new();
                newStation.Id = id;
                newStation.Name = name;
                newStation.Latitude = latitude;
                newStation.Longitude = longitude;
                newStation.ChargeSlots = chargeSlots;
                newStation.IsNotActive = false;
                stations.Add(newStation); 
                DalXmlService.SaveListToXMLSerializer<Station>(stations, STATION_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
    }
}
