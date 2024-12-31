using Framework.Base;
using Framework.Module.Parameter;
using Framework.ModuleBuilder;

namespace Framework.Module.ViewModel
{
    public abstract class BaseDeviceWithChannelsPdViewModel<TParams, TChannelParams, TChannelProcessData> : BaseDeviceViewModel<TParams>
        where TParams : BaseDeviceParam
        where TChannelParams : BaseChannelParam
        where TChannelProcessData : BaseProcessData
    {
        public readonly BaseDeviceWithChannelsPd<TParams, TChannelParams, TChannelProcessData> Device;

        protected BaseDeviceWithChannelsPdViewModel(BaseDeviceWithChannelsPd<TParams, TChannelParams, TChannelProcessData> device) : base(device)
        {
            Device = device;
        }

    }
}
