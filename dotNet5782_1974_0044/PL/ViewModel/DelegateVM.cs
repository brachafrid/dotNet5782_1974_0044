using System;

namespace PL
{
    public static class DelegateVM
    {
        public static void NotifyDroneChanged(int droneId)
        {
            DroneChangedEvent?.Invoke(null, new EntityChangedEventArgs(droneId));
        }
        public static void NotifyStationChanged(int stationId)
        {
            DroneChangedEvent?.Invoke(null, new EntityChangedEventArgs(stationId));
        }
        public static void NotifyCustomerChanged(int customerId)
        {
            DroneChangedEvent?.Invoke(null, new EntityChangedEventArgs(customerId));
        }
        public static void NotifyParcelChanged(int parcelId)
        {
            DroneChangedEvent?.Invoke(null, new EntityChangedEventArgs(parcelId));
        }
        public static void Reset()
        {
            DroneChangedEvent = null;
            CustomerChangedEvent = null;
            StationChangedEvent = null;
            Parcel = null;
        }
        public static event EventHandler<EntityChangedEventArgs> DroneChangedEvent;
        public static event EventHandler<EntityChangedEventArgs> ParcelChangedEvent;
        public static event EventHandler<EntityChangedEventArgs> CustomerChangedEvent;
        public static event EventHandler<EntityChangedEventArgs> StationChangedEvent;

        public static Action Parcel { set; get; }
    }
    public class EntityChangedEventArgs : EventArgs
    {
        public EntityChangedEventArgs(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }
}
