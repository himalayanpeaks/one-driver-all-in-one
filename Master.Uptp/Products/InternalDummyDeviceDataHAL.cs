using OneDriver.Framework.Libs.Announcer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.Master.Uptp.Products
{
    public class InternalDummyDeviceDataHAL : BaseDataForAnnouncement
    {
        public InternalDummyDeviceDataHAL(int channelNumber, int sample1, string sample2)
        {
            ChannelNumber = channelNumber;
            InternalSampleData1 = sample1;
            InternalSampleData2 = sample2;
            TimeStamp = DateTime.Now;
        }

        public InternalDummyDeviceDataHAL()
        {
            TimeStamp = DateTime.Now;
            InternalSampleData2 = string.Empty;
        }

        public int ChannelNumber { get; }
        public int InternalSampleData1 { get; }
        public string InternalSampleData2 { get; }
    }
}
