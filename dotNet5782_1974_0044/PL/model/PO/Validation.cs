using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    }
}
