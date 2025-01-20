using OneDriver.Framework.Module.Parameter;

namespace OneDriver.Probe.Abstract.Channels
{
    public class CommonChannelProcessData : BaseProcessData
    {
        private double temperature;

        public double Temperature
        {
            get => GetProperty(ref temperature);
            set => SetProperty(ref temperature, value);
        }
    }
}
