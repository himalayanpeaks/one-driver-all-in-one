namespace OneDriver.Device.Interface
{
    public static class Defines
    {
        public enum Devices
        {
            PowerSupplyVirtual,
            PowerSupplyKd3005p,
            ProbeVirtual,
            DummyDeviceVirtual,
            MasterUptVirtual,
            MasterUpt_1_3,
            MasterIoLinkTmg,
            DaqNiUsb
        }

        public enum AccessType
        {
            R,
            W,
            RW
        }

        public enum DataType
        {
            UINT,
            INT,
            Float32,
            Byte,
            BOOL,
            CHAR,
            Record,
            Array
        }
    }
}
