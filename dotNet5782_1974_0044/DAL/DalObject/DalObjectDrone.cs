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
        //-------------------------Adding---------------------------------
        /// <summary>
        ///  Gets parameters and create new drone 
        /// </summary>
        /// <param name="model"> Grone's model</param>
        /// <param name="MaxWeight"> The max weight that the drone can swipe (light- 0,medium - 1,heavy - 2)</param>
        public void addDrone(int id, string model, WeightCategories MaxWeight)
        {
            uniqueIDTaxCheck<Drone>(DataSorce.Drones, id);
            Drone newDrone = new ()
            {
                Id = id,
                Model = model,
                MaxWeight = MaxWeight
            };
            DataSorce.Drones.Add(newDrone);
        }

        //--------------------------------Update--------------------------------
        /// <summary>
        /// find aviable drone to assign it to parcell
        /// </summary>
        /// <param name="tmpDrone"></param>
        /// <param name="weight">the weight of the parcell</param>
        public void findSuitableDrone(out Drone tmpDrone, IDAL.DO.WeightCategories weight)
        {
            tmpDrone = DataSorce.Drones.FirstOrDefault(item => (weight <= item.MaxWeight));
            if (!(tmpDrone.Equals(default)))
            {
                DataSorce.Drones.Remove(tmpDrone);
                DataSorce.Drones.Add(tmpDrone);
            }
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
            DroneCharge tmpDroneCharge = new DroneCharge
            {
                Droneld = droneId,
                Stationld = getAvailbleStations().First().Id
            };
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double[] GetElectricityUse()
        {
            return new double[] { DataSorce.Config.Available, DataSorce.Config.LightWeightCarrier, DataSorce.Config.MediumWeightBearing, DataSorce.Config.CarriesHeavyWeight, DataSorce.Config.DroneLoadingRate };
        }

        //----------------------------------Display--------------------------------
        /// <summary>
        /// Find a drone that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested drone</param>
        /// <returns>A drone for display</returns>
        public Drone GetDrone(int id)=>DataSorce.Drones.First(item => item.Id == id);

        /// <summary>
        /// Prepares the list of Drones for display
        /// </summary>
        /// <returns>A list of drones</returns>
        public IEnumerable<Drone> GetDrones() => DataSorce.Drones;

        //-------------------------------------------------Removing-------------------------------------------------------------
        /// <summary>
        /// Removing a Drone from the list
        /// </summary>
        /// <param name="station"></param>
        public void RemoveDrone(Drone drone)
        {
            DataSorce.Drones.Remove(drone);
        }


    }

}
