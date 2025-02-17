using OneDriver.Daq.Abstract.Channels;
using OneDriver.Device.Interface.Daq;

namespace OneDriver.Daq.General.Channels
{
    public class ChannelParams : CommonChannelParams
    {
        public ChannelParams(string name, string physicalAddress, Definition.ChannelType type) : base(name, physicalAddress, type)
        {
        }
    }
}
