using System;
using DO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLApi
{
    public interface IDalDroneCharge
    {
        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation);
        public IEnumerable<DroneCharge> GetDronescharging();
        public void AddDRoneCharge(int droneId, int stationId);
        public void RemoveDroneCharge(int droneId);
        public DateTime GetTimeStartOfCharge(int droneId);
        (double, double, double, double, double) GetElectricity();
    }
}
