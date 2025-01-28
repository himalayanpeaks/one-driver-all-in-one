using Device.Interface.DummyDevice;
using OneDriver.Device.Interface.Master;
using OneDriver.Framework.Base;
using OneDriver.Framework.Libs.DeviceDescriptor;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using OneDriver.Framework.Module.Parameter;
using OneDriver.Master.Abstract.Channels;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OneDriver.Toolbox;
using Definition = OneDriver.Device.Interface.Master.Definition;

namespace OneDriver.Master.Abstract
{
    public abstract class CommonDevice<TDeviceParams, TSensorParam> :
        BaseDeviceWithChannelsPd<TDeviceParams, CommonChannelParams<TSensorParam>, 
            CommonChannelProcessData<TSensorParam>>, IMaster
        where TDeviceParams : CommonDeviceParams
        where TSensorParam : CommonSensorParameter, new()
    {
        protected CommonDevice(TDeviceParams parameters, IValidator validator, 
            ObservableCollection<BaseChannelWithProcessData<CommonChannelParams<TSensorParam>,
            CommonChannelProcessData<TSensorParam>>> elements, IDeviceDescriptor parameterDatabank) 
            : base(parameters, validator, elements)
        {
            ParameterDatabank = parameterDatabank;
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            Parameters.PropertyChanging += Parameters_PropertyChanging;
        }
        protected IDeviceDescriptor? ParameterDatabank { get; set; }
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

        public Definition.Error SelectSensorAtPort(int portNumber)
        {
            if (portNumber < Elements.Count)
                Parameters.SelectedChannel = portNumber;
            else
            {
                Log.Error(portNumber + " doesn't exist");
                return Definition.Error.ChannelError;
            }
            return Definition.Error.NoError;
        }
        public abstract int ConnectSensor();
        public abstract int DisconnectSensor();

        public Definition.Error UpdateDataFromSensor()
        {
            if (ParameterDatabank == null)
            {
                Log.Error("Database not connected");
                return Definition.Error.DatabaseNotConnected;
            }

            if (Parameters.IsConnected == false)
            {
                Log.Error("Sensor not connected");
                return Definition.Error.CommunicationError;
            }

            foreach (var param in Elements[Parameters.SelectedChannel].Parameters.StandardParameterCollection)
                ReadParameterFromSensor(param);
            foreach (var param in Elements[Parameters.SelectedChannel].Parameters.SpecificParameterCollection)
                ReadParameterFromSensor(param);
            foreach (var param in Elements[Parameters.SelectedChannel].Parameters.SystemParameterCollection)
                ReadParameterFromSensor(param);

            return Definition.Error.NoError;
        }

        public void UpdateDataFromAllSensors()
        {
            if (this.Elements.Count > 1)
                if (Parameters.IsConnected)
                    DisconnectSensor();
            for(int i = 0; i < Elements.Count; i++)
            {
                if (Parameters.IsConnected == false)
                {
                    DisconnectSensor();
                    SelectSensorAtPort(i++);
                    ConnectSensor();
                }
                UpdateDataFromSensor();
            }
        }

        public int ReadParameterFromSensor(string name, out string? value)
        {
            TSensorParam? foundParam = FindParam(name);
            value = null;
            if (foundParam == null)
                return (int)Definition.Error.ParameterNotFound;
            int err = ReadParameterFromSensor(foundParam);
            if (err == 0)
                value = foundParam.Value;
            return err;
        }

        public int ReadParameterFromSensor<T>(string name, out T? value)
        {
            value = default(T);
            int err = ReadParameterFromSensor(name, out var readValue);

            if (err == 0 && DataConverter.ConvertTo(readValue, out value))
                return 0;
            return err != 0 ? err : (int)DataConverter.DataError.UnsupportedDataType;
        }
        private TSensorParam FindCommand(string name)
        {
            var command = Elements[Parameters.SelectedChannel].Parameters.CommandCollection
                .FirstOrDefault(x => x.Name == name);

            if (command == null)
            {
                Log.Error("Command not found");
                return new TSensorParam { Value = Definition.Error.CommandNotFound.ToString() };
            }

            return command;
        }

        private TSensorParam FindParam(string name)
        {
            var parameter = Elements[Parameters.SelectedChannel].Parameters.SpecificParameterCollection
                                .FirstOrDefault(x => x.Name == name) ??
                            Elements[Parameters.SelectedChannel].Parameters.SystemParameterCollection
                                .FirstOrDefault(x => x.Name == name) ??
                            Elements[Parameters.SelectedChannel].Parameters.StandardParameterCollection
                                .FirstOrDefault(x => x.Name == name);

            return parameter ?? new TSensorParam();
        }

        public int WriteParameterToSensor(string name, string value)
        {
            if (DataConverter.ConvertTo<TSensorParam>(value, out var toWriteValue) == true)
                return WriteParameterToSensor(name, toWriteValue);
            return (int)DataConverter.DataError.InValidData;
        }

        public int WriteParameterToSensor<T>(string name, T value)
        {
            if (DataConverter.ConvertTo(value, out var toWriteValue))
                return WriteParameterToSensor(name, toWriteValue);
            return (int)DataConverter.DataError.InValidData;
        }

        public abstract int WriteCommandToSensor(string name, string value);
        public abstract int WriteCommandToSensor<T>(string name, T value);
        public abstract string GetErrorMessage(int errorCode);
        public abstract string?[] GetAllParamsFromSensor();
        public abstract void LoadDataFromPdb(string server, int deviceId, int protocolId);

        private int ReadParameterFromSensor(TSensorParam parameter)
        {
            int err = 0;
            try
            {
                if((err = ReadParam(parameter)) != 0)
                    Log.Error("Error in read: " + GetErrorMessage(err));
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
            return err;
        }

        protected abstract int ReadParam(TSensorParam param);
        protected abstract int WriteParam(TSensorParam param);
        protected abstract int WriteCommand(TSensorParam command);
    }
}
