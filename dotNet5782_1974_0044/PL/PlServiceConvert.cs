using PL.PO;
using System.Linq;
using System.Threading.Tasks;

namespace PL
{
    public static class PlServiceConvert
    {
        #region customer

        /// <summary>
        /// Convert customer (BO to PO)
        /// </summary>
        /// <param name="customer">BO.Customer</param>
        /// <returns>PO.Customer</returns>
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

        /// <summary>
        /// Convert back customer (PO to BO)
        /// </summary>
        /// <param name="customer">PO.Customer</param>
        /// <returns>BO.Customer</returns>
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

        /// <summary>
        /// Convert customer to list (BO to PO)
        /// </summary>
        /// <param name="customerToList">BO.CustomerToList</param>
        /// <returns>PO.CustomerToList</returns>
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

        /// <summary>
        /// Convert back customer to list (PO to BO)
        /// </summary>
        /// <param name="customerToList">PO.CustomerToList</param>
        /// <returns>BO.CustomerToList</returns>
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

        /// <summary>
        /// Convert add customer (PO.CustomerAdd to BO.Customer)
        /// </summary>
        /// <param name="customer">PO.CustomerAdd</param>
        /// <returns>BO.Customer</returns>
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

        /// <summary>
        /// Convert customer in parcel (BO.CustomerInParcel to PO.CustomerInParcel)
        /// </summary>
        /// <param name="customerInParcel">BO.CustomerInParcel</param>
        /// <returns>PO.CustomerInParcel</returns>
        public static CustomerInParcel ConvertCustomerInParcel(BO.CustomerInParcel customerInParcel)
        {
            return new CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }

        /// <summary>
        /// Convert back customer in parcel (PO.CustomerInParcel to BO.CustomerInParcel)
        /// </summary>
        /// <param name="customerInParcel">PO.CustomerInParcel</param>
        /// <returns>BO.CustomerInParcel</returns>
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

        /// <summary>
        /// Convert parcel in transfer (BO.ParcelInTransfer to PO.ParcelInTransfer)
        /// </summary>
        /// <param name="parcel">BO.ParcelInTransfer</param>
        /// <returns>PO.ParcelInTransfer</returns>
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

        /// <summary>
        /// Convert parcel at customer to list  (PO.ParcelAtCustomer to PO.ParcelToList)
        /// </summary>
        /// <param name="parcel">PO.ParcelAtCustomer</param>
        /// <returns>PO.ParcelToList</returns>
        public static async Task<ParcelToList> ConvertParcelAtCustomerToList(ParcelAtCustomer parcel)
        {
            return (await PLService.GetParcels()).FirstOrDefault(par => par.Id == parcel.Id);
        }

        /// <summary>
        /// Convert parcel (BO.Parcel to PO.Parcel)
        /// </summary>
        /// <param name="parcel">BO.Parcel</param>
        /// <returns>PO.Parcel</returns>
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

        /// <summary>
        /// Convert back parcel (PO.Parcel to BO.Parcel)
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert parcel to list (BO.ParcelToList to PO.ParcelToList)
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert back parcel add (PO.ParcelAdd to BO.Parcel)
        /// </summary>
        /// <param name="parcel">PO.ParcelAdd</param>
        /// <returns>BO.Parcel</returns>
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

        /// <summary>
        /// Convert parcel add to parcel at customer (PO.ParcelAdd to PO.ParcelAtCustomer)
        /// </summary>
        /// <param name="parcelAdd"></param>
        /// <returns></returns>
        public static ParcelAtCustomer ConvertParcelAddToParcelAtCustomer(ParcelAdd parcelAdd)
        {
            return new ParcelAtCustomer()
            {
                Weight = (WeightCategories)parcelAdd.Weight,
                Piority = (Priorities)parcelAdd.Piority,
                PackageMode = PackageModes.ASSOCIATED,

            };
        }

        /// <summary>
        /// Convert back parcel at customer (PO.ParcelAtCustomer to BO.ParcelAtCustomer)
        /// </summary>
        /// <param name="parcelAtCustomer">PO.ParcelAtCustomer</param>
        /// <returns>BO.ParcelAtCustomer</returns>
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

        /// <summary>
        /// Convert parcel at customer (BO.ParcelAtCustomer to PO.ParcelAtCustomer)
        /// </summary>
        /// <param name="parcelAtCustomer">BO.ParcelAtCustomer</param>
        /// <returns>PO.ParcelAtCustomer</returns>
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

