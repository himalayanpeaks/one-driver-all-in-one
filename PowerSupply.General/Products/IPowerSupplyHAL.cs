using OneDriver.Device.Interface.HardwareLayer;
using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using Definition = OneDriver.Device.Interface.PowerSupply.Definition;

namespace OneDriver.PowerSupply.General.Products
{
    public interface IPowerSupplyHAL : IDeviceHAL<InternalDataHAL>, IStringReader, IStringWriter
    {
        public string Identification { get; }
        public OneDriver.Device.Interface.PowerSupply.Definition.ControlMode[] Mode { get; }
        public Framework.Module.Definition.DeviceError SetMode(double channelNumber, OneDriver.Device.Interface.PowerSupply.Definition.ControlMode mode);
        public double MaxCurrentInAmpere { get; }
        public double MaxVoltageInVolts { get; }
        public string GetErrorMessage(int code);
        public Framework.Module.Definition.DeviceError SetDesiredVolts(double channelNumber, double volts);
        public Framework.Module.Definition.DeviceError GetActualVolts(double channelNumber, out double volts);
        public Framework.Module.Definition.DeviceError SetDesiredAmps(double channelNumber, double amps);
        public Framework.Module.Definition.DeviceError GetActualAmps(double channelNumber, out double amps);
        public Framework.Module.Definition.DeviceError AllOff();
        public Framework.Module.Definition.DeviceError AllOn();
    }
}
