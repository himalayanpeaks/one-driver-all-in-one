using OneDriver.ClimateChamber.Abstract;
using OneDriver.ClimateChamber.General.Products;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module.Parameter;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OneDriver.Framework.Base;

namespace OneDriver.ClimateChamber.General
{
    public class Device : CommonDevice<DeviceParams, ProcessData>
    {
        IClimateChamberHAL ClimateChamberHAL { get; set; }
        public Device(string name, IValidator validator, IClimateChamberHAL climateChamberHAL) :
            base(new DeviceParams(name), validator, new ProcessData())
        {
            ClimateChamberHAL = climateChamberHAL;
            Init();
        }

        private void Init()
        {
            Parameters.PropertyChanging += Parameters_PropertyChanging;
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            Parameters.PropertyReadRequested += Parameters_PropertyReadRequested;
            ClimateChamberHAL.AttachToProcessDataEvent(ProcessDataChanged);
        }

        private void Parameters_PropertyReadRequested(object sender, PropertyReadRequestedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Parameters.IsHumidityReached):
                    e.Value = true;
                    break;
                case nameof(Parameters.HighestPossibleTemperature):
                    e.Value = ClimateChamberHAL.MAX_TEMPERATURE;
                    break;
                case nameof(Parameters.LowestPossibleTemperature):
                    e.Value = ClimateChamberHAL.MIN_TEMPERATURE;
                    break;
                case nameof(Parameters.DesiredTemperature):
                    e.Value = ClimateChamberHAL.ReadDesiredTemperature();
                    break;
                case nameof(Parameters.DesiredHumidity):
                    e.Value = ClimateChamberHAL.ReadDesiredHumidity();
                    break;
            }
        }

        private void ProcessDataChanged(object sender, ClimateChamberDataHAL e)
        {
            this.ProcessData.InternalTemperature = e.CurrentTemperature;
            this.ProcessData.Humidity = e.CurrentHumidity;
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {

            }
        }

        public override int StartChamber(double desiredTemperature)
        {
            return ClimateChamberHAL.Start(desiredTemperature);
        }

        public override int StartChamber(int delayInSeconds, double desiredTemperature)
        {
            return ClimateChamberHAL.StartWithDelay(delayInSeconds, desiredTemperature);
        }

        public override int Stop()
        {
            return ClimateChamberHAL.Stop();
        }

        private void Parameters_PropertyChanging(object sender, Framework.Base.PropertyValidationEventArgs e)
        {
            //Write validity before property is changed here
            switch (e.PropertyName)
            {
                case nameof(Parameters.MinimumHumidity):
                    if ((int)e.NewValue < 0)
                        throw new ArgumentException("Value out of range, min = " + 0);
                    break;
                case nameof(Parameters.MaximumHumidity):
                    if ((int)e.NewValue > 100)
                        throw new ArgumentException("Value out of range, max = " + 100);
                    break;
            }
        }
      
        protected override int CloseConnection() => (int)ClimateChamberHAL.Close();
        protected override int OpenConnection(string initString) => (int)ClimateChamberHAL.Open(initString, validator);

    }
}
