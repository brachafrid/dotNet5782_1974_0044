using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;
using BLApi;

namespace PL
{
   public class StationHundler
   {
        IBL ibal=BLFactory.GetBL();
        public void AddStation(Station station)
        {
            
        }
        public void UpdateStation(int id, string name, int chargeSlots);
        public Station GetStation(int id);
        public IEnumerable<StationToList> GetStations();
        public IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots(Predicate<int> exsitEmpty);

        public BO.Station ConvertStation(Station station)
        {
            return new BO.Station()
            {
                Id = station.Id,
                Name = station.Name,
                Location = HundlerEntity.ConvertLocation(station.Location),
                AvailableChargingPorts = station.EmptyChargeSlots
            };
        }
    }
}
