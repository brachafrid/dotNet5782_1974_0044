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
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public Station GetStation(int id)
        {
            return DataSorce.stations.First(item => item.Id == id);
        }
        public Drone GetDrone(int id)
        {
            return DataSorce.drones.First(item => item.Id == id);
        }
        public Customer GetCustomer(int id)
        {
            return DataSorce.customers.First(item => item.Id == id);
        }
        public Parcel GetParcel(int id)
        {
            return DataSorce.parcels.First(item => item.Id == id);
        }
        public Station[] GetStations()
        {
            return DataSorce.stations.ToArray();
        }
        public Drone[] GetDrones()
        {
            return DataSorce.drones.ToArray();
        }
        public Parcel[] GetParcels()
        {
            return DataSorce.parcels.ToArray();
        }
        public Customer[] GetCustomers()
        {
            return DataSorce.customers.ToArray();
        }
        public Parcel[] GetParcelsNotAssignedToDrone()
        {
            return DataSorce.parcels.Where(item => item.DorneId == 0).ToArray();
        }
        public Station[] GetStationsWithEmptyChargeSlots()
        {
            return getAvailbleStations().ToArray();
        }
        private int countFullChargeSlots(int id)
        {
            int count = 0;
            foreach (DroneCharge item in DataSorce.droneCharges)
            {
                if (item.Droneld == id)
                    ++count;
            }
            return count;

        }
        private List<Station> getAvailbleStations()
        {
            return DataSorce.stations.Where(item => item.ChargeSlots > countFullChargeSlots(item.Id)).ToList();
        }
    }
}
