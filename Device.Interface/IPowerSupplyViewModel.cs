using Framework.Module;
using System.Windows.Input;

namespace Device.Interface
{
    public interface IPowerSupplyViewModel : IDeviceViewModel
    {
        ICommand CommandAllChannelsOn { get; }
        ICommand CommandAllChannelsOff { get; }
    }
}
