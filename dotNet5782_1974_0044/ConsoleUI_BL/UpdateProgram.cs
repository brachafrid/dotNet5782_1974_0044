//using System;



//namespace ConsoleUI_BL
//{
//    partial class Program
//    {
//        /// <summary>
//        /// Receives input from the user what type of organ to print as well as ID number and calls to the appropriate updating method
//        /// </summary>
//        /// <param name="dalObject"></param>
//        public static void SwitchUpdate(ref BL.IBL bl)
//        {
//            if (!Enum.TryParse(Console.ReadLine(), out Update option))
//            {
//                Console.WriteLine("The convertion faild  therefore the no option choose ");
//                option = (Update)Enum.GetValues(typeof(Update)).Length; ;
//            }

//            int id;
//            switch (option)
//            {
//                case Update.DroneName:
//                    {
//                        Console.WriteLine("enter an id of drone");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                        {
//                            Console.WriteLine("enter the new model name");
//                            bl.UpdateDrone(id, Console.ReadLine());
//                        }
//                        else
//                            Console.WriteLine("The conversion failed and therefore the updating was not made");
//                        break;
//                    }
//                case Update.StationDetails:
//                    {
//                        Console.WriteLine("enter an id of station");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                        {
//                            Console.WriteLine("if you only want update one details press enter instead enter an input");
//                            Console.WriteLine("the new  number of chrge slots ");
//                            if (!int.TryParse(Console.ReadLine(), out int chargeSlots) || chargeSlots == default)
//                                chargeSlots = 0;
//                            if (chargeSlots < 0)
//                            {
//                                Console.WriteLine("invalid charg slots");
//                            }
//                            Console.WriteLine("enter the new name");
//                            string name = Console.ReadLine();
//                            bl.UpdateStation(id, name, chargeSlots);
//                        }
//                        else
//                            Console.WriteLine("The conversion failed and therefore the updating was not made");
//                        break;
//                    }
//                case Update.CustomerDedails:
//                    {
//                        Console.WriteLine("enter an id of customer");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                        {
//                            Console.WriteLine("if you want only update one details press enter instead enter an input");
//                            Console.WriteLine("enter the new name");
//                            string name = Console.ReadLine();
//                            Console.WriteLine("enter the new number phone");
//                            string phone = Console.ReadLine();
//                            bl.UpdateCustomer(id, name, phone);
//                        }
//                        else
//                            Console.WriteLine("The conversion failed and therefore the updating was not made");
//                        break;
//                    }
//                case Update.SendDroneForCharg:
//                    {
//                        Console.WriteLine("enter an id of drone");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                            bl.SendDroneForCharg(id);
//                        break;

//                    }
//                case Update.RealsDroneFromChargh:
//                    {
//                        Console.WriteLine("enter an id of drone and time of charge in minute");
//                        if (int.TryParse(Console.ReadLine(), out id) && float.TryParse(Console.ReadLine(), out float timeOfCharge))
//                            bl.ReleaseDroneFromCharging(id, timeOfCharge);
//                        break;
//                    }
//                case Update.AssingParcelToDrone:
//                    {
//                        Console.WriteLine("enter an id of drone");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                            bl.AssingParcelToDrone(id);
//                        break;
//                    }
//                case Update.CollectParcelByDrone:
//                    {
//                        Console.WriteLine("enter an id of drone");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                            bl.ParcelCollectionByDrone(id);
//                        break;
//                    }
//                case Update.SupplyParcelToDestination:
//                    {
//                        Console.WriteLine("enter an id of drone");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                            bl.DeliveryParcelByDrone(id);
//                        break;
//                    }
//                default:
//                    break;

//            }
//        }
//    }
//}
