using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL:IblStationcs
    {

        public void AddStation(int id, string name, Location location, int chargeSlots)
        {
            if (ExistsIDTaxCheck(dal.GetStations(), id))
                throw new AnElementWithTheSameKeyAlreadyExistsInTheListException();
            dal.addStation(id,name, location.Longitude, location.Longitude, chargeSlots);
            
        }
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            if (!ExistsIDTaxCheck(dal.GetStations(), id))
                throw new KeyNotFoundException();
            

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
