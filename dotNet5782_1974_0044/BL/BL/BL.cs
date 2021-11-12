using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {
        IDAL.IDal dal = new DalObject.DalObject();
        bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
             T temp=lst.FirstOrDefault(item => (int)item.GetType().GetProperty("id").GetValue(item, null) == id);
            return !(temp.GetType().Equals(default(T)));
        }

    }
}

