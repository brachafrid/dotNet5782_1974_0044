using System;
using System.Collections.Generic;
namespace ConsoleUI
{
    class Program
    {
        enum Menu { Addi, Update, Display, DisplayList, Exit }
        enum Add { Drone, Station, Parcel, Customer }
        enum Update {AssingParcelToDrone,CollectParcelByDrone,SupplyParcelToDestination,SendingDroneForCharging,RealsingDroneFromCharghing }
        enum DisplayList { Stations, Drones, Customer, Parcels, AvailableChargingStations, }
        enum Display { Station,Drone,Customer,Parcel}
      
        static void Main(string[] args)
        {
            DisplayMenu(typeof(Menu));
            int choice;
            int.TryParse(Console.ReadLine(),out choice);
            switch (choice)
            {
                case :
                    DisplayMenu(typeof(Add));
                default:
            }
            
        }
        static public void DisplayMenu(Type en)
        {
                int idx = 0;
                foreach (var item in Enum.GetValues(en)) 
                {
                    Console.WriteLine(item + " press " + idx++);
                }  
            
        }




    }
}
