using System;
using System.Collections.Generic;
using DO;

namespace DLApi
{
    public interface IDal:IDalParcel,IDalDroneCharge,IDalDrone,IDalCustomer,IDalStation
    {
        /// <summary>
        /// Rerurn the administor password
        /// </summary>
        /// <returns>return true if contains the password</returns>
        public string GetAdministorPasssword();
    }
}
