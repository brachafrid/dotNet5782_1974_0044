using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace IBL
{
    public interface IblStationcs
    {
        public void AddStation(int id, string name, Location location, int chargeSlots);
        public void UpdateStation(int id, string name, int chargeSlots);
        public IDAL.DO.Station GetStation(int id);
        public IEnumerable<IDAL.DO.Station> GetStations();
        public IEnumerable<IDAL.DO.Station> GetStaionsWithEmptyChargeSlots();
    }
}
