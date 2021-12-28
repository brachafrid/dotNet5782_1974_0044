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
        public static Action Drone { set; get; }
        public static Action Customer { set; get; }
        public static Action Station { set; get; }
        public static Action Parcel { set; get; }
    }
}
