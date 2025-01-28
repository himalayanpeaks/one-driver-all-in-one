using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;

namespace OneDriver.Master.Uptp.Products
{
    public class VirtualDevice : DataTunnel<InternalDataHAL>, IUptHAL
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

        public int NumberOfChannels { get; } = 1;

        protected override void FetchDataForTunnel(out InternalDataHAL data)
        {
            data = new InternalDataHAL();
            //Example logic to generate process data
            if (IsOpen)
            {
                Random random = new Random();
                ushort distance = (ushort)random.Next(250, 261);

                // Fixed values for other fields
                byte scale = 0b11111101; // -3 (stored in 8-bit signed 2's complement)
                byte gap = 0; // 4-bit gap (set to 0)
                byte signalQuality = 0b10; // 2-bit signal quality
                byte bdch = 0b01; // 2-bit BDCH1/2

                // Combine the fields into a 4-byte array
                byte[] locaBytes = new byte[4];

                // Byte 0 and 1: 16-bit distance
                locaBytes[0] = (byte)(distance >> 8); // High byte
                locaBytes[1] = (byte)(distance & 0xFF); // Low byte

                // Byte 2: 8-bit scale
                locaBytes[2] = scale;

                // Byte 3: Combine gap (4 bits), signal quality (2 bits), and BDCH1/2 (2 bits)
                locaBytes[3] = (byte)((gap << 4) | (signalQuality << 2) | bdch);
                data = new InternalDataHAL(0, 288, locaBytes);
            }
        }

        public Definition.e_com ConnectSensorWithMaster()
        {
            return Definition.e_com.COM_ONLINE;
        }

        public Definition.e_com DisconnectSensorFromMaster()
        {
            return Definition.e_com.COM_ONLINE;

        }

        public Definition.e_error_codes ReadParam(ushort index, out byte[] data)
        {
            throw new NotImplementedException();
        }

        public Definition.e_error_codes WriteParam(ushort index, byte[] data)
        {
            throw new NotImplementedException();
        }

        public Definition.e_error_codes SetProcessData(ushort index, out int lengthInBytes)
        {
            throw new NotImplementedException();
        }

        public Definition.e_error_codes PowerOff()
        {
            throw new NotImplementedException();
        }

        public Definition.e_error_codes PowerOn()
        {
            throw new NotImplementedException();
        }
    }
}
