using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : IBlCustomer
    {
        #region Add
        /// <summary>
        /// Add a customer to the list of customers
        /// </summary>
        /// <param name="customerBL">The customer for Adding</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customerBL)
        {
            try
            {
                lock(dal)
                    dal.AddCustomer(customerBL.Id, customerBL.Phone, customerBL.Name, customerBL.Location.Longitude, customerBL.Location.Latitude);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }
        #endregion

        #region Return

        /// <summary>
        /// Retrieves the requested customer from the data and converts it to BL customer
        /// </summary>
        /// <param name="id">The requested customer id</param>
        /// <returns>A Bl customer to print</returns>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            try
            {
                lock (dal)
                    return MapCustomer(dal.GetCustomer(id));
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch(DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Retrieves the list of customers  from the data and converts it to station to list
        /// </summary>
        /// <returns>A list of statin to print</returns>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetCustomers()
        {
            try
            {
                lock (dal)
                    return dal.GetCustomers().Select(customer => MapCustomerToList(customer));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// Get active customers
        /// </summary>
        /// <returns>Active customers</returns>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetActiveCustomers()
        {
            try
            {
                lock (dal)
                    return dal.GetCustomers().Where(Customer => !Customer.IsNotActive).Select(Customer => MapCustomerToList(Customer));
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }

        }
        #endregion

        #region Update
        /// <summary>
        /// Update a customer in the customers list
        /// </summary>
        /// <param name="id">the id of the customer</param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int id, string name, string phone)
        {
            if (name.Equals(string.Empty) && phone.Equals(string.Empty))
                throw new ArgumentNullException("There is not field to update");
        
            try
            {
                lock (dal)
                    dal.UpdateCustomer(dal.GetCustomer(id), name,phone);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="id">id of customer</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int id)
        {
            try
            {
                lock (dal)
                    dal.DeleteCustomer(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch(DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
           
        }
        #endregion

        /// <summary>
        /// Check if customer is not active
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <returns>if customer is not active</returns>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public bool IsNotActiveCustomer(int id)
        {
            try
            {
                lock (dal)
                    return  dal.GetCustomers().Any(customer => customer.Id == id && customer.IsNotActive);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
            
        }
        
    }
}



