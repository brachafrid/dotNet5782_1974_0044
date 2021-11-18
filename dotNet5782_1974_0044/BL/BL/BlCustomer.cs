using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace IBL
{
    public partial class BL : IblCustomer
    {
        public void AddCustomer(Customer customer)
        {
            if (ExistsIDTaxCheck(dal.GetCustomers(), customer.Id))
            {
                throw new AnElementWithTheSameKeyAlreadyExistsInTheListException();
            }
            dal.AddCustomer(customer.Id, customer.Phone, customer.Name, customer.Location.Longitude, customer.Location.Latitude);
        }
        public Customer GetCustomer(int id)
        {
            try
            {
                return MapCustomer(dal.GetCustomer(id));
            }
            catch
            {
                throw new Exception();
            }
        }
        public void UpdateCustomer(int id, string name, string phone)
        {
            if (ExistsIDTaxCheck(dal.GetCustomers(), id))
                throw new AnElementWithTheSameKeyAlreadyExistsInTheListException();
            if (name.Equals(default) && phone.Equals(default))
                throw new ArgumentNullException("no field to update");
            IDAL.DO.Customer customer = dal.GetCustomer(id);
            dal.RemoveCustomer(customer);
            if (name.Equals(default))
                name = customer.Name;
            else if (phone.Equals(default))
                phone = customer.Phone;
            dal.AddCustomer(id, phone, name, customer.Longitude, customer.Latitude);
        }

       public IEnumerable<CustomerToList> GetCustomers()
        {
            return dal.GetCustomers().Select(customer => MapCustomerToList(customer));
        }

        private Customer MapCustomer(IDAL.DO.Customer customer)
        {
            return new Customer()
            {
                Id = customer.Id,
                Phone = customer.Phone,
                Name = customer.Name,
                Location = new BO.Location()
                {
                    Longitude = customer.Longitude,
                    Latitude = customer.Latitude
                },
                FromCustomer = getAllParcels().Select(parcel => ParcelToParcelAtCustomer(parcel, "sender")).ToList(),
                ToCustomer = getAllParcels().Select(parcel => ParcelToParcelAtCustomer(parcel, "Recive")).ToList()
            };
        }
        private CustomerToList MapCustomerToList(IDAL.DO.Customer customer)
        {
            return new CustomerToList()
            {
                Id = customer.Id,
                Phone = customer.Phone,
                Name = customer.Name,
                NumParcelReceived = dal.GetParcels().Count(parcel => parcel.TargetId == customer.Id && !parcel.Delivered.Equals(default)),
                NumParcelSentDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && !parcel.Delivered.Equals(default)),
                NumParcelSentNotDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default)),
                NumParcelWayToCustomer = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default)
                && !parcel.PickedUp.Equals(default))
            };
           
        }
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



