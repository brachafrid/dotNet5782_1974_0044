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
        public static bool LongitudeValid(double longitude) => longitude >= 0 && longitude <= 90;
        public static bool LatitudeValid(double latitude) => latitude > -90 && latitude <= 90;
        public static bool IdValid(int id) => id > 0;
        public static bool StringValid(string str) => str != string.Empty;
        public static bool LocationValid(Location location) => LongitudeValid(location.Latitude) && LatitudeValid(location.Longitude);
        public static bool PhoneValid(string phone)=>Regex.Match(phone, @"^(\+[0-9]{9})$").Success || Regex.Match(phone, @"^[1-9]\d{10}$").Success|| Regex.Match(phone, @"^[1-9]\d{9}$").Success;
        

    }
}
