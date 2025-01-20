using OneDriver.Framework.Libs.Announcer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.Probe.General.Products
{
    public class InternalProbeDataHAL : BaseDataForAnnouncement
    {
        public InternalProbeDataHAL(int channelNumber, double temperature, double humidity)
        {
            ChannelNumber = channelNumber;
            CurrentTemperature = temperature;
            CurrentHumidity = humidity;
            TimeStamp = DateTime.Now;
        }

        public InternalProbeDataHAL()
        {
            TimeStamp = DateTime.Now;
        }

        public int ChannelNumber { get; }
        public double CurrentTemperature { get; }
        public double CurrentHumidity { get; }
    }
}
