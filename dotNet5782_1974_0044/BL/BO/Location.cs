using System;


namespace IBL
{
    namespace BO
    {
        public class Location 
        {
            private double longitude;
            private double latitude;
            public double Longitude
            {
                get => longitude;
                set
                {
                    if (value >= 0 && value <= 90)
                        longitude = value;
                    else
                        throw new ArgumentOutOfRangeException();
                }
            }
            public double Latitude
            {
                get => latitude;
                set
                {
                    if (value > -90 && value <= 90)
                        latitude = value;
                    else
                        throw new ArgumentOutOfRangeException();
                }
            }
            public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }
}
