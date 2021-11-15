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
        public void AddCustomer(int id, string name, string phone, BO.Location location)
        {
            if (ExistsIDTaxCheck(dal.GetCustomers(), id))
            {
                throw new BO.AnElementWithTheSameKeyAlreadyExistsInTheListException();
            }
            dal.addCustomer(id, phone, name, location.Longitude, location.Latitude);
        }
        public Customer GetCustomer(int id)
        {
            try
            {
               return Map(dal.GetCustomer(id));
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
            else if()

        }

        IEnumerable<Customer> IblCustomer.GetCustomers()
        {
            return dal.GetCustomers().Select(customer => GetCustomer(customer.Id));
        }

        private Customer Map(IDAL.DO.Customer customer)
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
    }
}



