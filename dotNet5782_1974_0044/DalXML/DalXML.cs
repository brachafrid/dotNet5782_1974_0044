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
            if (!File.Exists(@"..\data\XMLParcel"))
                XMLTools.SaveListToXMLSerializer(InitializeParcel(), "XmlParcel");
            if (!File.Exists(@"..\data\XMLStation"))
                XMLTools.SaveListToXMLSerializer(InitializeStation(), "XMLStation");
            if (!File.Exists(@"..\data\XMLCustomer"))
                XMLTools.SaveListToXMLSerializer(InitializeCustomer(), "XMLCustomer");
            if (!File.Exists(@"..\data\XMLDroneCharge"))
                XMLTools.SaveListToXMLSerializer(new List<DroneCharge>(), "XMLDroneCharge");
        }
        public string GetAdministorPasssword()
        {
            throw new NotImplementedException();
        }
    }
}
