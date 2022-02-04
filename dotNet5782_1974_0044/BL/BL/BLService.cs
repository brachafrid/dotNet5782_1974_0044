using DLApi;
using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
  internal static class BLService
    {
       //static IDal dal { get; } = DLFactory.GetDL();
       // internal static IEnumerable<T> GetEntities<T>()
       // {
       //     Type t = typeof(T);
       //     return t.Name switch
       //     {
       //         { } when typeof(Drone).Name == t.Name => (IEnumerable<T>)dal.GetDrones(),
       //         { } when typeof(Parcel).Name == t.Name => (IEnumerable<T>)dal.GetParcels(),
       //         { } when typeof(Customer).Name == t.Name => (IEnumerable<T>)dal.GetCustomers(),
       //         { } when typeof(Station).Name == t.Name => (IEnumerable<T>)dal.GetStations(),
       //         { } when typeof(DroneCharge).Name == t.Name => (IEnumerable<T>),
       //         _ => null
       //     };
       // }
       // internal static void AddEntity<T>(T entity)
       // {
       //     Type t = typeof(T);
       //     switch(entity.GetType().Name)
       //     {
       //         case { } when entity is Drone drone:
       //             dal.AddDrone(drone);
       //             break;
       //         case { } when entity is Customer customer:
       //             DataSorce.Customers.Add(customer);
       //             break;
       //         case { } when entity is Parcel parcel:
       //             DataSorce.Parcels.Add(parcel);
       //             break;
       //         case { } when entity is Station station:
       //             DataSorce.Stations.Add(station);
       //             break;
       //         case { } when entity is DroneCharge droneCharge:
       //             DataSorce.DroneCharges.Add(droneCharge);
       //             break;
       //         default:
       //             break;
       //     }
       // }
       // internal static void RemoveEntity<T>(T entity)
       // {
       //     Type t = typeof(T);
       //     switch (entity.GetType().Name)
       //     {
       //         case { } when entity is Drone drone:
       //             DataSorce.Drones.Remove(drone);
       //             break;
       //         case { } when entity is Customer customer:
       //             DataSorce.Customers.Remove(customer);
       //             break;
       //         case { } when entity is Parcel parcel:
       //             DataSorce.Parcels.Remove(parcel);
       //             break;
       //         case { } when entity is Station station:
       //             DataSorce.Stations.Remove(station);
       //             break;
       //         case { } when entity is DroneCharge droneCharge:
       //             DataSorce.DroneCharges.Remove(droneCharge);
       //             break;
       //         default:
       //             break;
       //     }
       // }
    }
}
