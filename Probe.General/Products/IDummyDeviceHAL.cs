using Device.Interface.HardwareLayer;
using Framework.Libs.Announcer;
using Framework.Libs.Validator;
using Framework.Module;

namespace Probe.General.Products
{
    public interface IDummyDeviceHAL : IDeviceHAL<InternalProbeDataHAL>
    {
        void HALFunction();
    }
}
