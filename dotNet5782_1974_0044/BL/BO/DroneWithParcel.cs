﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
       public class DroneWithParcel
        {
            public int Id { get; init; }
            public double ChargingMode { get; set; }
            public Location CurrentLocation { get; set; }
            public override string ToString()
            {
                return this.ToStringProperties();
            }

        }
    }
    
}
