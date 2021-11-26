using System;
using System.Collections;
using System.Collections.Generic;


namespace ConsoleUI_BL
{
    partial class Program
    {
        /// <summary>
        /// Receives input from the user what type of organ to print as well as ID number and calls to the appropriate printing method
        /// </summary>
        /// <param name="dalObject"></param>
        public static void SwitchDisplay(ref IBL.IBL bl)
        {
            Display option;
            if (!Enum.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("The convertion faild  therefore the no option choose ");
                option = Display.Parcel + 1;
            }
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
            {
                Console.WriteLine("The convertion faild  therefore the no option choose ");
                option = DisplayList.AvailableChargingSations + 1;
            }
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
