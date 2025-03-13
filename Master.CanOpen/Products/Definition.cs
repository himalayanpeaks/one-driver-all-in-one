using System.Runtime.InteropServices;

namespace OneDriver.Master.CanOpen.Products
{
    public class Definition
    {
        public enum e_error_codes
        {
            RET_OK = 0,
            RET_CONNECTION_ERROR = 1,
            RET_READ_ERROR = 2,
            RET_WRITE_ERROR = 3,
        }
    }
}
