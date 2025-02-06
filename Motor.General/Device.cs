using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module.Parameter;
using OneDriver.Motor.Abstract;
using OneDriver.Motor.General.Products;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OneDriver.Framework.Base;

namespace OneDriver.Motor.General
{
    public class Device : CommonDevice<DeviceParams, ProcessData>
    {
        IMotorHAL _deviceHAL { get; set; }
        public Device(string name, IValidator validator, IMotorHAL deviceHAL) :
            base(new DeviceParams(name), validator, new ProcessData())
        {
            _deviceHAL = deviceHAL;
            Init();
        }

        private void Init()
        {
            Parameters.PropertyChanging += Parameters_PropertyChanging;
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            Parameters.PropertyReadRequested += Parameters_PropertyReadRequested;
            _deviceHAL.AttachToProcessDataEvent(ProcessDataChanged);
        }

        private void Parameters_PropertyReadRequested(object sender, PropertyReadRequestedEventArgs e)
        {
            switch (e.PropertyName)
            {

            }
        }

        private void ProcessDataChanged(object sender, InternalDataHAL e)
        {

           ProcessData.Position = e.Position;
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
