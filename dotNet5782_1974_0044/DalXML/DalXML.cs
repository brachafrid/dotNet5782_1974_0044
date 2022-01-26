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
        private DalXml()
        {
            Initilaztion();
        }

        private void Initilaztion()
        {
            if (!File.Exists(@"..\data\"+DRONE_PATH))
                XMLTools.SaveListToXMLSerializer(InitializeDrone(), DRONE_PATH);
            if (!File.Exists(@"..\data\"+STATION_PATH))
                XMLTools.SaveListToXMLSerializer(InitializeStation(),STATION_PATH);
            if (!File.Exists(@"..\data\"+CUSTOMER_PATH))
                XMLTools.SaveListToXMLSerializer(InitializeCustomer(), CUSTOMER_PATH);
            if (!File.Exists(@"..\data\"+PARCEL_PATH))
                XMLTools.SaveListToXMLSerializer(InitializeParcel(), PARCEL_PATH);
            if (!File.Exists(@"..\data\"+DRONE_CHARGE_PATH))
                XMLTools.SaveListToXMLSerializer(new List<DroneCharge>(), DRONE_CHARGE_PATH);
        }
        public string GetAdministorPasssword()
        {
            return "";
        }
    }
}
