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
        public static Dictionary<Type, Predicate<object>> functions = new();
        static Validation()
        {
            functions.Add(key: typeof(string), value: StringValid);
            functions.Add(key:typeof(int), value: IntValid);
            functions.Add(key: typeof(Location), value: LocationValid);            
            functions.Add(key:typeof(Enum), value: EnumValid);           //need to check 
        }
        public static bool LongitudeValid(object longitude) => (double)longitude>= 0 &&(double) longitude <= 90;
        public static bool LatitudeValid(object latitude) => (double)latitude > -90 && (double)latitude <= 90;
        public static bool IntValid(object num) =>(int) num > 0;
        public static bool StringValid(object str) =>(string) str != string.Empty;
        public static bool LocationValid(object location) => LongitudeValid((location as Location).Latitude) && LatitudeValid((location as Location).Longitude);
        public static bool PhoneValid(object phone) => Regex.Match((string)phone, @"^(\+[0-9]{9})$").Success || Regex.Match((string)phone, @"^[1-9]\d{10}$").Success || Regex.Match((string)phone, @"^[1-9]\d{9}$").Success;
        public static bool EnumValid(object parameter) => parameter != null;


    }
}
