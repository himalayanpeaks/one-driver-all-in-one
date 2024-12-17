using Framework.Module;

namespace Device.Interface
{
    public interface IPowerSupply : IDevice
    {
        int AllChannelsOn();
        int AllChannelsOff();
    }
}
