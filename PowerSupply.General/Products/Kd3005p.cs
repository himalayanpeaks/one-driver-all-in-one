using OneDriver.Framework.Libs.Announcer;
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
            Mode = new Definition.ControlMode[NumberOfChannels];

        }

        protected override void FetchDataForTunnel(out InternalDataHAL data)
        {
            data = new InternalDataHAL();
            for (var i = 1; i <= NumberOfChannels; i++)
            {
                Thread.Sleep(50);
                Write("VOUT" + i + "?");
                var volts = Read();
                //ActualVoltage[i - 1] = Convert.ToDouble(str, new CultureInfo("en-EN"));
                Thread.Sleep(50);
                Write("IOUT" + i + "?");
                var amps = Read();
                data = new InternalDataHAL(i,
                    Convert.ToDouble(volts, new CultureInfo("en-EN")), Convert.ToDouble(amps, new CultureInfo("en-EN")));
            }
        }

        public string Read()
        {
            try
            {
                ComPort.DiscardOutBuffer();
                return ComPort.ReadLine().ToString(new CultureInfo("en-EN"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "0";
            }
        }

        public void Write(string data)
        {
            ComPort.DiscardInBuffer();
            ComPort.WriteLine(data);
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
                    Identification = Read();
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

        public double[] VoltageLimit { get; }
        public double[] CurrentLimit { get; }
        public double[] ActualVoltage { get; }
        public double[] ActualCurrent { get; }
        public double MaxCurrentInAmpere { get; }
        public double MaxVoltageInVolts { get; }
        public string Identification { get; private set; }
        public Definition.ControlMode[] Mode { get; }
        public uint NumberOfChannels { get; }


        public string GetErrorMessage(int code)
        {
            return "";
        }

        public void SetDesiredVoltage(double channelNumber, double volts)
        {
            var str = "VSET" + channelNumber + ":" + volts.ToString(new CultureInfo("en-EN"));
            Write(str);
        }

        public double GetDesiredVoltage(double channelNumber)
        {
            var str = "VSET" + channelNumber + "?";
            Write(str);
            str = Read();
            return Convert.ToDouble(str, new CultureInfo("en-EN"));
        }

        public void AllOff()
        {
            Write("OUT0");
        }

        public void AllOn()
        {
            Write("OUT1");
        }

        public void StartProcessDataAnnouncer() => StartAnnouncingData();

        public void StopProcessDataAnnouncer() => StopAnnouncingData();

        public void AttachToProcessDataEvent(DataEventHandler processDataEventHandler)
            => DataEvent += processDataEventHandler;
    }
}
