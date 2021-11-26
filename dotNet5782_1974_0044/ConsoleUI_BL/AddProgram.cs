using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using IBL.BO;

namespace ConsoleUI_BL
{
    partial class Program
    {

        /// <summary>
        /// Receives input from the user what type of organ to print as well as ID number and calls to the appropriate adding method
        /// </summary>
        /// <param name="dalObject"></param>
        public static void SwitchAdd(ref IBL.IBL bl)
        {
            if (!Enum.TryParse(Console.ReadLine(), out Add option))
            {
                Console.WriteLine("The convertion faild  therefore the no option choose ");
                option = Add.Parcel + 1;
            }
                
            int id;
            switch (option)
            {
                case Add.Station:
                    {
                        Console.WriteLine("enter values to station properties:id,latitude,longitude,chargeSlots, name");
                        if (int.TryParse(Console.ReadLine(), out id) && double.TryParse(Console.ReadLine(), out double latitude) && double.TryParse(Console.ReadLine(), out double longitude) && int.TryParse(Console.ReadLine(), out int chargeslots))
                        {
                            if (latitude > 90 || latitude < -90)
                            {
                                Console.WriteLine("invalid latitude");
                                break;
                            }
                            if (longitude > 90 || longitude < 0)
                            {
                                Console.WriteLine("invalid longitude");
                                break;
                            }
                            if (chargeslots < 0)
                            {
                                Console.WriteLine("invalid charge slots");
                                break;
                            }
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
                        if (int.TryParse(Console.ReadLine(), out id) && Enum.TryParse(Console.ReadLine(), out WeightCategories maxWeight) && int.TryParse(Console.ReadLine(), out int stationId))
                        {
                            if ((int)maxWeight > 2 || (int)maxWeight < 0)
                            {
                                Console.WriteLine("invalid max weight max weight range is 0-2");
                                break;
                            }
                            Drone drone = new() { Id = id, WeightCategory = maxWeight, Model = Console.ReadLine() };
                            bl.AddDrone(drone, stationId);
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the addition was not made");

                        break;
                    }
                case Add.Customer:
                    {
                        Console.WriteLine("enter values to customer properties:id,latitude,longitude, name, phone");
                        if (int.TryParse(Console.ReadLine(), out id) && double.TryParse(Console.ReadLine(), out double latitude) && double.TryParse(Console.ReadLine(), out double longitude))
                        {
                            Location location = new();
                            location.Longitude = longitude;
                            location.Latitude = latitude;
                            string name = Console.ReadLine();
                            string phone;
                            if (latitude > 90 || latitude < -90)
                            {
                                Console.WriteLine("invalid latitude");
                                break;
                            }
                            if (longitude > 90 || longitude < 0)
                            {
                                Console.WriteLine("invalid longitude");
                                break;
                            }

                            Console.WriteLine("Enter phone");
                            phone = Console.ReadLine();
                            if (!(Regex.Match(phone, @"^((?:\+?)[0-9]{10})$").Success))
                            {
                                Console.WriteLine("invalid phone");
                                break;
                            }
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
                        Console.WriteLine("enter values to parcel properties: sender id,target id,weigth,priority");

                        if (int.TryParse(Console.ReadLine(), out int senderId) && int.TryParse(Console.ReadLine(), out int targetId) && Enum.TryParse(Console.ReadLine(), out WeightCategories weigth) && Enum.TryParse(Console.ReadLine(), out Priorities priority))
                        {
                            if ((int)weigth > 2 || (int)weigth < 0)
                            {
                                Console.WriteLine("invalid weight, weight range is 0-2");
                                break;
                            }
                            if ((int)priority > 2 || (int)priority < 0)
                            {
                                Console.WriteLine("invalid priority, priority range is 0-2");
                                break;
                            }
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
    }
}
