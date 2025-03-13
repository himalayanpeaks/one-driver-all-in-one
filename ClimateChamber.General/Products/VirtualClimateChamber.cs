using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;

namespace OneDriver.ClimateChamber.General.Products
{
    public class VirtualClimateChamber : DataTunnel<ClimateChamberDataHAL>, IClimateChamberHAL
    {

        public void AttachToProcessDataEvent(DataEventHandler processDataEventHandler) => DataEvent += processDataEventHandler;


        public ConnectionError Close()
        {
            IsOpen = false;
            StopProcessDataAnnouncer();
            return ConnectionError.NoError;
        }

        public ConnectionError Open(string initString, IValidator validator)
        {
            IsOpen = true;
            StartProcessDataAnnouncer();
            return ConnectionError.NoError;
        }

        public void StartProcessDataAnnouncer() => StartAnnouncingData();
        public void StopProcessDataAnnouncer() => StopAnnouncingData();

        private bool IsOpen = false;

        public int NumberOfChannels { get; } = 2;

        double currentTemperature = 0;
        double currentHumidity = 0;
        protected override void FetchDataForTunnel(out ClimateChamberDataHAL data)
        {
            data = new ClimateChamberDataHAL();
            //Example logic to generate process data
            if (IsOpen)
            {
                
                currentHumidity++;
                currentTemperature++;
                
                if(ReadDesiredTemperature() - 1 > ReadCurrentTemperature() ||
                   currentTemperature <= ReadDesiredTemperature() + 1)
                data = new ClimateChamberDataHAL(ReadCurrentTemperature(), ReadCurrentHumidity(), 
                    HasReachedDesiredTemperature(), HasReachedDesiredHumidity());
            }
        }

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
        => currentTemperature++;
        

        public double ReadCurrentHumidity()
            => currentHumidity++;

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
