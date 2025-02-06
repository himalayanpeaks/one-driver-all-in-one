using System.Collections.ObjectModel;
using System.ComponentModel;
using OneDriver.Device.Interface.DummyDevice;
using Serilog;
using OneDriver.DummyDevice.Abstract.Channels;
using OneDriver.Framework.Base;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using OneDriver.Framework.Module.Parameter;

namespace OneDriver.DummyDevice.Abstract
{
    public abstract class CommonDevice<TDeviceParams, TChannelParams, TChannelProcessData> :
        BaseDeviceWithChannelsPd<TDeviceParams, TChannelParams, TChannelProcessData>, IDummyDevice
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
                case nameof(Parameters.CommonDeviceParamDataExample):
                    if ((int)e.NewValue < 10)
                    {
                        Log.Error("Value of " + e.PropertyName + " out of range");
                        throw new ArgumentOutOfRangeException();
                    }
                    break;
            }
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Parameters.CommonDeviceParamDataExample):
                    Log.Information("CommonDeviceParamDataExample changed to " + Parameters.CommonDeviceParamDataExample);
                    break;
            }
        }

        public abstract void DummyDeviceFunction();
    }
}
