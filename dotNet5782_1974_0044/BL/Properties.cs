using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace IBL
{
    static class Properties
    {
        public static string ToStringProperties<T>(this T obj)
        {
            Type t = obj.GetType();
            string s = $"{t.Name}:" + Environment.NewLine;
            foreach (PropertyInfo item in t.GetProperties())
            {
                s += $"{item.Name} = {item.GetValue(obj)}" +
                    Environment.NewLine;
            }
            return s;
        }

    }
}

