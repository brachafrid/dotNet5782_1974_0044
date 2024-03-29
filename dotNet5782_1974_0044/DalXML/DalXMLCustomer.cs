﻿using DLApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dal
{
    public sealed partial class DalXml : IDalCustomer
    {
        const string CUSTOMER_PATH = "XmlCustomer.xml";
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude)
        {
            try
            {
                List<Customer> customers = DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
                if (IsExistsIDTaxCheck(customers, id))
                    throw new ThereIsAnObjectWithTheSameKeyInTheListException(id);
                customers.Add(new()
                {
                    Id = id,
                    Name = name,
                    Phone = phone,
                    Latitude = latitude,
                    Longitude = longitude,
                    IsNotActive = false
                });
                DalXmlService.SaveListToXMLSerializer(customers, CUSTOMER_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int id)
        {
            try
            {
                List<Customer> customers = DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
                Customer customer = customers.FirstOrDefault(item => item.Id == id);
                if (customer.Equals(default(Customer)))
                    throw new KeyNotFoundException($"the customer id {id} not exsits in data");
                customers.Remove(customer);
                customer.IsNotActive = true;
                customers.Add(customer);
                DalXmlService.SaveListToXMLSerializer(customers, CUSTOMER_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            try
            {
                Customer customer = DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH).FirstOrDefault(item => item.Id == id);
                if (customer.Equals(default(Customer)))
                    throw new KeyNotFoundException("There is no suitable customer in data");
                return customer;
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers()
        {
            try
            {
                return DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer, string name, string phone)
        {
            try
            {
                List<Customer> customers = DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
                customers.Remove(customer);
                if (!name.Equals(string.Empty))
                    customer.Name = name;
                if (!phone.Equals(string.Empty))
                    customer.Phone = phone;
                customers.Add(customer);
                DalXmlService.SaveListToXMLSerializer(customers, CUSTOMER_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

       
    }
}
