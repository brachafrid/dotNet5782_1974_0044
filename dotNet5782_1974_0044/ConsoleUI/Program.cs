using System;
using System.Collections;
using IDAL.DO;


namespace ConsoleUI
{
    class Program
    {
        enum Menu { Add, Update, Display, DisplayList, Exit }
        enum Add { Drone, Station, Parcel, Customer }
        enum Update { AssingParcelToDrone, CollectParcelByDrone, SupplyParcelToDestination, SendDroneForCharg, RealsDroneFromChargh }
        enum DisplayList { Sations, Drones, Customers, Parcels, AvailableChargingSations, ParcelNotAssignToDrone }
        enum Display { Station, Drone, Customer, Parcel }

        static void Main(string[] args)
        {
            IDAL.IDal dalObject =new DalObject.DalObject();
            Menu option;
            do
            {
                DisplayMenus(typeof(Menu));
                Enum.TryParse(Console.ReadLine(), out option);
                switch (option)
                {
                    case Menu.Add:
                        {
                            DisplayMenus(typeof(Add));
                            try
                            {
                                switchAdd(ref dalObject);
                            }
                            catch
                            {
                                Console.WriteLine("error");
                            }

                            break;
                        }

                    case Menu.Update:
                        {

                            DisplayMenus(typeof(Update));
                            try
                            {
                                switchUpdate(ref dalObject);
                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine("incorrect id");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch
                            {
                                Console.WriteLine("ERROR");
                            }

                            break;
                        }
                    case Menu.Display:
                        {
                            DisplayMenus(typeof(Display));
                            try
                            {
                                switchDisplay(ref dalObject);
                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine("incorrect id");
                            }
                            catch
                            {
                                Console.WriteLine("ERROR");
                            }

                            break;
                        }
                    case Menu.DisplayList:
                        {
                            DisplayMenus(typeof(DisplayList));
                            try
                            {
                                switchDisplayList(ref dalObject);

                            }
                            catch
                            {
                                Console.WriteLine("ERROR");
                            }


                            break;
                        }

                    case Menu.Exit:
                        break;
                    default:
                        break;

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
        public static void switchAdd(ref IDAL.IDal dalObject)
        {
            Add option;
            Enum.TryParse(Console.ReadLine(), out option);
            int id;
            switch (option)
            {
                case Add.Drone:
                    {
                        Console.WriteLine("enter values to drone properties:id,max wheight,model");
                        WeightCategories maxWeight;
                        if (int.TryParse(Console.ReadLine(), out id) && Enum.TryParse(Console.ReadLine(), out maxWeight))
                        {
                            string model = Console.ReadLine();
                            dalObject.addDrone(id, model, maxWeight);
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the addition was not made");

                        break;
                    }

                case Add.Station:
                    {
                        Console.WriteLine("enter values to station properties:id, name,latitude,longitude,chargeSlots");
                        double latitude, longitude;
                        int chargeslots;
                        if (int.TryParse(Console.ReadLine(), out id) && double.TryParse(Console.ReadLine(), out latitude) && double.TryParse(Console.ReadLine(), out longitude) && int.TryParse(Console.ReadLine(), out chargeslots))
                        {
                            string name = Console.ReadLine();
                            dalObject.addStation(id, name, latitude, longitude, chargeslots);
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the addition was not made");
                        break;
                    }

                case Add.Parcel:
                    {
                        Console.WriteLine("enter values to station properties: id,sender id,target id,weigth,priority");
                        int senderId, targetId;
                        WeightCategories weigth;
                        Priorities priority;
                        if (int.TryParse(Console.ReadLine(), out id)&& int.TryParse(Console.ReadLine(), out senderId)&&int.TryParse(Console.ReadLine(), out targetId)&&Enum.TryParse(Console.ReadLine(), out weigth)&& Enum.TryParse(Console.ReadLine(), out priority))
                        {
                            dalObject.ParcelsReception(id, senderId, targetId, weigth, priority);
                        }
                        else
                            Console.WriteLine("The conversion failed and therefore the addition was not made");

                        break;
                    }

                case Add.Customer:
                    {
                        Console.WriteLine("enter values to station properties:id, name,phone,latitude,longitude");
                        double latitude, longitude;
                        if (int.TryParse(Console.ReadLine(), out id)&&double.TryParse(Console.ReadLine(), out latitude)&&double.TryParse(Console.ReadLine(), out longitude))
                        {
                           
                            string name = Console.ReadLine();
                            string phone = Console.ReadLine();
                            dalObject.addCustomer(id, name, phone, latitude, longitude);
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
        public static void switchUpdate(ref IDAL.IDal dalObject)
        {
            Update option;
            Enum.TryParse(Console.ReadLine(), out option);
            int id;
            switch (option)
            {
                case Update.AssingParcelToDrone:
                    {
                        Console.WriteLine("enter an id of parcel");
                        int.TryParse(Console.ReadLine(), out id);
                        dalObject.AssignParcelDrone(id);
                        break;
                    }
                case Update.CollectParcelByDrone:
                    {
                        Console.WriteLine("enter an id of parcel");
                        int.TryParse(Console.ReadLine(), out id);
                        dalObject.CollectParcel(id);
                        break;
                    }
                case Update.SupplyParcelToDestination:
                    {
                        Console.WriteLine("enter an id of parcel");
                        int.TryParse(Console.ReadLine(), out id);
                        dalObject.SupplyParcel(id);
                        break;
                    }
                case Update.SendDroneForCharg:
                    {
                        Console.WriteLine("enter an id of drone");
                        int.TryParse(Console.ReadLine(), out id);
                        dalObject.SendDroneCharg(id);
                        break;
                    }
                case Update.RealsDroneFromChargh:
                    {
                        Console.WriteLine("enter an id of drone");
                        int.TryParse(Console.ReadLine(), out id);
                        dalObject.ReleasDroneCharg(id);
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
        public static void switchDisplay(ref IDAL.IDal dalObject)
        {
            Display option;
            Enum.TryParse(Console.ReadLine(), out option);
            int id;
            switch (option)
            {
                case Display.Station:
                    {
                        Console.WriteLine("enter an id of station");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalObject.GetStation(id));
                        break;
                    }
                case Display.Drone:
                    {
                        Console.WriteLine("enter an id of drone");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalObject.GetDrone(id));
                        break;
                    }
                case Display.Customer:
                    {
                        Console.WriteLine("enter an id of customer");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalObject.GetCustomer(id));
                        break;
                    }
                case Display.Parcel:
                    {
                        Console.WriteLine("enter an id of parcel");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalObject.GetParcel(id));
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
        public static void switchDisplayList(ref IDAL.IDal dalObject)
        {
            DisplayList option;
            Enum.TryParse(Console.ReadLine(), out option);
            switch (option)
            {
                case DisplayList.Sations:
                    printList(dalObject.GetStations());
                    break;
                case DisplayList.Drones:
                    printList(dalObject.GetDrones());
                    break;
                case DisplayList.Customers:
                    printList(dalObject.GetCustomers());
                    break;
                case DisplayList.Parcels:
                    printList(dalObject.GetParcels());
                    break;
                case DisplayList.AvailableChargingSations:
                    printList(dalObject.GetSationsWithEmptyChargeSlots());
                    break;
                case DisplayList.ParcelNotAssignToDrone:
                    printList(dalObject.GetParcelsNotAssignedToDrone());
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Prints the whole items in collection to the console
        /// </summary>
        /// <param name="list">collection for printing</param>
        public static void printList(IEnumerable list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}

