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
using System.Runtime.CompilerServices;

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
            try { 
            if (!File.Exists(DIR + CONFIG))
                    DalXmlUnit.InitializeConfig();
            if (!File.Exists(DIR + DRONE_PATH))
                DalXmlService.SaveListToXMLSerializer(DalXmlUnit.InitializeDrone(), DRONE_PATH);
            if (!File.Exists(DIR + STATION_PATH))
                DalXmlService.SaveListToXMLSerializer(DalXmlUnit.InitializeStation(), STATION_PATH);
            if (!File.Exists(DIR + CUSTOMER_PATH))
                DalXmlService.SaveListToXMLSerializer(DalXmlUnit.InitializeCustomer(), CUSTOMER_PATH);
            if (!File.Exists(DIR + PARCEL_PATH))
                DalXmlService.SaveListToXMLSerializer(DalXmlUnit.InitializeParcel(), PARCEL_PATH);
            if (!File.Exists(DIR + DRONE_CHARGE_PATH))
                DalXmlService.SaveListToXMLSerializer(new List<DroneCharge>(), DRONE_CHARGE_PATH);
            }
            catch(XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string GetAdministorPasssword()
        {
            try
            {
                return DalXmlService.LoadConfigToXML(CONFIG).Elements().Single(elem => elem.Name.ToString().Contains("Password")).Value;
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
    }
}
