using OneDriver.Device.Interface.DummyDevice;
using OneDriver.Framework.Base;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using OneDriver.Framework.Module.Parameter;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OneDriver.Device.Interface.Motor;

namespace OneDriver.Motor.Abstract
{
    public abstract class CommonDevice<TDeviceParams, TChannelProcessData> :
        BaseDeviceWithProcessData<TDeviceParams, TChannelProcessData>, IMotor
        where TDeviceParams : CommonDeviceParams
        where TChannelProcessData : CommonProcessData
    {
        protected CommonDevice(TDeviceParams parameters, IValidator validator,
            TChannelProcessData processData) : base(parameters, validator, processData)
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

            }
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {

            }
        }

        public int Run(bool waitTillTravelEnds)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public int GetLastError()
        {
            throw new NotImplementedException();
        }

        public string GetErrorMessage(int errorCode)
        {
            throw new NotImplementedException();
        }
    }
}
