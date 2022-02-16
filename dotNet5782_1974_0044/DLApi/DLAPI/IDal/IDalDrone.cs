using DO;
using System.Collections.Generic;

namespace DLApi
{
    public interface IDalDrone
    {

        /// <summary>
        /// Add new drone
        /// </summary>
        /// <param name="id">new drone's id</param>
        /// <param name="model">new drone's model</param>
        /// <param name="MaxWeight">new drone's max weight</param>
        public void AddDrone(int id, string model, WeightCategories MaxWeight);

        /// <summary>
        /// Returns drone according to id
        /// </summary>
        /// <param name="id">drone's id</param>
        /// <returns>drone</returns>
        public Drone GetDrone(int id);

        /// <summary>
        /// Returns the list of the drones
        /// </summary>
        /// <returns>list of drones</returns>
        public IEnumerable<Drone> GetDrones();

        /// <summary>
        /// Update model name of drone
        /// </summary>
        /// <param name="drone">drone</param>
        /// <param name="model">model name</param>
        public void UpdateDrone(Drone drone,string model);

        /// <summary>
        /// Deletes a certain drone according to ID no.
        /// </summary>
        /// <param name="id">drone's id</param>
        public void DeleteDrone(int id);
    }
}
