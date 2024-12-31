using Device.Interface.PowerSupply;
using Framework.Libs.Validator;
using Framework.Module;
using Framework.Module.Parameter;
using PowerSupply.Abstract.Channels;
using System.Collections.ObjectModel;

namespace PowerSupply.Abstract
{
    public abstract class CommonDevice<TDeviceParams, TChannelParams, TChannelProcessData> :
        BaseDeviceWithChannelsPd<TDeviceParams, TChannelParams, TChannelProcessData>, IPowerSupply
        where TDeviceParams : CommonDeviceParams
        where TChannelParams : CommonChannelParams
        where TChannelProcessData : CommonProcessData
    {
        protected CommonDevice(TDeviceParams parameters, IValidator validator, 
            ObservableCollection<BaseChannelWithProcessData<TChannelParams, TChannelProcessData>> elements) : base(parameters, validator, elements)
        {
        }

        public abstract int AllChannelsOff();
        public abstract int AllChannelsOn();

    }
}
