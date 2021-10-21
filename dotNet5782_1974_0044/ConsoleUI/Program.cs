using System;
using System.Collections.Generic;
namespace ConsoleUI
{
    class Program
    {
        enum Menu { Add, Update, Display, DisplayList, Exit }
        enum Add { Drone, Station, Parcel, Customer }
        enum Update { AssingParcelToDrone, CollectParcelByDrone, SupplyParcelToDestination, SendingDroneForCharging, RealsingDroneFromCharghing }
        enum DisplayList { Stations, Drones, Customer, Parcels, AvailableChargingStations, }
        enum Display { Station, Drone, Customer, Parcel }

        static void Main(string[] args)
        {
            DisplayMenus(typeof(Menu));
            int choice;
            Menu option;
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
                option = (Menu)choice;
                switch (option)
                {
                    case Menu.Add:
                        DisplayMenus(typeof(Add));
                        break;
                    case Menu.Update:
                        DisplayMenus(typeof(Update));
                        break;
                    case Menu.Display:
                        DisplayMenus(typeof(Display));
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

        static public void switchAdd()
        {
            int choice;
            Add option;
            int.TryParse(Console.ReadLine(), out choice);
            option = (Add)choice;
            switch (option)
            {
                case Add.Drone:
                    DalObject.DalObject.
                    break;
                case Add.Station:
                    DisplayMenus(typeof(Update));
                    break;
                case Add.Parcel:
                    DisplayMenus(typeof(Display));
                    break;
                case Add.Customer:
                    DisplayMenus(typeof(DisplayList));
                    break;
                default:
                    break;

            }

        }




    }
}
