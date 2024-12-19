using Framework.Module.Parameter;
using Framework.ModuleBuilder;

namespace Framework.Module.ViewModel
{
    public abstract class BaseDeviceWithChannelsViewModel<TParams, TChannel, TChannelParams> : BaseDeviceViewModel<TParams>
        where TParams : BaseDeviceParam
        where TChannel : BaseChannel<TChannelParams>
        where TChannelParams : BaseChannelParam
    {
        public readonly BaseDeviceWithChannels<TParams, TChannel, TChannelParams> Device;

        protected BaseDeviceWithChannelsViewModel(BaseDeviceWithChannels<TParams, TChannel, TChannelParams> device) : base(device)
        {
            Device = device;
        }

    }
}
