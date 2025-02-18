using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSNiUsbDll;
using NationalInstruments.DAQmx;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Toolbox;

namespace OneDriver.Daq.General.Products
{
    public class NiUsb : IDaqHAL
    {
        private NationalInstruments.DAQmx.Device Device { get; set; }

        public int Open(string initString, IValidator validator)
        {
            try
            {
                string deviceName = validator.ValidationRegex.Match(initString).Result("${devicename}");
                Device = DaqSystem.Local.LoadDevice(deviceName);
                CurrentDevice = deviceName;
            }
            catch (DaqException e)
            {
                return e.Error;
            }

            return 0;
        }

        public int Close()
        {
            return 0;
        }

        public IEnumerable<string> GetAllAnalogInChannels()
        {
            return Device.AIPhysicalChannels;
        }
        public IEnumerable<string> GetAllAnalogOutChannels()
        {
            return Device.AOPhysicalChannels;
        }

        public IEnumerable<string> GetAllDigitalInPorts()
        {
            return Device.DIPorts;
        }

        public IEnumerable<string> GetAllDigitalInLines()
        {
            return Device.DILines;
        }

        public IEnumerable<string> GetAllDigitalOutPorts()
        {
            return Device.DOPorts;
        }

        public IEnumerable<string> GetAllDigitalOutLines()
        {
            return Device.DOLines;
        }

        public IEnumerable<string> GetAllCounterInChannels()
        {
            return Device.CIPhysicalChannels;
        }
        public IEnumerable<string> GetAllCounterOutChannels()
        {
            return Device.COPhysicalChannels;
        }

        public IEnumerable<string> GetAvailableDevices()
        {
            return DaqSystem.Local.Devices;
        }

        private string CurrentDevice { get; set; }
        public int ReadDiChannels(IEnumerable<string> channelsName, double sampleTimeInSecond, double samplesPerSecond, out double[,] readBuffer, string? triggerChannel)
        {
            string error = "";
            NationalInstruments.DAQmx.Task myTask = new NationalInstruments.DAQmx.Task();
            readBuffer = new double[0, 0];
            try
            {

                if (samplesPerSecond > Device.DIMaximumRate)
                    samplesPerSecond = Device.DIMaximumRate;

                int samplesPerChannel = 0;
                var enumerable = channelsName as string[] ?? channelsName.ToArray();
                UInt32 samplesPerSecondPerChannel =
                    (UInt32)(samplesPerSecond / enumerable.Count() - (samplesPerSecond % enumerable.Count()));
                samplesPerChannel = (int)(samplesPerSecondPerChannel * sampleTimeInSecond);

                readBuffer = new double[enumerable.Count(), samplesPerChannel];

                foreach (string s in channelsName)
                    myTask.AddGlobalChannel(s);

                myTask.Timing.SampleTimingType = SampleTimingType.SampleClock;

                if (!string.IsNullOrEmpty(triggerChannel))
                {
                    myTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("PFI0",
                        DigitalEdgeStartTriggerEdge.Rising);
                    myTask.Triggers.ReferenceTrigger.ConfigureDigitalEdgeTrigger("PFI0",
                        DigitalEdgeReferenceTriggerEdge.Rising, 400);
                }

                myTask.Timing.ConfigureSampleClock(string.Empty, samplesPerSecondPerChannel,
                    SampleClockActiveEdge.Rising, SampleQuantityMode.FiniteSamples, samplesPerChannel);
                //myTask.Triggers.ReferenceTrigger.PretriggerSamples = Convert.ToInt64(samplesPerSecondPerChannel * 0.01); //ca. 10 ms burst
                myTask.Stream.Timeout = 20000;

                myTask.Start();
                try
                {
                    myTask.WaitUntilDone(15000);
                }
                catch (DaqException exception)
                {
                    error = exception.Message;
                    myTask.Stop();
                }

                if (error == string.Empty)
                {
                    DigitalSingleChannelReader myDigitalReader = new DigitalSingleChannelReader(myTask.Stream);
                    int ChannelsPerTask = myTask.DIChannels.Count;

                    //Read samples
                    try
                    {
                        for (long i = 0; i < samplesPerChannel; i++)
                        {
                            bool[] singleSampleAllLines = (myDigitalReader.ReadSingleSampleMultiLine());
                            for (long j = 0; j < channelsName.Count(); j++)
                                readBuffer[j, i] = Convert.ToDouble(singleSampleAllLines[j]);
                        }

                    }
                    catch (DaqException ex)
                    {
                        Log.Error(ex.Error.ToString() + ": " + ex.Message);
                        return ex.Error;
                    }

                    catch (IndexOutOfRangeException ex)
                    {
                        Log.Error(ex.ToString());
                        return ex.HResult;
                    }
                }
            }
            catch (DaqException ex)
            {
                Log.Error(ex.Error.ToString() + ": " + ex.Message);
                return ex.Error;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return ex.HResult;
            }

            return 0;
        }

