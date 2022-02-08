using System;
using System.Runtime.Serialization;



namespace BO
{
    [Serializable]
    public class ThereIsNoNearbyBaseStationThatTheDroneCanReachException : Exception
    {
        public ThereIsNoNearbyBaseStationThatTheDroneCanReachException() : base() { }
        public ThereIsNoNearbyBaseStationThatTheDroneCanReachException(string message) : base(message) { }
        public ThereIsNoNearbyBaseStationThatTheDroneCanReachException(string message, Exception inner) : base(message, inner) { }
        protected ThereIsNoNearbyBaseStationThatTheDroneCanReachException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message + "There is no nearby base station that the drone can reach exception";
        }

    }

    [Serializable]
    public class ThereIsAnObjectWithTheSameKeyInTheListException : Exception
    {
        public int Id { get; set; }

        public ThereIsAnObjectWithTheSameKeyInTheListException() : base() { }
        public ThereIsAnObjectWithTheSameKeyInTheListException(int id) : base() { Id = id; }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message) : base(message) { }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message, Exception inner) : base(message, inner) { }

        public ThereIsAnObjectWithTheSameKeyInTheListException(string message, int id) : this(message) { Id = id; }

        protected ThereIsAnObjectWithTheSameKeyInTheListException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            if(Id!=default)
                return Message + $"An element with the same key already exists in the list , the key: {Id}";
            return Message + "An element with the same key already exists in the list  ";

        }

    }
    [Serializable]
    public class ThereAreAssociatedOrgansException : Exception
    {

        public ThereAreAssociatedOrgansException() : base() { }
        public ThereAreAssociatedOrgansException(string message) : base(message) { }
        public ThereAreAssociatedOrgansException(string message, Exception inner) : base(message, inner) { }
        protected ThereAreAssociatedOrgansException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message + "There are associated organs, Cant delete";
        }

    }
    [Serializable]
    public class DeletedExeption : Exception
    {
        public int Id { get; set; }
        public DeletedExeption() : base() { }
        public DeletedExeption(int id) : base() { Id = id; }
        public DeletedExeption(string message) : base(message) { }
        public DeletedExeption(string message,int id) : base(message) { Id = id; }
        public DeletedExeption(string message, Exception inner) : base(message, inner) { }
        protected DeletedExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return "the element was deleted";
        }

    }
    [Serializable]
    internal class NotExsistSutibleParcelException : Exception
    {
        public NotExsistSutibleParcelException()
        {
        }

        public NotExsistSutibleParcelException(string message) : base(message)
        {
        }

        public NotExsistSutibleParcelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotExsistSutibleParcelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    internal class NotExsistSuitibleStationException : Exception
    {
        public NotExsistSuitibleStationException()
        {
        }

        public NotExsistSuitibleStationException(string message) : base(message)
        {
        }

        public NotExsistSuitibleStationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotExsistSuitibleStationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        public string FilePath { get; set; }
        public Exception XmlException { get; set; }

        public XMLFileLoadCreateException() { }
        public XMLFileLoadCreateException(string filePath, string message) : base(message) { FilePath = filePath; }
        public XMLFileLoadCreateException(string filePath, string message, Exception exception) : base(message)
        {
            FilePath = filePath;
            XmlException = exception;
        }

        public XMLFileLoadCreateException(string message) : base(message) { }

        public XMLFileLoadCreateException(string message, Exception innerException) : base(message, innerException) { }

        protected XMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
    [Serializable]
    public class TheDroneIsNotInChargingException : Exception
    {
        public int DroneId { get; set; }
        public TheDroneIsNotInChargingException() : base() { }
        public TheDroneIsNotInChargingException(int droneId) : base() { DroneId = droneId; }
        public TheDroneIsNotInChargingException(string message) : base(message) { }
        public TheDroneIsNotInChargingException(string message, int droneId) : base(message) { DroneId = droneId; }
        public TheDroneIsNotInChargingException(string message, Exception inner) : base(message, inner) { }
        protected TheDroneIsNotInChargingException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            if (DroneId != default)
                return "An element with the same key already exists in the list";
            return $"An element with the same key already exists in the list , the drone id is:{DroneId} ";
        }

    }
    [Serializable]
    public class InvalidDroneStateException : Exception
    {
        public InvalidDroneStateException()
        {
        }

        public InvalidDroneStateException(string message) : base(message)
        {
        }

        public InvalidDroneStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDroneStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class InvalidParcelStateException : Exception
    {
        public InvalidParcelStateException()
        {
        }

        public InvalidParcelStateException(string message) : base(message)
        {
        }

        public InvalidParcelStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidParcelStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}


