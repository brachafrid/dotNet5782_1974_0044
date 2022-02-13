using System;
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

        /// <summary>
        /// Add new drone
        /// </summary>
        /// <param name="id">new drone's id</param>
        /// <param name="model">new drone's model</param>
        /// <param name="MaxWeight">new drone's max weight</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, WeightCategories MaxWeight)
        {
            try { 
                List<Drone> drones = DalXmlService.LoadListFromXMLSerializer<Drone>(DRONE_PATH);
                if (ExistsIDTaxCheck(drones, id))
                    throw new ThereIsAnObjectWithTheSameKeyInTheListException(id);
                drones.Add(new()
                {
                    Id = id,
                    Model = model,
                    MaxWeight = MaxWeight,
                    IsNotActive = false

                });
                DalXmlService.SaveListToXMLSerializer(drones, DRONE_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a certain drone according to ID no.
        /// </summary>
        /// <param name="id">drone's id</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            try { 
                List<Drone> drones = DalXmlService.LoadListFromXMLSerializer<Drone>(DRONE_PATH);
                Drone drone = drones.FirstOrDefault(item => item.Id == id);
                if (drone.Equals(default(Drone)))
                    throw new KeyNotFoundException($"The drone id {id} not exsits in data");
                drones.Remove(drone);
                drone.IsNotActive = true;
                drones.Add(drone);
                DalXmlService.SaveListToXMLSerializer(drones, DRONE_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        /// <summary>
        /// Returns drone according to id
        /// </summary>
        /// <param name="id">drone's id</param>
        /// <returns>drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            try { 
                Drone drone = DalXmlService.LoadListFromXMLSerializer<Drone>(DRONE_PATH).FirstOrDefault(item => item.Id == id);
                if (drone.Equals(default(Drone)) || drone.IsNotActive)
                    throw new KeyNotFoundException($"There is not suitable drone in the data , the drone id {id}");
                return drone;
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        /// <summary>
        /// Returns the list of the drones
        /// </summary>
        /// <returns>list of drones</returns>
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

        /// <summary>
        /// Update model name of drone
        /// </summary>
        /// <param name="drone">drone</param>
        /// <param name="model">model name</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone drone, string model)
        {
            try { 
                List<Drone> drones = DalXmlService.LoadListFromXMLSerializer<Drone>(DRONE_PATH);
                drones.Remove(drone);
                drone.Model = model;
                drones.Add(drone);
                DalXmlService.SaveListToXMLSerializer(drones, DRONE_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        /// <summary>
        /// Get Electricity
        /// </summary>
        /// <returns>5 Electricity ratings </returns>
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
