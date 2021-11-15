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
        public int countFullChargeSlots(int id)
        {
            int count = 0;
            foreach (DroneCharge item in DataSorce.DroneCharges)
            {
                if (item.Droneld == id)
                    ++count;
            }
            return count;
        }
        public List<int> GetDronechargingInStation(int id)
        {
            List<int> list = new List<int>();
            foreach (var item in DataSorce.DroneCharges)
            {
                if (item.Stationld == id)
                    list.Add(item.Droneld);
            }
            return list;
        }
    }
}
