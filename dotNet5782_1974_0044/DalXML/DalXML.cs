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
    partial class DalXML : Singletone<DalXML>, IDal
    {
        public DalXML()
        {
            Creation();
            Initilaztion();
        }
        private void Creation()
        {
            if (!File.Exists(@"data\XMLDrone"))
                try
                {
                    FileStream file = new FileStream(@"data\XMLDrone", FileMode.Create);
                    file.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

        }

        private void Initilaztion()
        {

        }
        public string GetAdministorPasssword()
        {
            throw new NotImplementedException();
        }
    }
}
