using Framework.Libs.Validator;
using Framework.Module.Parameter;
using Probe.Abstract;
using Probe.General.Channels;
using Probe.General.Products;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Probe.General
{
    public class Device : CommonDevice<DeviceParams, ChannelParams, ChannelProcessData>
    {
        IDummyDeviceHAL _deviceHAL { get; set; }
        public Device(string name, IValidator validator, IDummyDeviceHAL deviceHAL) :
            base(new DeviceParams(name), validator, new ObservableCollection<BaseChannelWithProcessData<ChannelParams, ChannelProcessData>>())
        {
            _deviceHAL = deviceHAL;
            Init();
        }

        private void Init()
        {
            Parameters.PropertyChanging += Parameters_PropertyChanging;
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            _deviceHAL.AttachToProcessDataEvent(ProcessDataChanged);

            for (var i = 0; i < _deviceHAL.NumberOfChannels; i++)
            {
                var item = new BaseChannelWithProcessData<ChannelParams, ChannelProcessData>(new ChannelParams("Ch" + i.ToString()), new ChannelProcessData());
                Elements.Add(item);
                Elements[i].Parameters.PropertyChanged += Parameters_PropertyChanged;
                Elements[i].Parameters.PropertyChanging += Parameters_PropertyChanging;
            }
        }

        private void ProcessDataChanged(object sender, InternalProbeDataHAL e)
        {
            //Transfer data from HAL to ProcessData here
            Elements[e.ChannelNumber].ProcessData.Temperature = e.CurrentTemperature;
            Elements[e.ChannelNumber].ProcessData.TimeStamp = e.TimeStamp;
            Elements[e.ChannelNumber].ProcessData.Humidity = e.CurrentHumidity;
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {

            }
        }

        private void Parameters_PropertyChanging(object sender, Framework.Base.PropertyValidationEventArgs e)
        {
            //Write validity before property is changed here
            switch (e.PropertyName)
            {
               
            }
        }
        protected override int CloseConnection() => (int)_deviceHAL.Close();
        protected override int OpenConnection(string initString) => (int)_deviceHAL.Open(initString, validator);

    }
}
