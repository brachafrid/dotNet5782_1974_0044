using PL.PO;
using System.Linq;
using System.Threading.Tasks;

namespace PL
{
    public static class PlServiceConvert
    {
        #region customer
        public static Customer ConvertCustomer(BO.Customer customer)
        {
            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = ConvertLocation(customer.Location),
                FromCustomer = new(customer.FromCustomer.Select(item => ConvertParcelAtCustomer(item))),
                ToCustomer = new(customer.ToCustomer.Select(item => ConvertParcelAtCustomer(item)))
            };
        }
        public static BO.Customer ConvertBackCustomer(Customer customer)
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
        public static CustomerToList ConvertCustomerToList(BO.CustomerToList customerToList)
        {
            return new CustomerToList
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
        public static BO.CustomerToList ConvertBackCustomerToList(CustomerToList customerToList)
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
        public static BO.Customer ConvertAddCustomer(CustomerAdd customer)
        {
            return new()
            {
                Id = (int)customer.Id,
                Location = ConvertBackLocation(customer.Location),
                Name = customer.Name,
                Phone = customer.Phone,
            };
        }
        public static CustomerInParcel ConvertCustomerInParcel(BO.CustomerInParcel customerInParcel)
        {
            return new CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }
        public static BO.CustomerInParcel ConvertBackCustomerInParcel(CustomerInParcel customerInParcel)
        {
            return new BO.CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }
        #endregion

        #region parcel
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
        public static async Task<ParcelToList> ConvertParcelAtCustomerToList(ParcelAtCustomer parcel)
        {
            return (await PLService.GetParcels()).FirstOrDefault(par => par.Id == parcel.Id);
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
        public static ParcelToList ConvertParcelToList(BO.ParcelToList parcel)
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
        public static ParcelAtCustomer ConvertParcelAddToParcelAtCustomer(ParcelAdd parcelAdd)
        {
            return new ParcelAtCustomer()
            {
                Weight = (WeightCategories)parcelAdd.Weight,
                Piority = (Priorities)parcelAdd.Piority,
                PackageMode = PackageModes.ASSOCIATED,

            };
        }
        public static BO.ParcelAtCustomer ConvertBackParcelAtCustomer(ParcelAtCustomer parcelAtCustomer)
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
        public static ParcelAtCustomer ConvertParcelAtCustomer(BO.ParcelAtCustomer parcelAtCustomer)
        {
            return new ParcelAtCustomer()
            {
                Id = parcelAtCustomer.Id,
                Weight = (WeightCategories)parcelAtCustomer.WeightCategory,
                Piority = (Priorities)parcelAtCustomer.Priority,
                PackageMode = (PackageModes)parcelAtCustomer.State,
                Customer = ConvertCustomerInParcel(parcelAtCustomer.Customer)
            };
        }
        public static BO.ParcelInTransfer ConvertBackParcelInTransfer(ParcelInTransfer parcel)
        {
            return new()
            {
                Id = parcel.Id,
                Priority = (BO.Priorities)parcel.Piority,
                ParcelState = parcel.ParcelState,
                CollectionPoint = PlServiceConvert.ConvertBackLocation(parcel.CollectionPoint),
                CustomerReceives = PlServiceConvert.ConvertBackCustomerInParcel(parcel.CustomerReceives),
                CustomerSender = PlServiceConvert.ConvertBackCustomerInParcel(parcel.CustomerSender),
                DeliveryDestination = PlServiceConvert.ConvertBackLocation(parcel.DeliveryDestination),
                TransportDistance = parcel.TransportDistance,
                WeightCategory = (BO.WeightCategories)parcel.Weight
            };
        }
        #endregion

        #region station
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
        public static BO.Station ConverBackStationAdd(StationAdd station)
        {
            return new()
            {
                Id = (int)station.Id,
                Name = station.Name,
                AvailableChargingPorts = (int)station.EmptyChargeSlots,
                Location = ConvertBackLocation(station.Location)

            };
        }
        #endregion

        #region drone
        public static Drone ConvertDrone(BO.Drone drone)
        {
            return new Drone
            {
                Id = drone.Id,
                Model = drone.Model,
                BattaryMode = (float)drone.BattaryMode,
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
                Parcel = drone.Parcel == null ? null : PlServiceConvert.ConvertBackParcelInTransfer(drone.Parcel)
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
        #endregion

        #region location
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
        #endregion
    }
}
