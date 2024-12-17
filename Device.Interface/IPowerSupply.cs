using Framework.Module;

namespace DeviceInterface
{
    public interface IPowerSupply : IDevice
    {
        int AllChannelsOn();
        int AllChannelsOff();
    }
}
