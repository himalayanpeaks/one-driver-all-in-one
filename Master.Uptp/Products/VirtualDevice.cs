using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;

namespace OneDriver.Master.Uptp.Products
{
    public class VirtualDevice : DataTunnel<InternalDummyDeviceDataHAL>, IDummyDeviceHAL
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

        protected override void FetchDataForTunnel(out InternalDummyDeviceDataHAL data)
        {
            data = new InternalDummyDeviceDataHAL();
            //Example logic to generate process data
            if (IsOpen)
            {
                int processedData = 0;
                Random r = new Random();
                int channel = r.Next(0, NumberOfChannels);
                processedData = (int)r.NextInt64();
                data = new InternalDummyDeviceDataHAL(channel, processedData, r.NextSingle().ToString());
            }
        }

        public void HALFunction()
        {
        }
    }
}
