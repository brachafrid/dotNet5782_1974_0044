
using System.Collections.Generic;
using System.Linq;
using System;
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
        public void AddDrone(int id, string model, WeightCategories MaxWeight)
        {
            if (ExistsIDTaxCheck(DataSorce.Drones, id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException();
            Drone newDrone = new ()
            {
                Id = id,
                Model = model,
                MaxWeight = MaxWeight
            };
            DataSorce.Drones.Add(newDrone);
        }


        //----------------------------------Display--------------------------------
        /// <summary>
        /// Find a drone that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested drone</param>
        /// <returns>A drone for display</returns>
        public Drone GetDrone(int id)
        {
            Drone drone=DataSorce.Drones.FirstOrDefault(item => item.Id == id);
            if (drone.Equals(default(Drone)))
                throw new KeyNotFoundException("There is not suitable drone in the data");
            return drone;
        }

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
