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
            CommunicationError = Int32.MinValue + 3,
        }

        public enum Mode
        {
            Communication,
            ProcessData,
            StandardInputOutput
        }
    }
}
