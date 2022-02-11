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
    internal static class DalXmlUnit
    {
        private static string PARCEL_PATH = @"XmlParcel.xml";
        private static string DIR = @"..\..\data\";
        private static string CONFIG = @"XmlConfig.xml";
        private static string DRONE_PATH = @"XmlDrone.xml";
        private static string CUSTOMER_PATH = @"XmlCustomer.xml";

        static readonly Random Rnd = new();
        private static int DRONE_INIT = 5;
        private static int STATIONS_INIT = 2;
        private static int CUSTOMERS_INIT = 10;
        private static int PARCELS_INIT = 10;
        private static int RANGE_ENUM = 3;
        private static int PHONE_MIN = 100000000;
        private static int PHONE_MAX = 1000000000;
        private static int LATITUDE_MAX = 90;
        private static int LATITUDE_MIN = -90;
        private static int LONGITUDE_MAX = 90;
        private static int CHARGE_SLOTS_MAX = 100;
        private static int PARCELS_STATE = 4;

        internal static IEnumerable<Drone> InitializeDrone()
        {
            List<Drone> Drones = new();
            for (int i = 1; i <= DRONE_INIT; ++i)
                Drones.Add(RandomDrone(i));
            return Drones;
        }

        internal static void InitializeConfig()
        {
            new XDocument(
            new XElement("Config",
                            new XElement("IdParcel", 0),
                            new XElement("Available", 0.001),
                            new XElement("LightWeightCarrier", 0.002),
                            new XElement("MediumWeightBearing", 0.003),
                            new XElement("CarriesHeavyWeight", 0.004),
                            new XElement("DroneLoadingRate", Rnd.NextDouble()),
                            new XElement("AdministratorPassword", ""))
                      ).Save(DIR + CONFIG);
        }

        internal static IEnumerable<Parcel> InitializeParcel()
        {
            try
            {
                List<Parcel> Parcels = new();
                for (int i = 1; i <= PARCELS_INIT; ++i)
                {
                    Parcels.Add(RandParcel());
                    DalXmlService.SaveListToXMLSerializer(Parcels, PARCEL_PATH);
                }

                return Parcels;
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        internal static IEnumerable<Station> InitializeStation()
        {
            List<Station> Stations = new();
            for (int i = 1; i <= STATIONS_INIT; ++i)
                Stations.Add(RandomStation(i));
            return Stations;
        }
        internal static IEnumerable<Customer> InitializeCustomer()
        {
            List<Customer> Customers = new();
            for (int i = 1; i <= CUSTOMERS_INIT; ++i)
                Customers.Add(RandomCustomer(i));
            return Customers;

        }
        private static int AssignParcelDrone(WeightCategories weight)
        {

            Drone tmpDrone = DalXmlService.LoadListFromXMLSerializer<Drone>(DRONE_PATH).FirstOrDefault(item => weight <= item.MaxWeight);
            if (!tmpDrone.Equals(default(Drone)))
            {
                return tmpDrone.Id;
            }
            return 0;
        }
        private static Drone RandomDrone(int id)
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
        private static Station RandomStation(int id)
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
        private static Customer RandomCustomer(int id)
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
        private static Parcel RandParcel()
        {
            Parcel newParcel = new();
            XElement config = DalXmlService.LoadConfigToXML(CONFIG);
            XElement parcelId = config.Elements().Single(elem => elem.Name.ToString().Contains("Parcel"));
            newParcel.Id = int.Parse(parcelId.Value) + 1;
            config.SetElementValue(parcelId.Name, newParcel.Id);
            DalXmlService.SaveConfigToXML(config, CONFIG);
            List<Customer> customers = DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
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
                    Parcel tmp = DalXmlService.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH).FirstOrDefault(parcel => parcel.DorneId == newParcel.DorneId && parcel.Delivered == null);
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
    }

}

