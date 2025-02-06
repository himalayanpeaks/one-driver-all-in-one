using OneDriver.Framework.Module;

namespace OneDriver.Device.Interface.Motor
{
    public interface IMotor : IDevice
    {
        int Run(bool waitTillTravelEnds);
        void Stop();
        int GetLastError();
        string GetErrorMessage(int errorCode);
    }
}
