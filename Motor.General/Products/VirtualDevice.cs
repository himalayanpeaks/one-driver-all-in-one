using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;

namespace OneDriver.Motor.General.Products
{
    public class VirtualDevice : DataTunnel<InternalDataHAL>, IMotorHAL
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

        protected override void FetchDataForTunnel(out InternalDataHAL data)
        {
            data = new InternalDataHAL();
            //Example logic to generate process data
            if (IsOpen)
            {
                Random r = new Random();
                int position = r.Next(0, 500);
                data = new InternalDataHAL(200, r.NextSingle());
            }
        }

        public void HALFunction()
        {
        }

        public bool IsMotorReady { get; }
        public bool IsReferenceTravelDone { get; }

        public void Run(OneDriver.Device.Interface.Motor.Definition.TravelMode mode, OneDriver.Device.Interface.Motor.Definition.DirectionOfRotation direction = OneDriver.Device.Interface.Motor.Definition.DirectionOfRotation.Right, double position = 0,
            double speed = 0)
        {
            throw new NotImplementedException();
        }

        public void StopImmediately()
        {
            throw new NotImplementedException();
        }

        public int GetLastError()
        {
            throw new NotImplementedException();
        }

        public string GetErrorMessage(int errorCode)
        {
            throw new NotImplementedException();
        }

        public bool ResetError()
        {
            throw new NotImplementedException();
        }
    }
}
