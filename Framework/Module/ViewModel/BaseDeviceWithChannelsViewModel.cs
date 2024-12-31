using Framework.Module.Parameter;
using Framework.ModuleBuilder;

namespace Framework.Module.ViewModel
{
    public abstract class BaseDeviceWithChannelsViewModel<TDeviceParams, TChannelParams> : BaseDeviceViewModel<TDeviceParams>
        where TDeviceParams : BaseDeviceParam
        where TChannelParams : BaseChannelParam
    {
        public readonly BaseDeviceWithChannels<TDeviceParams, TChannelParams> Device;

        protected BaseDeviceWithChannelsViewModel(BaseDeviceWithChannels<TDeviceParams, TChannelParams> device) : base(device)
        {
            Device = device;
        }

    }
}
