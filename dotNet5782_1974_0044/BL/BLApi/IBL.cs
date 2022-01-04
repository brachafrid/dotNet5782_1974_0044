namespace BLApi
{
    public interface IBL : IBlDrone, IBlStations, IBlParcel, IBlCustomer
    {
        public string GetAdministorPasssword();
    }
}


