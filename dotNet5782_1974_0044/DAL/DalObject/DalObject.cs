using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Call to quick initialization function
        /// </summary>
        public DalObject()
        {
            DataSorce.Initialize(this);
        }

        /// <summary>
        /// Find if the id is unique in a spesific list
        /// </summary>
        /// <typeparam name="T">the type of list</typeparam>
        /// <param name="lst">the spesific list </param>
        /// <param name="id">the id to check</param>
        void uniqueIDTaxCheck<T>(List<T> lst, int id)
        {
            if (ExistsIDTaxCheck(lst, id))
                throw new Exception();
        }
        bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("id").GetValue(item, null) == id);
            return !(temp.GetType().Equals(default));
        }

    }

}

