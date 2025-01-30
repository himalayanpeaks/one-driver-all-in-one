using OneDriver.Device.Interface.HardwareLayer;
using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;

namespace OneDriver.Probe.General.Products
{
    public interface IDummyDeviceHAL : IDeviceHAL<InternalProbeDataHAL>
    {
        void HALFunction();
    }
}
