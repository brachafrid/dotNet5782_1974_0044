

namespace BLApi
{
    public static class BLFactory
    {
        /// <summary>
        /// Return instance of BL
        /// </summary>
        /// <returns>IBL</returns>
        public static IBL GetBL()
        {
            return BL.BL.Instance;
        }
    }
}