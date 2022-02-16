using System;

namespace PL
{
    public static class RefreshEvents
    {
        /// <summary>
        /// Notify drone changed
        /// </summary>
        /// <param name="droneId">id of drone</param>
        public static void NotifyDroneChanged(int? droneId=null)
        {
            DroneChangedEvent?.Invoke(null, new EntityChangedEventArgs(droneId));
        }

        /// <summary>
        /// Notify station changed
        /// </summary>
        /// <param name="stationId">id of station</param>
        public static void NotifyStationChanged(int? stationId=null)
        {
            StationChangedEvent?.Invoke(null, new EntityChangedEventArgs(stationId));
        }

        /// <summary>
        /// Notify customer changed
        /// </summary>
        /// <param name="customerId">id of customer</param>
        public static void NotifyCustomerChanged(int? customerId=null)
        {
            CustomerChangedEvent?.Invoke(null, new EntityChangedEventArgs(customerId));
        }

        /// <summary>
        /// Notify parcel changed
        /// </summary>
        /// <param name="parcelId">id of parcel</param>
        public static void NotifyParcelChanged(int? parcelId=null)
        {
            ParcelChangedEvent?.Invoke(null, new EntityChangedEventArgs(parcelId));
        }

        /// <summary>
        /// Reset
        /// </summary>
        public static void Reset()
        {
            DroneChangedEvent = null;
            CustomerChangedEvent = null;
            StationChangedEvent = null;
            ParcelChangedEvent = null;
        }
        public static event EventHandler<EntityChangedEventArgs> DroneChangedEvent;
        public static event EventHandler<EntityChangedEventArgs> ParcelChangedEvent;
        public static event EventHandler<EntityChangedEventArgs> CustomerChangedEvent;
        public static event EventHandler<EntityChangedEventArgs> StationChangedEvent;

    }

    /// <summary>
    /// class - entity changed event arguments
    /// </summary>
    public class EntityChangedEventArgs : EventArgs
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">id</param>
        public EntityChangedEventArgs(int? id)
        {
            Id = id;
        }
        public int? Id { get; private set; }
    }
}
