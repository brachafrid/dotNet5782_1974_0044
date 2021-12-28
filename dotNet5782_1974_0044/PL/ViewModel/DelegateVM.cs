using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public delegate object Events(object param = null);
    public static class DelegateVM
    {
        public static Events Drone { set; get; }
        public static Events Customer { set; get; }
        public static Events Station { set; get; }
        public static Events Parcel { set; get; }
    }
}
