using OneDriver.Framework.Module;
using System.Windows.Input;

namespace Device.Interface.PowerSupply
{
    public interface IPowerSupplyViewModel : IDeviceViewModel
    {
        ICommand CommandAllChannelsOn { get; }
        ICommand CommandAllChannelsOff { get; }
    }
}
