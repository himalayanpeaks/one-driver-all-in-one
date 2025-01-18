using Probe.Abstract.Channels;

namespace Probe.General.Channels
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
