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
        /// Convert a DAL customer to BL Customer To List
        /// </summary>
        /// <param name="parcel">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private CustomerToList MapCustomerToList(IDAL.DO.Customer customer)
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
                && parcel.PickedUp!=null)
            };

        }

        /// <summary>
        /// Convert a DAL customer to BL Customer In Parcel
        /// </summary>
        /// <param name="parcel">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private CustomerInParcel MapCustomerInParcel(IDAL.DO.Customer customer)
        {
            return new CustomerInParcel()
            {
                Id = customer.Id,
                Name = customer.Name
            };
        }
    }
}
