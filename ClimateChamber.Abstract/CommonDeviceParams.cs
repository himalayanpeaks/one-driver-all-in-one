using OneDriver.Framework.Module.Parameter;

namespace OneDriver.ClimateChamber.Abstract
{
    public class CommonDeviceParams : BaseDeviceParam
    {
        private double _maximumTemperature;
        private double _maximumWarningTemperatureLimit;
        private double _maximumAlarmTemperatureLimit;

        private double _minimumTemperature;
        private double _minimumAlarmTemperatureLimit;
        private double _minimumWarningTemperatureLimit;

        public double HighestPossibleTemperature => GetProperty<double>();
        public double LowestPossibleTemperature => GetProperty<double>();
        public double DesiredTemperature => GetProperty<double>();

        public double MaximumTemperature
        {
            get => _maximumTemperature;
            set => SetProperty(ref _maximumTemperature, value);
        }

        public double MinimumTemperature
        {
            get => _minimumTemperature;
            set => SetProperty(ref _minimumTemperature, value);
        }

        public double MinimumAlarmTemperatureLimit
        {
            get => _minimumAlarmTemperatureLimit;
            set => SetProperty(ref _minimumAlarmTemperatureLimit, value);
        }

        public double MaximumAlarmTemperatureLimit
        {
            get => _maximumAlarmTemperatureLimit;
            set => SetProperty(ref _maximumAlarmTemperatureLimit, value);
        }

        public double MinimumWarningTemperatureLimit
        {
            get => _minimumWarningTemperatureLimit;
            set => SetProperty(ref _minimumWarningTemperatureLimit, value);
        }

        public double MaximumWarningTemperatureLimit
        {
            get => _maximumWarningTemperatureLimit;
            set => SetProperty(ref _maximumWarningTemperatureLimit, value);
        }
        public CommonDeviceParams(string name) : base(name)
        {
        }
    }
}
