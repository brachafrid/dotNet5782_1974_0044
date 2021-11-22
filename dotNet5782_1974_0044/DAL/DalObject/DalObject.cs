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
        /// Find if the id is exist in a spesific list
        /// </summary>
        /// <typeparam name="T">the type of list</typeparam>
        /// <param name="lst">the spesific list </param>
        /// <param name="id">the id to check</param>
        bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
            if (lst.Count() <= 0)
                return false;
           T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id);
            
            return !(temp.Equals(default(T)));
        }

    }

}

