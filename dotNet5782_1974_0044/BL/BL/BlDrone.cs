using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IblDrone
    {
        public void AddDrone(int id, string model, BO.WeightCategories MaximumWeight, int stationId)
        {
            throw new NotImplementedException();
        }
        public IDAL.DO.Drone GetDrone(int id)
        {
            throw new NotImplementedException();
        }
        public void ParcelCollectionByDrone(int DroneId)
        {
            throw new NotImplementedException();
        }
        public void ReleaseDroneFromCharging(int id, float timeOfCharg)
        {
            throw new NotImplementedException();
        }

        public void SendDroneForCharg(int id)
        {
            throw new NotImplementedException();
        }
        public void UpdateDrone(int id, string model)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<IDAL.DO.Drone> GetDrones()
        {
            throw new NotImplementedException();
        }

    }
}
