using System;


namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            private double longitude;
            private double latitude;
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude
            {
                get
                {
                    return longitude;
                }
                set
                {
                    if (value < 0 || value > 90)
                        throw new ArgumentException("invalid longitude");
                    longitude = value;
                }
            }
            public double Latitude
            {
                get
                {
                    return latitude;
                }
                set
                {
                    if (value < -90 || value > 90)
                        throw new ArgumentException("invalid longitude");
                    latitude = value;
                }
            }

            public override string ToString()
            {
                return $"Cusomer ID:{Id} Name:{Name} Latitude:{Latitude} Longitude:{Longitude}";
            }

        }
    }
}
