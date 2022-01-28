using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLApi;
using DO;

namespace Dal
{
    public sealed partial class DalXml
    {
        const string DRONE_PATH = @"XmlDrone.xml";

        public void AddDrone(int id, string model, WeightCategories MaxWeight)
        {
            try { 
                List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DRONE_PATH);
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
                XMLTools.SaveListToXMLSerializer<Drone>(drones, DRONE_PATH);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }

        public void DeleteDrone(int id)
        {
            try { 
                List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DRONE_PATH);
                Drone drone = drones.FirstOrDefault(item => item.Id == id);
                drones.Remove(drone);
                drone.IsNotActive = true;
                drones.Add(drone);
                XMLTools.SaveListToXMLSerializer<Drone>(drones, DRONE_PATH);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }
        public Drone GetDrone(int id)
        {
            try { 
                Drone drone = XMLTools.LoadListFromXMLSerializer<Drone>(DRONE_PATH).FirstOrDefault(item => item.Id == id);
                if (drone.Equals(default(Drone)) || drone.IsNotActive)
                    throw new KeyNotFoundException("There is not suitable drone in the data");
                return drone;
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }

        public IEnumerable<Drone> GetDrones()
        {
            try { 
                return XMLTools.LoadListFromXMLSerializer<Drone>(DRONE_PATH).Where(d => !d.IsNotActive);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }
        public void RemoveDrone(Drone drone)
        {
            try { 
                List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DRONE_PATH);
                drones.Remove(drone);
                XMLTools.SaveListToXMLSerializer<Drone>(drones, DRONE_PATH);
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }

        public (double, double, double, double, double) GetElectricity()
        {
            XElement config = XMLTools.LoadConfigToXML(CONFIG);

            var electricity=config.Elements().Select(elem => double.Parse(elem.Value));
            return (electricity.ElementAt(1), electricity.ElementAt(2), electricity.ElementAt(3), electricity.ElementAt(4), electricity.ElementAt(5));
        }
    }
}
