﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class DroneInCharging
    {
        public int Id { get; init; }
        public double ChargingMode { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


