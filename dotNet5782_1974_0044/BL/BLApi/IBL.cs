namespace BLApi
{
    public interface IBL : IBlDrone, IBlStations, IBlParcel, IBlCustomer
    {
        /// <summary>
        /// Get administor passsword
        /// </summary>
        /// <returns>administor passsword</returns>
        public string GetAdministorPasssword();
    }
}


