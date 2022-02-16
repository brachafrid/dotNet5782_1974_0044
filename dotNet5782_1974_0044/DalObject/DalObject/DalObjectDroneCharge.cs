using DLApi;
using System.Collections.Generic;
using System;
using System.Linq;
using DO;
using System.Runtime.CompilerServices;
using System.Collections;

namespace Dal
{
    public partial class DalObject:IDalDroneCharge
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int CountFullChargeSlots(int id)=> DalObjectService.GetEntities<DroneCharge>().Count(Drone => Drone.Stationld == id);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation)=> DalObjectService.GetEntities<DroneCharge>().Where(item => inTheStation(item.Stationld)).Select(item => item.Droneld);


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDronescharging() => DalObjectService.GetEntities<DroneCharge>();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(int droneId, int stationId)
        {
            DalObjectService.AddEntity(new DroneCharge() { Droneld = droneId, Stationld = stationId, StartCharging = DateTime.Now });
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveDroneCharge(int droneId)
        {
            DroneCharge droneCharge = DalObjectService.GetEntities<DroneCharge>().FirstOrDefault(droneCharge => droneCharge.Droneld == droneId);
            if (droneCharge.Equals(default(DroneCharge)))
                throw new TheDroneIsNotInChargingException("The drone is not in charging",droneId);
            DalObjectService.RemoveEntity(droneCharge);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DateTime GetTimeStartOfCharge(int droneId)
        {
            DroneCharge droneCharge = DalObjectService.GetEntities<DroneCharge>().FirstOrDefault(drone => drone.Droneld == droneId);
            if (droneCharge.Equals(default(DroneCharge)))
                throw new TheDroneIsNotInChargingException("The drone is not in charging", droneId);
            return droneCharge.StartCharging;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public (double, double, double, double, double) GetElectricity()
        {
            return (DataSorce.Config.Available, DataSorce.Config.LightWeightCarrier, DataSorce.Config.MediumWeightBearing, DataSorce.Config.CarriesHeavyWeight, DataSorce.Config.DroneLoadingRate);

        }
    }

}
