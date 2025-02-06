using OneDriver.Device.Interface.HardwareLayer;
using OneDriver.Framework.Libs.Announcer;


namespace OneDriver.Motor.General.Products
{
    public interface IMotorHAL : IDeviceHAL<InternalDataHAL>
    {
        bool IsMotorReady { get; }
        bool IsReferenceTravelDone { get; }
        void Run(OneDriver.Device.Interface.Motor.Definition.TravelMode mode, OneDriver.Device.Interface.Motor.Definition.DirectionOfRotation direction = OneDriver.Device.Interface.Motor.Definition.DirectionOfRotation.Right, double position = 0, double speed = 0);
        void StopImmediately();
        int GetLastError();
        string GetErrorMessage(int errorCode);
        bool ResetError();
    }
}
