using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL
    {
        /// <summary>
        /// Convert a BL parcel to Parcel At Customer
        /// </summary>
        /// <param name="parcel">The parcel to convert</param>
        /// <param name="type">The type of the customer</param>
        /// <returns>The converted parcel</returns>
        private ParcelAtCustomer ParcelToParcelAtCustomer(Parcel parcel, string type)
        {
            ParcelAtCustomer newParcel = new ParcelAtCustomer
            {
                Id = parcel.Id,
                WeightCategory = parcel.Weight,
                Priority = parcel.Priority,
                Status = parcel.AssignmentTime == default ? PackageModes.DEFINED : parcel.CollectionTime == default ? PackageModes.ASSOCIATED : parcel.DeliveryTime == default ? PackageModes.COLLECTED : PackageModes.PROVIDED
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

        /// <summary>
        /// Convert a DAL parcel to Parcel In Transfer
        /// </summary>
        /// <param name="id">The requested parcel to convert</param>
        /// <returns>The converted parcel</returns>
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

        /// <summary>
        /// Convert a DAL parcel to  Parcel To List
        /// </summary>
        /// <param name="parcel">The parcel to convert</param>
        /// <returns>The converted parcel</returns>
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

        /// <summary>
        /// Creates a dictionary of Parcel To List and the distances the drone is required to travel to pick them up
        /// </summary>
        /// <param name="aviableDrone"></param>
        /// <returns>A dictionary of converted parcels and distance</returns>
        private Dictionary<ParcelToList, double> creatParcelDictionaryToAssign(DroneToList aviableDrone)
        {
            double minDistance;
            Dictionary<ParcelToList, double> parcels = new();
            foreach (var item in dal.GetParcels())
            {
                if (item.DorneId == 0 && (WeightCategories)item.Weigth <= aviableDrone.Weight && calculateElectricity(aviableDrone.CurrentLocation,aviableDrone.BatteryStatus, mapParcelToList(item).CustomerSender.Location, mapParcelToList(item).CustomerReceives.Location, (WeightCategories)item.Weigth, out minDistance) <= aviableDrone.BatteryStatus)
                {
                    parcels.Add(mapParcelToList(item), minDistance);
                }
            }
            return parcels;
        }
    }
}