        /// <summary>
        /// Convert back parcel in transfer (PO.ParcelInTransfer to BO.ParcelInTransfer)
        /// </summary>
        /// <param name="parcel">PO.ParcelInTransfer</param>
        /// <returns>BO.ParcelInTransfer</returns>
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

        /// <summary>
        /// Converter  back station (PO.Station to BO.Station)
        /// </summary>
        /// <param name="station">PO.Station</param>
        /// <returns>BO.Station</returns>
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

        /// <summary>
        /// Converter station (BO.Station to PO.Station)
        /// </summary>
        /// <param name="station">BO.Station</param>
        /// <returns>PO.Station</returns>
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

        /// <summary>
        /// Converter station to list (BO.StationToList to PO.StationToList)
        /// </summary>
        /// <param name="station">BO.StationToList</param>
        /// <returns>PO.StationToList</returns>
        public static StationToList ConverterStationToList(BO.StationToList station)
        {
            return new StationToList()
            {
                Id = station.Id,
                Name = station.Name,
                ChargeSlots = station.EmptyChargeSlots + station.FullChargeSlots
            };
        }

        /// <summary>
        /// Convert back station add (PO.StationAdd to BO.Station)
        /// </summary>
        /// <param name="station">PO.StationAdd</param>
        /// <returns>BO.Station</returns>
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

        /// <summary>
        /// Convert drone (BO.Drone to PO.Drone)
        /// </summary>
        /// <param name="drone">BO.Drone</param>
        /// <returns>PO.Drone</returns>
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

        /// <summary>
        /// Convert back drone (PO.Drone to BO.Drone)
        /// </summary>
        /// <param name="drone">PO.Drone</param>
        /// <returns>BO.Drone</returns>
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

        /// <summary>
        /// Convert drone to list (BO.DroneToList to PO.DroneToList)
        /// </summary>
        /// <param name="drone">BO.DroneToList</param>
        /// <returns>PO.DroneToList</returns>
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

        /// <summary>
        /// Convert back drone to add (PO.DroneAdd to BO.Drone)
        /// </summary>
        /// <param name="drone">PO.DroneAdd</param>
        /// <returns>BO.Drone</returns>
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

        /// <summary>
        /// Convert back drone charging (PO.DroneInCharging to BO.DroneInCharging)
        /// </summary>
        /// <param name="droneInCharging">PO.DroneInCharging</param>
        /// <returns>BO.DroneInCharging</returns>
        public static BO.DroneInCharging ConvertBackDroneCharging(DroneInCharging droneInCharging)
        {
            return new BO.DroneInCharging()
            {
                Id = droneInCharging.Id,
                ChargingMode = droneInCharging.ChargingMode
            };
        }

        /// <summary>
        /// Convert drone charging (BO.DroneInCharging to PO.DroneInCharging)
        /// </summary>
        /// <param name="droneInCharging">BO.DroneInCharging</param>
        /// <returns>PO.DroneInCharging</returns>
        public static DroneInCharging ConvertDroneCharging(BO.DroneInCharging droneInCharging)
        {
            return new DroneInCharging()
            {
                Id = droneInCharging.Id,
                ChargingMode = droneInCharging.ChargingMode
            };
        }

        /// <summary>
        /// Convert drone with parcel (BO.DroneWithParcel to PO.DroneWithParcel)
        /// </summary>
        /// <param name="drone">BO.DroneWithParcel</param>
        /// <returns>PO.DroneWithParcel</returns>
        public static DroneWithParcel ConvertDroneWithParcel(BO.DroneWithParcel drone)
        {
            return new DroneWithParcel()
            {
                Id = drone.Id,
                ChargingMode = drone.ChargingMode,
                CurrentLocation = ConvertLocation(drone.CurrentLocation)
            };
        }

        /// <summary>
        /// Convert back drone with parcel (PO.DroneWithParcel to BO.DroneWithParcel)
        /// </summary>
        /// <param name="drone">PO.DroneWithParcel</param>
        /// <returns>BO.DroneWithParcel</returns>
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

        /// <summary>
        /// Convert back location (PO.Location to BO.Location)
        /// </summary>
        /// <param name="location">PO.Location</param>
        /// <returns>BO.Location</returns>
        public static BO.Location ConvertBackLocation(Location location)
        {
            return new BO.Location()
            {
                Longitude = (double)location.Longitude,
                Latitude = (double)location.Latitude
            };
        }

        /// <summary>
        /// Convert location (BO.Location to PO.Location)
        /// </summary>
        /// <param name="location">BO.Location</param>
        /// <returns>PO.Location</returns>
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
