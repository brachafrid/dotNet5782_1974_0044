using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        ///  Gets parameters and create new drone 
        /// </summary>
        /// <param name="model"> Grone's model</param>
        /// <param name="MaxWeight"> The max weight that the drone can swipe (light- 0,medium - 1,heavy - 2)</param>
        public void addDrone(int id, string model, WeightCategories MaxWeight)
        {
            uniqueIDTaxCheck<Drone>(DataSorce.Drones, id);
            Drone newDrone = new Drone();
            newDrone.Id = id;
            newDrone.Model = model;
            newDrone.MaxWeight = MaxWeight;
            DataSorce.Drones.Add(newDrone);
        }
        /// <summary>
        /// Sends drone to charge.
        /// Find available charge solt
        /// Create new droneCharge object, initializ it and add to DroneCharges list.
        /// Update the drone's status.
        /// </summary>
        /// <param name="droneId"> id of drone</param>
        public void SendDroneCharg(int droneId)
        {
            DroneCharge tmpDroneCharge = new DroneCharge();
            tmpDroneCharge.Droneld = droneId;
            tmpDroneCharge.Stationld = getAvailbleStations().First().Id;
            DataSorce.DroneCharges.Add(tmpDroneCharge);
            Drone tmpDrone = DataSorce.Drones.First(item => item.Id == droneId);
            DataSorce.Drones.Remove(tmpDrone);
            DataSorce.Drones.Add(tmpDrone);
        }
        /// <summary>
        /// Releases the drone from charging.
        /// Update battary and status.
        /// Remove the droneCharge object from DroneCharges list
        /// </summary>
        /// <param name="droneId"> id of drone</param>
        public void ReleasDroneCharg(int droneId)
        {
            DataSorce.DroneCharges.Remove(DataSorce.DroneCharges.First(item => item.Droneld == droneId));
            Drone tmpDrone = DataSorce.Drones.FirstOrDefault(item => item.Id == droneId);
            if (!(tmpDrone.Equals(default(Drone))))
            {
                DataSorce.Drones.Remove(tmpDrone);
                DataSorce.Drones.Add(tmpDrone);
            }
        }

    }
}
