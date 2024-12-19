using Framework.Libs.Validator;
using PowerSupply.Abstract;
using PowerSupply.General.Channels;
using System.Collections.ObjectModel;
using System.Threading.Channels;

namespace PowerSupply.General
{
    public class Device : CommonDevice<DeviceParams, Channels.Channel, ChannelParams, ChannelProcessData>
    {
        public Device(DeviceParams parameters, IValidator validator, ObservableCollection<Channels.Channel> elements, ChannelProcessData channelProcessData) : base(parameters, validator, elements, channelProcessData)
        {
        }

        protected override int CloseConnection()
        {
            throw new NotImplementedException();
        }

        protected override int OpenConnection(string initString)
        {
            throw new NotImplementedException();
        }
    }
}
