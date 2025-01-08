using DummyDevice.Abstract;
using DummyDevice.General.Channels;
using DummyDevice.General.Products;
using Framework.Libs.Validator;
using Framework.Module.Parameter;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DummyDevice.General
{
    public class Device : CommonDevice<DeviceParams, ChannelParams, ChannelProcessData>
    {
        IDummyDeviceHAL _deviceHAL { get; set; }
        public Device(string name, IValidator validator, IDummyDeviceHAL deviceHAL) : 
            base(new DeviceParams(name), validator, new ObservableCollection<BaseChannelWithProcessData<ChannelParams, ChannelProcessData>>())
        {
            _deviceHAL = deviceHAL; 
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

        private void ProcessDataChanged(object sender, InternalDummyDeviceDataHAL e)
        {
            //Transfer data from HAL to ProcessData here
            Elements[e.ChannelNumber].ProcessData.CommonProcessSampleData = e.InternalSampleData1;
            Elements[e.ChannelNumber].ProcessData.TimeStamp = e.TimeStamp;
            Elements[e.ChannelNumber].ProcessData.GeneralProcessSampleData = e.InternalSampleData2;  
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Parameters.CommonDeviceParamDataExample):
                    //What shall happen if this property is chaged
                    break;
            }
        }

        private void Parameters_PropertyChanging(object sender, Framework.Base.PropertyValidationEventArgs e)
        {
            //Write validity before property is changed here
            switch (e.PropertyName)
            {
                case nameof(Parameters.CommonDeviceParamDataExample):
                    if ((int)e.NewValue > 25)
                    {
                        Log.Error("new value greater than 25 is not allowed");
                        throw new ArgumentOutOfRangeException();
                    }                    
                    break;
            }
        }
        public override void DummyDeviceFunction()
        {
            _deviceHAL.HALFunction();
        }
        protected override int CloseConnection() => (int)_deviceHAL.Close();
        protected override int OpenConnection(string initString) => (int)_deviceHAL.Open(initString, validator);

    }
}
