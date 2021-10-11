﻿using System;

namespace IDAL
{
    namespace DO
    {
       public struct Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }
    }
}