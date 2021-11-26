using System;
using System.Collections.Generic;
using IBL.BO;
using System.ComponentModel;


namespace ConsoleUI_BL
{
    partial class Program
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
                {
                    Console.WriteLine("The conversion failed  therefore the no option choose");
                    option =Menu.Exit+1;
                }
                    
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
                catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
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
    }
}


