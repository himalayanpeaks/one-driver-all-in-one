namespace OneDriver.Device.Interface.Master
{
    public class Definition
    {
        public enum Error
        {
            NoError = 0,
            ChannelError = Int32.MinValue,
            ParameterNotFound = Int32.MinValue + 1,
        }

        public enum Mode
        {
            Communication,
            ProcessData,
            StandardInputOutput
        }
    }
}
