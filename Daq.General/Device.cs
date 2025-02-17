using OneDriver.Daq.Abstract;
using OneDriver.Daq.General.Channels;
using OneDriver.Daq.General.Products;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module.Parameter;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OneDriver.Device.Interface.Daq;
using OneDriver.Framework.Base;
using Definition = OneDriver.Device.Interface.Daq.Definition;

namespace OneDriver.Daq.General
{
    public class Device : CommonDevice<DeviceParams, ChannelParams>
    {
        IDaqHAL _deviceHAL { get; set; }
        public Device(string name, IValidator validator, IDaqHAL deviceHAL) :
            base(
                new DeviceParams(name), validator, 
                new ObservableCollection<BaseChannel<ChannelParams>>())
        {
            _deviceHAL = deviceHAL;
            Init();
        }

        private void Init()
        {
            Parameters.PropertyChanging += Parameters_PropertyChanging;
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            Parameters.PropertyReadRequested += Parameters_PropertyReadRequested;
        }

        private void Parameters_PropertyReadRequested(object sender, PropertyReadRequestedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
            }
        }

        public override int ReadAiChannels(IEnumerable<string> channelsName, double sampleTimeInSecond, out double[,] readBuffer)
        {
            throw new NotImplementedException();
        }

        public override int WriteAoChannels(IEnumerable<string> channelsName, bool writeContinuously, double[,] dataToWrite)
        {
            throw new NotImplementedException();
        }

        public override int StartSamplingAi(IEnumerable<string> channelsName)
        {
            throw new NotImplementedException();
        }

        public override int StopSamplingAi(out double readTimeInSecond, out double[,] readBuffer)
        {
            throw new NotImplementedException();
        }

        public override int ReadDiChannels(IEnumerable<string> channelsName, string? triggerChannel, double sampleTimeInSecond,
            out double[,] readBuffer)
        {
            throw new NotImplementedException();
        }

        public override int ReadPfiChannelTimePeriod(string aChannelName, int aNumberOfWaves, out double[] aTimePeriodBufferInMs,
            out double[] aDutyCycleBuffer)
        {
            throw new NotImplementedException();
        }

        public override int SetTriState(List<string> channels)
        {
            throw new NotImplementedException();
        }

        public override int SetDoChannels(string channelsNameState)
        {
            throw new NotImplementedException();
        }

        public override int GetDiChannels(IEnumerable<string> channels, out bool[] states)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetAvailableChannels()
        {
            throw new NotImplementedException();
        }

        public override void ResetCard()
        {
            throw new NotImplementedException();
        }

        public override void StopAllTasks()
        {
            throw new NotImplementedException();
        }

        public override string GetErrorMessage(int errorCode)
        {
            throw new NotImplementedException();
        }

        private void Parameters_PropertyChanging(object sender, Framework.Base.PropertyValidationEventArgs e)
        {
            //Write validity before property is changed here
            switch (e.PropertyName)
            {
            }
        }

        protected override int CloseConnection()
        {
            Elements.Clear();
            return (int)_deviceHAL.Close();
        }

        protected override int OpenConnection(string initString)
        {
            int err = (int)_deviceHAL.Open(initString, validator);
            if (err != 0)
                return err;
            Parameters.SamplesPerSecond = _deviceHAL.MaxAIRate;
            #region ChannelSearchAndAdd

            int index = 0;
            foreach (var channelName in _deviceHAL.GetAllAnalogInChannels() ?? Enumerable.Empty<string>())
                Elements.Add(new BaseChannel<ChannelParams>(new ChannelParams("Ch" + index++.ToString(),
                    channelName, Definition.ChannelType.AnalogIn)));
            foreach (var channelName in _deviceHAL.GetAllAnalogOutChannels() ?? Enumerable.Empty<string>())
                Elements.Add(new BaseChannel<ChannelParams>(new ChannelParams("Ch" + index++.ToString(),
                    channelName, Definition.ChannelType.AnalogOut)));
            foreach (var channelName in _deviceHAL.GetAllDigitalInLines() ?? Enumerable.Empty<string>())
                Elements.Add(new BaseChannel<ChannelParams>(new ChannelParams("Ch" + index++.ToString(),
                    channelName, Definition.ChannelType.DigitalIn)));
            foreach (var channelName in _deviceHAL.GetAllDigitalOutLines() ?? Enumerable.Empty<string>())
                Elements.Add(new BaseChannel<ChannelParams>(new ChannelParams("Ch" + index++.ToString(),
                    channelName, Definition.ChannelType.DigitalOut)));
            foreach (var channelName in _deviceHAL.GetAllCounterInChannels() ?? Enumerable.Empty<string>())
                Elements.Add(new BaseChannel<ChannelParams>(new ChannelParams("Ch" + index++.ToString(),
                    channelName, Definition.ChannelType.CounterIn)));
            foreach (var channelName in _deviceHAL.GetAllCounterOutChannels() ?? Enumerable.Empty<string>())
                Elements.Add(new BaseChannel<ChannelParams>(new ChannelParams("Ch" + index++.ToString(),
                    channelName, Definition.ChannelType.CounterOut)));
            #endregion

            for (var i = 0; i < Elements.Count; i++)
            {
                Elements[i].PropertyChanging += Parameters_PropertyChanging;
                Elements[i].PropertyReadRequested += Parameters_PropertyReadRequested;
                Elements[i].PropertyChanged += Parameters_PropertyChanged;
            }
            return err;
        }
    }
}
