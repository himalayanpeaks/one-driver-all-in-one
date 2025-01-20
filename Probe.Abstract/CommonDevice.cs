using Device.Interface.DummyDevice;
using Device.Interface.Probe;
using OneDriver.Framework.Base;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using OneDriver.Framework.Module.Parameter;
using OneDriver.Probe.Abstract.Channels;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OneDriver.Probe.Abstract
{
    public abstract class CommonDevice<TDeviceParams, TChannelParams, TChannelProcessData> :
        BaseDeviceWithChannelsPd<TDeviceParams, TChannelParams, TChannelProcessData>, IProbe
        where TDeviceParams : CommonDeviceParams
        where TChannelParams : CommonChannelParams
        where TChannelProcessData : CommonChannelProcessData
    {
        protected CommonDevice(TDeviceParams parameters, IValidator validator, ObservableCollection<BaseChannelWithProcessData<TChannelParams, TChannelProcessData>> elements) : base(parameters, validator, elements)
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

    }
}
