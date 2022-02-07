﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLApi;
using DO;

namespace Dal
{
    public sealed partial class DalXml:IDalDrone
    {
        const string DRONE_PATH = @"XmlDrone.xml";
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, WeightCategories MaxWeight)
        {
            try { 
                List<Drone> drones = DalXmlService.LoadListFromXMLSerializer<Drone>(DRONE_PATH);
                if (ExistsIDTaxCheck(drones, id))
                    throw new ThereIsAnObjectWithTheSameKeyInTheListException();
                Drone newDrone = new()
                {
                    Id = id,
                    Model = model,
                    MaxWeight = MaxWeight,
                    IsNotActive = false

                };
                drones.Add(newDrone);
                DalXmlService.SaveListToXMLSerializer<Drone>(drones, DRONE_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            try { 
                List<Drone> drones = DalXmlService.LoadListFromXMLSerializer<Drone>(DRONE_PATH);
                Drone drone = drones.FirstOrDefault(item => item.Id == id);
                drones.Remove(drone);
                drone.IsNotActive = true;
                drones.Add(drone);
                DalXmlService.SaveListToXMLSerializer<Drone>(drones, DRONE_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            try { 
                Drone drone = DalXmlService.LoadListFromXMLSerializer<Drone>(DRONE_PATH).FirstOrDefault(item => item.Id == id);
                if (drone.Equals(default(Drone)) || drone.IsNotActive)
                    throw new KeyNotFoundException("There is not suitable drone in the data");
                return drone;
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones()
        {
            try { 
                return DalXmlService.LoadListFromXMLSerializer<Drone>(DRONE_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveDrone(Drone drone)
        {
            try { 
                List<Drone> drones = DalXmlService.LoadListFromXMLSerializer<Drone>(DRONE_PATH);
                drones.Remove(drone);
                DalXmlService.SaveListToXMLSerializer<Drone>(drones, DRONE_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public (double, double, double, double, double) GetElectricity()
        {
            try
            {
                XElement config = DalXmlService.LoadConfigToXML(CONFIG);
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
