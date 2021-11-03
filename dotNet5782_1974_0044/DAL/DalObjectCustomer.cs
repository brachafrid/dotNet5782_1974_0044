﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        //----------------------------------------------Adding-----------------------------------
        /// <summary>
        /// Gets parameters and create new customer 
        /// </summary>
        /// <param name="phone">The customer`s number phone</param>
        /// <param name="name">The customer`s name</param>
        /// <param name="longitude">>The position of the customer in relation to the longitude</param>
        /// <param name="latitude">>The position of the customer in relation to the latitude</param>
        public void addCustomer(int id, string phone, string name, double longitude, double latitude)
        {
            uniqueIDTaxCheck<Customer>(DataSorce.Customers, id);
            Customer newCustomer = new Customer();
            newCustomer.Id = id;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = latitude;
            newCustomer.Longitude = longitude;
            DataSorce.Customers.Add(newCustomer);
        }
        //-----------------------------------------Display----------------------------------
        /// <summary>
        /// Prepares the list of customer for display
        /// </summary>
        /// <returns>A list of customer</returns>
        public IEnumerable<Customer> GetCustomers() => DataSorce.Customers;

        /// <summary>
        /// Find a customer that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested customer</param>
        /// <returns>A customer for display</returns>
        public Customer GetCustomer(int id)
        {
            return DataSorce.Customers.First(item => item.Id == id);
        }
    }

}