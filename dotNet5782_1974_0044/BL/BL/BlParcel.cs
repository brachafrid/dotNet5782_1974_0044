using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;


namespace IBL
{
    public partial class BL : IblParcel
    {
        public void AddParcel(Parcel parcel)
        {
            if (!ExistsIDTaxCheck(dal.GetCustomers(), parcel.CustomerSender.Id))
                throw new KeyNotFoundException("sender not exist");
            if (!ExistsIDTaxCheck(dal.GetCustomers(), parcel.CustomerReceives.Id))
                throw new KeyNotFoundException("target not exist");
            dal.ParcelsReception(parcel.CustomerSender.Id, parcel.CustomerReceives.Id, (IDAL.DO.WeightCategories)parcel.Weight, (IDAL.DO.Priorities)parcel.Priority);
        }
        public IEnumerable<ParcelToList> GetParcelsNotAssignedToDrone()
        {
            return dal.GetParcelsNotAssignedToDrone().Select(parcel => mapParcelToList(parcel));
        }
        public Parcel GetParcel(int id)
        {
            if (!ExistsIDTaxCheck(dal.GetParcels(), id))
                throw new KeyNotFoundException();
            return mapParcel(dal.GetParcel(id));
        }
        public IEnumerable<ParcelToList> GetParcels()
        {
            return dal.GetParcels().Select(parcel => mapParcelToList(parcel));
        }
        private ParcelAtCustomer ParcelToParcelAtCustomer(Parcel parcel, string type)
        {
            ParcelAtCustomer newParcel = new ParcelAtCustomer
            {
                Id = parcel.Id,
                WeightCategory = parcel.Weight,
                Priority = parcel.Priority,
                DroneStatus = drones.Find(drone => drone.Id == parcel.Drone.Id).DroneStatus
            };
            if (type == "sender")
            {
                newParcel.Customer = new CustomerInParcel()
                {
                    Id = parcel.CustomerReceives.Id,
                    Name = parcel.CustomerReceives.Name
                };
            }
            else
            {
                newParcel.Customer = new CustomerInParcel()
                {
                    Id = parcel.CustomerSender.Id,
                    Name = parcel.CustomerSender.Name
                };
            }

            return newParcel;
        }

        private ParcelInTransfer CreateParcelInTransfer(int id)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(id);
            IDAL.DO.Customer sender = dal.GetCustomer(parcel.SenderId);
            IDAL.DO.Customer target = dal.GetCustomer(parcel.TargetId);
            return new ParcelInTransfer
            {
                Id = id,
                WeightCategory = (BO.WeightCategories)parcel.Weigth,
                Priority = (BO.Priorities)parcel.Priority,
                ParcelStatus = !parcel.PickedUp.Equals(default),
                CollectionPoint = new BO.Location() { Longitude = sender.Longitude, Latitude = sender.Latitude },
                DeliveryDestination = new BO.Location() { Longitude = target.Longitude, Latitude = target.Latitude },
                TransportDistance = Distance(new Location() { Longitude = sender.Longitude, Latitude = sender.Latitude }, new Location() { Longitude = sender.Longitude, Latitude = sender.Latitude }),
                CustomerSender = new CustomerInParcel() { Id = sender.Id, Name = sender.Name },
                CustomerReceives = new CustomerInParcel() { Id = target.Id, Name = target.Name }
            };
        }

