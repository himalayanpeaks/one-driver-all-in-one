using Framework.Base;
using Framework.Module.Parameter;

namespace Framework.Module.ViewModel
{
    public abstract class BaseDeviceWithProcessDataViewModel<TParams, TProcessData> : BaseDeviceViewModel
        where TParams : BaseDeviceParam
        where TProcessData : IParameter
    {
        public readonly BaseDeviceWithProcessData<TParams, TProcessData> Device;

        protected BaseDeviceWithProcessDataViewModel(BaseDeviceWithProcessData<TParams, TProcessData> device)
        {
            Device = device;
        }

        protected override void Connect()
        {
            var result = Device.Connect(InitString);
            if (result != Definition.DeviceError.NoError)
            {
                // Handle connection error
            }
        }

        protected override void Disconnect()
        {
            var result = Device.Disconnect();
            if (result != Definition.DeviceError.NoError)
            {
                // Handle disconnection error
            }
        }
    }
}
