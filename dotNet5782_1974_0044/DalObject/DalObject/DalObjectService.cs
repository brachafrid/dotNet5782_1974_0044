using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
  internal static class DalObjectService
    {
        internal static IEnumerable<T> GetEntities<T>()
        {
            Type t = typeof(T);
            return t switch
            {
                { } when typeof(Drone) == t => (IEnumerable<T>)DataSorce.Drones,
                { } when typeof(Parcel) == t => (IEnumerable<T>)DataSorce.Parcels,
                { } when typeof(Customer) == t => (IEnumerable<T>)DataSorce.Customers,
                { } when typeof(Station) == t => (IEnumerable<T>)DataSorce.Stations,
                { } when typeof(DroneCharge) == t => (IEnumerable<T>)DataSorce.DroneCharges,
                _ => null
            };
        }
        internal static void AddEntity<T>(T entity)
        {
            Type t = typeof(T);
            switch(entity.GetType().Name)
            {
                case { } when entity is Drone drone:
                    DataSorce.Drones.Add(drone);
                    break;
                case { } when entity is Customer customer:
                    DataSorce.Customers.Add(customer);
                    break;
                case { } when entity is Parcel parcel:
                    DataSorce.Parcels.Add(parcel);
                    break;
                case { } when entity is Station station:
                    DataSorce.Stations.Add(station);
                    break;
                case { } when entity is DroneCharge droneCharge:
                    DataSorce.DroneCharges.Add(droneCharge);
                    break;
                default:
                    break;
            }
        }
        internal static void RemoveEntity<T>(T entity)
        {
            Type t = typeof(T);
            switch (entity.GetType().Name)
            {
                case { } when entity is Drone drone:
                    DataSorce.Drones.Remove(drone);
                    break;
                case { } when entity is Customer customer:
                    DataSorce.Customers.Remove(customer);
                    break;
                case { } when entity is Parcel parcel:
                    DataSorce.Parcels.Remove(parcel);
                    break;
                case { } when entity is Station station:
                    DataSorce.Stations.Remove(station);
                    break;
                case { } when entity is DroneCharge droneCharge:
                    DataSorce.DroneCharges.Remove(droneCharge);
                    break;
                default:
                    break;
            }
        }
    }
}
