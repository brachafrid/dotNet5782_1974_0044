using DLApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Dal
{
    partial class DalXml : IDalDroneCharge
    {
        const string DRONE_CHARGE_PATH = @"XmlDroneCharge.xml";

        /// <summary>
        /// Remove drone charge
        /// </summary>
        /// <param name="droneId">drone's id</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveDroneCharge(int droneId)
        {
            try
            {
                List<DroneCharge> droneCharges = DalXmlService.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH);
                droneCharges.RemoveAll(drone => drone.Droneld == droneId);
                DalXmlService.SaveListToXMLSerializer(droneCharges, DRONE_CHARGE_PATH);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Get start time of charging
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <returns>start time of charging</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DateTime GetTimeStartOfCharge(int droneId)
        {
            try
            {
                DroneCharge droneCharge = DalXmlService.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH).FirstOrDefault(drone => drone.Droneld == droneId);
                if (droneCharge.Equals(default(DroneCharge)))
                    throw new KeyNotFoundException($"The drone id {droneId} not in charging");
                return droneCharge.StartCharging;
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Get drone charging in station
        /// </summary>
        /// <param name="inTheStation">Predicate type of int inTheStation</param>
        /// <returns>drone charging in station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<int> GetDronechargingInStation(Predicate<int> inTheStation)
        {
            try
            {
                return DalXmlService.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH).FindAll(item => inTheStation(item.Stationld)).Select(item => item.Droneld);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Get drones charging
        /// </summary>
        /// <returns>drones charging</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDronescharging()
        {
            try
            {
                return DalXmlService.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Add drone charge
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <param name="stationId">station's id</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(int droneId, int stationId)
        {
            try
            {
                List<DroneCharge> droneCharges = DalXmlService.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH);
                droneCharges.Add(new DroneCharge() { Droneld = droneId, Stationld = stationId, StartCharging = DateTime.Now });
                DalXmlService.SaveListToXMLSerializer(droneCharges, DRONE_CHARGE_PATH);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public int CountFullChargeSlots(int id)
        {
            try
            {
                return DalXmlService.LoadListFromXMLSerializer<DroneCharge>(DRONE_CHARGE_PATH).Count(Drone => Drone.Stationld == id);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public (double, double, double, double, double) GetElectricity()
        {
            try
            {
                XElement config = DalXmlService.LoadXElementToXML(CONFIG);
                var electricity = config.Elements().Select(elem => double.Parse(elem.Value));
                return (electricity.ElementAt(1), electricity.ElementAt(2), electricity.ElementAt(3), electricity.ElementAt(4), electricity.ElementAt(5));
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }

        }

    }

}