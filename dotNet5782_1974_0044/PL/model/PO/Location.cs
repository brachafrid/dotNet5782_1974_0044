using System;
using System.ComponentModel;

namespace PL.PO
    {
        public class Location : INotifyPropertyChanged
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
                        throw new ArgumentOutOfRangeException("invalid longitude");
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
                        throw new ArgumentOutOfRangeException("invalid latitude");
                }
            }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
            {
                return this.ToStringProperties();
            }
        }
    }
