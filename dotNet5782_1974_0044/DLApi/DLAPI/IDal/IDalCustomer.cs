using System;
using System.Collections.Generic;
using DO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLApi
{
  public  interface IDalCustomer
    {
        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="id">customer's id</param>
        /// <param name="phone">customer's phone</param>
        /// <param name="name">customer's name</param>
        /// <param name="longitude">customer's longitude</param>
        /// <param name="latitude">customer's latitude</param>
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude);
        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customer">customer</param>
        /// <param name="name">new name</param>
        /// <param name="phone">new phone</param>
        public void UpdateCustomer(Customer customer, string name,string phone);
        /// <summary>
        /// Returns to a specific customer according to id
        /// </summary>
        /// <param name="id">customer's id</param>
        /// <returns>customer</returns>
        public Customer GetCustomer(int id);
        /// <summary>
        /// Gets the list of the customers
        /// </summary>
        /// <returns>The list of the customers</returns>
        public IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Deletes a customer according to id
        /// </summary>
        /// <param name="id">customer's id</param>
        public void DeleteCustomer(int id);
    }
}
