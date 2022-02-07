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
        //-----------------------------------------------------------Adding------------------------------------------------------------------------
        /// <summary>
        /// Add a customer to the list of customers
        /// </summary>
        /// <param name="customerBL">The customer for Adding</param>
        public void AddCustomer(Customer customerBL)
        {
            try
            {
                dal.AddCustomer(customerBL.Id, customerBL.Phone, customerBL.Name, customerBL.Location.Longitude, customerBL.Location.Latitude);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {

                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }

        }

        //--------------------------------------------------Return-----------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the requested customer from the data and converts it to BL customer
        /// </summary>
        /// <param name="id">The requested customer id</param>
        /// <returns>A Bl customer to print</returns>
        public Customer GetCustomer(int id)
        {
            try
            {
                return MapCustomer(dal.GetCustomer(id));
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }
        //-------------------------------------------------------Updating-----------------------------------------------------------------------------
        /// <summary>
        /// Update a customer in the customers list
        /// </summary>
        /// <param name="id">the id of the customer</param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void UpdateCustomer(int id, string name, string phone)
        {
            if (name.Equals(string.Empty) && phone.Equals(string.Empty))
                throw new ArgumentNullException("There is not field to update");
            DO.Customer customer;
            try
            {
                customer = dal.GetCustomer(id);
                dal.RemoveCustomer(customer);
                if (name.Equals(string.Empty))
                    name = customer.Name;
                else if (phone.Equals(string.Empty))
                    phone = customer.Phone;
                dal.AddCustomer(id, phone, name, customer.Longitude, customer.Latitude);

            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }


        }
        public void DeleteCustomer(int id)
        {
            dal.DeleteCustomer(id);
        }
        public bool IsActiveCustomer(int id) => dal.GetCustomers().FirstOrDefault(Customer => Customer.Id == id).IsNotActive;

        //-------------------------------------------------Return List-----------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the list of customers  from the data and converts it to station to list
        /// </summary>
        /// <returns>A list of statin to print</returns>
        public IEnumerable<CustomerToList> GetCustomers()
        {
            return dal.GetCustomers().Select(customer => MapCustomerToList(customer));
        }

        public IEnumerable<CustomerToList> GetActiveCustomers() => dal.GetCustomers().Where(Customer => !Customer.IsNotActive).Select(Customer => MapCustomerToList(Customer));


        //-----------------------------------------------Help function-----------------------------------------------------------------------------------
        /// <summary>
        /// Convert a DAL customer to BL customer
        /// </summary>
        /// <param name="parcel">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private Customer MapCustomer(DO.Customer customer)
        {
            return new Customer()
            {
                Id = customer.Id,
                Phone = customer.Phone,
                Name = customer.Name,
                Location = new()
                {
                    Longitude = customer.Longitude,
                    Latitude = customer.Latitude
                },
                FromCustomer = GetAllParcels().Where(Parcel => Parcel.CustomerSender.Id == customer.Id).Select(parcel => ParcelToParcelAtCustomer(parcel, "sender")).ToList(),
                ToCustomer = GetAllParcels().Where(Parcel => Parcel.CustomerReceives.Id == customer.Id).Select(parcel => ParcelToParcelAtCustomer(parcel, "Recive")).ToList()
            };
        }


    }
}