        public int SetDoChannels(string channelsNameState)
        {
            string error = "";

            NationalInstruments.DAQmx.Task localTask = new NationalInstruments.DAQmx.Task();

            /** Seperate Names */
            channelsNameState = Regex.Replace(channelsNameState, @"\s", "");
            string[] channelsName = StringValueSeparator.SeparateLines(channelsNameState).Split(',');

            /** Seperate States */
            bool[] channelsState = new bool[channelsName.Length];
            Extensions.ConvertFloatArrayToBoolArray(StringValueSeparator.SeperateValues(channelsNameState), out channelsState).ToString();

            try
            {
                foreach (string s in channelsName)
                    localTask.AddGlobalChannel(s);
                bool[,] writeValues = new bool[localTask.DOChannels.Count, 1];
                for (int i = 0; i < localTask.DOChannels.Count; i++)
                    writeValues[i, 0] = channelsState[i];


                DigitalMultiChannelWriter writer = new DigitalMultiChannelWriter(localTask.Stream);
                writer.WriteSingleSampleMultiLine(true, writeValues);
            }
            catch (DaqException ex)
            {
                Log.Error(ex.Error.ToString() + ": " + ex.Message);
                return ex.Error;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return ex.HResult;
            }
            return 0;
        }

        public int GetDiChannels(IEnumerable<string> channels, out bool[] states)
        {
            string error = "";
            NationalInstruments.DAQmx.Task localTask = new NationalInstruments.DAQmx.Task();

            states = new bool[channels.Count()];
            try
            {
                foreach (string s in channels)
                    localTask.AddGlobalChannel(s);
                DigitalMultiChannelReader reader = new DigitalMultiChannelReader(localTask.Stream);
                states = reader.ReadSingleSampleSingleLine();
            }
            catch (DaqException ex)
            {
                Log.Error(ex.Error.ToString() + ": " + ex.Message);
                return ex.Error;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return ex.HResult;
            }
            localTask.Stop();
            localTask.Dispose();
            return 0;
        }

        public int SetTriState(string aChannelsNameState)
        {
            string error = "";
            string[] channels = StringValueSeparator.SeparateCommaStrings(aChannelsNameState);
            NationalInstruments.DAQmx.Task localTask = new NationalInstruments.DAQmx.Task();
            try
            {
                foreach (string s in channels)
                    localTask.AddGlobalChannel(s);
                DIChannelCollection diCol = localTask.DIChannels;
                DOChannelCollection doCol = localTask.DOChannels;
                foreach (DIChannel diC in diCol)
                    diC.Tristate = true;
                foreach (DOChannel doC in doCol)
                    doC.Tristate = true;
                localTask.Start();
            }
            catch (DaqException ex)
            {
                Log.Error(ex.Error.ToString() + ": " + ex.Message);
                return ex.Error;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return ex.HResult;
            }
            localTask.Stop();
            localTask.Dispose();
            return 0;
        }

        public int ReadPfiChannelTimePeriod(string aChannelName, int aNumberOfWaves, out double[] aTimePeriodBufferInMs,
            out double[] aDutyCycleBuffer)
        {
            int error = 0;
            NationalInstruments.DAQmx.Task localTask = new NationalInstruments.DAQmx.Task();
            aTimePeriodBufferInMs = new double[aNumberOfWaves];
            aDutyCycleBuffer = new double[aNumberOfWaves];
            CIDataTime[] timeAnalysis = new CIDataTime[aNumberOfWaves];

            try
            {
                localTask.AddGlobalChannel(aChannelName);
                localTask.Timing.SampleTimingType = SampleTimingType.Implicit;
                localTask.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, 2 * aNumberOfWaves);
                localTask.Stream.Timeout = 180000;
                CounterSingleChannelReader reader = new CounterSingleChannelReader(localTask.Stream);

                double[] timings = reader.ReadMultiSampleDouble(2 * aNumberOfWaves);
                if (timings != null)
                {
                    double[] high = new double[aNumberOfWaves]; int highCounter = 0;
                    double[] low = new double[aNumberOfWaves]; int lowCounter = 0;
                    for (int i = 0; i < timings.Length; i++)
                    {
                        if (i % 2 == 0)
                            high[highCounter++] = timings[i];
                        else
                            low[lowCounter++] = timings[i];
                    }
                    for (int i = 0; i < aTimePeriodBufferInMs.Length; i++)
                    {
                        aTimePeriodBufferInMs[i] = Math.Round((high[i] + low[i]) * 1000, 2);
                        aDutyCycleBuffer[i] = high[i] / (high[i] + low[i]);
                    }
                }
            }
            catch (DaqException ex)
            {
                error = ex.Error;
            }

