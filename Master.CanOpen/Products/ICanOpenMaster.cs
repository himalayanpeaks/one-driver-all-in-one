using OneDriver.Device.Interface.HardwareLayer;
using OneDriver.Framework.Libs.Announcer;
using static OneDriver.Master.CanOpen.Products.Definition;

namespace OneDriver.Master.CanOpen.Products
{
    public interface ICanOpenMaster : IDeviceHAL<InternalDataHAL>
    {
        public byte SensorNodeId { get; set; }
        public byte MaxNumberOfBusNodeIds { get; }
        public bool IsSensorConnected { get; }
        public Definition.e_error_codes ConnectCanMasterWithPc(string hardwareId);
        public Definition.e_error_codes DisconnectCanMasterFromPc();
        public Definition.e_error_codes ConnectSensorWithMaster();

        public Definition.e_error_codes DisconnectSensorFromMaster();

        public Definition.e_error_codes ReadRecord(
            UInt16 index,
            byte subIndex,
            out byte?[] ReadBuffer,
            out uint ErrorCode
        );
        public Definition.e_error_codes WriteRecord(
            UInt16 index,
            byte subIndex,
            byte[] WriteBuffer,
            out uint ErrorCode
        );
        public static string[] SearchHardwareIds() => throw new NotImplementedException();

    }
}
