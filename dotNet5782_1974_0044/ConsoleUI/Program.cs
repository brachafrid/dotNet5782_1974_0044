using System;
using System.Collections.Generic;
using DalObject;
using IDAL.DO;
namespace ConsoleUI
{
    class Program
    {
        enum Menu { Add, Update, Display, DisplayList, Exit }
        enum Add { Drone, Station, Parcel, Customer }
        enum Update { AssingParcelToDrone, CollectParcelByDrone, SupplyParcelToDestination, SendingDroneForCharging, RealsingDroneFromCharghing }
        enum DisplayList { Stations, Drones, Customers, Parcels, AvailableChargingStations, ParcelnotAssignToDrone }
        enum Display { Station, Drone, Customer, Parcel }

        static void Main(string[] args)
        {
            DalObject.DalObject dalObject = new DalObject.DalObject();
            DisplayMenus(typeof(Menu));
            Menu option;
            do
            {
                Menu.TryParse(Console.ReadLine(), out option);
                switch (option)
                {
                    case Menu.Add:
                        DisplayMenus(typeof(Add));
                        switchAdd(ref dalObject);
                        break;
                    case Menu.Update:
                        DisplayMenus(typeof(Update));
                        switchUpdate(ref dalObject);
                        break;
                    case Menu.Display:
                        DisplayMenus(typeof(Display));
                        switchDisplay(ref dalObject);
                        break;
                    case Menu.DisplayList:
                        DisplayMenus(typeof(DisplayList));
                        break;
                    case Menu.Exit:
                        break;
                    default:
                        break;

                }
            } while (option != Menu.Exit);


        }
        static public void DisplayMenus(Type en)
        {
            int idx = 0;
            foreach (var item in Enum.GetValues(en))
            {
                Console.WriteLine(item + " press " + idx++);
            }
        }

        public static void switchAdd(ref DalObject.DalObject dalObject)
        {
            Add option;
            Add.TryParse(Console.ReadLine(), out option);
            switch (option)
            {
                case Add.Drone:
                    {
                        Console.WriteLine("enter values to drone properties: model,max wheight");
                        string model = Console.ReadLine();
                        WeightCategories maxWeight;
                        WeightCategories.TryParse(Console.ReadLine(), out maxWeight);
                        dalObject.addDrone(model, maxWeight);
                        break;
                    }

                case Add.Station:
                    {
                        Console.WriteLine("enter values to station properties: name,latitude,longitude,chargeSlots");
                        string name = Console.ReadLine();
                        double latitude;
                        double.TryParse(Console.ReadLine(), out latitude);
                        double longitude;
                        double.TryParse(Console.ReadLine(), out longitude);
                        int chargeslots;
                        int.TryParse(Console.ReadLine(), out chargeslots);
                        dalObject.addStation(name, latitude, longitude, chargeslots);
                        break;
                    }

                case Add.Parcel:
                    {
                        Console.WriteLine("enter values to station properties: sender id,target id,weigth,priority");
                        int senderId;
                        int.TryParse(Console.ReadLine(), out senderId);
                        int targetId;
                        int.TryParse(Console.ReadLine(), out targetId);
                        WeightCategories weigth;
                        WeightCategories.TryParse(Console.ReadLine(), out weigth);
                        Prioripies priority;
                        Prioripies.TryParse(Console.ReadLine(), out priority);
                        dalObject.parcelsReception(senderId, targetId, weigth, priority);
                        break;
                    }

                case Add.Customer:
                    {
                        Console.WriteLine("enter values to station properties: name,phone,latitude,longitude");
                        string name = Console.ReadLine();
                        string phone = Console.ReadLine();
                        double latitude;
                        double.TryParse(Console.ReadLine(), out latitude);
                        double longitude;
                        double.TryParse(Console.ReadLine(), out longitude);
                        dalObject.addCustomer(name, phone, latitude, longitude);
                        break;
                    }
                default:
                    break;

            }
        }
        public static void switchUpdate(ref DalObject.DalObject dalObject)
        {
            Update option;
            Update.TryParse(Console.ReadLine(), out option);
            int id;
            switch (option)
            {
                case Update.AssingParcelToDrone:
                    {
                        Console.WriteLine("enter a id of parcel");
                        int.TryParse(Console.ReadLine(), out id);
                        dalObject.AssignParcelDrone(id);
                        break;
                    }
                case Update.CollectParcelByDrone:
                    {
                        Console.WriteLine("enter a id of parcel");
                        int.TryParse(Console.ReadLine(), out id);
                        dalObject.CollectParcel(id);
                        break;
                    }
                case Update.SupplyParcelToDestination:
                    {
                        Console.WriteLine("enter a id of parcel");
                        int.TryParse(Console.ReadLine(), out id);
                        dalObject.SupplyParcel(id);
                        break;
                    }
                case Update.SendingDroneForCharging:
                    {
                        Console.WriteLine("enter a id of drone");
                        int.TryParse(Console.ReadLine(), out id);
                        dalObject.SendingDroneCharging(id);
                        break;
                    }
                case Update.RealsingDroneFromCharghing:
                    {
                        Console.WriteLine("enter a id of drone");
                        int.TryParse(Console.ReadLine(), out id);
                        dalObject.ReleasingDroneCharging(id);
                        break;
                    }
                default:
                    break;

            }
        }
        public static void switchDisplay(ref DalObject.DalObject dalObject)
        {
            Display option;
            Display.TryParse(Console.ReadLine(), out option);
            int id;
            switch (option)
            {
                case Display.Station:
                    {
                        Console.WriteLine("enter a id of station");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalObject.GetStation(id));
                        break;
                    }
                case Display.Drone:
                    {
                        Console.WriteLine("enter a id of drone");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalObject.GetDrone(id));
                        break;
                    }
                case Display.Customer:
                    {
                        Console.WriteLine("enter a id of customer");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalObject.GetCustomer(id));
                        break;
                    }
                case Display.Parcel:
                    {
                        Console.WriteLine("enter a id of parcel");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalObject.GetParcel(id));
                        break;
                    }
                default:
                    break;

            }
        }
        public static void switchDisplayList(ref DalObject.DalObject dalObject)
        {
            DisplayList option;
            DisplayList.TryParse(Console.ReadLine(), out option);
            int id;
            switch (option)
            {
                case DisplayList.Stations:
                    {
                        foreach (Station item in dalObject.GetStations())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                case DisplayList.Drones:
                    {
                        foreach (Drone item in dalObject.GetDrones())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                case DisplayList.Customers:
                    {
                        foreach (Customer item in dalObject.GetCustomers())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                case DisplayList.Parcels:
                    {
                        foreach (Parcel item in dalObject.GetParcels())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                case DisplayList.AvailableChargingStations:
                    {
                        foreach (Station item in dalObject.GetStationsWithEmptyChargeSlots())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                case DisplayList.ParcelnotAssignToDrone:
                    {
                        foreach (Parcel item in dalObject.GetParcelsNotAssignedToDrone())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }


}

