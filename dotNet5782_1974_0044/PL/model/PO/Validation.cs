﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static class Validation
    {
        public static bool LongitudeValid(double longitude) => longitude >= 0 && longitude <= 90;
        public static bool LatitudeValid(double latitude) => latitude > -90 && latitude <= 90;
    }
}
