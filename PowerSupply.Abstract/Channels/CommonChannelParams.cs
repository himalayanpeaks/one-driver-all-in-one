using OneDriver.Device.Interface.PowerSupply;
using OneDriver.Framework.Module.Parameter;

namespace OneDriver.PowerSupply.Abstract.Channels
{
    public class CommonChannelParams : BaseChannelParam
    {
        private double _desiredVolts;
        private double _desiredAmps;
        private Definition.ControlMode _controlMode;

        public double DesiredVolts
        {
            get => _desiredVolts;
            set => SetProperty(ref _desiredVolts, value);
        }

        public double DesiredAmps
        {
            get => _desiredAmps;
            set => SetProperty(ref _desiredAmps, value);
        }

        public Definition.ControlMode ControlMode
        {
            get => _controlMode;
            set => SetProperty(ref _controlMode, value);
        }

        public CommonChannelParams(string name) : base(name)
        {
            
        }
    }
}
