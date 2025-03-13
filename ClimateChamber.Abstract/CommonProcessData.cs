using OneDriver.Framework.Module.Parameter;

namespace OneDriver.ClimateChamber.Abstract
{
    public class CommonProcessData : BaseProcessData
    {
        private double _temperature;
        private bool _hasTemperatureReached;

        public double Temperature
        {
            get => _temperature;
            protected set => SetProperty(ref _temperature, value);
        }
        public bool HasTemperatureReached
        {
            get => _hasTemperatureReached;
            protected set => SetProperty(ref _hasTemperatureReached, value);
        }
    }
}