        private Dictionary<ParcelToList,double> creatParcelListToAssign(DroneToList aviableDrone)
        {
            double minDistance;
            Dictionary<ParcelToList,double> parcels = new Dictionary<ParcelToList, double>();
            foreach (var item in dal.GetParcels())
            {
                if (item.DorneId != 0 &&  (WeightCategories)item.Weigth<=aviableDrone.Weight && calculateElectricity(aviableDrone, mapParcelToList(item),out minDistance) <= aviableDrone.BatteryStatus)
                {
                    parcels.Add(mapParcelToList(item),minDistance);
                    
                }     
            }
            return parcels;
        }
        private ParcelToList mapParcelToList(IDAL.DO.Parcel parcel)
        {
            PackageModes PackageMode;
            if (!parcel.Delivered.Equals(default))
                PackageMode = PackageModes.PROVIDED;
            else if (!parcel.PickedUp.Equals(default))
                PackageMode = PackageModes.COLLECTED;
            else if (!parcel.Sceduled.Equals(default))
                PackageMode = PackageModes.ASSOCIATED;
            else
                PackageMode = PackageModes.DEFINED;
            return new ParcelToList()
            {
                Id = parcel.Id,
                CustomerReceives = GetCustomer(parcel.TargetId),
                CustomerSender = GetCustomer(parcel.SenderId),
                Weight = (BO.WeightCategories)parcel.Weigth,
                Piority = (BO.Priorities)parcel.Priority,
                PackageMode = PackageMode
            };
        }
        private Parcel mapParcel(IDAL.DO.Parcel parcel)
        {
            DroneToList tmpDrone = drones.FirstOrDefault(drone => drone.Id == parcel.DorneId);
            if (tmpDrone.Equals(default))
                throw new KeyNotFoundException();
            return new Parcel()
            {
                Id = parcel.Id,
                CustomerReceives = MapCustomerInParcel(dal.GetCustomer(parcel.TargetId)),
                CustomerSender = MapCustomerInParcel(dal.GetCustomer(parcel.SenderId)),
                Weight = (BO.WeightCategories)parcel.Weigth,
                Priority = (BO.Priorities)parcel.Priority,
                AssignmentTime = parcel.Sceduled,
                CollectionTime = parcel.PickedUp,
                CreationTime = parcel.Requested,
                DeliveryTime = parcel.Delivered,
                Drone = mapDroneWithParcel(tmpDrone)
            };
        }

        private void AssigningDroneToParcel(int parcelId,int droneId)
        {
            IDAL.DO.Parcel parcel=dal.GetParcel(parcelId);
            dal.RemoveParcel(parcel);
            parcel.DorneId = droneId;
            parcel.Sceduled = DateTime.Now;
            dal.ParcelsReception( parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id);
        }
        private void ParcelcollectionDrone(int parcelId)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
            dal.RemoveParcel(parcel);
            parcel.PickedUp = DateTime.Now;
            dal.ParcelsReception( parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id);
        }
        private void ParcelDeliveredDrone(int parcelId)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
            dal.RemoveParcel(parcel);
            parcel.Delivered = DateTime.Now;
            dal.ParcelsReception( parcel.SenderId, parcel.TargetId, parcel.Weigth, parcel.Priority, parcel.Id);
        }
        private IEnumerable<Parcel> getAllParcels()
        {
            return dal.GetParcels().Select(Parcel => GetParcel(Parcel.Id));
        }
        private double calculateElectricity(DroneToList aviableDrone, ParcelToList parcel,out double minDistance)
        {
            DroneToList tempDrone = aviableDrone;
            double electricity;
            IDAL.DO.Station station;
            electricity = Distance(aviableDrone.CurrentLocation, parcel.CustomerSender.Location) * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE] +
                        Distance(parcel.CustomerSender.Location, parcel.CustomerReceives.Location) * dal.GetElectricityUse()[(int)parcel.Weight + 1];
            tempDrone.BatteryStatus -= electricity;
            station = ClosetStationPossible(dal.GetStations(), tempDrone, out minDistance);
            electricity += Distance(parcel.CustomerReceives.Location,
                         new Location() { Latitude = station.Latitude, Longitude = station.Longitude }) * dal.GetElectricityUse()[(int)DroneStatuses.AVAILABLE];
            minDistance = Distance(aviableDrone.CurrentLocation, parcel.CustomerSender.Location) + 
                Distance(parcel.CustomerSender.Location, parcel.CustomerReceives.Location) + 
                Distance(parcel.CustomerReceives.Location,new Location() { Latitude = station.Latitude, Longitude = station.Longitude });
            return electricity;
        }

    }
}