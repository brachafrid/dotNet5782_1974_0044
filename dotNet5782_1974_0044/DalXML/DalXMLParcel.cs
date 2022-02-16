using DLApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Dal
{
    public sealed partial class DalXml : IDalParcel
    {
        const string PARCEL_PATH = @"XmlParcel.xml";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime? requested = null, DateTime? sceduled = null, DateTime? pickedUp = null, DateTime? delivered = null)
        {
            try
            {
                if (!ExistsIDTaxCheckNotDelited(GetCustomers(), SenderId))
                    throw new KeyNotFoundException($"Sender id {SenderId} not exist in data");
                if (!ExistsIDTaxCheckNotDelited(GetCustomers(), TargetId))
                    throw new KeyNotFoundException($"Target id {TargetId} not exist in data");

                XElement xElementParcel = DalXmlService.LoadXElementToXML(PARCEL_PATH);
                XElement config = DalXmlService.LoadXElementToXML(CONFIG);
                XElement parcelId = config.Elements().Single(elem => elem.Name.ToString().Contains("Parcel"));
                DalXmlService.SaveXElementToXML(config, CONFIG);
                xElementParcel.Add(DalXmlService.ConvertParcelToXElement(new()
                {
                    Id = id == 0 ? int.Parse(parcelId.Value) + 1 : id,
                    SenderId = SenderId,
                    TargetId = TargetId,
                    Weigth = Weigth,
                    Priority = Priority,
                    Requested = requested == null ? DateTime.Now : requested,
                    Sceduled = sceduled,
                    PickedUp = pickedUp,
                    Delivered = delivered,
                    DorneId = droneId,
                }));
                config.SetElementValue(parcelId.Name, int.Parse(parcelId.Value) + 1);
                DalXmlService.SaveXElementToXML(config, CONFIG);
                DalXmlService.SaveXElementToXML(xElementParcel, PARCEL_PATH);
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
                XElement xElementParcel = DalXmlService.LoadXElementToXML(PARCEL_PATH);
                XElement parcel = xElementParcel.Elements().FirstOrDefault(elem => int.Parse(elem.Element("Id").Value) == id);
                if (parcel==default(XElement))
                    throw new KeyNotFoundException($"The parcel id {id} not exsits in data");
                parcel.SetElementValue("IsNotActive", true);
                DalXmlService.SaveXElementToXML(xElementParcel, PARCEL_PATH);
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
                XElement xElementParcel = DalXmlService.LoadXElementToXML(PARCEL_PATH).Elements().FirstOrDefault(elem => int.Parse(elem.Element("Id").Value) == id);
                if (xElementParcel.Equals(default(XElement)))
                    throw new KeyNotFoundException("There is not suitable parcel in data");
                return DalXmlService.ConvertXElementToParcel(xElementParcel);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Get the list of the parcels
        /// </summary>
        /// <returns>list of parcels</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels()
        {
            try
            {
                return DalXmlService.LoadXElementToXML(PARCEL_PATH).Elements().Select(elem => DalXmlService.ConvertXElementToParcel(elem));
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone(Predicate<int> notAssign)
        {
            try
            {
                return DalXmlService.LoadXElementToXML(PARCEL_PATH).Elements().Where(elem=>notAssign(int.Parse(elem.Element("DorneId").Value))).Select(elem => DalXmlService.ConvertXElementToParcel(elem));
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel, Parcel newParcel)
        {
            try
            {
                XElement xElementParcels = DalXmlService.LoadXElementToXML(PARCEL_PATH);
                XElement xElementParcel = xElementParcels.Elements().FirstOrDefault(elem => int.Parse(elem.Element("Id").Value) == parcel.Id);
                if (xElementParcels == default(XElement))
                    throw new KeyNotFoundException($"The parcel id {parcel.Id}  not exsits in data");
                xElementParcel.Remove();                   
                xElementParcels.Add(DalXmlService.ConvertParcelToXElement(newParcel));
                DalXmlService.SaveXElementToXML(xElementParcels, PARCEL_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

    }
}
