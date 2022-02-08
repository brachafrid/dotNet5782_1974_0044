using System;
using System.Runtime.Serialization;


namespace DO
{

    [Serializable]
    public class ThereIsAnObjectWithTheSameKeyInTheListException : Exception
    {
        public int Id { get; set; }
        public ThereIsAnObjectWithTheSameKeyInTheListException() : base() { }
        public ThereIsAnObjectWithTheSameKeyInTheListException(int id) : base() { Id = id; }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message) : base(message) { }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message,int id) : base(message) { Id = id; }
        public ThereIsAnObjectWithTheSameKeyInTheListException(string message, Exception inner) : base(message, inner) { }
        protected ThereIsAnObjectWithTheSameKeyInTheListException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            if(Id==default)
                 return "An element with the same key already exists in the list";
            return $"An element with the same key already exists in the list , the key is: {Id}";
        }

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
            if (DroneId !=default)
                return "An element with the same key already exists in the list";
            return $"An element with the same key already exists in the list , the drone id is:{DroneId} ";
        }

    }


}


