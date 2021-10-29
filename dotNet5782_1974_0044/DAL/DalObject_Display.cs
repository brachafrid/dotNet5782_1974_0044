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
      /// Find a satation that has tha same id number as the parameter
      /// </summary>
      /// <param name="id">The id number of the requested station/param>
      /// <returns>A station for display</returns>
        public Station GetStation(int id)
        {
            return DataSorce.Sations.First(item => item.Id == id);
        }
        /// <summary>
        /// Find a drone that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested drone</param>
        /// <returns>A drone for display</returns>
        public Drone GetDrone(int id)
        {
            return DataSorce.Drones.First(item => item.Id == id);
        }
        /// <summary>
        /// Find a customer that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested customer</param>
        /// <returns>A customer for display</returns>
        public Customer GetCustomer(int id)
        {
            return DataSorce.customers.First(item => item.Id == id);
        }
        /// <summary>
        /// Find a parcel that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested parcel</param>
        /// <returns>A parcel for display</returns>
        public Parcel GetParcel(int id)
        {
            return DataSorce.Parcels.First(item => item.Id == id);
        }
        /// <summary>
        ///  Prepares the list of Sations for display
        /// </summary>
        /// <returns>A array of Sations</returns>
        public Station[] GetSations()
        {
            return DataSorce.Sations.ToArray();
        }
        /// <summary>
        /// Prepares the list of Drones for display
        /// </summary>
        /// <returns>A array of Drones</returns>
        public Drone[] GetDrones()
        {
            return DataSorce.Drones.ToArray();
        }
        /// <summary>
        /// Prepares the list of Parcels for display
        /// </summary>
        /// <returns>A array of parcel</returns>
        public Parcel[] GetParcels()
        {
            return DataSorce.Parcels.ToArray();
        }
        /// <summary>
        /// Prepares the list of customer for display
        /// </summary>
        /// <returns>A array of customer</returns>
        public Customer[] GetCustomers()
        {
            return DataSorce.customers.ToArray();
        }
        /// <summary>
        /// Find the Parcels that not assign to drone
        /// </summary>
        /// <returns>A array of the requested Parcels</returns>
        public Parcel[] GetParcelsNotAssignedToDrone()
        {
            return DataSorce.Parcels.FindAll(item => item.DorneId == 0).ToArray();
        }
        /// <summary>
        /// Find the satation that have empty charging slots
        /// </summary>
        /// <returns>A array of the requested station</returns>
        public IEnumerable<Station> GetSationsWithEmptyChargeSlots()
        {
            return getAvailbleSations().ToList();
        }
        /// <summary>
        /// Count a number of charging slots occupied at a particular station 
        /// </summary>
        /// <param name="id">the id number of a station</param>
        /// <returns>The counter of empty slots</returns>
        private int countFullChargeSlots(int id)
        {
            int count = 0;
            foreach (DroneCharge item in DataSorce.DroneCharges)
            {
                if (item.Droneld == id)
                    ++count;
            }
            return count;
        }
        /// <summary>
        /// Checks which base Sations are available for charging
        /// </summary>
        /// <returns>A list of avaiable satations</returns>
        private List<Station> getAvailbleSations()
        {
            return DataSorce.Sations.FindAll(item => item.ChargeSlots > countFullChargeSlots(item.Id));
        }
    }
}
