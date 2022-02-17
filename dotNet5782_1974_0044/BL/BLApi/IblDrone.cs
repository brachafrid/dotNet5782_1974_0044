
using System.Collections.Generic;
using System;




namespace BLApi
{
    public interface IBlDrone
    {
        /// <summary>
        /// Add a drone to the list of drones in data and also convert it to Drone To List and add it to BL list
        /// </summary>
        /// <param name="droneBl">The drone for Adding</param>
        ///<param name="stationId">The station to put the drone</param>
        public void AddDrone(BO.Drone drone, int stationId);

        /// <summary>
        /// Update a drone in the Stations list
        /// </summary>
        /// <param name="id">The drone to update</param>
        /// <param name="name">The new name</param>
        public void UpdateDrone(int id, string model);

        /// <summary>
        /// Send a drone for charging in the closet station with empty charge slots tha t the drone can arrive there
        /// </summary>
        /// <param name="id">The id of the drone</param>
        public void SendDroneForCharg(int id);

        /// <summary>
        /// Realse the drone from charging
        /// </summary>
        /// <param name="id">The drone to realsing</param>
        /// <param name="timeOfCharg">The time of charging</param>
        public void ReleaseDroneFromCharging(int id);

        /// <summary>
        /// Retrieves the requested drone from the data and converts it to BL drone
        /// </summary>
        /// <param name="id">The requested drone id</param>
        /// <returns>A Bl drone to print</returns>
        public BO.Drone GetDrone(int id);

        /// <summary>
        /// Recrieves the list of drones from BL
        /// </summary>
        /// <returns>A list of drones to print</returns>
        public IEnumerable<BO.DroneToList> GetAllDrones();

        /// <summary>
        /// Get active drones
        /// </summary>
        /// <returns>active drones</returns>
        public IEnumerable<BO.DroneToList> GetActiveDrones();

        /// <summary>
        /// Assign parcel to drone in according to weight and distance (call to help function)
        /// </summary>
        /// <param name="droneId">The drone to assign it a parcel</param>
        public void AssingParcelToDrone(int droneId);

        /// <summary>
        /// Collecting the parcel from the sender 
        /// </summary>
        /// <param name="droneId">The drone that collect</param>
        public void ParcelCollectionByDrone(int DroneId);

        /// <summary>
        /// Deliverd the parcel to the reciver 
        /// </summary>
        /// <param name="droneId">The drone that deliverd</param>
        public void DeliveryParcelByDrone(int droneId);

        /// <summary>
        /// Delete drone
        /// </summary>
        /// <param name="id">id of drone</param>
        public void DeleteDrone(int id);

        /// <summary>
        /// Check if drone is not active
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <returns>if drone is not active</returns>
        public bool IsNotActiveDrone(int id);

        /// <summary>
        /// Start drone simulator
        /// </summary>
        /// <param name="id">id  </param>
        /// <param name="update">Action update</param>
        /// <param name="IsCheckStop">Func Check Stop</param>
        public void StartDroneSimulator(int id, Action<int?, int?, int?, int?> update, Func<bool> IsCheckStop);
    }
}


