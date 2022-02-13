using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using DO;


namespace Dal
{

    internal static class DalXmlService
    {
        static string dir = @"..\..\data\";
        static DalXmlService()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        #region SaveLoadWithXMLSerializer
        internal static void SaveListToXMLSerializer<T>(IEnumerable<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dir + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        internal static XElement ConvertStationToXElement(Station station)
        {
          return  new XElement("Station",
               new XElement("Id", station.Id),
               new XElement("Name", station.Name),
               new XElement("Latitude", station.Latitude),
               new XElement("Longitude", station.Longitude),
               new XElement("ChargeSlots", station.ChargeSlots),
               new XElement("IsNotActive", station.IsNotActive));
        }
        internal static Station ConvertXElementToStation(XElement xElementStation)
        {
            return new Station()
            {
                Id = int.Parse(xElementStation.Element("Id").Value),
                Name = xElementStation.Element("Name").Value,
                Latitude = double.Parse(xElementStation.Element("Latitude").Value),
                Longitude = double.Parse(xElementStation.Element("Longitude").Value),
                ChargeSlots = int.Parse(xElementStation.Element("ChargeSlots").Value),
                IsNotActive = bool.Parse(xElementStation.Element("IsNotActive").Value)
            };
        }
        internal static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir + filePath, FileMode.Open);
                    List<T> list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
               throw new XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
            
        }

        internal static void SaveXElementToXML(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dir + filePath);
            }
            catch (Exception ex)
            {
                throw new XMLFileLoadCreateException($"fail to create xml file: {filePath}", ex);
            }
        }
        internal static XElement LoadXElementToXML(string filePath)
        {
            try
            {
                if (!File.Exists(dir + filePath))
                {
                    throw new XMLFileLoadCreateException($"fail to load xml file: {filePath}");
                }
                XDocument document = XDocument.Load(dir + filePath);
                return document.Root;
            }
            catch (Exception)
            {

                throw new XMLFileLoadCreateException();
            }

        }
        #endregion
    }


}


