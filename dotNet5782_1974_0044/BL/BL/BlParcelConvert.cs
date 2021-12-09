using System.Collections.Generic;
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
            ParcelAtCustomer newParcel = new()
            {
                Id = parcel.Id,
                WeightCategory = parcel.Weight,
                Priority = parcel.Priority,
                State = parcel.AssignmentTime == null ? PackageModes.DEFINED : parcel.CollectionTime == null ? PackageModes.ASSOCIATED : parcel.DeliveryTime == null ? PackageModes.COLLECTED : PackageModes.PROVIDED
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
                ParcelState = parcel.PickedUp!=null,
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
        private ParcelToList MapParcelToList(IDAL.DO.Parcel parcel)
        {
            PackageModes PackageMode;
            if (parcel.Delivered!=null)
                PackageMode = PackageModes.PROVIDED;
            else if (parcel.PickedUp != null)
                PackageMode = PackageModes.COLLECTED;
            else if (parcel.Sceduled != null)
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
        private Dictionary<ParcelToList, double> CreatParcelDictionaryToAssign(DroneToList aviableDrone)
        {
            Dictionary<ParcelToList, double> parcels = new();
            foreach (var item in dal.GetParcels())
            {
                if (item.DorneId == 0 && (WeightCategories)item.Weigth <= aviableDrone.Weight && CalculateElectricity(aviableDrone.CurrentLocation,aviableDrone.BatteryState, MapParcelToList(item).CustomerSender.Location, MapParcelToList(item).CustomerReceives.Location, (WeightCategories)item.Weigth, out double minDistance) <= aviableDrone.BatteryState)
                {
                    parcels.Add(MapParcelToList(item), minDistance);
                }
            }
            return parcels;
        }
    }
}
