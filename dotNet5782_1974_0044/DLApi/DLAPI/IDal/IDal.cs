using System;
using System.Collections.Generic;
using DO;

namespace DLApi
{
    public interface IDal:IDalParcel,IDalDroneCharge,IDalDrone,IDalCustomer,IDalStation
    {     
        public string GetAdministorPasssword();
    }
}
