using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class KeyAlreadyExistsException : ArgumentException
    {
        public KeyAlreadyExistsException()
            : base("An element with the same key already exists in the collection")
        {

        }
    }
}
