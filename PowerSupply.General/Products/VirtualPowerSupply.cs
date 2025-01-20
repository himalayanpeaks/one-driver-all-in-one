using Device.Interface.PowerSupply;
using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using System.Security.Cryptography;

namespace OneDriver.PowerSupply.General.Products
{
    public class VirtualPowerSupply : DataTunnel<InternalDataHAL>, IPowerSupplyHAL
    {
        public double[] VoltageLimit { get; }

        public double[] CurrentLimit { get; }

        public double[] ActualVoltage { get; }

        public double[] ActualCurrent { get; }

        public double MaxCurrentInAmpere { get; }

        public double MaxVoltageInVolts { get; }
        public uint NumberOfChannels { get; } = 2;
        public string Identification { get; }

        public VirtualPowerSupply()
        {
            Identification = "Virtual power supply";
            MaxCurrentInAmpere = 3;
            MaxVoltageInVolts = 40;
            VoltageLimit = new double[NumberOfChannels];
            CurrentLimit = new double[NumberOfChannels];
            ActualVoltage = new double[NumberOfChannels];
            ActualCurrent = new double[NumberOfChannels];
            VoltageLimit[0] = 20;
            VoltageLimit[1] = 40;
            CurrentLimit[0] = 0;
            CurrentLimit[1] = 3;
            Mode = new Definition.ControlMode[NumberOfChannels];
            Mode[0] = Definition.ControlMode.Voltage;
            Mode[1] = Definition.ControlMode.Current;
        }

        public Definition.ControlMode[] Mode { get; }

        private double[] SetVoltage { get; }
        private double[] SetCurrent { get; }
        protected override void FetchDataForTunnel(out InternalDataHAL data)
        {
            data = new InternalDataHAL();
            if (IsOpen)
            {
                double voltage = 0, current = 0;
                Random r = new Random();
                int channel = r.Next(0, 2);
                if (Mode[channel] == Definition.ControlMode.Voltage)
                {
                    voltage = VoltageLimit[channel];
                    current = r.NextDouble() * MaxCurrentInAmpere;
                }
                if (Mode[channel] == Definition.ControlMode.Current)
                {
                    current = CurrentLimit[channel];
                    voltage = r.NextDouble() * MaxVoltageInVolts;
                }
                data = new InternalDataHAL(channel, voltage, current);

            }
        }
        public void AllOff()
        {

        }

        public void AllOn()
        {

        }

        public void AttachToProcessDataEvent(DataEventHandler processDataEventHandler) => DataEvent += processDataEventHandler;

        private bool IsOpen = false;
        public ConnectionError Close()
        {
            IsOpen = false;
            StopProcessDataAnnouncer();
            return ConnectionError.NoError;
        }

        public double GetDesiredVoltage(double channelNumber)
        {
            return VoltageLimit[(int)channelNumber];
        }

        public int GetError()
        {
            throw new NotImplementedException();
        }

        public string GetErrorMessage(int code)
        {
            throw new NotImplementedException();
        }

        public ConnectionError Open(string initString, IValidator validator)
        {
            IsOpen = true;
            StartProcessDataAnnouncer();
            return ConnectionError.NoError;
        }

        public string Read()
        {
            return "0";
        }

        public void SetDesiredVoltage(double channelNumber, double volts)
        {
            VoltageLimit[(int)channelNumber] = volts;
        }

        public void StartProcessDataAnnouncer() => StartAnnouncingData();


        public void StopProcessDataAnnouncer() => StopAnnouncingData();

        public void Write(string data)
        {
        }
    }
}