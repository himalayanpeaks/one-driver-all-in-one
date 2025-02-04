﻿using OneDriver.Framework.Libs.Announcer;
using System.IO.Ports;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using OneDriver.Toolbox;
using Serilog;
using System.Globalization;

namespace OneDriver.PowerSupply.General.Products
{
    public class Kd3005p : DataTunnel<InternalDataHAL>, IPowerSupplyHAL
    {
        public Kd3005p()
        {
            ComPort = new SerialPort
            {
                ReadTimeout = 150,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                BaudRate = 9600
            };
            ComPort.ReadTimeout = 50;
            MaxCurrentInAmpere = 5;
            MaxVoltageInVolts = 30;
            NumberOfChannels = 1;
            Mode = new OneDriver.Device.Interface.PowerSupply.Definition.ControlMode[NumberOfChannels];

        }

        protected override void FetchDataForTunnel(out InternalDataHAL data)
        {
            data = new InternalDataHAL();
            for (var i = 0; i < NumberOfChannels; i++)
            {
                Thread.Sleep(50);
                GetActualVolts(i, out var volts);
                Thread.Sleep(50);
                GetActualAmps(i, out var amps);
                data = new InternalDataHAL(i, volts, amps);
            }
        }

        public Framework.Module.Definition.DeviceError Read(out string readData)
        {
            readData = "";
            try
            {
                ComPort.DiscardOutBuffer();
                readData = ComPort.ReadLine().ToString(new CultureInfo("en-EN"));
            }
            catch (TimeoutException e)
            {
                Log.Error(e.ToString());
                return Framework.Module.Definition.DeviceError.Timeout;
            }
            catch (InvalidOperationException e)
            {
                Log.Error(e.ToString());
                return Framework.Module.Definition.DeviceError.ConnectionError;
            }
            catch (ArgumentException e)
            {
                Log.Error(e.ToString());
                return Framework.Module.Definition.DeviceError.DataIsNull;
            }
            return Framework.Module.Definition.DeviceError.NoError;
        }

        public Framework.Module.Definition.DeviceError Write(string data)
        {
            try
            {
                ComPort.DiscardInBuffer();
                ComPort.WriteLine(data);
            }
            catch (TimeoutException e)
            {
                Log.Error(e.ToString());
                return Framework.Module.Definition.DeviceError.Timeout;
            }
            catch (InvalidOperationException e)
            {
                Log.Error(e.ToString());
                return Framework.Module.Definition.DeviceError.ConnectionError;
            }
            catch(ArgumentException e)
            {
                Log.Error(e.ToString());
                return Framework.Module.Definition.DeviceError.DataIsNull;
            }
            return Framework.Module.Definition.DeviceError.NoError;
        }

        private SerialPort ComPort { get; set; }

        public ConnectionError Open(string initString, IValidator validator)
        {
            ComPort.PortName = validator.ValidationRegex.Match(initString).Groups[1].Value;
            try
            {
                if (!ComPort.IsOpen)
                {
                    ComPort.Open();
                    Write("*IDN?");
                    if((Read(out var identification) is var err) && err != Framework.Module.Definition.DeviceError.NoError)
                    {
                        Log.Error(ComPort.PortName + " " + ConnectionError.CommunicationError.GetDescription());
                        return ConnectionError.CommunicationError;
                    }
                    StartProcessDataAnnouncer();
                }
                else
                    Log.Error(ComPort.PortName + " " + ConnectionError.AlreadyOpened.GetDescription());

            }
            catch (ArgumentException e)
            {
                Log.Error(ComPort.PortName + " " + ConnectionError.InvalidName.GetDescription());
                return ConnectionError.InvalidName;
            }
            catch (UnauthorizedAccessException e)
            {
                Log.Error(ComPort.PortName + " " + ConnectionError.UnauthorizedAccess.GetDescription());
                return ConnectionError.UnauthorizedAccess;
            }
            catch (System.IO.IOException e)
            {
                Log.Error(ComPort.PortName + " " + ConnectionError.IOError.GetDescription());
                return ConnectionError.IOError;
            }
            catch (InvalidOperationException e)
            {
                Log.Error(ComPort.PortName + " " + ConnectionError.InvalidOperation.GetDescription());
                return ConnectionError.InvalidOperation;
            }
            catch (Exception e)
            {
                Log.Error(ComPort.PortName + " " + ConnectionError.UnknownError.GetDescription());
                return ConnectionError.UnknownError;
            }

            return ConnectionError.NoError;
        }

