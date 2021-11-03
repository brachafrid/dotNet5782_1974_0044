using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;

namespace BL
{
   public class BL : IBL.IBL
    {
        void IBL.IBL.AddCustomer(int id, string name, string phone)
        {
            throw new NotImplementedException();
        }

        void IBL.IBL.AddDrone(int id, IBL.BO.WeightCategories MaximumWeight, int stationId)
        {
            throw new NotImplementedException();
        }

        void IBL.IBL.AddStation(int id, string name, Location location, int chargeSlots)
        {
            throw new NotImplementedException();
        }

        void IBL.IBL.DeliveryParcelByDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        IDAL.DO.Customer IBL.IBL.GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IDAL.DO.Customer> IBL.IBL.GetCustomers()
        {
            throw new NotImplementedException();
        }

        IDAL.DO.Drone IBL.IBL.GetDrone(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IDAL.DO.Drone> IBL.IBL.GetDrones()
        {
            throw new NotImplementedException();
        }

        Parcel IBL.IBL.GetParcel(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Parcel> IBL.IBL.GetParcels()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Parcel> IBL.IBL.GetParcelsNotAssignedToDrone()
        {
            throw new NotImplementedException();
        }

        IEnumerable<IDAL.DO.Station> IBL.IBL.GetSationsWithEmptyChargeSlots()
        {
            throw new NotImplementedException();
        }

        IDAL.DO.Station IBL.IBL.GetStation(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IDAL.DO.Station> IBL.IBL.GetStations()
        {
            throw new NotImplementedException();
        }

        void IBL.IBL.ParcelCollectionByDrone(int DroneId)
        {
            throw new NotImplementedException();
        }

        void IBL.IBL.ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, IBL.BO.WeightCategories Weight, IBL.BO.Priorities priority)
        {
            throw new NotImplementedException();
        }

        void IBL.IBL.ReleaseDroneFromCharging(int id, float timeOfCharg)
        {
            throw new NotImplementedException();
        }

        void IBL.IBL.SendDroneForCharg(int id)
        {
            throw new NotImplementedException();
        }

        void IBL.IBL.UpdateCusomer(int id, string name, string phone)
        {
            throw new NotImplementedException();
        }

        void IBL.IBL.UpdateDrone(int id, string name)
        {
            throw new NotImplementedException();
        }

        void IBL.IBL.UpdateStation(int id, string name, int chargeSlots)
        {
            throw new NotImplementedException();
        }
    }
}

