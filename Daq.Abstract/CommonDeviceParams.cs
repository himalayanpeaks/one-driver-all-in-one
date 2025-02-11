using OneDriver.Framework.Module.Parameter;

namespace OneDriver.Daq.Abstract
{
    public class CommonDeviceParams : BaseDeviceParam
    {
        private double _samplesPerSecond;

        public double SamplesPerSecond
        {
            get => _samplesPerSecond;
            set => SetProperty(ref _samplesPerSecond, value);
        }
        public double MaxSamplesPerSecond => GetProperty<double>();
        public string[]? AvailableDaqDevices => GetProperty<string[]>();
        public CommonDeviceParams(string name) : base(name)
        {
        }
    }
}
