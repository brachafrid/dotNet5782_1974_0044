using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{
    public sealed partial class DalXml
    {
        const string STATION_PATH = @"XmlStation.xml"; 
        public void RemoveStation(Station station)
        {
            try
            {
                List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH);
                stations.Remove(station);
                XMLTools.SaveListToXMLSerializer<Station>(stations, STATION_PATH);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }

        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty)
        {
            try { 
                return XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH).FindAll(item => exsitEmpty(item.ChargeSlots - CountFullChargeSlots(item.Id)) );
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }

        public Station GetStation(int id)
        {
            try { 
                Station station = XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH).FirstOrDefault(item => item.Id == id);
                if (station.Equals(default(Station)) )
                    throw new KeyNotFoundException("There is no suitable station in data");
                return station;
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }

        public IEnumerable<Station> GetStations()
        {
            try { 
                return XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }

        public void DeleteStation(int id)
        {
            try { 
                List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH);
                Station station = stations.FirstOrDefault(item => item.Id == id);
                stations.Remove(station);
                station.IsNotActive = true;
                stations.Add(station);
                XMLTools.SaveListToXMLSerializer<Station>(stations, STATION_PATH);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }

        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            try { 
                List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH);
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
                XMLTools.SaveListToXMLSerializer<Station>(stations, STATION_PATH);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }
    }
}
