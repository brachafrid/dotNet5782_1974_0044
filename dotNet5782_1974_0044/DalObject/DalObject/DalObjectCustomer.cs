using DLApi;
using System.Collections.Generic;
using System.Linq;
using DO;
using System.Runtime.CompilerServices;

namespace Dal
{
    public partial class DalObject:IDalCustomer
    {
        //----------------------------------------------Adding-----------------------------------
        /// <summary>
        /// Gets parameters and create new customer 
        /// </summary>
        /// <param name="phone">The customer`s number phone</param>
        /// <param name="name">The customer`s name</param>
        /// <param name="longitude">>The position of the customer in relation to the longitude</param>
        /// <param name="latitude">>The position of the customer in relation to the latitude</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude)
        {
            if (ExistsIDTaxCheck(DataSorce.Customers, id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(id);
            Customer newCustomer = new ();
            newCustomer.Id = id;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = latitude;
            newCustomer.Longitude = longitude;
            newCustomer.IsNotActive = false;
            DalObjectService.AddEntity(newCustomer);
        }
        //-----------------------------------------Display----------------------------------
        /// <summary>
        /// Prepares the list of customer for display
        /// </summary>
        /// <returns>A list of customer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers() => DalObjectService.GetEntities<Customer>();

        /// <summary>
        /// Find a customer that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested customer</param>
        /// <returns>A customer for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            Customer customer= DalObjectService.GetEntities<Customer>().FirstOrDefault(item => item.Id == id );
            if (customer.Equals(default(Customer)))
                 throw new KeyNotFoundException("There is no suitable customer in data"); 
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
            if (customer.Equals(default))
                throw new KeyNotFoundException("There is no suitable customer in data so nthe deleted failed");
            DalObjectService.RemoveEntity(customer);
            customer.IsNotActive = true;
            DalObjectService.AddEntity(customer);
        }

    }

}
