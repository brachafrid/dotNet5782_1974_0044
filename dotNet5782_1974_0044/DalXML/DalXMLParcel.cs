using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;

namespace Dal
{
    public sealed partial class DalXml
    {
        const string PARCEL_PATH = @"XmlParcel.xml";
        
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime? requested = null, DateTime? sceduled = null, DateTime? pickedUp = null, DateTime? delivered = null)
        {
            try
            {
                List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH);
                if (!ExistsIDTaxCheckNotDelited(GetCustomers(), SenderId))
                    throw new KeyNotFoundException("Sender not exist");
                if (!ExistsIDTaxCheckNotDelited(GetCustomers(), TargetId))
                    throw new KeyNotFoundException("Target not exist");
                Parcel newParcel = new();

                XElement config = XMLTools.LoadConfigToXML(CONFIG);
                XElement parcelId = config.Elements().Single(elem => elem.Name.ToString().Contains("Parcel"));
                newParcel.Id = id == 0 ? int.Parse(parcelId.Value) + 1 : id;
                config.SetElementValue(parcelId.Name, newParcel.Id);
                XMLTools.SaveConfigToXML(config, CONFIG);

                newParcel.SenderId = SenderId;
                newParcel.TargetId = TargetId;
                newParcel.Weigth = Weigth;
                newParcel.Priority = Priority;
                newParcel.Requested = requested == null ? DateTime.Now : requested;
                newParcel.Sceduled = sceduled;
                newParcel.PickedUp = pickedUp;
                newParcel.Delivered = delivered;
                newParcel.DorneId = droneId;
                parcels.Add(newParcel);
                XMLTools.SaveListToXMLSerializer<Parcel>(parcels, PARCEL_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        public void DeleteParcel(int id)
        {
            try
            {
                List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH);
                Parcel parcel = parcels.FirstOrDefault(item => item.Id == id);
                parcels.Remove(parcel);
                parcel.IsNotActive = true;
                parcels.Add(parcel);
                XMLTools.SaveListToXMLSerializer<Parcel>(parcels, PARCEL_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        public Parcel GetParcel(int id)
        {
            try
            {
                Parcel parcel = XMLTools.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH).FirstOrDefault(item => item.Id == id);
                if (parcel.Equals(default(Parcel)))
                    throw new KeyNotFoundException("There is not suitable parcel in data");
                return parcel;
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        public IEnumerable<Parcel> GetParcels()
        {
            try { 
                return XMLTools.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone(Predicate<int> notAssign)
        {
            try { 
                return XMLTools.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH).FindAll(item => notAssign(item.DorneId));
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        public void RemoveParcel(Parcel parcel)
        {
            try { 
                List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH);
                parcels.Remove(parcel);
                XMLTools.SaveListToXMLSerializer<Parcel>(parcels, PARCEL_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        static bool ExistsIDTaxCheckNotDelited<T>(IEnumerable<T> lst, int id)
        {
            if (!lst.Any())
                return false;
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id && !(bool)item.GetType().GetProperty("IsNotActive").GetValue(item));
            return !temp.Equals(default(T));
        }
    }
}
