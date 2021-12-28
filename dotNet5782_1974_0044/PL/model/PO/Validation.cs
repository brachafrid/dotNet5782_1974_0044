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
        // private static bool LongitudeValid(object longitude) => (double)longitude>= 0 &&(double) longitude <= 90;
        // private static bool LatitudeValid(object latitude) => (double)latitude > -90 && (double)latitude <= 90;
        //public static bool Valid(this int num) => num > 0;
        //public static bool Valid(this string str) => str != string.Empty;
        //public static bool Valid(this Location location) => LongitudeValid(location.Latitude) && LatitudeValid(location.Longitude);
        //public static bool PhoneValid(this string phone) => Regex.Match(phone, @"^(\+[0-9]{9})$").Success || Regex.Match(phone, @"^[1-9]\d{10}$").Success || Regex.Match(phone, @"^[1-9]\d{9}$").Success;
        //public static bool Valid(Enum parameter) => parameter != null;
        public static Dictionary<string, Predicate<object>> functions = new();
        static Validation()
        {
            //functions.Add(key: typeof(string), value: StringValid);
            //functions.Add(key: typeof(int?), value: IntValid);
            //functions.Add(key: typeof(Location), value: LocationValid);
            //functions.Add(key: typeof(WeightCategories), value: EnumValid);
            //functions.Add(key: typeof(PackageModes), value: EnumValid);
            //functions.Add(key: typeof(DroneState), value: EnumValid);
            //functions.Add(key: typeof(int), value: IntValid);
            functions.Add(key: "Model", value: StringValid);
            functions.Add(key: "Id", value: IntValid);
            functions.Add(key: "Location", value: LocationValid);
            functions.Add(key: "Weight", value: EnumValid);
            functions.Add(key: "DroneState", value: EnumValid);
            functions.Add(key: "StationId", value: IntValid);
            functions.Add(key: "EmptyChargeSlots", value: IntValid);
            functions.Add(key: "Name", value: StringValid);
            functions.Add(key: "Longitude", value: LongitudeValid);
            functions.Add(key: "Latitude", value: LatitudeValid);
            functions.Add(key: "Phone", value: PhoneValid);


        }
        public static bool LongitudeValid(object longitude) =>longitude != null&& (double)longitude >= 0 && (double)longitude <= 90;
        public static bool LatitudeValid(object latitude) =>latitude != null&& (double)latitude > -90 && (double)latitude <= 90;
        public static bool IntValid(object num) =>  num != null && (int)num > 0;
        public static bool StringValid(object str) => (string)str != string.Empty && (string)str != default(string);
        public static bool LocationValid(object location) =>location != null &&( LongitudeValid((location as Location).Longitude) && LatitudeValid((location as Location).Latitude));
        public static bool PhoneValid(object phone) =>phone != null&&( Regex.Match((string)phone, @"^(\+[0-9]{9})$").Success || Regex.Match((string)phone, @"^0[0-9]\d{7}$").Success || Regex.Match((string)phone, @"^05[0-9]\d{7}$").Success);
        public static bool EnumValid(object parameter) => parameter != null;


    }
}
