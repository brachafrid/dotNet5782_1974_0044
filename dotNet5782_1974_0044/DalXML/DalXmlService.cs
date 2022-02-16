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
        static string dir = @"..\..\..\..\data\";

        /// <summary>
        /// constructor check if folder exist create if not
        /// </summary>
        static DalXmlService()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        #region SaveLoadWithXMLSerializer
        /// <summary>
        /// Save list with XML serializer
        /// </summary>
        /// <typeparam name="T">The type of the list to save</typeparam>
        /// <param name="list">The list to save</param>
        /// <param name="filePath">The path of the list file</param>
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
                throw new XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
       

        /// <summary>
        /// Load list with XML serializer
        /// </summary>
        /// <typeparam name="T">The ttype of list to load</typeparam>
        /// <param name="filePath">The path of the list file</param>
        /// <returns>The list to load</returns>
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
        #endregion

        #region SaveLoadWithXElement
        /// <summary>
        /// Save xelement to XML 
        /// </summary>
        /// <param name="rootElem">root element to save</param>
        /// <param name="filePath">the path of file</param>
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

        /// <summary>
        /// Load xelement from XML
        /// </summary>
        /// <param name="filePath">the path of file</param>
        /// <returns>root of document</returns>
        internal static XElement LoadXElementToXML(string filePath)
        {
            try
            {
                if (!File.Exists(dir + filePath))
                {
                    new XElement("Parcels").Save(dir + filePath);
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

        #region ConverStation
        /// <summary>
        /// convert station to xelement
        /// </summary>
        /// <param name="station"> the station to convert</param>
        /// <returns>xelment</returns>
        internal static XElement ConvertStationToXElement(Station station)
        {
            return new XElement("Station",
                 new XElement(nameof( station.Id), station.Id),
                 new XElement(nameof(station.Name), station.Name),
                 new XElement(nameof(station.Latitude), station.Latitude),
                 new XElement(nameof(station.Latitude), station.Longitude),
                 new XElement(nameof(station.ChargeSlots), station.ChargeSlots),
                 new XElement(nameof(station.IsNotActive), station.IsNotActive));
        }
        /// <summary>
        /// conver xelement to station
        /// </summary>
        /// <param name="xElementStation">the element to convert</param>
        /// <returns> station</returns>
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
        #endregion

        #region ConvertParcel
        /// <summary>
        /// Convert parcel to xelement parcel
        /// </summary>
        /// <param name="parcel">The parcel to convert</param>
        /// <returns>DO.Parcel</returns>
        internal static XElement ConvertParcelToXElement(Parcel parcel)
        {
            return new XElement("Parcel",
                 new XElement(nameof(parcel.Id), parcel.Id),
                 new XElement(nameof(parcel.SenderId), parcel.SenderId),
                 new XElement(nameof(parcel.TargetId), parcel.TargetId),
                 new XElement(nameof(parcel.Weigth), parcel.Weigth),
                 new XElement(nameof(parcel.Priority), parcel.Priority),
                 new XElement(nameof(parcel.Requested), parcel.Requested),
                 new XElement(nameof(parcel.Sceduled), parcel.Sceduled),
                 new XElement(nameof(parcel.PickedUp), parcel.PickedUp),
                 new XElement(nameof(parcel.Delivered), parcel.Delivered),
                 new XElement(nameof(parcel.DorneId), parcel.DorneId),
                 new XElement(nameof(parcel.IsNotActive), parcel.IsNotActive));
        }

        /// <summary>
        /// convert xelement of parcel to DO.Parcel
        /// </summary>
        /// <param name="xElementParcel">the Xelement Parcel to convert</param>
        /// <returns>xelement</returns>
        internal static Parcel ConvertXElementToParcel(XElement xElementParcel)
        {
            return new Parcel()
            {
                Id = int.Parse(xElementParcel.Element("Id")?.Value),
                SenderId=int.Parse(xElementParcel.Element("SenderId")?.Value),
                TargetId = int.Parse(xElementParcel.Element("TargetId")?.Value),
                Weigth = (WeightCategories)Enum.Parse(typeof(WeightCategories), xElementParcel.Element("Weigth")?.Value),
                Priority = (Priorities)Enum.Parse(typeof(Priorities), xElementParcel.Element("Priority")?.Value),
                Requested = (DateTime)xElementParcel.Element("Requested"),
                Sceduled= xElementParcel.Element("Sceduled").Value == string.Empty ? null : (DateTime)xElementParcel.Element("Sceduled"),
                PickedUp = xElementParcel.Element("PickedUp").Value == string.Empty ? null : (DateTime?)xElementParcel.Element("PickedUp"),
                Delivered = xElementParcel.Element("Delivered").Value == string.Empty ? null : (DateTime?)xElementParcel.Element("Delivered"),
                DorneId=int.Parse(xElementParcel.Element("DorneId")?.Value),
                IsNotActive = bool.Parse(xElementParcel.Element("IsNotActive")?.Value)
            };
        }
        #endregion
    }


}


