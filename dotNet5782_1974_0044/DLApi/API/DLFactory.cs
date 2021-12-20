using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DLApi
{
    public static class DLFactory
    {
        public static IDal GetDL()
        {
            Assembly.LoadFrom($@"{Directory.GetCurrentDirectory()}\..\..\..\..\{DalConfig.DalType}\bin\Debug\net5.0\{DalConfig.DalType}.dll");
            Type type = Type.GetType($"{DalConfig.Namespace}.{DalConfig.DalType}, {DalConfig.DalType}");
            if (type == null)
                throw new DalConfigException("Can't find such project");
            IDal dal = (IDal)type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).GetValue(null);
            if (dal == null)
                throw new DalConfigException("Can't Get Dal Instance");
            return dal;
        }
    }
}

