using DLApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dal
{
    public sealed partial class DalXml : IDalCustomer
    {
        const string CUSTOMER_PATH = @"XmlCustomer.xml";

        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="id">customer's id</param>
        /// <param name="phone">customer's phone</param>
        /// <param name="name">customer's name</param>
        /// <param name="longitude">customer's longitude</param>
        /// <param name="latitude">customer's latitude</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude)
        {
            try
            {
                List<Customer> customers = DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
                if (ExistsIDTaxCheck(customers, id))
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

        /// <summary>
        /// Deletes a customer according to id
        /// </summary>
        /// <param name="id">customer's id</param>
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

        /// <summary>
        /// Returns to a specific customer according to id
        /// </summary>
        /// <param name="id">customer's id</param>
        /// <returns>customer</returns>
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

        /// <summary>
        /// Gets the list of the customers
        /// </summary>
        /// <returns>The list of the customers</returns>
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

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customer">customer</param>
        /// <param name="name">new name</param>
        /// <param name="phone">new phone</param>
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

        /// <summary>
        /// Return if specific ID is on the generic list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst">generic list</param>
        /// <param name="id"></param>
        /// <returns>If the requested ID is on the list</returns>
        static bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id) where T : IIdentifyable
        {
            if (!lst.Any())
                return false;
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id);
            return !temp.Equals(default(T));
        }
    }
}
