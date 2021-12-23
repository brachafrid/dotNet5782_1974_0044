using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PL.PO
{
    public static class Validation
    {
        static Dictionary<string, Predicate<object>> functions = new();
        static Validation()
        {
            functions.Add(key: "longitude", value: LongitudeValid);
            functions.Add(key: "latitude", value: LatitudeValid);
        }
        public static bool LongitudeValid(object longitude) => (double)longitude>= 0 &&(double) longitude <= 90;
        public static bool LatitudeValid(object latitude) => (double)latitude > -90 && (double)latitude <= 90;
        public static bool IntValid(object num) =>(int) num > 0;
        public static bool StringValid(string str) => str != string.Empty;
        public static bool LocationValid(Location location) => LongitudeValid(location.Latitude) && LatitudeValid(location.Longitude);
        public static bool PhoneValid(string phone)=>Regex.Match(phone, @"^(\+[0-9]{9})$").Success || Regex.Match(phone, @"^[1-9]\d{10}$").Success|| Regex.Match(phone, @"^[1-9]\d{9}$").Success;
        

    }
}
