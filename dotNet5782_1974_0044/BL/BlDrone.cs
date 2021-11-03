using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public partial class BL:IBL.IblDrone
   {
        public void AddDrone(int id, IBL.BO.WeightCategories MaximumWeight, int stationId)
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
        public void UpdateDrone(int id, string name)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<IDAL.DO.Drone> GetDrones()
        {
            throw new NotImplementedException();
        }
    }
}
