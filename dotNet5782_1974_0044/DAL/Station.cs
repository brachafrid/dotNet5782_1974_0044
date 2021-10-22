using System;


namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            private double longitude;
            private double latitude;
            private string latitudeSexagesimal;
            private int chargeSlots;
            public int Id { get; set; }
            public string Name { get; set; }

            string xx = IntToString(value, new char[] { '0','1','2','3','4','5','6','7','8','9',
                     'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                     'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x'});
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
            public string LatitudeSexagesimal
            {
                get
                {
                    return latitudeSexagesimal;
                }
                set
                {
                    latitudeSexagesimal = IntToString((int)Latitude, new char[] { '0','1','2','3','4','5','6','7','8','9',
                     'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                     'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x'});
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

            public static string IntToString(int value, char[] baseChars)
            {
                string result = string.Empty;
                int targetBase = baseChars.Length;

                do
                {
                    result = baseChars[value % targetBase] + result;
                    value = value / targetBase;
                }
                while (value > 0);

                return result;
            }
            public override string ToString()
            {
                return $"Station ID:{Id} Name:{Name} Latitude{LatitudeSexagesimal}";
            }
        }
    }
}
