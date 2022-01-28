using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using DLApi;
using DO;
using System.IO;
using System.Xml.Serialization;

namespace Dal
{
    public sealed partial class DalXml : Singletone<DalXml>, IDal
    {
        const string DIR = @"..\..\data\";
        const string CONFIG = @"XmlConfig.xml";
        private DalXml()
        {
            Initilaztion();
        }

        private void Initilaztion()
        {
            //try { 
            if (!File.Exists(DIR + CONFIG))
                InitializeConfig();
            if (!File.Exists(DIR + DRONE_PATH))
                XMLTools.SaveListToXMLSerializer(InitializeDrone(), DRONE_PATH);
            if (!File.Exists(DIR + STATION_PATH))
                XMLTools.SaveListToXMLSerializer(InitializeStation(), STATION_PATH);
            if (!File.Exists(DIR + CUSTOMER_PATH))
                XMLTools.SaveListToXMLSerializer(InitializeCustomer(), CUSTOMER_PATH);
            if (!File.Exists(DIR + PARCEL_PATH))
                XMLTools.SaveListToXMLSerializer(InitializeParcel(), PARCEL_PATH);
            if (!File.Exists(DIR + DRONE_CHARGE_PATH))
                XMLTools.SaveListToXMLSerializer(new List<DroneCharge>(), DRONE_CHARGE_PATH);
            //}
            //catch
            //{
            //    throw new XMLFileLoadCreateException();
            //}
        }

        public string GetAdministorPasssword()
        {
            return "";
        }
    }
}
