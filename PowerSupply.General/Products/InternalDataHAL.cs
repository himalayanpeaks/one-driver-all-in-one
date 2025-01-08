using Framework.Libs.Announcer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSupply.General.Products
{
    public class InternalDataHAL : BaseDataForAnnouncement
    {
        public InternalDataHAL(int channelNumber, double voltage, double current)
        {
            ChannelNumber = channelNumber;
            CurrentVoltage = voltage;
            CurrentCurrent = current;
            TimeStamp = DateTime.Now;
        }

        public InternalDataHAL()
        {
            CurrentVoltage = 0;
            CurrentCurrent = 0;
            ChannelNumber = 0;
            TimeStamp = DateTime.Now;
        }

        public int ChannelNumber { get; }
        public double CurrentVoltage { get; }
        public double CurrentCurrent { get; }
    }
}
