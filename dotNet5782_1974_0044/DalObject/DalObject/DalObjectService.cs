using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
  public partial class DalObject
    {
        private IEnumerable<T> getEntities<T>()
        {
            Type t = typeof(T);
            return t.Name switch
            {
                { } when typeof(Drone).Name == t.Name => (IEnumerable<T>)DataSorce.Drones.Where(c => !c.IsActive),
                { } when typeof(Parcel).Name == t.Name => (IEnumerable<T>)DataSorce.Parcels.Where(c => !c.IsActive),
                { } when typeof(Customer).Name == t.Name => (IEnumerable<T>)DataSorce.Customers.Where(c =>! c.IsActive),
                { } when typeof(Station).Name == t.Name => (IEnumerable<T>)DataSorce.Stations.Where(c => !c.IsActive),
                { } when typeof(DroneCharge).Name == t.Name => (IEnumerable<T>)DataSorce.DroneCharges

            };
        }
        private void AddEntity<T>(T entity)
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
            }
        }
        private void RemoveEntity<T>(T entity)
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
            }
        }
    }
}
