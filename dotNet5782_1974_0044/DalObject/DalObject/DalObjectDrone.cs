using DLApi;
using System.Collections.Generic;
using System.Linq;
using System;
using DO;
using System.Runtime.CompilerServices;
namespace Dal
{
    public partial class DalObject:IDalDrone
    {
        //-------------------------Adding---------------------------------
        /// <summary>
        ///  Gets parameters and create new drone 
        /// </summary>
        /// <param name="model"> Grone's model</param>
        /// <param name="MaxWeight"> The max weight that the drone can swipe (light- 0,medium - 1,heavy - 2)</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, WeightCategories MaxWeight)
        {
            if (ExistsIDTaxCheck(DataSorce.Drones, id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException();
            Drone newDrone = new()
            {
                Id = id,
                Model = model,
                MaxWeight = MaxWeight,
                IsNotActive = false

            };
            DalObjectService.AddEntity(newDrone);
        }


        //----------------------------------Display--------------------------------
        /// <summary>
        /// Find a drone that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested drone</param>
        /// <returns>A drone for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            Drone drone = DalObjectService.GetEntities<Drone>().FirstOrDefault(item => item.Id == id);
            if (drone.Equals(default(Drone)) )
                throw new KeyNotFoundException("There is not suitable drone in the data");
            return drone;
        }

        /// <summary>
        /// Prepares the list of Drones for display
        /// </summary>
        /// <returns>A list of drones</returns>
         [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones() => DalObjectService.GetEntities<Drone>();


        //-------------------------------------------------Removing-------------------------------------------------------------
        /// <summary>
        /// Removing a Drone from the list
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone drone,string model)
        {
            DalObjectService.RemoveEntity(drone);
            drone.Model = model;
            DalObjectService.AddEntity(drone);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            Drone drone = DalObjectService.GetEntities<Drone>().FirstOrDefault(item => item.Id == id);
            DalObjectService.RemoveEntity(drone);
            drone.IsNotActive = true;
            DalObjectService.AddEntity(drone);
        }
    }
}
