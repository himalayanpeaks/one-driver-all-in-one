using Framework.Module.Parameter;
using Framework.ModuleBuilder;

namespace Framework.Module.ViewModel
{
    public abstract class BaseDeviceWithChannelsViewModel<TParams, TChannel, TChannelParams> : BaseDeviceViewModel
        where TParams : MinimumDeviceParamBase
        where TChannel : ChannelBase<TChannelParams>
        where TChannelParams : MinimumChannelParamBase
    {
        public readonly BaseDeviceWithChannels<TParams, TChannel, TChannelParams> Device;

        protected BaseDeviceWithChannelsViewModel(BaseDeviceWithChannels<TParams, TChannel, TChannelParams> device)
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
