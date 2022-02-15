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

        /// <summary>
        /// Add new parcel
        /// </summary>
        /// <param name="SenderId">Sender Id</param>
        /// <param name="TargetId">Target Id</param>
        /// <param name="Weigth">Parcel Weigth</param>
        /// <param name="Priority">Parcel Priority</param>
        /// <param name="id">Parcel id</param>
        /// <param name="droneId">drone Id</param>
        /// <param name="requested">requested</param>
        /// <param name="sceduled">sceduled</param>
        /// <param name="pickedUp">pickedUp</param>
        /// <param name="delivered">delivered</param>
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
                DalXmlService.SaveXElementToXML(xElementParcel, PARCEL_PATH);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.FilePath, ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Delete parcel
        /// </summary>
        /// <param name="id">parcel's id</param>
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

        /// <summary>
        /// Get parcel
        /// </summary>
        /// <param name="id">parcel'sid</param>
        /// <returns>parcel</returns>
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

        /// <summary>
        /// Get parcels not assigned to drone
        /// </summary>
        /// <param name="notAssign">(Predicate type of int: notAssign</param>
        /// <returns>parcels not assigned to drone</returns>
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

        /// <summary>
        /// Update parcel
        /// </summary>
        /// <param name="parcel">parcel</param>
        /// <param name="newParcel">new parcel</param>
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

        /// <summary>
        /// check if id exist and not delited
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst">generic list</param>
        /// <param name="id">id</param>
        /// <returns>if id exist and active</returns>
        static bool ExistsIDTaxCheckNotDelited<T>(IEnumerable<T> lst, int id) where T : IIdentifyable, IActiveable
        {
            if (!lst.Any())
                return false;
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id && !(bool)item.GetType().GetProperty("IsNotActive").GetValue(item));
            return !temp.Equals(default(T));
        }
    }
}
