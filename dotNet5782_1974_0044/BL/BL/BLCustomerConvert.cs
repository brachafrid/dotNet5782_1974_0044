using System.Linq;
using BO;

namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// Convert a DAL customer to BL Customer To List
        /// </summary>
        /// <param name="parcel">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private CustomerToList MapCustomerToList(DO.Customer customer)
        {
            return new CustomerToList()
            {
                Id = customer.Id,
                Phone = customer.Phone,
                Name = customer.Name,
                NumParcelReceived = dal.GetParcels().Count(parcel => parcel.TargetId == customer.Id && parcel.Delivered!=null),
                NumParcelSentDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered!=null),
                NumParcelSentNotDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered==null),
                NumParcelWayToCustomer = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered==null
                && parcel.PickedUp!=null),
                IsNotActive = customer.IsNotActive
            };

        }

        /// <summary>
        /// Convert a DAL customer to BL Customer In Parcel
        /// </summary>
        /// <param name="parcel">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private CustomerInParcel MapCustomerInParcel(DO.Customer customer)
        {
            return new CustomerInParcel()
            {
                Id = customer.Id,
                Name = customer.Name
            };
        }

        /// <summary>
        /// Convert a DAL customer to BL customer
        /// </summary>
        /// <param name="parcel">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private Customer MapCustomer(DO.Customer customer)
        {
            return new Customer()
            {
                Id = customer.Id,
                Phone = customer.Phone,
                Name = customer.Name,
                Location = new()
                {
                    Longitude = customer.Longitude,
                    Latitude = customer.Latitude
                },
                FromCustomer = GetAllParcels().Where(Parcel => Parcel.CustomerSender.Id == customer.Id).Select(parcel => ParcelToParcelAtCustomer(parcel, "sender")).ToList(),
                ToCustomer = GetAllParcels().Where(Parcel => Parcel.CustomerReceives.Id == customer.Id).Select(parcel => ParcelToParcelAtCustomer(parcel, "Recive")).ToList()
            };
        }
    }
}
