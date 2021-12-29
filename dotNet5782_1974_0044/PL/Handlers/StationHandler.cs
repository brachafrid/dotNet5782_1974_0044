using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.PO;
using BLApi;

namespace PL
{
   public class StationHandler
   {
        IBL ibal = BLFactory.GetBL();
        public void AddStation(StationAdd station)
        {
            ibal.AddStation(ConverBackStationAdd(station));
        }
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            ibal.UpdateStation(id, name, chargeSlots);
        }
        public Station GetStation(int id)
        {
            return ConverterStation(ibal.GetStation(id));
        }
        public IEnumerable<StationToList> GetStations()
        {
            return ibal.GetStations().Select(item => ConverterStationToList(item));
        }
        public IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots()
        {
            return ibal.GetStaionsWithEmptyChargeSlots((int chargeSlots)=>chargeSlots>0).Select(item => ConverterStationToList(item));
        }

        public BO.Station ConverterBackStation(Station station)
        {
            return new BO.Station()
            {
                Id = station.Id,
                Name = station.Name,
                Location = LocationHandler.ConvertBackLocation(station.Location),
                AvailableChargingPorts = station.EmptyChargeSlots,
                DroneInChargings = station.DroneInChargings.Select(item=> DroneChargingHandler.ConvertBackDroneCharging(item)).ToList()
            };
        }

        public Station ConverterStation( BO.Station station)
        {
            return new Station()
            {
                Id = station.Id,
                Name = station.Name,
                Location = LocationHandler.ConvertLocation(station.Location),
                EmptyChargeSlots = station.AvailableChargingPorts,
                DroneInChargings = station.DroneInChargings.Select(item => DroneChargingHandler.ConvertDroneCharging(item)).ToList()
            };
        }

        public StationToList ConverterStationToList(BO.StationToList station)
        {
            return new StationToList()
            {
                Id = station.Id,
                Name = station.Name,
                ChargeSlots = station.EmptyChargeSlots + station.FullChargeSlots
            };
        }
        public BO.Station ConverBackStationAdd(PO.StationAdd station)
        {
            return new()
            {
                Id = (int)station.Id,
                Name = station.Name,
                AvailableChargingPorts = (int)station.EmptyChargeSlots,
                Location = LocationHandler.ConvertBackLocation(station.Location)

            };
        }
    }
}
