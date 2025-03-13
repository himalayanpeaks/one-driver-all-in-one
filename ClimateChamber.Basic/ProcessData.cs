using OneDriver.ClimateChamber.Abstract;

namespace OneDriver.ClimateChamber.Basic
{
    public class ProcessData : CommonProcessData
    {
        private double _internalTemperature;
        private bool _hasInternalTemperatureReached;

        internal double InternalTemperature
        {
            get => _internalTemperature;
            set => SetProperty(ref _internalTemperature, value);
        }
        internal bool HasInternalTemperatureReached
        {
            get => _hasInternalTemperatureReached;
            set => SetProperty(ref _hasInternalTemperatureReached, value);
        }

        public ProcessData()
        {
            this.PropertyChanged += ProcessData_PropertyChanged;
        }

        private void ProcessData_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ProcessData.InternalTemperature):
                    Temperature = InternalTemperature;
                    break;
                case nameof(ProcessData.HasInternalTemperatureReached):
                    HasTemperatureReached = HasInternalTemperatureReached;
                    break;
            }
        }
    }
}
