using System.Collections.ObjectModel;
using System.ComponentModel;
using OneDriver.Framework.Base;
using OneDriver.Framework.Libs.DeviceDescriptor;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module.Parameter;
using OneDriver.Master.Abstract;
using OneDriver.Master.Abstract.Channels;
using OneDriver.Master.IoLink.Channels;
using OneDriver.Master.IoLink.Products;
using OneDriver.Toolbox;
using ParameterTool.NSwagClass.Generator.Interface;
using Serilog;
using DataType = OneDriver.Device.Interface.Defines.DataType;
using static OneDriver.Master.IoLink.Products.Definition;
namespace OneDriver.Master.IoLink
{
    public class Device : CommonDevice<DeviceParams, SensorParameter>
    {
        private IIoLinkMaster DeviceHAL { get; set; }
        public Device(string name, IValidator validator, IIoLinkMaster deviceHAL, IDeviceDescriptor parameterDatabank) :
            base(new DeviceParams(name), validator,
                new ObservableCollection<BaseChannelWithProcessData<CommonChannelParams<SensorParameter>,
                    CommonChannelProcessData<SensorParameter>>>(), parameterDatabank)
        {
            DeviceHAL = deviceHAL;
            Init();
        }

        private void Init()
        {
            Parameters.PropertyChanging += Parameters_PropertyChanging;
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            Parameters.PropertyReadRequested += Parameters_PropertyReadRequested;
            DeviceHAL.AttachToProcessDataEvent(ProcessDataChanged);

            for (var i = 0; i < DeviceHAL.NumberOfChannels; i++)
            {
                var item = new BaseChannelWithProcessData<CommonChannelParams<SensorParameter>,
                    CommonChannelProcessData<SensorParameter>>(new CommonChannelParams<SensorParameter>("Param_" + i.ToString()), new());
                Elements.Add(item);
                Elements[i].Parameters.PropertyChanged += Parameters_PropertyChanged;
                Elements[i].Parameters.PropertyChanging += Parameters_PropertyChanging;
            }
        }

