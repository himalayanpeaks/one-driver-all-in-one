using OneDriver.Framework.Module;
using OneDriver.Framework.Module.Parameter;

namespace OneDriver.DeviceParam.Ui
{
    public class DeviceParametersViewModel<TParams> : BaseDeviceViewModel<TParams>
        where TParams : BaseDeviceParam
    {
        public DynamicPropertyWrapper ParametersWrapper { get; }

        public DeviceParametersViewModel(BaseDevice<TParams> device) : base(device)
        {
            ParametersWrapper = new DynamicPropertyWrapper(device.Parameters);
        }
    }

}
