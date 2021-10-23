using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            internal string latitudeSexagesimal;
            internal string longitudeSexagesimal;
            public override string ToString()
            {
                return $"Cusomer ID:{Id} Name:{Name} Latitude:{Latitude} Longitude:{Longitude} Latitude in sexagesimal:{latitudeSexagesimal} Longitude in sexagesimal:{longitudeSexagesimal}";
            }

        }
    }
}
