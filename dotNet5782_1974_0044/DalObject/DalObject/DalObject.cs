
using DLApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Runtime.CompilerServices;

namespace Dal
{
    //singelton pattern step #1 - recommend using "sealed" class to avoid derivated violation
    public sealed partial class DalObject:Singletone<DalObject>, IDal
    {
        /// <summary>
        /// Call to quick initialization function
        /// </summary>
        DalObject()
        {
            DataSorce.Initialize(this);
        }

        /// <summary>
        /// Find if the id is exist in a spesific list
        /// </summary>
        /// <typeparam name="T">The type of list</typeparam>
        /// <param name="lst">The spesific list </param>
        /// <param name="id">The id to check</param>
        static bool ExistsIDTaxCheck<T>(IEnumerable<T> lst, int id)where T: IIdentifyable
        {
            if (!lst.Any())
                return false;
           T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id);
            return !temp.Equals(default(T));
        }
        /// <summary>
        /// C
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        static bool ExistsIDTaxCheckNotDelited<T>(IEnumerable<T> lst, int id)where T:IActiveable,IIdentifyable
        {
            if (!lst.Any())
                return false;
            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id&& !(bool)item.GetType().GetProperty("IsNotActive").GetValue(item));
            return !temp.Equals(default(T));
        }
        /// <summary>
        ///  return the password of the administrator;
        /// </summary>
        /// <returns>password of the administrator</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string GetAdministorPasssword()
        {
            return DataSorce.Administrator_Password;
        }

       
        
    }

}

