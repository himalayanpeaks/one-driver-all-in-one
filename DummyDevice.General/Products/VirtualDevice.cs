using Framework.Libs.Announcer;
using Framework.Libs.Validator;
using Framework.Module;

namespace DummyDevice.General.Products
{
    public class VirtualDevice : DataTunnel<InternalDummyDeviceDataHAL>, IDummyDeviceHAL
    {

        public void AttachToProcessDataEvent(DataTunnel<InternalDummyDeviceDataHAL>.DataEventHandler processDataEventHandler) => DataEvent += processDataEventHandler;


        public ConnectionError Close()
        {
            IsOpen = false;
            return ConnectionError.NoError;
        }

        public ConnectionError Open(string initString, IValidator validator)
        {
            IsOpen = true;
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
                int channel = r.Next(0, 1);
                processedData = (int)r.NextInt64();
                data = new InternalDummyDeviceDataHAL(channel, processedData, r.NextSingle().ToString());
            }
        }

        public void HALFunction()
        {
        }
    }
}
