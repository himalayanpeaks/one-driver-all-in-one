using OneDriver.Framework.Base;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using System.ComponentModel;
using System.Globalization;
using OneDriver.Device.Interface.ClimateChamber;
using Definition = OneDriver.Device.Interface.Master.Definition;

namespace OneDriver.ClimateChamber.Abstract
{
    public abstract class CommonDevice<TDeviceParams, TProcessData> :
        BaseDeviceWithProcessData<TDeviceParams, TProcessData>, IClimateChamber  
        where TDeviceParams : CommonDeviceParams
        where TProcessData : CommonProcessData
    {
        protected CommonDevice(TDeviceParams parameters, IValidator validator, TProcessData processData) : base(parameters, validator, processData)
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
                case nameof(Parameters.MaximumTemperature):
                    if ((double)e.NewValue > Parameters.HighestPossibleTemperature)
                        throw new ArgumentOutOfRangeException(e.PropertyName, Parameters.HighestPossibleTemperature.ToString(CultureInfo.CurrentCulture));
                    break;
                case nameof(Parameters.MinimumTemperature):
                    if ((double)e.NewValue < Parameters.LowestPossibleTemperature)
                        throw new ArgumentOutOfRangeException(e.PropertyName, Parameters.LowestPossibleTemperature.ToString(CultureInfo.CurrentCulture));
                    break;
            }
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
        }

        public abstract int StartChamber(double desiredTemperature);
        public int Start(double desiredTemperature)
        {
            if (desiredTemperature > Parameters.MaximumTemperature ||
                desiredTemperature < Parameters.MinimumTemperature)
                return (int)Framework.Module.Definition.DeviceError.ValueOutOfRange;
            
            return StartChamber(desiredTemperature);
        }

        public abstract int StartChamber(int delayInSeconds, double desiredTemperature);
        
        public int StartWithDelay(int delayInSeconds, double desiredTemperature)
        {
            if (desiredTemperature > Parameters.MaximumTemperature ||
                desiredTemperature < Parameters.MinimumTemperature)
                return (int)Framework.Module.Definition.DeviceError.ValueOutOfRange;
            return StartChamber(delayInSeconds, desiredTemperature);
        }
        public abstract int Stop();
    }
}
