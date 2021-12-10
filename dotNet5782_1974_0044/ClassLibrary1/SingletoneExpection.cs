using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    [Serializable]
   public class SingletoneExpection:Exception
    {
        public SingletoneExpection(string text) : base(text) { }
        public SingletoneExpection(string text, Exception e):base(text, e) { }
        protected SingletoneExpection(SerializationInfo info,StreamingContext context) : base(info, context) { }

    }
}
