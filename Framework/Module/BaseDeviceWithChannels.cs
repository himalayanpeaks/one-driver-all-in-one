using Framework.Libs.Validator;
using Framework.Module.Parameter;
using System.Collections.ObjectModel;

namespace Framework.Module
{
    public abstract class BaseDeviceWithChannels<TDeviceParams, TChannelParams> : BaseDevice<TDeviceParams>
        where TDeviceParams : BaseDeviceParam
        where TChannelParams : BaseChannelParam
    {

        public BaseDeviceWithChannels(TDeviceParams parameters, IValidator validator, ObservableCollection<BaseChannel<TChannelParams>> elements) : base(parameters, validator)
        {
            Elements = elements;
        }

        public ObservableCollection<BaseChannel<TChannelParams>> Elements { get; set; }

    }
}
