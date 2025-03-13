using System.Runtime.InteropServices;
using System.Text;
using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using Serilog;
using static OneDriver.Master.CanOpen.Products.Definition;


namespace OneDriver.Master.CanOpen.Products
{
    public class Hms : DataTunnel<InternalDataHAL>, ICanOpenMaster
    {
        protected override void FetchDataForTunnel(out InternalDataHAL data)
        {
            throw new NotImplementedException();
        }

        public ConnectionError Open(string initString, IValidator validator)
        {
            throw new NotImplementedException();
        }

        public ConnectionError Close()
        {
            throw new NotImplementedException();
        }

        public int NumberOfChannels { get; }
        public byte SensorNodeId { get; set; }
        public byte MaxNumberOfBusNodeIds { get; }
        public bool IsSensorConnected { get; }
        public e_error_codes ConnectCanMasterWithPc(string hardwareId)
        {
            throw new NotImplementedException();
        }

        public e_error_codes DisconnectCanMasterFromPc()
        {
            throw new NotImplementedException();
        }

        public e_error_codes ConnectSensorWithMaster()
        {
            throw new NotImplementedException();
        }

        public e_error_codes DisconnectSensorFromMaster()
        {
            throw new NotImplementedException();
        }

        public e_error_codes ReadRecord(ushort index, byte subIndex, out byte?[] ReadBuffer, out uint ErrorCode)
        {
            throw new NotImplementedException();
        }

        public e_error_codes WriteRecord(ushort index, byte subIndex, byte[] WriteBuffer, out uint ErrorCode)
        {
            throw new NotImplementedException();
        }
        public void StartProcessDataAnnouncer() => StartAnnouncingData();

        public void StopProcessDataAnnouncer() => StopAnnouncingData();

        public void AttachToProcessDataEvent(DataEventHandler processDataEventHandler)
            => DataEvent += processDataEventHandler;
    }
}
