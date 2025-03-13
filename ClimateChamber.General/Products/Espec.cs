using System.IO.Ports;
using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using OneDriver.Toolbox;
using Serilog;

namespace OneDriver.ClimateChamber.General.Products
{
    public class Espec :  DataTunnel<ClimateChamberDataHAL>, IClimateChamberHAL
    {
        
        public Espec()
        {
            ComPort = new SerialPort();
        }
        protected override void FetchDataForTunnel(out ClimateChamberDataHAL data)
        {
            throw new NotImplementedException();
        }
        private SerialPort ComPort { get; set; }
        public ConnectionError Open(string initString, IValidator validator)
        {
            ComPort = new SerialPort()
            {
                PortName = validator.ValidationRegex.Match(initString).Result("${com}"),
                BaudRate = int.Parse(validator.ValidationRegex.Match(initString).Result("${baud}")),
                StopBits = StopBits.One,
                Parity = Parity.None,
                NewLine = "\n",
                ReadTimeout = 2500,
            };
            if (ComPort.IsOpen)
                return ConnectionError.AlreadyOpened;
            try
            {
                ComPort.Open();
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

        public void StartProcessDataAnnouncer() => StartAnnouncingData();

        public void StopProcessDataAnnouncer() => StopAnnouncingData();
        

        public void AttachToProcessDataEvent(DataEventHandler processDataEventHandler)
        => DataEvent += processDataEventHandler;

        public int NumberOfChannels { get; } = 1;
        public double MAX_TEMPERATURE { get; } = 100;
        public double MIN_TEMPERATURE { get; } = -40;
        public double ReadDesiredTemperature()
        {
            throw new NotImplementedException();
        }

        public double ReadDesiredHumidity()
        {
            throw new NotImplementedException();
        }

        public double ReadCurrentTemperature()
        {
            throw new NotImplementedException();
        }

        public double ReadCurrentHumidity()
        {
            throw new NotImplementedException();
        }

        public void Start(double desiredTemperature, double desiredHumidity)
        {
            throw new NotImplementedException();
        }

        public int Start(double desiredTemperature)
        {
            throw new NotImplementedException();
        }

        public int StartWithDelay(double desiredTemperature, double desiredHumidity)
        {
            throw new NotImplementedException();
        }

        public int StartWithDelay(double desiredTemperature)
        {
            throw new NotImplementedException();
        }

        public bool HasReachedDesiredTemperature()
        {
            throw new NotImplementedException();
        }

        public bool HasReachedDesiredHumidity()
        {
            throw new NotImplementedException();
        }

        public int Stop()
        {
            throw new NotImplementedException();
        }
    }
}
