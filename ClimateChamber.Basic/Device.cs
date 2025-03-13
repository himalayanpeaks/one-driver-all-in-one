using System.ComponentModel;
using OneDriver.ClimateChamber.Abstract;
using OneDriver.ClimateChamber.Basic.Products;
using OneDriver.Framework.Base;
using OneDriver.Framework.Libs.Validator;

namespace OneDriver.ClimateChamber.Basic
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

                case nameof(Parameters.HighestPossibleTemperature):
                    e.Value = ClimateChamberHAL.MAX_TEMPERATURE;
                    break;
                case nameof(Parameters.LowestPossibleTemperature):
                    e.Value = ClimateChamberHAL.MIN_TEMPERATURE;
                    break;
                case nameof(Parameters.DesiredTemperature):
                    e.Value = ClimateChamberHAL.ReadDesiredTemperature();
                    break;

            }
        }

        private void ProcessDataChanged(object sender, ClimateChamberDataHAL e)
        {
            this.ProcessData.InternalTemperature = e.CurrentTemperature;

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
               
            }
        }
      
        protected override int CloseConnection() => (int)ClimateChamberHAL.Close();
        protected override int OpenConnection(string initString) => (int)ClimateChamberHAL.Open(initString, validator);

    }
}
