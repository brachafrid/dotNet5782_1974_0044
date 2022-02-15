using System.Collections.Generic;




namespace BO
{
    public class Station
    {
        /// <summary>
        /// Station key
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Station name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Station location
        /// </summary>
        public Location Location { get; set; }
        /// <summary>
        /// Number available charging ports of Station 
        /// </summary>
        public int AvailableChargingPorts { get; set; }
        /// <summary>
        /// IEnumerable of drone in chargings in station
        /// </summary>
        public IEnumerable<DroneInCharging> DroneInChargings { get; set; }
        public override string ToString()
        {
            return this.ToStringProperties();
        }
    }
}

