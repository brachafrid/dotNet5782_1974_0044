using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
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
