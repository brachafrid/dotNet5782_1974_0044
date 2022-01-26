using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{
    public sealed partial class DalXml
    {
        const string CUSTOMER_PATH = @"XMLCustomer.xml";
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude)
        {
            List<Customer> customers = XMLTools.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
            if (ExistsIDTaxCheck(customers, id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException();
            Customer newCustomer = new();
            newCustomer.Id = id;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Latitude = latitude;
            newCustomer.Longitude = longitude;
            newCustomer.IsDeleted = false;
            customers.Add(newCustomer);
            XMLTools.SaveListToXMLSerializer<Customer>(customers, CUSTOMER_PATH);
        }

        public void DeleteCustomer(int id)
        {
            List<Customer> customers = XMLTools.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
            Customer customer = customers.FirstOrDefault(item => item.Id == id);
            customers.Remove(customer);
            customer.IsDeleted = true;
            customers.Add(customer);
            XMLTools.SaveListToXMLSerializer<Customer>(customers, CUSTOMER_PATH);
        }

        public Customer GetCustomer(int id)
        {
            Customer customer = XMLTools.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH).FirstOrDefault(item => item.Id == id);
            if (customer.Equals(default(Customer)) || customer.IsDeleted == true)
                throw new KeyNotFoundException("There is no suitable customer in data");
            return customer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return XMLTools.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH).Where(c => c.IsDeleted == false);
        }

        public void RemoveCustomer(Customer customer)
        {
            List<Customer> customers = XMLTools.LoadListFromXMLSerializer<Customer>(CUSTOMER_PATH);
            customers.Remove(customer);
            XMLTools.SaveListToXMLSerializer<Customer>(customers, CUSTOMER_PATH);
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
