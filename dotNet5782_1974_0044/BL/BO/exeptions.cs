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

        public ThereIsAnObjectWithTheSameKeyInTheListException() : base() { }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message) : base(message) { }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message, Exception inner) : base(message, inner) { }
        protected ThereIsAnObjectWithTheSameKeyInTheListException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message + "An element with the same key already exists in the list";
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
        public DeletedExeption() : base() { }
        public DeletedExeption(string message) : base(message) { }
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

}


