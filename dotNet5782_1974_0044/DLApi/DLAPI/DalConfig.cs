﻿using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DLApi
{
    class DalConfig
    {
        internal static string DalType;
        internal static string Namespace;

        /// <summary>
        /// ctor
        /// </summary>
        static DalConfig()
        {
            XElement dalConfig = null;
            try
            {
                dalConfig = XElement.Load($@"{Directory.GetCurrentDirectory()}..\..\..\..\..\config.xml");

            }
            catch (Exception e)
            {
                throw new DalConfigException("Can't get data from config file", e);
            }

            string dalName = dalConfig.Element("dal").Value;
            var dalPackage = dalConfig.Element("dal-packages")
                                      .Element(dalName);
            DalType = dalPackage.Attribute("class-name").Value;
            Namespace = dalPackage.Attribute("namespace").Value;
        }
    }
   
    
}
