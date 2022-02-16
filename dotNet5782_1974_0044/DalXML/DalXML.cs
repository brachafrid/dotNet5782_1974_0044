using DLApi;
using DO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Utilities;

namespace Dal
{
    public sealed partial class DalXml : Singletone<DalXml>, IDal
    {
        const string DIR = @"..\..\..\..\data\";
        const string CONFIG = "XmlConfig.xml";
        /// <summary>
        /// constructor initilaztion The Xml file if they not exsits
        /// </summary>
        private DalXml()
        {
            try
            {
                Initilaztion();
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex);
            }

        }
        /// <summary>
        /// Initializes the XML files and save them
        /// </summary>
        private void Initilaztion()
        {

            if (!File.Exists(DIR + CONFIG))
                DalXmlService.SaveXElementToXML(DalXmlUnit.InitializeConfig(),CONFIG);
            if (!File.Exists(DIR + DRONE_PATH))
                DalXmlService.SaveListToXMLSerializer(DalXmlUnit.InitializeDrone(), DRONE_PATH);
            if (!File.Exists(DIR + STATION_PATH))
                DalXmlService.SaveXElementToXML(DalXmlUnit.InitializeStation(), STATION_PATH);
            if (!File.Exists(DIR + CUSTOMER_PATH))
                DalXmlService.SaveListToXMLSerializer(DalXmlUnit.InitializeCustomer(), CUSTOMER_PATH);
            if (!File.Exists(DIR + PARCEL_PATH))
                DalXmlService.SaveXElementToXML(DalXmlUnit.InitializeParcel(), PARCEL_PATH);
            if (!File.Exists(DIR + DRONE_CHARGE_PATH))
                DalXmlService.SaveListToXMLSerializer(new List<DroneCharge>(), DRONE_CHARGE_PATH);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string GetAdministorPasssword()
        {
            try
            {
                return DalXmlService.LoadXElementToXML(CONFIG).Elements().Single(elem => elem.Name.ToString().Contains("Password")).Value;
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
    }
}
