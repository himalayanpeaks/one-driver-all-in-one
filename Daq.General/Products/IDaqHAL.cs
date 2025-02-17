using OneDriver.Device.Interface.HardwareLayer;
using OneDriver.Framework.Libs.Validator;

namespace OneDriver.Daq.General.Products
{
    public interface IDaqHAL
    {
        int Open(string initString, IValidator validator);
        int Close();

        IEnumerable<string> GetAllAnalogInChannels();
        IEnumerable<string> GetAllDigitalInPorts();
        IEnumerable<string> GetAllDigitalInLines();
        IEnumerable<string> GetAllCounterInChannels();
        IEnumerable<string> GetAllAnalogOutChannels();
        IEnumerable<string> GetAllDigitalOutPorts();
        IEnumerable<string> GetAllDigitalOutLines();
        IEnumerable<string> GetAllCounterOutChannels();
        IEnumerable<string> GetAvailableDevices();

        int ReadAiChannels(IEnumerable<string> channelsName, double sampleTimeInSecond, double samplesPerSecond,
            out double[,] readBuffer);
        int BeginReadAi(IEnumerable<string> channelsName, double samplesPerSecond);
        int EndReadAi(out double[,] readBuffer, out double sampleTimeInSecond);
        int WriteAoChannels(IEnumerable<string> channelsName, double samplesPerSecond, bool writeContinuously,
            double[,] dataToWrite);

        int ReadDiChannels(IEnumerable<string> channelsName, double sampleTimeInSecond, double samplesPerSecond,
            out double[,] readBuffer, string? triggerChannel);

        int SetDoChannels(string channelsNameState);
        int GetDiChannels(IEnumerable<string> channels, out bool[] states);
        int SetTriState(string aChannelsNameState);

        int ReadPfiChannelTimePeriod(string aChannelName, int aNumberOfWaves, out double[] aTimePeriodBufferInMs,
            out double[] aDutyCycleBuffer);

        void Reset();
        public double MaxAIRate { get; }
        public double MaxAORate { get; }
        public double MaxDIRate { get; }
        public double MaxDORate { get; }
    }
}
