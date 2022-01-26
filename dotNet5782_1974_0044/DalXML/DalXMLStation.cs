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
        const string STATION_PATH = @"XMLStation.xml"; 
        public void RemoveStation(Station station)
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH);
            stations.Remove(station);
            XMLTools.SaveListToXMLSerializer<Station>(stations, STATION_PATH);
        }

        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty)
        {
            return XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH).FindAll(item => exsitEmpty(item.ChargeSlots - CountFullChargeSlots(item.Id)) && !item.IsDeleted);
        }

        public Station GetStation(int id)
        {
            Station station = XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH).FirstOrDefault(item => item.Id == id);
            if (station.Equals(default(Station)) || station.IsDeleted == true)
                throw new KeyNotFoundException("There is no suitable station in data");
            return station;
        }

        public IEnumerable<Station> GetStations()
        {
            return XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH).Where(s => !s.IsDeleted);
        }

        public void DeleteStation(int id)
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH);
            Station station = stations.FirstOrDefault(item => item.Id == id);
            stations.Remove(station);
            station.IsDeleted = true;
            stations.Add(station);
            XMLTools.SaveListToXMLSerializer<Station>(stations, STATION_PATH);
        }

        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(STATION_PATH);
            if (ExistsIDTaxCheck(stations, id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException();
            Station newStation = new();
            newStation.Id = id;
            newStation.Name = name;
            newStation.Latitude = latitude;
            newStation.Longitude = longitude;
            newStation.ChargeSlots = chargeSlots;
            newStation.IsDeleted = false;
            stations.Add(newStation); 
            XMLTools.SaveListToXMLSerializer<Station>(stations, STATION_PATH);
        }
    }
}
