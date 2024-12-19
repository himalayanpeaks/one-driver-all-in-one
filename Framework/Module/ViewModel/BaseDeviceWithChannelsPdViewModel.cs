using Framework.Base;
using Framework.Module.Parameter;
using Framework.ModuleBuilder;

namespace Framework.Module.ViewModel
{
    public abstract class BaseDeviceWithChannelsPdViewModel<TParams, TChannel, TChannelParams, TChannelProcessData> : BaseDeviceViewModel<TParams>
        where TParams : BaseDeviceParam
        where TChannel : BaseChannelWithProcessData<TChannelParams, TChannelProcessData>
        where TChannelParams : BaseChannelParam
        where TChannelProcessData : IParameter
    {
        public readonly BaseDeviceWithChannelsPd<TParams, TChannel, TChannelParams, TChannelProcessData> Device;

        protected BaseDeviceWithChannelsPdViewModel(BaseDeviceWithChannelsPd<TParams, TChannel, TChannelParams, TChannelProcessData> device) : base(device)
        {
            Device = device;
        }

    }
}
