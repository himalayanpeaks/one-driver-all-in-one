using OneDriver.Framework.Module;

namespace Device.Interface.PowerSupply
{
    public interface IPowerSupply : IDevice
    {
        int AllChannelsOn();
        int AllChannelsOff();
    }
}
