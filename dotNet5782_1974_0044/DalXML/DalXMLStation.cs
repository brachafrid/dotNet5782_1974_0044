using DLApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Dal
{
    public sealed partial class DalXml : IDalStation
    {
        const string STATION_PATH = @"XmlStation.xml";
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(Station station, string name, int chargeSlots)
        {
            try
            {
                XElement xElementStations = DalXmlService.LoadXElementToXML(STATION_PATH);
                XElement xElementStation = xElementStations.Elements().FirstOrDefault(elem => int.Parse(elem.Element("Id").Value) == station.Id);
                if (xElementStation == default(XElement))
                    throw new KeyNotFoundException($"The station id {station.Id}  not exsits in dta");
                if (!name.Equals(string.Empty))
                    xElementStation.SetElementValue("Name",name);
                if (chargeSlots != 0)
                    xElementStation.SetElementValue("ChargeSlots", chargeSlots);
                DalXmlService.SaveXElementToXML(xElementStations, STATION_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetSationsWithEmptyChargeSlots(Predicate<int> exsitEmpty)
        {
            try
            {
                return DalXmlService.LoadXElementToXML(STATION_PATH).Elements()
                    .Where(elem => !bool.Parse(elem.Element("IsNotActive").Value) && exsitEmpty(int.Parse(elem.Element("ChargeSlots").Value) - CountFullChargeSlots(int.Parse(elem.Element("Id").Value))))
                    .Select(elem => DalXmlService.ConvertXElementToStation(elem));
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id)
        {
            try
            {
                XElement xElementStation = DalXmlService.LoadXElementToXML(STATION_PATH).Elements().FirstOrDefault(elem => int.Parse(elem.Element("Id").Value) == id);
                if (xElementStation.Equals(default(XElement)))
                    throw new KeyNotFoundException("There is no suitable station in data");
                return DalXmlService.ConvertXElementToStation(xElementStation);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations()
        {
            try
            {
                return DalXmlService.LoadXElementToXML(STATION_PATH).Elements().Select(elem => DalXmlService.ConvertXElementToStation(elem));
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(int id)
        {
            try
            {
                XElement xElementStations = DalXmlService.LoadXElementToXML(STATION_PATH);
                XElement station = xElementStations.Elements().FirstOrDefault(elem => int.Parse(elem.Element("Id").Value) == id);
                if (station == default(XElement))
                    throw new KeyNotFoundException($"The station id {id}  not exsits in dta");
                station.SetElementValue("IsNotActive", true);
                DalXmlService.SaveXElementToXML(xElementStations, STATION_PATH);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            try
            {
                XElement xElementStations = DalXmlService.LoadXElementToXML(STATION_PATH);
                if (xElementStations.Elements().FirstOrDefault(elem => int.Parse(elem.Element("Id").Value) == id) != default(XElement))
                    throw new ThereIsAnObjectWithTheSameKeyInTheListException(id);
                xElementStations.Add(DalXmlService.ConvertStationToXElement(new()
                {
                    Id = id,
                    Name = name,
                    Latitude = latitude,
                    Longitude = longitude,
                    ChargeSlots = chargeSlots,
                    IsNotActive = false
                }));
                DalXmlService.SaveXElementToXML(xElementStations, STATION_PATH);

            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
    }
}
