

namespace BLApi
{
    public static class BLFactory
    {
        public static IBL GetBL()
        {
            return BL.BL.Instance;
        }
    }
}
