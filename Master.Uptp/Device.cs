using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module.Parameter;
using OneDriver.Master.Abstract;
using OneDriver.Master.Abstract.Channels;
using OneDriver.Master.Uptp.Channels;
using OneDriver.Master.Uptp.Products;
using Serilog;
using OneDriver.Toolbox;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OneDriver.Framework.Libs.DeviceDescriptor;

namespace OneDriver.Master.Uptp
{
    public class Device : CommonDevice<DeviceParams, SensorParameter>
    {
        IUptHAL _deviceHAL { get; set; }
        public Device(string name, IValidator validator, IUptHAL deviceHAL, IDeviceDescriptor parameterDatabank) :
            base(new DeviceParams(name), validator, 
                new ObservableCollection<BaseChannelWithProcessData<CommonChannelParams<SensorParameter>, 
                    CommonChannelProcessData<SensorParameter>>>(), parameterDatabank)
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

        private void ProcessDataChanged(object sender, InternalDataHAL e)
        {
            var local = Elements[e.ChannelNumber].ProcessData.PdInCollection.FindAll(x => x.Index == e.Index);
            foreach (var parameter in local)
                parameter.Value =
                    DataConverter.MaskByteArray(e.Data, parameter.Offset, parameter.LengthInBits,
                        parameter.DataType, true);
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

        public override int ConnectSensor()
        {
            throw new NotImplementedException();
        }

        public override int DisconnectSensor()
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

        protected override int ReadParam(SensorParameter param)
        {
            throw new NotImplementedException();
        }

        protected override int WriteParam(SensorParameter param)
        {
            throw new NotImplementedException();
        }

        protected override int WriteCommand(SensorParameter command)
        {
            throw new NotImplementedException();
        }
    }
}
