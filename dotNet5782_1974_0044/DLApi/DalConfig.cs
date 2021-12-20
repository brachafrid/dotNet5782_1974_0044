using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
    class DalConfig
    {
        internal static string DalType;
        internal static string Namespace;

        static DalConfig()
        {
            XElement dalConfig = null;
            try
            {
                dalConfig = XElement.Load($@"{Directory.GetCurrentDirectory()}\..\..\..\..\config.xml");

            }
            catch (Exception e)
            {
                throw new DalConfigException("Can't get data from config file", e);
            }

            string dalName = dalConfig.Element("dal").Value;
            var dalPackage = dalConfig.Element("dal-packages")
                                      .Element(dalName);
            DalType = dalPackage.Element("class-name").Value;
            Namespace = dalPackage.Element("namespace").Value;
        }
    }

    public class DalConfigException : Exception
    {
        public DalConfigException(string message) : base(message) { }
        public DalConfigException(string message, Exception inner) : base(message, inner) { }
    }
}