            catch (Exception ex)
            {
                error = ex.HResult;
            }

            localTask.Stop();
            localTask.Dispose();
            return error;
        }

        public void Reset()
        {
            DaqSystem.Local.LoadDevice(CurrentDevice).Reset();
        }

        public string GetErrorMessage(int errorCode)
        {
            try
            {
                // Attempt to throw an exception with the given error code
                throw new DaqException("Simulated exception", errorCode);
            }
            catch (DaqException ex)
            {
                // Return the error message associated with the error code
                return ex.Message;
            }
        }

        public int ReadAiChannels(IEnumerable<string> channelsName, double sampleTimeInSecond, double samplesPerSecond,
            out double[,] readBuffer)
        {
            NationalInstruments.DAQmx.Task localTask = new NationalInstruments.DAQmx.Task();

            if (samplesPerSecond > Device.AIMaximumSingleChannelRate)
                samplesPerSecond = Device.AIMaximumSingleChannelRate;

            int samplesPerChannel = 0;
            var enumerable = channelsName as string[] ?? channelsName.ToArray();
            UInt32 samplesPerSecondPerChannel = (UInt32)(samplesPerSecond / enumerable.Count() - (samplesPerSecond % enumerable.Count()));
            samplesPerChannel = (int)(samplesPerSecondPerChannel * sampleTimeInSecond);

            readBuffer = new double[enumerable.Count(), samplesPerChannel];
            try
            {
                foreach (string s in enumerable)
                    localTask.AddGlobalChannel(s);

                localTask.Timing.SampleTimingType = SampleTimingType.SampleClock;
                localTask.Timing.ConfigureSampleClock(string.Empty, samplesPerSecondPerChannel, SampleClockActiveEdge.Rising, SampleQuantityMode.FiniteSamples, samplesPerChannel);
                localTask.Stream.Timeout = 70000;

                var reader = new AnalogMultiChannelReader(localTask.Stream);
                readBuffer = reader.ReadMultiSample(samplesPerChannel);
            }
            catch (DaqException ex)
            {
                Log.Error(ex.Error.ToString() + ": " + ex.Message);
                return ex.Error;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return ex.HResult;
            }
            return 0;
        }
        private void AnalogInCallback(IAsyncResult ar)
        {
            if (mStopAiSampling == false)
            {
                mCallbackAIQ.Enqueue(mGlobalAnalogChannelReader.EndReadMultiSample(ar));
                mGlobalAnalogChannelReader.BeginReadMultiSample(SAMPLES_PER_CHANNEL_BUFFER, AnalogInCallback, mGlobalTask);
            }
        }
        public int BeginReadAi(IEnumerable<string> channelsName, double samplesPerSecond)
        {

            mGlobalTask = new NationalInstruments.DAQmx.Task();
            mCallbackAIQ = new Queue<Double[,]>();
            if (samplesPerSecond > Device.AIMaximumSingleChannelRate)
                samplesPerSecond = Device.AIMaximumSingleChannelRate;
            mAnalogSamples = new Queue<double>[channelsName.Count()];
            for (int i = 0; i < mAnalogSamples.Length; i++)
                mAnalogSamples[i] = new Queue<double>();

            double samplesPerSecondPerChannel = samplesPerSecond / channelsName.Count() - (samplesPerSecond % channelsName.Count());

            try
            {
                foreach (string s in channelsName)
                    mGlobalTask.AddGlobalChannel(s);

                mGlobalTask.Timing.SampleTimingType = SampleTimingType.SampleClock;

                mGlobalTask.Timing.ConfigureSampleClock(string.Empty, samplesPerSecondPerChannel, SampleClockActiveEdge.Rising, SampleQuantityMode.ContinuousSamples);
                mGlobalTask.Stream.Timeout = 20000;

                mGlobalAnalogChannelReader = new AnalogMultiChannelReader(mGlobalTask.Stream);
                mStopAiSampling = false;
                mGlobalAnalogChannelReader.BeginReadMultiSample(SAMPLES_PER_CHANNEL_BUFFER, AnalogInCallback, mGlobalTask);
            }
            catch (DaqException ex)
            {
                Log.Error(ex.Error.ToString() + ": " + ex.Message);
                return ex.Error;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return ex.HResult;
            }
            return 0;
        }

