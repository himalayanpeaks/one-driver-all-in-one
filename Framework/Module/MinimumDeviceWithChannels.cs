using Framework.Module.Parameter;
using Framework.ModuleBuilder;
using System.Collections.ObjectModel;

namespace Framework.Module
{
    public abstract class MinimumDeviceWithChannels<TParams, TChannel, TChannelParams> : MinimumDevice<TParams>
        where TParams : MinimumDeviceParam
        where TChannel : Channel<TChannelParams>
        where TChannelParams: MinimumChannelParam
    {

        public MinimumDeviceWithChannels(TParams parameters, ObservableCollection<TChannel> elements) : base(parameters)
        {
            Elements = elements;
        }

        public ObservableCollection<TChannel> Elements { get; set; }

    }
}
