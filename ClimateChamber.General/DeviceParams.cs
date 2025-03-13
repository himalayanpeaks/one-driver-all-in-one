using OneDriver.ClimateChamber.Abstract;

namespace OneDriver.ClimateChamber.General
{
    public class DeviceParams : CommonDeviceParams
    {
        private double _maximumHumidity;
        private double _minimumHumidity;
        private double _desiredHumidity;
        public double DesiredHumidity
        {
            get => _desiredHumidity;
            set => SetProperty(ref _desiredHumidity, value);
        }

        public double MaximumHumidity
        {
            get => _maximumHumidity;
            set => SetProperty(ref _maximumHumidity, value);
        }

        public double MinimumHumidity
        {
            get => _minimumHumidity;
            set => SetProperty(ref _minimumHumidity, value);
        }

        public bool IsHumidityReached => GetProperty<bool>();
        public DeviceParams(string name) : base(name)
        {

        }
    }
}
