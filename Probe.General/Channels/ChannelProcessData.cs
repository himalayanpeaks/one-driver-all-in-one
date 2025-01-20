using OneDriver.Probe.Abstract.Channels;

namespace OneDriver.Probe.General.Channels
{
    public class ChannelProcessData : CommonChannelProcessData
    {
        private double humidity;

        public double Humidity
        {
            get => GetProperty(ref humidity);
            set => SetProperty(ref humidity, value);
        }
    }
}
