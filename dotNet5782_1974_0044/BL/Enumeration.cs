﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public enum WeightCategories { LIGHT, MEDIUM, HEAVY }
        public enum Priorities { REGULAR, FAST, EMERGENCY }
        public enum DroneStatuses { AVAILABLE, MAINTENANCE, DELIVERY }
        public enum PackageModes { DEFINED,ASSOCIATED,COLLECTED,PROVIDED}
    }
}
