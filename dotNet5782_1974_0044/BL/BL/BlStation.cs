using IBL.BO;
using System;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL:IblStations
    {

        public void AddStation(Station stationBL)
        {
            if (ExistsIDTaxCheck(dal.GetStations(), stationBL.Id))
                throw new ThereIsAnObjectWithTheSameKeyInTheList();
            dal.AddStation(stationBL.Id, stationBL.Name, stationBL.Location.Longitude, stationBL.Location.Longitude, stationBL.AvailableChargingPorts);    
        }
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            if (!ExistsIDTaxCheck(dal.GetStations(), id))
                throw new KeyNotFoundException();
            IDAL.DO.Station satationDl = dal.GetStation(id);
            if (name.Equals(default) && chargeSlots==-1)
                throw new ArgumentNullException("For updating at least one parameter must be initialized ");
            if ( chargeSlots < dal.CountFullChargeSlots(satationDl.Id))
                throw new ArgumentOutOfRangeException("The number of charging slots is smaller than the number of slots used");
            dal.RemoveStation(satationDl);
            dal.AddStation(id, name.Equals(default) ? satationDl.Name : name, satationDl.Longitude, satationDl.Latitude, chargeSlots.Equals(default) ? satationDl.ChargeSlots : chargeSlots);
        }
        public IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots()
        {
            IEnumerable<IDAL.DO.Station> list = dal.GetSationsWithEmptyChargeSlots();
            List<StationToList> stations = new List<StationToList>();
            foreach (var item in list)
            {
                stations.Add(MapStationToList(item));
            }
            return stations;
        }
        public Station GetStation(int id)
        {
            if (!ExistsIDTaxCheck(dal.GetStations(), id))
                throw new KeyNotFoundException();
            return MapStation(dal.GetStation(id));
        }

        public IEnumerable<StationToList> GetStations()
        {
            IEnumerable<IDAL.DO.Station> list = dal.GetStations();
            List<StationToList> stations = new List<StationToList>();
            foreach (var item in list)
            {
                stations.Add(MapStationToList(item));
            }
            return stations;
        }
        private BO.Station MapStation(IDAL.DO.Station station)
        {
            return new Station() {
                Id = station.Id,
                Name = station.Name,
                Location = new Location() { Latitude=station.Latitude,Longitude=station.Longitude },
                AvailableChargingPorts=station.ChargeSlots-dal.CountFullChargeSlots(station.Id),
                DroneInChargings=CreatListDroneInCharging(station.Id)
            };
        }
        private BO.StationToList MapStationToList(IDAL.DO.Station station)
        {
            return new StationToList()
            {
                Id = station.Id,
                Name = station.Name,
                EmptyChargeSlots= station.ChargeSlots - dal.CountFullChargeSlots(station.Id),
                FullChargeSlots= dal.CountFullChargeSlots(station.Id)
            };
        }


    }
}
