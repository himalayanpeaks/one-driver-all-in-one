using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using Definition = OneDriver.Device.Interface.PowerSupply.Definition;

namespace OneDriver.PowerSupply.General.Products
{
    public interface IPowerSupplyHAL : IStringReader, IStringWriter
    {
        ConnectionError Open(string initString, IValidator validator);
        ConnectionError Close();
        public double[] VoltageLimit { get; }
        public double[] CurrentLimit { get; }
        public double[] ActualVoltage { get; }
        public double[] ActualCurrent { get; }
        public double MaxCurrentInAmpere { get; }
        public double MaxVoltageInVolts { get; }
        public string Identification { get; }
        public Definition.ControlMode[] Mode { get; }

        public uint NumberOfChannels { get; }
        public int GetError();
        public string GetErrorMessage(int code);
        public void SetDesiredVoltage(double channelNumber, double volts);
        public double GetDesiredVoltage(double channelNumber);
        public void AllOff();
        public void AllOn();
        public void StartProcessDataAnnouncer();
        public void StopProcessDataAnnouncer();

        public void AttachToProcessDataEvent(DataTunnel<InternalDataHAL>.DataEventHandler processDataEventHandler);
    }
}
