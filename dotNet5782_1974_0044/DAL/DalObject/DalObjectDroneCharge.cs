using System;
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
        /// Count a number of charging slots occupied at a particular station 
        /// </summary>
        /// <param name="id">the id number of a station</param>
        /// <returns>The counter of empty slots</returns>
        public int CountFullChargeSlots(int id)
        {
            int count = 0;
            foreach (DroneCharge item in DataSorce.DroneCharges)
            {
                if (item.Stationld == id)
                    ++count;
            }
            return count;
        }
        /// <summary>
        /// Finds all the drones that are charged at a particular station
        /// </summary>
        /// <param name="id">The id of particular station</param>
        /// <returns>A list of DroneCarge</returns>
        public List<int> GetDronechargingInStation(int id)
        {
            List<int> list = new ();
            foreach (var item in DataSorce.DroneCharges)
            {
                if (item.Stationld == id)
                    list.Add(item.Droneld);
            }
            return list;
        }
        /// <summary>
        /// Gets parameters and create new DroneCharge 
        /// </summary>
        /// <param name="droneId">The drone to add</param>
        /// <param name="stationId">The station to add the drone</param>
        public void AddDRoneCharge(int droneId,int stationId)
        {
            DataSorce.DroneCharges.Add(new DroneCharge() { Droneld = droneId, Stationld = stationId });
        }
        /// <summary>
        /// Remove DroneCharge object from the list
        /// </summary>
        /// <param name="droneId">The drone to remove</param>
        public void RemoveDroneCharge(int droneId)
        {
            DataSorce.DroneCharges.RemoveAll(item=>item.Droneld==droneId);
        }
    }
}
