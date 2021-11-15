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
        public IEnumerable<IDAL.DO.Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }
        public void UpdateCustomer(int id, string name, string phone)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Customer> IblCustomer.GetCustomers()
        {
            throw new NotImplementedException();
        }

        private BO.Customer Map(IDAL.DO.Customer customer)
        {
            return new BO.Customer()
            {
                Id = customer.Id,
                Phone = customer.Phone,
                Name = customer.Name,
                Location = new BO.Location()
                {
                    Longitude = customer.Longitude,
                    Latitude = customer.Latitude
                },
                FromCustomer = dal.GetParcels().Where(parcel=>parcel.SenderId == customer.Id)
            };
        }
    }
}



