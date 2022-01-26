using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{
    partial class DalXml
    {
        const string DRONE_CHARGE_PATH = @"XMLDroneCharge.xml";
        public void RemoveDroneCharge(int droneId)
        {
            List<DroneCharge> droneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH);
            droneCharges.RemoveAll(drone => drone.Droneld == droneId);
            XMLTools.SaveListToXMLSerializer<DroneCharge>(droneCharges, DRONE_CHARGE_PATH);
        }

        public DateTime GetTimeStartOfCharge(int droneId)
        {
            return XMLTools.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH).FirstOrDefault(drone => drone.Droneld == droneId).StartCharging;
        }

        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation)
        {
            return XMLTools.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH).FindAll(item => inTheStation(item.Stationld)).Select(item => item.Droneld);
        }
        public void AddDRoneCharge(int droneId, int stationId)
        {
            List<DroneCharge> droneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH);
            droneCharges.Add(new DroneCharge() { Droneld = droneId, Stationld = stationId, StartCharging = DateTime.Now });
            XMLTools.SaveListToXMLSerializer<DroneCharge>(droneCharges, DRONE_CHARGE_PATH);
        }
        public int CountFullChargeSlots(int id)
        {
            return XMLTools.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH).Count(Drone => Drone.Stationld == id);
        }
    }
}
