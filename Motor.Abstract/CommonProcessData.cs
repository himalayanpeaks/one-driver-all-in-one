using OneDriver.Framework.Module.Parameter;

namespace OneDriver.Motor.Abstract
{
    public class CommonProcessData : BaseProcessData
    {
        private double _position;

        public double Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }
    }
}
