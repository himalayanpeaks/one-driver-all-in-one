using OneDriver.Framework.Module;

namespace OneDriver.Device.Interface.Probe
{
    public interface IProbe : IDevice
    {
        double ReadTemperature(string channel);
    }
}
