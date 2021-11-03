using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        [Serializable]
       public class AnElementWithTheSameKeyAlreadyExistsInTheListException : ArgumentException
        {
          public  AnElementWithTheSameKeyAlreadyExistsInTheListException()
                :base("An element with the same key already exists in the list")
            {
            }
        }

    }

}
