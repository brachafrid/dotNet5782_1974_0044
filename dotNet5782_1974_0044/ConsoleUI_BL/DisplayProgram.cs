using IBL.BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


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
            if (!Enum.TryParse(Console.ReadLine(), out Display option))
            {
                Console.WriteLine("The convertion faild  therefore the no option choose ");
                option = (Display)Enum.GetValues(typeof(Display)).Length; ;
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
            if (!Enum.TryParse(Console.ReadLine(), out DisplayList option))
            {
                Console.WriteLine("The convertion faild  therefore the no option choose ");
                option = (DisplayList)Enum.GetValues(typeof(DisplayList)).Length;
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
                    PrintList(bl.GetStaionsWithEmptyChargeSlots((int emptyChargeSlots) => emptyChargeSlots > 0));
                    break;
                case DisplayList.ParcelNotAssignToDrone:
                    PrintList(bl.GetParcelsNotAssignedToDrone((int droneId) => droneId == 0));
                    break;
                default:
                    PrintList(bl.GetDronesScreenOut((WeightCategories weightCategories) => weightCategories == WeightCategories.HEAVY));
                    break;
            }
        }
        /// <summary>
        /// Prints the whole items in collection to the console
        /// </summary>
        /// <param name="list">collection for printing</param>
        public static void PrintList<T>(IEnumerable<T> list)
        {
            if (!list.Any())
                Console.WriteLine("empty list");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

        }
    }
}
