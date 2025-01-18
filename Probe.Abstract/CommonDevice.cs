using Device.Interface.DummyDevice;
using Device.Interface.Probe;
using Framework.Base;
using Framework.Libs.Validator;
using Framework.Module;
using Framework.Module.Parameter;
using Probe.Abstract.Channels;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Probe.Abstract
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
