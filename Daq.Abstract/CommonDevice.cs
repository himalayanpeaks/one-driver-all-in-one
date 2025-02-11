using OneDriver.Daq.Abstract.Channels;
using OneDriver.Framework.Base;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using OneDriver.Framework.Module.Parameter;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OneDriver.Device.Interface.Daq;

namespace OneDriver.Daq.Abstract
{
    public abstract class CommonDevice<TDeviceParams, TChannelParams, TChannelProcessData> :
        BaseDeviceWithChannelsPd<TDeviceParams, TChannelParams, TChannelProcessData>, IDaq
        where TDeviceParams : CommonDeviceParams
        where TChannelParams : CommonChannelParams
        where TChannelProcessData : CommonChannelProcessData
    {
        protected CommonDevice(TDeviceParams parameters, IValidator validator, ObservableCollection<BaseChannelWithProcessData<TChannelParams, TChannelProcessData>> elements) : base(parameters, validator, elements)
        {
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            Parameters.PropertyChanging += Parameters_PropertyChanging;
            Parameters.PropertyReadRequested += Parameters_PropertyReadRequested;
        }

        private void Parameters_PropertyReadRequested(object sender, PropertyReadRequestedEventArgs e)
        {
            switch (e.PropertyName)
            {

            }
        }

        /// <summary>
        /// Write here the validation of a param before its new value of a param is accepted 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Parameters_PropertyChanging(object sender, PropertyValidationEventArgs e)
        {
            switch (e.PropertyName)
            {

            }
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {

            }
        }
        public abstract int ReadAiChannels(IEnumerable<string> channelsName, double sampleTimeInSecond, out double[,] readBuffer);
        public abstract int WriteAoChannels(IEnumerable<string> channelsName, bool writeContinuously, double[,] dataToWrite);
        public abstract int StartSamplingAi(IEnumerable<string> channelsName);
        public abstract int StopSamplingAi(out double readTimeInSecond, out double[,] readBuffer);
        public abstract int ReadDiChannels(IEnumerable<string> channelsName, string? triggerChannel, double sampleTimeInSecond,
            out double[,] readBuffer);
        public abstract int ReadPfiChannelTimePeriod(string aChannelName, int aNumberOfWaves, out double[] aTimePeriodBufferInMs,
            out double[] aDutyCycleBuffer);
        public abstract int SetTriState(List<string> channels);
        public abstract int SetDoChannels(string channelsNameState);
        public abstract int GetDiChannels(IEnumerable<string> channels, out bool[] states);
        public abstract List<string> GetAvailableChannels();
        public abstract void ResetCard();
        public abstract void StopAllTasks();
        public abstract string GetErrorMessage(int errorCode);
    }
}
