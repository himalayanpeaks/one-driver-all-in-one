using Device.Interface.HardwareLayer;
using Framework.Libs.Announcer;
using Framework.Libs.Validator;
using Framework.Module;

namespace DummyDevice.General.Products
{

    public interface IDummyDeviceHAL : IDeviceHAL<InternalDummyDeviceDataHAL>
    {
        void HALFunction();
    }
}
