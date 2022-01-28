using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using DO;


namespace Dal
{
    public sealed partial class DalXml
    {
        static readonly Random Rnd = new();
        private const int DRONE_INIT = 5;
        private const int STATIONS_INIT = 2;
        private const int CUSTOMERS_INIT = 10;
        private const int PARCELS_INIT = 10;
        private const int RANGE_ENUM = 3;
        private const int PHONE_MIN = 100000000;
        private const int PHONE_MAX = 1000000000;
        private const int LATITUDE_MAX = 90;
        private const int LATITUDE_MIN = -90;
        private const int LONGITUDE_MAX = 90;
        private const int CHARGE_SLOTS_MAX = 100;
        private const int PARCELS_STATE = 4;


        public const string Administrator_Password = "";


        public IEnumerable<Drone> InitializeDrone()
        {
            List<Drone> Drones = new();
            for (int i = 1; i <= DRONE_INIT; ++i)
                Drones.Add(RandomDrone(i));
            return Drones;
        }

        public void InitializeConfig()
        {
            new XDocument(
            new XElement("Config",
                            new XElement("IdParcel", 0),
                            new XElement("Available", 0.001),
                            new XElement("LightWeightCarrier", 0.002),
                            new XElement("MediumWeightBearing", 0.003),
                            new XElement("CarriesHeavyWeight", 0.004),
                            new XElement("DroneLoadingRate", Rnd.NextDouble()))
                      ).Save(DIR+CONFIG);                   
        }

        public IEnumerable<Parcel> InitializeParcel()
        {
            try
            {
                List<Parcel> Parcels = new();
                for (int i = 1; i <= PARCELS_INIT; ++i)
                {
                    Parcels.Add(RandParcel());
                    XMLTools.SaveListToXMLSerializer(Parcels, PARCEL_PATH);
                }

                return Parcels;
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }
        public IEnumerable<Station> InitializeStation()
        {
            List<Station> Stations = new();
            for (int i = 1; i <= STATIONS_INIT; ++i)
                Stations.Add(RandomStation(i));
            return Stations;
        }
        public IEnumerable<Customer> InitializeCustomer()
        {
            List<Customer> Customers = new();
            for (int i = 1; i <= CUSTOMERS_INIT; ++i)
                Customers.Add(RandomCustomer(i));
            return Customers;

        }
        private int AssignParcelDrone(WeightCategories weight)
        {
            try
            {
                Drone tmpDrone = XMLTools.LoadListFromXMLSerializer<Drone>(DRONE_PATH).FirstOrDefault(item => weight <= item.MaxWeight);
                if (!tmpDrone.Equals(default(Drone)))
                {
                    return tmpDrone.Id;
                }
                return 0;
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }
        private Drone RandomDrone(int id)
        {
            string model = $"Model_Drone_ {'a' + id}_{id * Rnd.Next()}";
            WeightCategories maxWeight = (WeightCategories)Rnd.Next(RANGE_ENUM);
            return new Drone()
            {
                Id = id,
                Model = model,
                MaxWeight = maxWeight,
                IsNotActive = false
            };
        }
        private Station RandomStation(int id)
        {
            string name = $"station_{'a' + id}";
            double latitude = Rnd.Next(LATITUDE_MIN, LATITUDE_MAX) + Rnd.NextDouble();
            double longitude = Rnd.Next(LONGITUDE_MAX) + Rnd.NextDouble();
            int chargeSlots = Rnd.Next(1, CHARGE_SLOTS_MAX);
            return new Station()
            {
                Id = id,
                Name = name,
                Longitude = longitude,
                Latitude = latitude,
                ChargeSlots = chargeSlots,
                IsNotActive = false
            };
        }
        private Customer RandomCustomer(int id)
        {
            string name = $"Customer_ { id}_{id * Rnd.Next()}";
            string phone = $"0{Rnd.Next(PHONE_MIN, PHONE_MAX)}";
            double latitude = Rnd.Next(LATITUDE_MIN, LATITUDE_MAX) + Rnd.NextDouble();
            double longitude = Rnd.Next(LONGITUDE_MAX) + Rnd.NextDouble();
            return new Customer()
            {
                Id = id,
                Name = name,
                Longitude = longitude,
                Latitude = latitude,
                Phone = phone,
                IsNotActive = false
            };
        }
        private Parcel RandParcel()
        {
            try
            {
                Parcel newParcel = new();                
                XElement config=XMLTools.LoadConfigToXML(CONFIG);
                XElement parcelId = config.Elements().Single(elem => elem.Name.ToString().Contains("Parcel"));
                newParcel.Id =int.Parse(parcelId.Value)+1;
                config.SetElementValue(parcelId.Name,newParcel.Id);
                XMLTools.SaveConfigToXML(config, CONFIG);
                List<Customer> customers = XMLTools.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
                newParcel.SenderId = customers[Rnd.Next(0, customers.Count)].Id;
                do
                {
                    newParcel.TargetId = customers[Rnd.Next(0, customers.Count)].Id;
                } while (newParcel.TargetId == newParcel.SenderId);
                newParcel.Weigth = (WeightCategories)Rnd.Next(RANGE_ENUM);
                newParcel.Priority = (Priorities)Rnd.Next(RANGE_ENUM);
                newParcel.Requested = DateTime.Now; ;
                newParcel.Sceduled = default;
                newParcel.PickedUp = default;
                newParcel.Delivered = default;
                newParcel.DorneId = 0;
                newParcel.IsNotActive = false;
                int state = Rnd.Next(PARCELS_STATE);
                if (state != 0)
                {
                    newParcel.DorneId = AssignParcelDrone(newParcel.Weigth);
                    if (newParcel.DorneId != 0)
                    {
                        Parcel tmp = XMLTools.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH).FirstOrDefault(parcel => parcel.DorneId == newParcel.DorneId && parcel.Delivered == null);
                        if (tmp.DorneId == 0)
                        {
                            newParcel.Sceduled = DateTime.Now;
                            if (state == 2)
                            {
                                newParcel.PickedUp = DateTime.Now;
                            }

                        }
                        if (state == 3)
                        {
                            newParcel.Sceduled = DateTime.Now;
                            newParcel.PickedUp = DateTime.Now;
                            newParcel.Delivered = DateTime.Now;
                        }

                    }

                }
                return newParcel;
            }
            catch
            {
                throw new XMLFileLoadCreateException();
            }
        }
    }

}

