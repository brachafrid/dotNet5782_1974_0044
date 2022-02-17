  using DLApi;
using System.Collections.Generic;
using System.Linq;
using DO;
using System.Runtime.CompilerServices;

namespace Dal
{
    public partial class DalObject:IDalCustomer
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude)
        {
            if (IsExistsIDTaxCheck(DataSorce.Customers, id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(id);
            DalObjectService.AddEntity(new Customer()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Latitude = latitude,
                Longitude = longitude,
                IsNotActive = false
            });
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers() => DalObjectService.GetEntities<Customer>();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            Customer customer= DalObjectService.GetEntities<Customer>().FirstOrDefault(item => item.Id == id );
            if (customer.Equals(default(Customer)))
                 throw new KeyNotFoundException($"There is no suitable customer in data , the id: {id}"); 
            return customer;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer, string name,string phone)
        {
            DalObjectService.RemoveEntity(customer);
            if (!name.Equals(string.Empty))
                customer.Name = name;
            if (!phone.Equals(string.Empty))
                customer.Phone = phone;
            DalObjectService.AddEntity(customer);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int id)
        {
            Customer customer = DalObjectService.GetEntities<Customer>().FirstOrDefault(item => item.Id == id);
            if (customer.Equals(default(Customer)))
                throw new KeyNotFoundException("There is no suitable customer in data so nthe deleted failed");
            DalObjectService.RemoveEntity(customer);
            customer.IsNotActive = true;
            DalObjectService.AddEntity(customer);
        }

    }

}
