using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL:IBL.IblStationcs
    {
      public  void UpdateStation(int id, string name, int chargeSlots)
        {
            throw new NotImplementedException();
        }
        public void AddStation(int id, string name, Location location, int chargeSlots)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<IDAL.DO.Station> GetStaionsWithEmptyChargeSlots()
        {
            throw new NotImplementedException();
        }
        public IDAL.DO.Station GetStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDAL.DO.Station> GetStations()
        {
            throw new NotImplementedException();
        }
    }
}
