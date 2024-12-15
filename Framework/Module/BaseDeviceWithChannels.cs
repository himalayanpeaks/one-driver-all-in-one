using Framework.Base;
using Framework.Module.Parameter;
using System.Collections.ObjectModel;

namespace Framework.Module
{
    public abstract class BaseDeviceWithChannels<TParams, TChannel, TChannelParams> : BaseDevice<TParams>
        where TParams : BaseDeviceParam
        where TChannel : BaseChannel<TChannelParams>
        where TChannelParams : BaseChannelParam
    {

        public BaseDeviceWithChannels(TParams parameters, IValidator validator, ObservableCollection<TChannel> elements) : base(parameters, validator)
        {
            Elements = elements;
        }

        public ObservableCollection<TChannel> Elements { get; set; }

    }
}
