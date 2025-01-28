using OneDriver.Framework.Libs.Announcer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.Master.Uptp.Products
{
    public class InternalDataHAL : BaseDataForAnnouncement
    {
        public InternalDataHAL(int channelNumber, ushort index, byte[] data)
        {
            TimeStamp = DateTime.Now;
            ChannelNumber = channelNumber;
            Index = index;
            Data = data;
        }

        public InternalDataHAL()
        {
            TimeStamp = DateTime.Now;
            Data = null;
            ChannelNumber = 0;
        }

        public int ChannelNumber { get; } = 0;
        public ushort Index { get; }
        public byte[]? Data { get; }
    }
}
