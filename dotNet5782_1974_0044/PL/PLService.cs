using BLApi;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public static class PLService
    {
        private static IBL ibal = BLFactory.GetBL();
        public static string GetAdministorPasssword()
        {
            return ibal.GetAdministorPasssword();
        }
        public static void AddCustomer(CustomerAdd customer)
        {
            ibal.AddCustomer(ConvertAddCustomer(customer));
        }
        public static Customer GetCustomer(int id)
        {
            return ConvertCustomer(ibal.GetCustomer(id));
        }
        public static IEnumerable<CustomerToList> GetCustomers()
        {
            return ibal.GetActiveCustomers().Select(customer => ConvertCustomerToList(customer));
        }
        public static void UpdateCustomer(int id, string name, string phone)
        {
            ibal.UpdateCustomer(id, name, phone);
        }
        public static void DeleteCustomer(int id)
        {
            ibal.DeleteCustomer(id);
        }
        public static bool IsNotActiveCustomer(int id) => ibal.IsNotActiveCustomer(id);

        public static PO.Customer ConvertCustomer(BO.Customer customer)
        {
            return new PO.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = ConvertLocation(customer.Location),
                FromCustomer = new(customer.FromCustomer.Select(item => ConvertParcelAtCustomer(item))),
                ToCustomer = new(customer.ToCustomer.Select(item => ConvertParcelAtCustomer(item)))
            };
        }
        public static BO.Customer ConvertBackCustomer(PO.Customer customer)
        {
            return new BO.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = ConvertBackLocation(customer.Location),
                FromCustomer = customer.FromCustomer.Select(item => ConvertBackParcelAtCustomer(item)),
                ToCustomer = customer.ToCustomer.Select(item => ConvertBackParcelAtCustomer(item))
            };
        }
        public static PO.CustomerToList ConvertCustomerToList(BO.CustomerToList customerToList)
        {
            return new PO.CustomerToList
            {
                Id = customerToList.Id,
                Name = customerToList.Name,
                Phone = customerToList.Phone,
                NumParcelSentDelivered = customerToList.NumParcelSentDelivered,
                NumParcelSentNotDelivered = customerToList.NumParcelSentNotDelivered,
                NumParcelReceived = customerToList.NumParcelReceived,
                NumParcelWayToCustomer = customerToList.NumParcelWayToCustomer
            };
        }
        public static BO.CustomerToList ConvertBackCustomerToList(PO.CustomerToList customerToList)
        {
            return new BO.CustomerToList
            {
                Id = customerToList.Id,
                Name = customerToList.Name,
                Phone = customerToList.Phone,
                NumParcelSentDelivered = customerToList.NumParcelSentDelivered,
                NumParcelSentNotDelivered = customerToList.NumParcelSentNotDelivered,
                NumParcelReceived = customerToList.NumParcelReceived,
                NumParcelWayToCustomer = customerToList.NumParcelWayToCustomer
            };
        }
        public static BO.Customer ConvertAddCustomer(PO.CustomerAdd customer)
        {
            return new()
            {
                Id = (int)customer.Id,
                Location = ConvertBackLocation(customer.Location),
                Name = customer.Name,
                Phone = customer.Phone,
            };
        }
        public static ParcelInTransfer ConvertParcelInTransfer(BO.ParcelInTransfer parcel)
        {
            return new()
            {
                Id = parcel.Id,
                Piority = (Priorities)parcel.Priority,
                ParcelState = parcel.ParcelState,
                CollectionPoint = ConvertLocation(parcel.CollectionPoint),
                CustomerReceives = ConvertCustomerInParcel(parcel.CustomerReceives),
                CustomerSender = ConvertCustomerInParcel(parcel.CustomerSender),
                DeliveryDestination = ConvertLocation(parcel.DeliveryDestination),
                TransportDistance = parcel.TransportDistance,
                Weight = (WeightCategories)parcel.WeightCategory
            };
        }
        public static ParcelToList ConvertParcelAtCustomerToList(ParcelAtCustomer parcel)
        {
            return GetParcels().FirstOrDefault(par => par.Id == parcel.Id);
        }
        public static BO.ParcelInTransfer ConvertBackParcelInTransfer(ParcelInTransfer parcel)
        {
            return new()
            {
                Id = parcel.Id,
                Priority = (BO.Priorities)parcel.Piority,
                ParcelState = parcel.ParcelState,
                CollectionPoint = ConvertBackLocation(parcel.CollectionPoint),
                CustomerReceives = ConvertBackCustomerInParcel(parcel.CustomerReceives),
                CustomerSender = ConvertBackCustomerInParcel(parcel.CustomerSender),
                DeliveryDestination = ConvertBackLocation(parcel.DeliveryDestination),
                TransportDistance = parcel.TransportDistance,
                WeightCategory = (BO.WeightCategories)parcel.Weight
            };
        }
        public static void AddStation(StationAdd station)
        {
            ibal.AddStation(ConverBackStationAdd(station));
        }
        public static void UpdateStation(int id, string name, int chargeSlots)
        {
            ibal.UpdateStation(id, name, chargeSlots);
        }
        public static Station GetStation(int id)
        {
            return ConverterStation(ibal.GetStation(id));
        }
        public static void DeleteStation(int id)
        {
            ibal.DeleteStation(id);
        }
        public static bool IsNotActiveStation(int id) => ibal.IsNotActiveStation(id);
        public static IEnumerable<StationToList> GetStations()
        {
            return ibal.GetActiveStations().Select(item => ConverterStationToList(item));
        }
        public static IEnumerable<StationToList> GetStaionsWithEmptyChargeSlots()
        {
            return ibal.GetStaionsWithEmptyChargeSlots((int chargeSlots) => chargeSlots > 0).Select(item => ConverterStationToList(item));
        }
        public static BO.Station ConverterBackStation(Station station)
        {
            return new BO.Station()
            {
                Id = station.Id,
                Name = station.Name,
                Location = ConvertBackLocation(station.Location),
                AvailableChargingPorts = station.EmptyChargeSlots,
                DroneInChargings = station.DroneInChargings.Select(item => ConvertBackDroneCharging(item)).ToList()
            };
        }
        public static Station ConverterStation(BO.Station station)
        {
            return new Station()
            {
                Id = station.Id,
                Name = station.Name,
                Location = ConvertLocation(station.Location),
                EmptyChargeSlots = station.AvailableChargingPorts,
                DroneInChargings = station.DroneInChargings.Select(item => ConvertDroneCharging(item))
            };
        }
        public static StationToList ConverterStationToList(BO.StationToList station)
        {
            return new StationToList()
            {
                Id = station.Id,
                Name = station.Name,
                ChargeSlots = station.EmptyChargeSlots + station.FullChargeSlots
            };
        }
        public static BO.Station ConverBackStationAdd(PO.StationAdd station)
        {
            return new()
            {
                Id = (int)station.Id,
                Name = station.Name,
                AvailableChargingPorts = (int)station.EmptyChargeSlots,
                Location = ConvertBackLocation(station.Location)

            };
        }
        public static Parcel ConvertParcel(BO.Parcel parcel)
        {
            return new Parcel()
            {
                Id = parcel.Id,
                Weight = (WeightCategories)parcel.Weight,
                Piority = (Priorities)parcel.Priority,
                DeliveryTime = parcel.DeliveryTime,
                AssignmentTime = parcel.AssignmentTime,
                CollectionTime = parcel.CollectionTime,
                CreationTime = parcel.CreationTime,
                CustomerReceives = ConvertCustomerInParcel(parcel.CustomerReceives),
                CustomerSender = ConvertCustomerInParcel(parcel.CustomerSender),
                Drone = parcel.Drone != null ? ConvertDroneWithParcel(parcel.Drone) : null,
            };
        }
        public static BO.Parcel ConvertBackParcel(Parcel parcel)
        {
            return new()
            {
                Id = parcel.Id,
                Weight = (BO.WeightCategories)parcel.Weight,
                Priority = (BO.Priorities)parcel.Piority,
                DeliveryTime = parcel.DeliveryTime,
                AssignmentTime = parcel.AssignmentTime,
                CollectionTime = parcel.CollectionTime,
                CreationTime = parcel.CreationTime,
                CustomerReceives = ConvertBackCustomerInParcel(parcel.CustomerReceives),
                CustomerSender = ConvertBackCustomerInParcel(parcel.CustomerSender),
                Drone = ConvertBackDroneWithParcel(parcel.Drone)
            };
        }
        private static ParcelToList ConvertParcelToList(BO.ParcelToList parcel)
        {
            return new()
            {
                Id = parcel.Id,
                PackageMode = (PackageModes)parcel.PackageMode,
                Piority = (Priorities)parcel.Piority,
                Weight = (WeightCategories)parcel.Weight,
                CustomerReceives = ConvertCustomer(parcel.CustomerReceives),
                CustomerSender = ConvertCustomer(parcel.CustomerSender)
            };
        }
        public static void AddParcel(ParcelAdd parcel)
        {
            ibal.AddParcel(ConvertBackParcelAdd(parcel));
        }
        public static void DeleteParcel(int id)
        {
            ibal.DeleteParcel(id);
        }
        public static bool IsNotActiveParcel(int id) => ibal.IsNotActiveParcel(id);
        public static Parcel GetParcel(int id) => ConvertParcel(ibal.GetParcel(id));
        public static IEnumerable<ParcelToList> GetParcels() => ibal.GetActiveParcels().Select(parcel => ConvertParcelToList(parcel));
        public static IEnumerable<ParcelToList> GetParcelsNotAssignedToDrone => ibal.GetParcelsNotAssignedToDrone((int num) => num == 0).Select(parcel => ConvertParcelToList(parcel));
        public static BO.Parcel ConvertBackParcelAdd(ParcelAdd parcel)
        {
            return new()
            {
                Priority = (BO.Priorities)parcel.Piority,
                Weight = (BO.WeightCategories)parcel.Weight,
                CustomerReceives = ConvertBackCustomerInParcel(new() { Id = parcel.CustomerReceives }),
                CustomerSender = ConvertBackCustomerInParcel(new() { Id = parcel.CustomerSender })
            };
        }
        public static PO.ParcelAtCustomer ConvertParcelAddToParcelAtCustomer(PO.ParcelAdd parcelAdd)
        {
            return new PO.ParcelAtCustomer()
            {
                Weight = (PO.WeightCategories)parcelAdd.Weight,
                Piority = (PO.Priorities)parcelAdd.Piority,
                PackageMode = PO.PackageModes.ASSOCIATED,

            };
        }
        public static BO.ParcelAtCustomer ConvertBackParcelAtCustomer(PO.ParcelAtCustomer parcelAtCustomer)
        {
            return new BO.ParcelAtCustomer()
            {
                Id = parcelAtCustomer.Id,
                WeightCategory = (BO.WeightCategories)parcelAtCustomer.Weight,
                Priority = (BO.Priorities)parcelAtCustomer.Piority,
                State = (BO.PackageModes)parcelAtCustomer.PackageMode,
                Customer = ConvertBackCustomerInParcel(parcelAtCustomer.Customer)
            };
        }
        public static PO.ParcelAtCustomer ConvertParcelAtCustomer(BO.ParcelAtCustomer parcelAtCustomer)
        {
            return new PO.ParcelAtCustomer()
            {
                Id = parcelAtCustomer.Id,
                Weight = (PO.WeightCategories)parcelAtCustomer.WeightCategory,
                Piority = (PO.Priorities)parcelAtCustomer.Priority,
                PackageMode = (PO.PackageModes)parcelAtCustomer.State,
                Customer = ConvertCustomerInParcel(parcelAtCustomer.Customer)
            };
        }
        public static BO.Location ConvertBackLocation(Location location)
        {
            return new BO.Location()
            {
                Longitude = (double)location.Longitude,
                Latitude = (double)location.Latitude
            };
        }
        public static Location ConvertLocation(BO.Location location)
        {
            return new Location()
            {
                Longitude = (double)location.Longitude,
                Latitude = (double)location.Latitude
            };
        }
        public static DroneWithParcel ConvertDroneWithParcel(BO.DroneWithParcel drone)
        {
            return new DroneWithParcel()
            {
                Id = drone.Id,
                ChargingMode = drone.ChargingMode,
                CurrentLocation = ConvertLocation(drone.CurrentLocation)
            };
        }
        public static BO.DroneWithParcel ConvertBackDroneWithParcel(DroneWithParcel drone)
        {
            return new()
            {
                Id = drone.Id,
                ChargingMode = drone.ChargingMode,
                CurrentLocation = ConvertBackLocation(drone.CurrentLocation)
            };
        }
        public static void AddDrone(DroneAdd drone)
        {
            ibal.AddDrone(ConvertBackDroneToAdd(drone), drone.StationId);
        }
        public static void UpdateDrone(int id, string model)
        {
            ibal.UpdateDrone(id, model);
        }
        public static void SendDroneForCharg(int id)
        {

            ibal.SendDroneForCharg(id);
        }
        public static void ReleaseDroneFromCharging(int id)
        {
            ibal.ReleaseDroneFromCharging(id);
        }
        public static void DeleteDrone(int id)
        {
            ibal.DeleteDrone(id);
        }
        public static bool IsNotActiveDrone(int id) => ibal.IsNotActiveDrone(id);
        public static Drone GetDrone(int id)
        {
            try
            {
                return ConvertDrone(ibal.GetDrone(id));
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }

        }
        public static IEnumerable<DroneToList> GetDrones()
        {
            return ibal.GetActiveDrones().Select(item => ConvertDroneToList(item));
        }
        public static void AssingParcelToDrone(int droneId)
        {
            ibal.AssingParcelToDrone(droneId);
        }
        public static void ParcelCollectionByDrone(int droneId)
        {
            ibal.ParcelCollectionByDrone(droneId);
        }
        public static void DeliveryParcelByDrone(int droneId)
        {
            ibal.DeliveryParcelByDrone(droneId);
        }
        public static Drone ConvertDrone(BO.Drone drone)
        {
            return new Drone
            {
                Id = drone.Id,
                Model = drone.Model,
                BattaryMode =(float) drone.BattaryMode,
                DroneState = (DroneState)drone.DroneState,
                Weight = (WeightCategories)drone.WeightCategory,
                Location = ConvertLocation(drone.CurrentLocation),
                Parcel = drone.Parcel == null ? null : ConvertParcelInTransfer(drone.Parcel)
            };
        }
        public static BO.Drone ConvertBackDrone(Drone drone)
        {
            return new BO.Drone
            {
                Id = drone.Id,
                Model = drone.Model,
                BattaryMode = drone.BattaryMode,
                DroneState = (BO.DroneState)drone.DroneState,
                WeightCategory = (BO.WeightCategories)drone.Weight,
                CurrentLocation = ConvertBackLocation(drone.Location),
                Parcel = drone.Parcel == null ? null : ConvertBackParcelInTransfer(drone.Parcel)
            };
        }
        public static DroneToList ConvertDroneToList(BO.DroneToList drone)
        {
            return new DroneToList
            {
                Id = drone.Id,
                DroneModel = drone.DroneModel,
                BatteryState = drone.BatteryState,
                DroneState = (DroneState)drone.DroneState,
                Weight = (WeightCategories)drone.Weight,
                CurrentLocation = ConvertLocation(drone.CurrentLocation),
                ParcelId = drone.ParcelId
            };
        }
        public static BO.Drone ConvertBackDroneToAdd(DroneAdd drone)
        {
            return new()
            {
                Id = drone.Id != null ? (int)drone.Id : 0,
                Model = drone.Model,
                WeightCategory = (BO.WeightCategories)drone.Weight,
                DroneState = (BO.DroneState)drone.DroneState,

            };
        }
        public static BO.DroneInCharging ConvertBackDroneCharging(DroneInCharging droneInCharging)
        {
            return new BO.DroneInCharging()
            {
                Id = droneInCharging.Id,
                ChargingMode = droneInCharging.ChargingMode
            };
        }
        public static DroneInCharging ConvertDroneCharging(BO.DroneInCharging droneInCharging)
        {
            return new DroneInCharging()
            {
                Id = droneInCharging.Id,
                ChargingMode = droneInCharging.ChargingMode
            };
        }
        public static PO.CustomerInParcel ConvertCustomerInParcel(BO.CustomerInParcel customerInParcel)
        {
            return new PO.CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }
        public static BO.CustomerInParcel ConvertBackCustomerInParcel(PO.CustomerInParcel customerInParcel)
        {
            return new BO.CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }
        public static void StartDroneSimulator(int id, Action<int?, int?, int?, int?> update, Func<bool> checkStop)
        {
            ibal.StartDroneSimulator(id, update, checkStop);
        }
    }
}
