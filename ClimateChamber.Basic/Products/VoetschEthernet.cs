using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using OneDriver.Toolbox;
using Serilog;

namespace OneDriver.ClimateChamber.Basic.Products
{
    public class VoetschEthernet :  DataTunnel<ClimateChamberDataHAL>, IClimateChamberHAL
    {
        public VoetschEthernet()
        {
     
        }
        protected override void FetchDataForTunnel(out ClimateChamberDataHAL data)
        {
            throw new NotImplementedException();
        }
        private string IpAddress { get; set; }
        private int Port { get; set; }

        public ConnectionError Open(string initString, IValidator validator)
        {
            IpAddress = validator.ValidationRegex.Match(initString).Result("${ipaddress}");
            Port = int.Parse(validator.ValidationRegex.Match(initString).Result("${port}"));
            try
            {
                // IpAddress.Open();
            }
            catch (ArgumentException e)
            {
                Log.Error(IpAddress + ":" + Port + " " + ConnectionError.InvalidName.GetDescription());
                return ConnectionError.InvalidName;
            }
            catch (UnauthorizedAccessException e)
            {
                Log.Error(IpAddress + ":" + Port + " " + ConnectionError.UnauthorizedAccess.GetDescription());
                return ConnectionError.UnauthorizedAccess;
            }
            catch (System.IO.IOException e)
            {
                Log.Error(IpAddress + ":" + Port + " " + ConnectionError.IOError.GetDescription());
                return ConnectionError.IOError;
            }
            catch (InvalidOperationException e)
            {
                Log.Error(IpAddress + ":" + Port + " " + ConnectionError.InvalidOperation.GetDescription());
                return ConnectionError.InvalidOperation;
            }
            catch (Exception e)
            {
                Log.Error(IpAddress + ":" + Port + " " + ConnectionError.UnknownError.GetDescription());
                return ConnectionError.UnknownError;
            }

            return ConnectionError.NoError;

        }

        public ConnectionError Close()
        {
            try
            {
                //IpAddress.Close();
            }
            catch (Exception e)
            {
                Log.Error(IpAddress + ":" + Port + " " + ConnectionError.ErrorInDisconnecting.GetDescription());
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
