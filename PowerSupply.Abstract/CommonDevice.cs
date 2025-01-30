using OneDriver.Device.Interface.PowerSupply;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using OneDriver.Framework.Module.Parameter;
using OneDriver.PowerSupply.Abstract.Channels;
using System.Collections.ObjectModel;

namespace OneDriver.PowerSupply.Abstract
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
        public int SetVolts(int channelNumber, double volts)
        {
            throw new NotImplementedException();
        }

        public int SetAmps(int channelNumber, double amps)
        {
            throw new NotImplementedException();
        }

        public abstract int AllChannelsOn();

    }
}
