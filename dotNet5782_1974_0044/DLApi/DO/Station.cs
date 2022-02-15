
using System;



namespace DO
{
    public struct Station : IIdentifyable, IActiveable
    {
        private double longitude;
        private double latitude;
        private int chargeSlots;
        /// <summary>
        /// The id of the station
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of station
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The longitude of the station
        /// </summary>
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
        /// <summary>
        /// The latitude of the station
        /// </summary>
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
        /// <summary>
        /// The number of charge slots of the station
        /// </summary>
        public int ChargeSlots
        {
            get { return chargeSlots; }
            set
            {

                if (value <= 0)
                    throw new ArgumentException("num of Charge Slots must be positive");
                chargeSlots = value;
            }
        }
        /// <summary>
        /// Is the station active
        /// </summary>
        public bool IsNotActive { get; set; }

        public override string ToString()
        {
            return $"Station ID:{Id} Name:{Name} Latitude:{latitude} Longitude:{longitude} ";
        }
    }
}

