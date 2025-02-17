using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;

namespace OneDriver.Daq.General.Products
{
    public class VirtualDevice :  IDaqHAL
    {


        private bool IsOpen = false;

        public int NumberOfChannels { get; } = 2;

       
        public void HALFunction()
        {
        }

        public int Open(string initString, IValidator validator)
        {
            throw new NotImplementedException();
        }

        public int Close()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllAnalogInChannels()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllDigitalInPorts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllDigitalInLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllCounterInChannels()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllAnalogOutChannels()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllDigitalOutPorts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllDigitalOutLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllCounterOutChannels()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAvailableDevices()
        {
            throw new NotImplementedException();
        }

        public int ReadAiChannels(IEnumerable<string> channelsName, double sampleTimeInSecond, double samplesPerSecond,
            out double[,] readBuffer)
        {
            throw new NotImplementedException();
        }

        public int BeginReadAi(IEnumerable<string> channelsName, double samplesPerSecond)
        {
            throw new NotImplementedException();
        }

        public int EndReadAi(out double[,] readBuffer, out double sampleTimeInSecond)
        {
            throw new NotImplementedException();
        }

        public int WriteAoChannels(IEnumerable<string> channelsName, double samplesPerSecond, bool writeContinuously, double[,] dataToWrite)
        {
            throw new NotImplementedException();
        }

        public int ReadDiChannels(IEnumerable<string> channelsName, double sampleTimeInSecond, double samplesPerSecond,
            out double[,] readBuffer, string? triggerChannel)
        {
            throw new NotImplementedException();
        }

        public int SetDoChannels(string channelsNameState)
        {
            throw new NotImplementedException();
        }

        public int GetDiChannels(IEnumerable<string> channels, out bool[] states)
        {
            throw new NotImplementedException();
        }

        public int SetTriState(string aChannelsNameState)
        {
            throw new NotImplementedException();
        }

        public int ReadPfiChannelTimePeriod(string aChannelName, int aNumberOfWaves, out double[] aTimePeriodBufferInMs,
            out double[] aDutyCycleBuffer)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public double MaxAIRate { get; }
        public double MaxAORate { get; }
        public double MaxDIRate { get; }
        public double MaxDORate { get; }
    }
}
