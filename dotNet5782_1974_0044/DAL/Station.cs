using System;

namespace IDAL
{
    namespace DO
    {
       public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public override string ToString()
            {
                return $"Station ID:{Id} Name:{Name} ";
            }
        }
    }
}
