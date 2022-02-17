
using System.Collections.Generic;

using BO;

namespace BLApi
{
    public interface IBlCustomer
    {
        /// <summary>
        /// Add a customer to the list of customers
        /// </summary>
        /// <param name="customerBL">The customer for Adding</param>
        public void AddCustomer(Customer customer);

        /// <summary>
        /// Retrieves the requested customer from the data and converts it to BL customer
        /// </summary>
        /// <param name="id">The requested customer id</param>
        /// <returns>A Bl customer to print</returns>
        public Customer GetCustomer(int id);

        /// <summary>
        /// Retrieves the list of customers  from the data and converts it to station to list
        /// </summary>
        /// <returns>A list of statin to print</returns>
        public IEnumerable<CustomerToList> GetAllCustomers();

        /// <summary>
        /// Get active customers
        /// </summary>
        /// <returns>Active customers</returns>
        public IEnumerable<CustomerToList> GetActiveCustomers();

        /// <summary>
        /// Update a customer in the customers list
        /// </summary>
        /// <param name="id">the id of the customer</param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void UpdateCustomer(int id, string name, string phone);

        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="id">id of customer</param>
        public void DeleteCustomer(int id);

        /// <summary>
        /// Check if customer is not active
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <returns>if customer is not active</returns>
        public bool IsNotActiveCustomer(int id);
    }
}

