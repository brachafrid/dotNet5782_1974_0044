using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class ThereIsNoNearbyBaseStationThatTheDroneCanReachException : Exception
        {
            public ThereIsNoNearbyBaseStationThatTheDroneCanReachException():base(){}
            public ThereIsNoNearbyBaseStationThatTheDroneCanReachException(string message) : base(message) { }
            public ThereIsNoNearbyBaseStationThatTheDroneCanReachException(string message, Exception inner) : base(message, inner) { }
            protected ThereIsNoNearbyBaseStationThatTheDroneCanReachException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public override string ToString()
            {
                return "There is no nearby base station that the drone can reach exception";
            }

        }

        [Serializable]
        public class ThereIsAnObjectWithTheSameKeyInTheList : Exception
        {
            public ThereIsAnObjectWithTheSameKeyInTheList() : base() { }
            public ThereIsAnObjectWithTheSameKeyInTheList(string message) : base(message) { }
            public ThereIsAnObjectWithTheSameKeyInTheList(string message, Exception inner) : base(message, inner) { }
            protected ThereIsAnObjectWithTheSameKeyInTheList(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public override string ToString()
            {
                return "An element with the same key already exists in the list";
            }

        }

    }

}
