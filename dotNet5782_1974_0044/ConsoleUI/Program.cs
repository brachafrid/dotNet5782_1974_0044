using System;
using System.Collections.Generic;
namespace ConsoleUI
{

    class Program
    {
        enum Menu { Add, Update, Display, DisplayList, Exit }
        enum Add { Drone, Station, Parcel, Customer }
        enum Update {AssingParcelToDrone,CollectParcelByDrone,SupplyParcelToDestination,SendingDroneForCharging,RealsingDroneFromCharghing }
        enum DisplayList { Stations, Drones, Customer, Parcels, AvailableChargingStations, }
        enum Display { Station,Drone,Customer,Parcel}
      
        static void Main(string[] args)
        {
            DisplayMenu(();
        }
        static public void DisplayMenu(Enum en)
        {
                int idx = 0;
                foreach (var item in Enum.GetValues(en.GetType())) 
                {
                    Console.WriteLine(item + " press " + idx++);
                }  
            
        }




    }
}
