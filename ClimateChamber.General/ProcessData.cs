using OneDriver.ClimateChamber.Abstract;
using OneDriver.ClimateChamber.General.Products;

namespace OneDriver.ClimateChamber.General
{
    public class ProcessData : CommonProcessData
    {
        private double _humidity;
        private double _internalTemperature;
        private bool _hasHumidityReached;
        private bool _hasInternalTemperatureReached;

        public double Humidity
        {
            get => _humidity;
            internal set => SetProperty(ref _humidity, value);
        }
        public bool HasHumidityReached
        {
            get => _hasHumidityReached;
            internal set => SetProperty(ref _hasHumidityReached, value);
        }

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