        public ConnectionError Close()
        {
            try
            {
                ComPort.Close();
            }
            catch (Exception e)
            {
                Log.Error(ComPort.PortName + " " + ConnectionError.ErrorInDisconnecting.GetDescription());
                return ConnectionError.ErrorInDisconnecting;
            }
            return ConnectionError.NoError;
        }

        public double MaxCurrentInAmpere { get; }
        public double MaxVoltageInVolts { get; }
        public string Identification { get; private set; }
        public OneDriver.Device.Interface.PowerSupply.Definition.ControlMode[] Mode { get; }
        public Framework.Module.Definition.DeviceError SetMode(double channelNumber, OneDriver.Device.Interface.PowerSupply.Definition.ControlMode mode)
        {
            return Framework.Module.Definition.DeviceError.NoError;
        }

        public uint NumberOfChannels { get; }


        public string GetErrorMessage(int code)
        {
            if (Enum.IsDefined(typeof(Framework.Module.Definition.DeviceError), code))
                return ((Framework.Module.Definition.DeviceError)code).ToString();

            return $"Unknown error code: {code}";
        }


        public Framework.Module.Definition.DeviceError SetDesiredVolts(double channelNumber, double volts)
        {
            var str = "VSET" + (channelNumber + 1) + ":" + volts.ToString(new CultureInfo("en-EN"));
            var err = Write(str);
            if (err != Framework.Module.Definition.DeviceError.NoError)
                return err;
            else
                Write("OUT" + (channelNumber + 1));

            return Framework.Module.Definition.DeviceError.NoError;
        }

        public Framework.Module.Definition.DeviceError GetActualVolts(double channelNumber, out double volts)
        {
            volts = 0;
            string command = $"VOUT{channelNumber + 1}?";

            if (Write(command) is var err && err != Framework.Module.Definition.DeviceError.NoError)
                return err;

            if (Read(out var val) is var readErr && readErr != Framework.Module.Definition.DeviceError.NoError)
                return readErr;

            if (double.TryParse(val, NumberStyles.Float, new CultureInfo("en-EN"), out volts))
                return Framework.Module.Definition.DeviceError.NoError;
            Log.Error(val + " : Invalid response from device.");
            return Framework.Module.Definition.DeviceError.InvalidResponse; 
        }


        public Framework.Module.Definition.DeviceError SetDesiredAmps(double channelNumber, double amps)
        {
            var str = "ISET" + (channelNumber + 1) + ":" + amps.ToString(new CultureInfo("en-EN"));
            var err = Write(str);
            if(err != Framework.Module.Definition.DeviceError.NoError)
                return err;
            else
                Write("OUT" + (channelNumber + 1));
            return Framework.Module.Definition.DeviceError.NoError;
        }

        public Framework.Module.Definition.DeviceError GetActualAmps(double channelNumber, out double amps)
        {
            amps = 0;
            string command = $"IOUT{channelNumber + 1}?";

            if (Write(command) is var err && err != Framework.Module.Definition.DeviceError.NoError)
                return err;

            if (Read(out var val) is var readErr && readErr != Framework.Module.Definition.DeviceError.NoError)
                return readErr;

            if (double.TryParse(val, NumberStyles.Float, new CultureInfo("en-EN"), out amps))
                return Framework.Module.Definition.DeviceError.NoError;
            Log.Error(val + " : Invalid response from device.");
            return Framework.Module.Definition.DeviceError.InvalidResponse;
        }

        public Framework.Module.Definition.DeviceError AllOff()
        {
            return Write("OUT0");
        }

        public Framework.Module.Definition.DeviceError AllOn()
        {
            return Write("OUT1");
        }

        public void StartProcessDataAnnouncer() => StartAnnouncingData();

        public void StopProcessDataAnnouncer() => StopAnnouncingData();

        public void AttachToProcessDataEvent(DataEventHandler processDataEventHandler)
            => DataEvent += processDataEventHandler;
    }
}
