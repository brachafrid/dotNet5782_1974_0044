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
        const string DRONE_CHARGE_PATH = @"XmlDroneCharge.xml";
        public void RemoveDroneCharge(int droneId)
        {
            try
            {
                List<DroneCharge> droneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH);
                droneCharges.RemoveAll(drone => drone.Droneld == droneId);
                XMLTools.SaveListToXMLSerializer<DroneCharge>(droneCharges, DRONE_CHARGE_PATH);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }

        public DateTime GetTimeStartOfCharge(int droneId)
        {
            try { 
                return XMLTools.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH).FirstOrDefault(drone => drone.Droneld == droneId).StartCharging;
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }

        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation)
        {
            try { 
                return XMLTools.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH).FindAll(item => inTheStation(item.Stationld)).Select(item => item.Droneld);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }
        public void AddDRoneCharge(int droneId, int stationId)
        {
            try { 
                List<DroneCharge> droneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH);
                droneCharges.Add(new DroneCharge() { Droneld = droneId, Stationld = stationId, StartCharging = DateTime.Now });
                XMLTools.SaveListToXMLSerializer<DroneCharge>(droneCharges, DRONE_CHARGE_PATH);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }
        public int CountFullChargeSlots(int id)
        {
            try { 
                return XMLTools.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH).Count(Drone => Drone.Stationld == id);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }
    }
}
