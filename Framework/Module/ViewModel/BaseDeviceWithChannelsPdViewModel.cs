using Framework.Base;
using Framework.Module.Parameter;
using Framework.ModuleBuilder;

namespace Framework.Module.ViewModel
{
    public abstract class BaseDeviceWithChannelsPdViewModel<TParams, TChannel, TChannelParams, TChannelProcessData> : BaseDeviceViewModel
        where TParams : MinimumDeviceParamBase
        where TChannel : ChannelWithProcessDataBase<TChannelParams, TChannelProcessData>
        where TChannelParams : MinimumChannelParamBase
        where TChannelProcessData : IParameter
    {
        public readonly BaseDeviceWithChannelsPd<TParams, TChannel, TChannelParams, TChannelProcessData> Device;

        protected BaseDeviceWithChannelsPdViewModel(BaseDeviceWithChannelsPd<TParams, TChannel, TChannelParams, TChannelProcessData> device)
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
