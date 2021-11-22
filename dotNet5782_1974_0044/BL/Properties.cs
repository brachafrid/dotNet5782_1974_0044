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
            string s = "";
            foreach (var item in t.GetProperties())
            {
                if (item.GetValue(obj) != null && !(item.PropertyType.IsGenericType && (item.PropertyType.GetGenericTypeDefinition() == typeof(List<>))))
                {
                    s += FormatString(item.Name) + $": {item.GetValue(obj)}" +
                                        Environment.NewLine;
                }
            }
            return s;
        }

        static string FormatString(string str)
        {
            string s = str[0].ToString();
            
                for (int i = 1; i < str.Length; i++)
                {
                    if (char.IsUpper(str[i]))
                        s +=" "+ str[i].ToString().ToLower();
                    else
                        s += str[i].ToString();
                }
            return s;


        }
    }
}



