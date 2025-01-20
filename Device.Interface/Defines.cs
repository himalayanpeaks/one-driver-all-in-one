namespace OneDriver.Device.Interface
{
    public static class Defines
    {
        public enum Devices
        {
            PowerSupplyVirtual,
            ProbeVirtual,
            DummyDeviceVirtual
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
