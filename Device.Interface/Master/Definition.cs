namespace OneDriver.Device.Interface.Master
{
    public class Definition
    {
        public enum Error
        {
            NoError = 0,
            ChannelError = Int32.MinValue,
            ParameterNotFound = Int32.MinValue + 1,
            DatabaseNotConnected = Int32.MinValue + 2,
            SensorCommunicationError = Int32.MinValue + 3,
            CommandNotFound = Int32.MinValue + 4,
            SensorNotFound = Int32.MinValue + 5,
            UptNotConnected = Int32.MinValue + 6,
            HasdIdNotFound = Int32.MinValue + 7,
        }

        public enum Mode
        {
            Communication,
            ProcessData,
            StandardInputOutput
        }
    }
}
