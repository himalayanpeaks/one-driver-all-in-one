using CommandsPD4I;
using OneDriver.Device.Interface;
using OneDriver.Motor.Abstract;
using OneDriver.Motor.General.Products;
using OneDriver.Device.Interface.Motor;

namespace OneDriver.Motor.Factory
{
    public class BasicMotorDevice
    {
        public CommonDeviceParams? Parameters { get; set; }
        public IMotor? Methods { get; set; }
        public CommonProcessData? ProcessData { get; set; }
    }
    public class ObjectFactory
    {
        public static BasicMotorDevice CreateDevice(Defines.Devices deviceType)
        {
            BasicMotorDevice device = new BasicMotorDevice();
            switch (deviceType)
            {
                case Defines.Devices.DummyDeviceVirtual:
                    var obj = new General.Device("NanotecMotor", new NanotecValidator(), new Nanotec(new ComMotorCommands()));
                    device.Methods = obj;
                    device.Parameters = obj.Parameters;
                    device.ProcessData = obj.ProcessData;
                    break;
            }
            return device;
        }

    }
}
