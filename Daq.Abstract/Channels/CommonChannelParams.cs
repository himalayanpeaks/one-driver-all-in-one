using OneDriver.Framework.Module.Parameter;
using System.Net.NetworkInformation;
using OneDriver.Device.Interface.Daq;

namespace OneDriver.Daq.Abstract.Channels
{
    public class CommonChannelParams : BaseChannelParam
    {
        private Definition.ChannelType _type;
        private string _physicalAddress;

        public CommonChannelParams(string name, string physicalAddress, Definition.ChannelType type)
            : base(name)
        {
            _physicalAddress = physicalAddress;
            _type = type;
        }
        public string PhysicalAddress
        {
            get => _physicalAddress;
            set => SetProperty(ref _physicalAddress, value);
        }

        public Definition.ChannelType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

    }
}
