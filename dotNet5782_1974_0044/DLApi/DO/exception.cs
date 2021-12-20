using System;
using System.Runtime.Serialization;


namespace DLApi
{
    namespace DO
    {

        [Serializable]
        public class ThereIsAnObjectWithTheSameKeyInTheListException : Exception
        {
            public ThereIsAnObjectWithTheSameKeyInTheListException() : base() { }
            public ThereIsAnObjectWithTheSameKeyInTheListException(string message) : base(message) { }
            public ThereIsAnObjectWithTheSameKeyInTheListException(string message, Exception inner) : base(message, inner) { }
            protected ThereIsAnObjectWithTheSameKeyInTheListException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public override string ToString()
            {
                return "An element with the same key already exists in the list";
            }

        }

    }

}
