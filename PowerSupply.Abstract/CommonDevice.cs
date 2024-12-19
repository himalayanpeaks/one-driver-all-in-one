using Device.Interface.PowerSupply;
using Framework.Libs.Validator;
using Framework.Module;
using PowerSupply.Abstract.Channels;
using System.Collections.ObjectModel;

namespace PowerSupply.Abstract
{
    public abstract class CommonDevice<TParams, TChannel, TChannelParams, TChannelProcessData> :
        BaseDeviceWithChannelsPd<TParams, TChannel, TChannelParams, TChannelProcessData>, IPowerSupply
        where TParams : CommonDeviceParams
        where TChannel : CommonChannel<TChannelParams, TChannelProcessData>
        where TChannelParams : CommonChannelParams
        where TChannelProcessData : CommonProcessData
    {
        protected CommonDevice(TParams parameters, IValidator validator, ObservableCollection<TChannel> elements, TChannelProcessData channelProcessData) : base(parameters, validator, elements, channelProcessData)
        {
        }

        public int AllChannelsOff()
        {
            throw new NotImplementedException();
        }

        public int AllChannelsOn()
        {
            throw new NotImplementedException();
        }
    }
}
