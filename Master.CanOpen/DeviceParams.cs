using OneDriver.Master.Abstract;

namespace OneDriver.Master.CanOpen
{
    public class DeviceParams : CommonDeviceParams
    {
        public DeviceParams(string name) : base(name)
        {
            ProtocolId = 2;
        }
    }
}
