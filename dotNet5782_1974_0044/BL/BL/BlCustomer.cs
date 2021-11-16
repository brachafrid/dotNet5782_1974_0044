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
            dal.addCustomer(customer.Id, customer.Phone, customer.Name, customer.Location.Longitude, customer.Location.Latitude);
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
            if (name.Equals(default(string)) && phone.Equals(default(string)))
                throw new ArgumentNullException("no field to update");
            IDAL.DO.Customer customer = dal.GetCustomer(id);
            if (name.Equals(default(string)))
                name = customer.Name;
            else if (phone.Equals(default(string)))
                phone = customer.Phone;
            dal.addCustomer(id, phone, name, customer.Longitude, customer.Latitude);
        }

        IEnumerable<CustomerToList> IblCustomer.GetCustomers()
        {
            return dal.GetCustomers().Select(customer => mapCustomerToList(customer));
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
                FromCustomer = GetParcels().Select(parcel => ParcelToParcelAtCustomer(parcel, "sender")).ToList(),
                ToCustomer = GetParcels().Select(parcel => ParcelToParcelAtCustomer(parcel, "Recive")).ToList()
            };
        }
        private CustomerToList mapCustomerToList(IDAL.DO.Customer customer)
        {
            return new CustomerToList()
            {
                Id = customer.Id,
                Phone = customer.Phone,
                Name = customer.Name,
                NumParcelReceived = dal.GetParcels().Count(parcel => parcel.TargetId == customer.Id && !parcel.PickedUp.Equals(default(DateTime))),
                NumParcelSentDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && !parcel.Delivered.Equals(default(DateTime))),
                NumParcelSentNotDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default(DateTime))),
                NumParcelWayToCustomer
                
            }
        }
    }
}



