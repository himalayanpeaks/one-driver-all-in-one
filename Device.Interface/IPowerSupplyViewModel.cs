using Framework.Module;
using System.Windows.Input;

namespace DeviceInterface
{
    public interface IPowerSupplyViewModel : IDeviceViewModel
    {
        ICommand CommandAllChannelsOn { get; }
        ICommand CommandAllChannelsOff { get; }
    }
}