        public int EndReadAi(out double[,] readBuffer, out double sampleTimeInSecond)
        {
            mStopAiSampling = true;
            sampleTimeInSecond = 0;
            Double[][,] localArray = mCallbackAIQ.ToArray();
            readBuffer = new double[0, 0];
            int numberOfChannels = 0, numberOfSamplesPerChannels = 0;
            if (localArray != null && localArray.Length > 0)
            {
                numberOfChannels = localArray.ElementAt(0).GetLength(0);
                numberOfSamplesPerChannels = localArray.ElementAt(0).GetLength(1) * localArray.Length;
            }
            else
                return -1;

            readBuffer = new double[numberOfChannels, numberOfSamplesPerChannels];
            int i = 0;

            try
            {
                for (int length = 0; length < localArray.Length; length++)
                {
                    Double[,] current = localArray[length];
                    int l = 0;


                    for (int j = 0; j < numberOfChannels; j++)
                    {
                        l = i * current.GetLength(1);
                        for (int k = 0; k < current.GetLength(1); k++)
                        {
                            readBuffer[j, l++] = current[j, k];
                        }
                    }
                    i++;
                }
                sampleTimeInSecond = Convert.ToDouble(readBuffer.GetLength(0) *
                                                      readBuffer.GetLength(1)) / mGlobalTask.Timing.SampleClockRate;
            }
            catch (Exception ex)
            {
                mCallbackAIQ.Clear();
                mGlobalTask.Stop();
                mGlobalTask.Dispose();
                mGlobalTask = null;
            }
            mCallbackAIQ.Clear();
            if (!mGlobalTask.Equals(null))
            {
                mGlobalTask.Stop();
                mGlobalTask.Dispose();
                mGlobalTask = null;
            }

            return 0;
        }

        public int WriteAoChannels(IEnumerable<string> channelsName, double samplesPerSecond, bool writeContinuously, double[,] dataToWrite)
        {
            if (samplesPerSecond > Device.AOMaximumRate)
                samplesPerSecond = Device.AOMaximumRate;
            NationalInstruments.DAQmx.Task localTask = new NationalInstruments.DAQmx.Task();
            try
            {
                foreach (string s in channelsName)
                    localTask.AddGlobalChannel(s);
                localTask.Timing.SampleTimingType = SampleTimingType.SampleClock;
                if (!writeContinuously)
                    localTask.Timing.ConfigureSampleClock(string.Empty, samplesPerSecond, SampleClockActiveEdge.Rising,
                        SampleQuantityMode.FiniteSamples);
                else
                    localTask.Timing.ConfigureSampleClock(string.Empty, samplesPerSecond, SampleClockActiveEdge.Rising,
                        SampleQuantityMode.ContinuousSamples);
                AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(localTask.Stream);
                writer.WriteMultiSample(true, dataToWrite);
            }
            catch (DaqException ex)
            {
                Log.Error(ex.Error.ToString() + ": " + ex.Message);
                return ex.Error;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return ex.HResult;
            }
            return 0;
        }
        private NationalInstruments.DAQmx.Task mGlobalTask { get; set; }
        private Queue<Double[,]> mCallbackAIQ = new Queue<double[,]>();
        Queue<double>[] mAnalogSamples;
        const int MaximumSamplingDurationInSec = 100;
        AnalogMultiChannelReader mGlobalAnalogChannelReader;
        DigitalMultiChannelReader mGlobalDigitalChannelReader;
        AnalogMultiChannelReader myGlobalAnalogMultichannelReader = null;
        IAsyncResult handle = null;
        int SAMPLES_TO_READ_FROM_BUFFER = 100;
        Boolean mStopAiSampling = false;
        Queue<Double[,]> SamplesFromBuffer;
        long SAMPLES_PER_CHANNEL = 0;
        int NUMBER_OF_CHANNELS = 0;
        int CountEnterCallback = 0;
        Queue<double> mPfiSamples;
        const int SAMPLES_PER_CHANNEL_BUFFER = 100;
        public double MaxAIRate => Device.AIMaximumSingleChannelRate;
        public double MaxAORate => Device.AOMaximumRate;
        public double MaxDIRate => Device.DIMaximumRate;
        public double MaxDORate => Device.AOMaximumRate;
    }
}
