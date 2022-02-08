using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;
using DLApi;
using System.Runtime.CompilerServices;

namespace Dal
{
    public sealed partial class DalXml:IDalParcel
    {
        const string PARCEL_PATH = @"XmlParcel.xml";
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime? requested = null, DateTime? sceduled = null, DateTime? pickedUp = null, DateTime? delivered = null)
        {
            try
            {
                List<Parcel> parcels = DalXmlService.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH);
                if (!ExistsIDTaxCheckNotDelited(GetCustomers(), SenderId))
                    throw new KeyNotFoundException($"Sender id {SenderId} not exist in data");
                if (!ExistsIDTaxCheckNotDelited(GetCustomers(), TargetId))
                    throw new KeyNotFoundException($"Target id {TargetId} not exist in data");
                Parcel newParcel = new();
                XElement config = DalXmlService.LoadConfigToXML(CONFIG);
                XElement parcelId = config.Elements().Single(elem => elem.Name.ToString().Contains("Parcel"));
                newParcel.Id = id == 0 ? int.Parse(parcelId.Value) + 1 : id;
                config.SetElementValue(parcelId.Name, newParcel.Id);
                DalXmlService.SaveConfigToXML(config, CONFIG);
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
                DalXmlService.SaveListToXMLSerializer(parcels, PARCEL_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            try
            {
                List<Parcel> parcels = DalXmlService.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH);
                Parcel parcel = parcels.FirstOrDefault(item => item.Id == id);
                if (parcel.Equals(default(Parcel)))
                    throw new KeyNotFoundException($"The parcel id {id} not exsits in data");
                parcels.Remove(parcel);
                parcel.IsNotActive = true;
                parcels.Add(parcel);
                DalXmlService.SaveListToXMLSerializer(parcels, PARCEL_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            try
            {
                Parcel parcel = DalXmlService.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH).FirstOrDefault(item => item.Id == id);
                if (parcel.Equals(default(Parcel)))
                    throw new KeyNotFoundException("There is not suitable parcel in data");
                return parcel;
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels()
        {
            try { 
                return DalXmlService.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone(Predicate<int> notAssign)
        {
            try { 
                return DalXmlService.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH).FindAll(item => notAssign(item.DorneId));
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel, Parcel newParcel)
        {
            try { 
                List<Parcel> parcels = DalXmlService.LoadListFromXMLSerializer<Parcel>(PARCEL_PATH);
                parcels.Remove(parcel);
                parcels.Add(newParcel);
                DalXmlService.SaveListToXMLSerializer(parcels, PARCEL_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        static bool ExistsIDTaxCheckNotDelited<T>(IEnumerable<T> lst, int id) where T:IIdentifyable,IActiveable
        {
            if (!lst.Any())
                return false;
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id && !(bool)item.GetType().GetProperty("IsNotActive").GetValue(item));
            return !temp.Equals(default(T));
        }
    }
}
