
using System;



namespace DO
{
    public struct Customer : IIdentifyable, IActiveable
    {
        private double longitude;
        private double latitude;

        /// <summary>
        /// The customer Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///  The customer name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The customer phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The customer longitude
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
        /// The customer latitude
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
        /// Is the customer active
        /// </summary>
        public bool IsNotActive { get; set; }

        public override string ToString()
        {
            return $"Cusomer ID:{Id} Name:{Name} Latitude:{Latitude} Longitude:{Longitude}";
        }

    }
}

