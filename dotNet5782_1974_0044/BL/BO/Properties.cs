using System;
using System.Collections.Generic;


namespace BO
{
    static class Properties
    {
        /// <summary>
        /// Return the string of the properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">object</param>
        /// <returns>string of the properties</returns>
        public static string ToStringProperties<T>(this T obj)
        {
            Type t = obj.GetType();
            string s = "";
            foreach (var item in t.GetProperties())
            {
                if (item.GetValue(obj) != null && !(item.PropertyType.IsGenericType && (item.PropertyType.GetGenericTypeDefinition() == typeof(List<>))))
                {
                    s += FormatString(item.Name) + $": {item.GetValue(obj)}" +
                                        '\n';
                }
            }
            string str = "";
            for( int i =0; i<s.Length;++i)
            {
                if (s[i] != '\n')
                    str += s[i];
                else if (i + 1 < s.Length && s[i + 1] != '\n')
                    str += s[i];
            }
            str += '\n';
            return str;
        }

        /// <summary>
        /// Format string
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
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



