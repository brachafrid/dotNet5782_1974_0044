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

        /// <summary>
        /// constructor
        /// </summary>
        static DalXmlService()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        #region SaveLoadWithXMLSerializer
        /// <summary>
        /// Save list to XML serializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">generic list</param>
        /// <param name="filePath">the path of file</param>
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

        /// <summary>
        /// Load list from XML serializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">the path of file</param>
        /// <returns>generic list</returns>
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

        /// <summary>
        /// Save config to XML
        /// </summary>
        /// <param name="rootElem">root element</param>
        /// <param name="filePath">the path of file</param>
        internal static void SaveConfigToXML(XElement rootElem, string filePath)
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
        /// Load config to XML
        /// </summary>
        /// <param name="filePath">the path of file</param>
        /// <returns>root of document</returns>
        internal static XElement LoadConfigToXML(string filePath)
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


