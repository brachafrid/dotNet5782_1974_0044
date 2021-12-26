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
        private static bool LongitudeValid(object longitude) => (double)longitude>= 0 &&(double) longitude <= 90;
        private static bool LatitudeValid(object latitude) => (double)latitude > -90 && (double)latitude <= 90;
        public static bool Valid(this int num) => num > 0;
        public static bool Valid(this string str) => str != string.Empty;
        public static bool Valid(this Location location) => LongitudeValid(location.Latitude) && LatitudeValid(location.Longitude);
        public static bool PhoneValid(this string phone) => Regex.Match(phone, @"^(\+[0-9]{9})$").Success || Regex.Match(phone, @"^[1-9]\d{10}$").Success || Regex.Match(phone, @"^[1-9]\d{9}$").Success;
        public static bool Valid(Enum parameter) => parameter != null;


    }
}
