using OneDriver.Motor.Abstract;

namespace OneDriver.Motor.General
{
    public class DeviceParams : CommonDeviceParams
    {
        public DeviceParams(string name) : base(name)
        {
            PropertyChanged += DeviceParams_PropertyChanged;
        }

        private void DeviceParams_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OriginShift):
                case nameof(AxisLength):
                    MinimumPosition = -1 * OriginShift;
                    MaximumPosition = AxisLength - OriginShift;
                    break;
            }
        }
    }
}
