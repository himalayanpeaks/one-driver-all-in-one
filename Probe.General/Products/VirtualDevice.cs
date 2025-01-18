using Framework.Libs.Announcer;
using Framework.Libs.Validator;
using Framework.Module;

namespace Probe.General.Products
{
    public class VirtualDevice : DataTunnel<InternalProbeDataHAL>, IDummyDeviceHAL
    {

        public void AttachToProcessDataEvent(DataTunnel<InternalProbeDataHAL>.DataEventHandler processDataEventHandler) => DataEvent += processDataEventHandler;


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

        protected override void FetchDataForTunnel(out InternalProbeDataHAL data)
        {
            data = new InternalProbeDataHAL();
            //Example logic to generate process data
            if (IsOpen)
            {                
                Random r = new Random();
                int channel = r.Next(0, NumberOfChannels);                
                data = new InternalProbeDataHAL(channel, r.Next(20, 30), r.Next(50, 70));
            }
        }

        public void HALFunction()
        {
        }
    }
}