        private const int HashIndex = 253;
        private void Parameters_PropertyReadRequested(object sender, PropertyReadRequestedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CommonChannel<CommonSensorParameter>.Parameters.HashId):
                    var err = DeviceHAL.ReadRecord(HashIndex, 0, out var readVal, 
                        out _ , out _, out _);
                    if (err == Definition.t_eInternal_Return_Codes.RETURN_OK)
                    {
                        byte[] byteArray = readVal.ToArray();
                        e.Value = BitConverter.ToString(byteArray).Replace("-", "");
                    }
                    break;
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
                case nameof(Parameters.SelectedChannel):
                    DeviceHAL.SensorPortNumber = Parameters.SelectedChannel;
                    break;
                case nameof(Parameters.Mode):
                    switch (Parameters.Mode)
                    {
                        case OneDriver.Device.Interface.Master.Definition.Mode.Communication:
                            DeviceHAL.StopProcessDataAnnouncer();
                            break;
                        case OneDriver.Device.Interface.Master.Definition.Mode.ProcessData:
                            DeviceHAL.StartProcessDataAnnouncer();
                            break;
                        case OneDriver.Device.Interface.Master.Definition.Mode.StandardInputOutput:
                            DeviceHAL.StopProcessDataAnnouncer();
                            break;
                    }
                    break;
            }
        }

        protected override void AddData(ParameterDetailsResponse paramDetail, CommonSensorParameter commonParameter)
        {
            commonParameter.PropertyChanging += Parameters_PropertyChanging;
            commonParameter.PropertyChanged += Parameters_PropertyChanged;
            switch (paramDetail.CategoryName)
            {
                case "SpecificParameter":
                    Elements[Parameters.SelectedChannel].Parameters.SpecificParameterCollection.Add(new SensorParameter(commonParameter));
                    break;
                case "StandardParameter":
                    Elements[Parameters.SelectedChannel].Parameters.StandardParameterCollection.Add(new SensorParameter(commonParameter));
                    break;
                case "SystemParameter":
                    Elements[Parameters.SelectedChannel].Parameters.SystemParameterCollection.Add(new SensorParameter(commonParameter));
                    break;
                case "StandardCommand":
                    Elements[Parameters.SelectedChannel].Parameters.CommandCollection.Add(new SensorParameter(commonParameter));
                    break;
                case "ProcessData":
                    Elements[Parameters.SelectedChannel].ProcessData.PdInCollection.Add(new SensorParameter(commonParameter));
                    break;
            }
        }

        private void Parameters_PropertyChanging(object sender, OneDriver.Framework.Base.PropertyValidationEventArgs e)
        {
            //Write validity before property is changed here
            switch (e.PropertyName)
            {

            }
        }

        protected override int CloseConnection() => (int)DeviceHAL.Close();
        protected override int OpenConnection(string initString) => (int)DeviceHAL.Open(initString, validator);

        public override int ConnectSensor()
        {
            var err = DeviceHAL.ConnectSensorWithMaster();
            Log.Information(err.ToString());

            return (err == Definition.t_eInternal_Return_Codes.RETURN_OK) ? 0
                : (int)OneDriver.Device.Interface.Master.Definition.Error.SensorCommunicationError;
        }

        public override int DisconnectSensor() => (int)DeviceHAL.DisconnectSensorFromMaster();

        private int WriteParameterToSensor(SensorParameter parameter)
        {
            int err = 0;
            try
            {
                err = WriteParam(parameter);
            }
            catch (Exception e)
            {
                Log.Error("Error in " + parameter.Index + " " + err + " " + e);
                return err;
            }

            return err;
        }

        protected override string GetErrorAsText(int errorCode)
        {
            if (Enum.IsDefined(typeof(t_eInternal_Return_Codes), errorCode))
                return ((t_eInternal_Return_Codes)errorCode).ToString();

            return "UnknownError";
        }


        protected override int ReadParam(SensorParameter param)
        {
            param.Value = null;
            var err = DeviceHAL.ReadRecord(Convert.ToUInt16(param.Index),
                Convert.ToByte(param.Subindex), out var data, out _, out _, out _);

            if (Equals(data, null))
                throw new Exception("index: " + param.Index + " read value is null");
            if (data.Length == 0)
                throw new Exception("index: " + param.Index + " no data available");


            if (param.DataType == DataType.UINT || param.DataType == DataType.INT || param.DataType == DataType.Float32 ||
                param.DataType == DataType.Byte || param.DataType == DataType.BOOL)
            {
                DataConverter.ToNumber(data, param.DataType, param.LengthInBits, true, out string?[] valueData);
                if (valueData == null)
                {
                    param.Value = string.Join(";", data.Select(x => x.ToString()).ToArray());
                    throw new Exception("index: " + param.Index + " data length mismatch");
                }

                param.Value = string.Join(";", valueData);
            }

            if (param.DataType == DataType.CHAR)
            {
                DataConverter.ToString(data, param.DataType, param.LengthInBits, true, out var val);
                param.Value = val;
            }
            return (int)err;
        }

        protected override int WriteParam(SensorParameter param)
        {
            if (string.IsNullOrEmpty(param.Value))
                Log.Error(param.Name + " Data null");

            string[] dataToWrite = param.Value.Split(';').ToArray();
            DataConverter.DataError dataError;
            if ((dataError = DataConverter.ToByteArray(dataToWrite, param.DataType, param.LengthInBits,
                    true, out var returnedData, param.ArrayCount)) != DataConverter.DataError.NoError)
                return (int)dataError;
            return (int)DeviceHAL.WriteRecord((ushort)param.Index, (byte)param.Subindex, returnedData,
                out _, out _);
        }

        protected override int WriteCommand(SensorParameter command) => WriteParam(command);
    }
}
