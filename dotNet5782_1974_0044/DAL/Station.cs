﻿using System;


namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            private double longitude;
            private double latitude;
            internal string latitudeSexagesimal;
            internal string longitudeSexagesimal;
            private int chargeSlots;
            public int Id { get; set; }
            public string Name { get; set; }

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
                    if (value < 0 || value > 180)
                        throw new ArgumentException("invalid longitude");
                    latitude = value;
                }
            }

            public int ChargeSlots
            {
                get { return chargeSlots; }
                set
                {
           
                    if (value <= 0)
                        throw new ArgumentException("sum of Charge Slots must be positive");
                    chargeSlots = value;
                }
            }

            public override string ToString()
            {
                return $"Station ID:{Id} Name:{Name} Latitude:{latitude} Longitude:{longitude} ";
            }
        }
    }
}
