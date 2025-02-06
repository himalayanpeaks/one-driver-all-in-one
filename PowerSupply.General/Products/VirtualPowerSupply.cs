using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;

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
        public Framework.Module.Definition.DeviceError SetMode(double channelNumber, OneDriver.Device.Interface.PowerSupply.Definition.ControlMode mode)
        {
            throw new NotImplementedException();
        }

        public int NumberOfChannels { get; } = 2;
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
            Mode = new OneDriver.Device.Interface.PowerSupply.Definition.ControlMode[NumberOfChannels];
            Mode[0] = OneDriver.Device.Interface.PowerSupply.Definition.ControlMode.Voltage;
            Mode[1] = OneDriver.Device.Interface.PowerSupply.Definition.ControlMode.Current;
        }

        public OneDriver.Device.Interface.PowerSupply.Definition.ControlMode[] Mode { get; }

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
                if (Mode[channel] == OneDriver.Device.Interface.PowerSupply.Definition.ControlMode.Voltage)
                {
                    voltage = VoltageLimit[channel];
                    current = r.NextDouble() * MaxCurrentInAmpere;
                }
                if (Mode[channel] == OneDriver.Device.Interface.PowerSupply.Definition.ControlMode.Current)
                {
                    current = CurrentLimit[channel];
                    voltage = r.NextDouble() * MaxVoltageInVolts;
                }
                data = new InternalDataHAL(channel, voltage, current);

            }
        }

        public Framework.Module.Definition.DeviceError GetActualAmps(double channelNumber, out double amps)
        {
            throw new NotImplementedException();
        }

        public Framework.Module.Definition.DeviceError AllOff()
        {
            return Framework.Module.Definition.DeviceError.NoError;
        }

        public Framework.Module.Definition.DeviceError AllOn()
        {
            return Framework.Module.Definition.DeviceError.NoError;
        }

        public void AttachToProcessDataEvent(DataEventHandler processDataEventHandler) => DataEvent += processDataEventHandler;

        private bool IsOpen = false;
        public ConnectionError Close()
        {
            IsOpen = false;
            StopProcessDataAnnouncer();
            return ConnectionError.NoError;
        }

        public Framework.Module.Definition.DeviceError GetActualVolts(double channelNumber, out double volts)
        {
            volts = VoltageLimit[(int)channelNumber];
            return Framework.Module.Definition.DeviceError.NoError;
        }

        public Framework.Module.Definition.DeviceError SetDesiredAmps(double channelNumber, double amps)
        {
            amps = VoltageLimit[(int)channelNumber];
            return Framework.Module.Definition.DeviceError.NoError;
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

        public Framework.Module.Definition.DeviceError SetDesiredVolts(double channelNumber, double volts)
        {
            VoltageLimit[(int)channelNumber] = volts;
            return Framework.Module.Definition.DeviceError.NoError;
        }

        public void StartProcessDataAnnouncer() => StartAnnouncingData();


        public void StopProcessDataAnnouncer() => StopAnnouncingData();

        public Framework.Module.Definition.DeviceError Write(string data)
        {
            return Framework.Module.Definition.DeviceError.NoError;
        }

        public Framework.Module.Definition.DeviceError Read(out string readData)
        {
            readData = "0";
            return Framework.Module.Definition.DeviceError.NoError;
        }
    }
}