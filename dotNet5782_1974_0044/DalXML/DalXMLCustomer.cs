using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DLApi;
using System.Runtime.CompilerServices;

namespace Dal
{
    public sealed partial class DalXml:IDalCustomer
    {
        const string CUSTOMER_PATH = @"XmlCustomer.xml";
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude)
        {
            try
            {
                List<Customer> customers = DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
                if (ExistsIDTaxCheck(customers, id))
                    throw new ThereIsAnObjectWithTheSameKeyInTheListException();
                Customer newCustomer = new();
                newCustomer.Id = id;
                newCustomer.Name = name;
                newCustomer.Phone = phone;
                newCustomer.Latitude = latitude;
                newCustomer.Longitude = longitude;
                newCustomer.IsNotActive = false;
                customers.Add(newCustomer);
                DalXmlService.SaveListToXMLSerializer<Customer>(customers, CUSTOMER_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int id)
        {
            try { 
                List<Customer> customers = DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
                Customer customer = customers.FirstOrDefault(item => item.Id == id);
                customers.Remove(customer);
                customer.IsNotActive = true;
                customers.Add(customer);
                DalXmlService.SaveListToXMLSerializer<Customer>(customers, CUSTOMER_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            try { 
                Customer customer = DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH).FirstOrDefault(item => item.Id == id);
                if (customer.Equals(default(Customer)))
                    throw new KeyNotFoundException("There is no suitable customer in data");
                return customer;
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers()
        {

            try { 
                return DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveCustomer(Customer customer)
        {
            try { 
                List<Customer> customers = DalXmlService.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
                customers.Remove(customer);
                DalXmlService.SaveListToXMLSerializer(customers, CUSTOMER_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        static bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
            if (!lst.Any())
                return false;
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id);
            return !temp.Equals(default(T));
        }
    }
}
