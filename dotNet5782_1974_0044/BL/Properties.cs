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
            string s= "";
            foreach (PropertyInfo item in t.GetProperties())
            {
                if (item.GetValue(obj) != null && !(item.PropertyType.IsGenericType && (item.PropertyType.GetGenericTypeDefinition() == typeof(List<>))))
                {
                    s += $"{item.Name} = {item.GetValue(obj)}" +
                                        Environment.NewLine;
                }

            }
            return s;
        }

    }
}

