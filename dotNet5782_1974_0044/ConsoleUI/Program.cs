using System;

namespace ConsoleUI
{

    class Program
    {
        enum Menu { Add, Update, Display,DisplayList, Exit }
        enum Add { Drone, Station, Parcel, Customer}
        enum Update { }
        enum DisplayList {Stations, Drones,Customer,Parcels, AvailableChargingStations, }
        enum Display { }
        static void Main(string[] args)
        {

        }
        static public void DisplayMenu(Enum en)
        {
            int idx = 0;
            foreach (var item in Enum.GetValues(typeof(en)))
            {
                Console.WriteLine(item + " press " + idx++);
            }
        }

    }
}
