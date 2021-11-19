﻿using System;
using System.Collections;
using System.Collections.Generic;
using IBL.BO;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ConsoleUI_BL
{

    class Program
    {
        enum Menu { Add, Update, Display, DisplayList, Exit }
        enum Add { Station, Drone, Customer, Parcel }
        enum Update { DroneName, StationDetails, CustomerDedails, SendDroneForCharg, RealsDroneFromChargh, AssingParcelToDrone, CollectParcelByDrone, SupplyParcelToDestination }
        enum DisplayList { Sations, Drones, Customers, Parcels, ParcelNotAssignToDrone, AvailableChargingSations }
        enum Display { Station, Drone, Customer, Parcel }

        static void Main()
        {
            IBL.IBL bal = new IBL.BL();
            Menu option;
            do
            {
                DisplayMenus(typeof(Menu));
                if (!Enum.TryParse(Console.ReadLine(), out option))
                    Console.WriteLine();
                try
                {
                    switch (option)

                    {
                        case Menu.Add:
                            {
                                DisplayMenus(typeof(Add));
                                SwitchAdd(ref bal);
                                break;
                            }

                        case Menu.Update:
                            {

                                DisplayMenus(typeof(Update));
                                SwitchUpdate(ref bal);
                                break;
                            }
                        case Menu.Display:
                            {
                                DisplayMenus(typeof(Display));
                                SwitchDisplay(ref bal);
                                break;
                            }
                        case Menu.DisplayList:
                            {
                                DisplayMenus(typeof(DisplayList));
                                SwitchDisplayList(ref bal);
                                break;
                            }
                        case Menu.Exit:
                            break;
                        default:
                            break;

                    }
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message==string.Empty?ex:ex.Message);
                }
                catch (ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
                {
                    Console.WriteLine(ex);
                }
                catch (ThereIsAnObjectWithTheSameKeyInTheList ex)
                {
                    Console.WriteLine(ex);
                }
                catch (ArgumentNullException ex)
                {

                    Console.WriteLine(ex.Message == string.Empty ? ex : ex.Message);
                }
                catch (InvalidEnumArgumentException ex)
                {

                    Console.WriteLine(ex.Message == string.Empty ? ex : ex.Message);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message == string.Empty ? ex : ex.Message);
                }
            } while (option != Menu.Exit);


        }
        /// <summary>
        /// gets enum and prints his values
        /// </summary>
        /// <param name="en"> type of enum</param>
        static public void DisplayMenus(Type en)
        {
            int idx = 0;
            foreach (var item in Enum.GetValues(en))
            {
                string tmp = item.ToString();
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (char.IsUpper(tmp[i]) && i != 0)
                        Console.Write(" {0}", tmp[i].ToString().ToLower());
                    else
                        Console.Write(tmp[i]);
                }
                Console.WriteLine(" press " + idx++);
            }
        }
        /// <summary>
        /// Receives input from the user what type of organ to print as well as ID number and calls to the appropriate adding method
        /// </summary>
        /// <param name="dalObject"></param>
        public static void SwitchAdd(ref IBL.IBL bl)
        {
            Add option;
            if (!Enum.TryParse(Console.ReadLine(), out option))
                Console.WriteLine("There is no suitable option");
            int id;
            switch (option)
            {
                case Add.Station:
                    {
                        Console.WriteLine("enter values to station properties:id,latitude,longitude,chargeSlots, name");
                        double latitude, longitude;
                        int chargeslots;
                        if (int.TryParse(Console.ReadLine(), out id) && double.TryParse(Console.ReadLine(), out latitude) && double.TryParse(Console.ReadLine(), out longitude) && int.TryParse(Console.ReadLine(), out chargeslots))
                        {
                            string name = Console.ReadLine();
                            Location location = new();
                            location.Longitude = longitude;
                            location.Latitude = latitude;
                            Station station = new() { Id = id, Name = name, Location = location, AvailableChargingPorts = chargeslots, DroneInChargings = new List<DroneInCharging>() };
                            bl.AddStation(station);
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the addition was not made");
                        break;
                    }
                case Add.Drone:
                    {
                        Console.WriteLine("enter values to drone properties:id,max wheight,station id,model");
                        WeightCategories maxWeight;
                        int stationId;
                        if (int.TryParse(Console.ReadLine(), out id) && Enum.TryParse(Console.ReadLine(), out maxWeight) && int.TryParse(Console.ReadLine(), out stationId))
                        {
                            Drone drone = new() { Id = id, WeightCategory = maxWeight, Model = Console.ReadLine() };
                            bl.AddDrone(drone, stationId);
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the addition was not made");

                        break;
                    }
                case Add.Customer:
                    {
                        double latitude, longitude;
                        Console.WriteLine("enter values to station properties:id,latitude,longitude, name");
                        if (int.TryParse(Console.ReadLine(), out id) && double.TryParse(Console.ReadLine(), out latitude) && double.TryParse(Console.ReadLine(), out longitude))
                        {
                            Location location = new();
                            location.Longitude = longitude;
                            location.Latitude = latitude;
                            string name = Console.ReadLine();
                            string phone;
                            bool correctPhone = true;
                            do
                            {
                                Console.WriteLine("Enter phone");
                                phone = Console.ReadLine();
                                if (!(Regex.Match(phone, @"^((?:\+?)[0-9]{10})$").Success))
                                correctPhone = false;
                            } while (!correctPhone);

                            bl.AddCustomer(new Customer()
                            {
                                Id = id,
                                Name = name,
                                Phone = phone,
                                Location = location
                            });
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the addition was not made");
                        break;
                    }
                case Add.Parcel:
                    {
                        Console.WriteLine("enter values to station properties: sender id,target id,weigth,priority");
                        int senderId, targetId;
                        WeightCategories weigth;
                        Priorities priority;

                        if (int.TryParse(Console.ReadLine(), out senderId) && int.TryParse(Console.ReadLine(), out targetId) && Enum.TryParse(Console.ReadLine(), out weigth) && Enum.TryParse(Console.ReadLine(), out priority))
                        {
                            bl.AddParcel(new Parcel()
                            {
                                CustomerReceives = new CustomerInParcel()
                                {
                                    Id = targetId
                                },
                                CustomerSender = new CustomerInParcel()
                                {
                                    Id = senderId
                                },
                                Weight = weigth,
                                Priority = priority

                            });
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the addition was not made");

                        break;
                    }

                default:
                    break;

            }
        }
        /// <summary>
        /// Receives input from the user what type of organ to print as well as ID number and calls to the appropriate updating method
        /// </summary>
        /// <param name="dalObject"></param>
        public static void SwitchUpdate(ref IBL.IBL bl)
        {
            Update option;
            if (!Enum.TryParse(Console.ReadLine(), out option))
                Console.WriteLine("There is no suitable option");
            int id;
            switch (option)
            {
                case Update.DroneName:
                    {
                        Console.WriteLine("enter an id of drone");
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("enter the new model name");
                            bl.UpdateDrone(id, Console.ReadLine());
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the updating was not made");
                        break;
                    }
                case Update.StationDetails:
                    {
                        int chargeSlots;
                        Console.WriteLine("enter an id of station");
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("if you want only update one details press enter instead enter an input");
                            Console.WriteLine("the new  number of chrge slots ");
                            if (!int.TryParse(Console.ReadLine(), out chargeSlots))
                                chargeSlots = -1;
                            Console.WriteLine("enter the new name");
                            string name = Console.ReadLine();
                            bl.UpdateStation(id, name, chargeSlots);
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the updating was not made");
                        break;
                    }
                case Update.CustomerDedails:
                    {
                        Console.WriteLine("enter an id of customer");
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("if you want only update one details press enter instead enter an input");
                            Console.WriteLine("enter the new name");
                            string name = Console.ReadLine();
                            Console.WriteLine("enter the new number phone");
                            string phone = Console.ReadLine();
                            bl.UpdateCustomer(id, name, phone);
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the updating was not made");
                        break;
                    }
                case Update.SendDroneForCharg:
                    {
                        Console.WriteLine("enter an id of drone");
                        if (int.TryParse(Console.ReadLine(), out id))
                            bl.SendDroneForCharg(id);
                        break;

                    }
                case Update.RealsDroneFromChargh:
                    {
                        float timeOfCharge;
                        Console.WriteLine("enter an id of drone and time of charge");
                        if (int.TryParse(Console.ReadLine(), out id) && float.TryParse(Console.ReadLine(), out timeOfCharge))
                            bl.ReleaseDroneFromCharging(id, timeOfCharge);
                        break;
                    }
                case Update.AssingParcelToDrone:
                    {
                        Console.WriteLine("enter an id of drone");
                        if (int.TryParse(Console.ReadLine(), out id))
                            bl.AssingParcelToDrone(id);
                        break;
                    }
                case Update.CollectParcelByDrone:
                    {
                        Console.WriteLine("enter an id of drone");
                        if (int.TryParse(Console.ReadLine(), out id))
                            bl.ParcelCollectionByDrone(id);
                        break;
                    }
                case Update.SupplyParcelToDestination:
                    {
                        Console.WriteLine("enter an id of parcel");
                        if (int.TryParse(Console.ReadLine(), out id))
                            bl.DeliveryParcelByDrone(id);
                        break;
                    }
                default:
                    break;

            }
        }
        /// <summary>
        /// Receives input from the user what type of organ to print as well as ID number and calls to the appropriate printing method
        /// </summary>
        /// <param name="dalObject"></param>
        public static void SwitchDisplay(ref IBL.IBL bl)
        {
            Display option;
            if (!Enum.TryParse(Console.ReadLine(), out option))
                Console.WriteLine("There is no suitable option");
            int id;
            switch (option)
            {
                case Display.Station:
                    {
                        Console.WriteLine("enter an id of station");
                        if (int.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine(bl.GetStation(id));
                        break;
                    }
                case Display.Drone:
                    {
                        Console.WriteLine("enter an id of drone");
                        if (int.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine(bl.GetDrone(id));
                        break;
                    }
                case Display.Customer:
                    {
                        Console.WriteLine("enter an id of customer");
                        if (int.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine(bl.GetCustomer(id));
                        break;
                    }
                case Display.Parcel:
                    {
                        Console.WriteLine("enter an id of parcel");
                        if (int.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine(bl.GetParcel(id));
                        break;
                    }
                default:
                    break;

            }
        }
        /// <summary>
        /// Receives input from the user and calls the printing method accordingly 
        /// </summary>
        /// <param name="dalObject"></param>
        public static void SwitchDisplayList(ref IBL.IBL bl)
        {
            DisplayList option;
            if (!Enum.TryParse(Console.ReadLine(), out option))
                Console.WriteLine("There is no suitable option");
            switch (option)
            {
                case DisplayList.Sations:
                    PrintList(bl.GetStations());
                    break;
                case DisplayList.Drones:
                    PrintList(bl.GetDrones());
                    break;
                case DisplayList.Customers:
                    PrintList(bl.GetCustomers());
                    break;
                case DisplayList.Parcels:
                    PrintList(bl.GetParcels());
                    break;
                case DisplayList.AvailableChargingSations:
                    PrintList(bl.GetStaionsWithEmptyChargeSlots());
                    break;
                case DisplayList.ParcelNotAssignToDrone:
                    PrintList(bl.GetParcelsNotAssignedToDrone());
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Prints the whole items in collection to the console
        /// </summary>
        /// <param name="list">collection for printing</param>
        public static void PrintList(IEnumerable list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}


