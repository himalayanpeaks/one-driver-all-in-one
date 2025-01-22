using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module.Parameter;
using OneDriver.Master.Abstract;
using OneDriver.Master.Abstract.Channels;
using OneDriver.Master.Uptp.Channels;
using OneDriver.Master.Uptp.Products;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OneDriver.Master.Uptp
{
    public class Device : CommonDevice<DeviceParams, SensorParameter>
    {
        IDummyDeviceHAL _deviceHAL { get; set; }
        public Device(string name, IValidator validator, IDummyDeviceHAL deviceHAL) :
            base(new DeviceParams(name), validator, 
                new ObservableCollection<BaseChannelWithProcessData<CommonChannelParams<SensorParameter>, 
                    CommonChannelProcessData<SensorParameter>>>())
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
                var item = new BaseChannelWithProcessData<CommonChannelParams<SensorParameter>,
                    CommonChannelProcessData<SensorParameter>> (new CommonChannelParams<SensorParameter>("Param_" + i.ToString()), new ());
                Elements.Add(item);
                Elements[i].Parameters.PropertyChanged += Parameters_PropertyChanged;
                Elements[i].Parameters.PropertyChanging += Parameters_PropertyChanging;
            }
        }

        private void ProcessDataChanged(object sender, InternalDummyDeviceDataHAL e)
        {
            //Transfer data from HAL to ProcessData here
           /* Elements[e.ChannelNumber].ProcessData.CommonProcessSampleData = e.InternalSampleData1;
            Elements[e.ChannelNumber].ProcessData.TimeStamp = e.TimeStamp;
            Elements[e.ChannelNumber].ProcessData.GeneralProcessSampleData = e.InternalSampleData2;
           */
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

        public override int SelectSensorAtPort(int portNumber)
        {
            throw new NotImplementedException();
        }

        public override int ConnectSensor()
        {
            throw new NotImplementedException();
        }

        public override int DisconnectSensor()
        {
            throw new NotImplementedException();
        }

        public override int UpdateDataFromSensor()
        {
            throw new NotImplementedException();
        }

        public override int UpdateDataFromAllSensors()
        {
            throw new NotImplementedException();
        }

        public override int ReadParameterFromSensor(string name, out string? value)
        {
            throw new NotImplementedException();
        }

        public override int ReadParameterFromSensor<T>(string name, out T? value) where T : default
        {
            throw new NotImplementedException();
        }

        public override int WriteParameterToSensor(string name, string value)
        {
            throw new NotImplementedException();
        }

        public override int WriteParameterToSensor<T>(string name, T value)
        {
            throw new NotImplementedException();
        }

        public override int WriteCommandToSensor(string name, string value)
        {
            throw new NotImplementedException();
        }

        public override int WriteCommandToSensor<T>(string name, T value)
        {
            throw new NotImplementedException();
        }

        public override string GetErrorMessage(int errorCode)
        {
            throw new NotImplementedException();
        }

        public override string?[] GetAllParamsFromSensor()
        {
            throw new NotImplementedException();
        }

        public override void LoadDataFromPdb(string server, int deviceId, int protocolId)
        {
            throw new NotImplementedException();
        }
    }
}
