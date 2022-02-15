using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class DroneInCharging
    {
        /// <summary>
        /// Drone in charging key
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Drone in charging charging mode
        /// </summary>
        public double ChargingMode { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}


