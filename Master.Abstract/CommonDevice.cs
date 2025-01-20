using Device.Interface.DummyDevice;
using OneDriver.Device.Interface.Master;
using OneDriver.Framework.Base;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using OneDriver.Framework.Module.Parameter;
using OneDriver.Master.Abstract.Channels;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OneDriver.Master.Abstract
{
    public abstract class CommonDevice<TDeviceParams, TSensorParam> :
        BaseDeviceWithChannelsPd<TDeviceParams, CommonChannelParams<TSensorParam>, 
            CommonChannelProcessData<TSensorParam>>, IMaster
        where TDeviceParams : CommonDeviceParams
        where TSensorParam : CommonSensorParameter
    {
        protected CommonDevice(TDeviceParams parameters, IValidator validator, 
            ObservableCollection<BaseChannelWithProcessData<CommonChannelParams<TSensorParam>,
            CommonChannelProcessData<TSensorParam>>> elements) 
            : base(parameters, validator, elements)
        {
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            Parameters.PropertyChanging += Parameters_PropertyChanging;
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
                case nameof(Parameters.ProtocolId):
                    break;
            }
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Parameters.ProtocolId):
                    break;
            }
        }

        public abstract int SelectSensorAtPort(int portNumber);
        public abstract int ConnectSensor();
        public abstract int DisconnectSensor();
        public abstract int UpdateDataFromSensor();
        public abstract int UpdateDataFromAllSensors();
        public abstract int ReadParameterFromSensor(string name, out string? value);
        public abstract int ReadParameterFromSensor<T>(string name, out T? value);
        public abstract int WriteParameterToSensor(string name, string value);
        public abstract int WriteParameterToSensor<T>(string name, T value);
        public abstract int WriteCommandToSensor(string name, string value);
        public abstract int WriteCommandToSensor<T>(string name, T value);
        public abstract string GetErrorMessage(int errorCode);
        public abstract string?[] GetAllParamsFromSensor();
        public abstract void LoadDataFromPdb(string server, int deviceId, int protocolId);
    }
}
