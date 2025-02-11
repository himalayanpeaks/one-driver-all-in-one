using OneDriver.Framework.Module;

namespace OneDriver.Device.Interface.Daq
{
    public interface IDaq : IDevice
    {
        int ReadAiChannels(IEnumerable<string> channelsName, double sampleTimeInSecond, out double[,] readBuffer);
        int WriteAoChannels(IEnumerable<string> channelsName, bool writeContinuously, double[,] dataToWrite);
        int StartSamplingAi(IEnumerable<string> channelsName);
        int StopSamplingAi(out double readTimeInSecond, out double[,] readBuffer);
        int ReadDiChannels(IEnumerable<string> channelsName, string? triggerChannel, double sampleTimeInSecond, out double[,] readBuffer);
        int ReadPfiChannelTimePeriod(string aChannelName, int aNumberOfWaves, out double[] aTimePeriodBufferInMs,
            out double[] aDutyCycleBuffer);
        int SetTriState(List<string> channels);
        int SetDoChannels(string channelsNameState);
        int GetDiChannels(IEnumerable<string> channels, out bool[] states);
        List<string> GetAvailableChannels();
        void ResetCard();
        void StopAllTasks();
        string GetErrorMessage(int errorCode);
    }
}
