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


       // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetAllCustomers()
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



