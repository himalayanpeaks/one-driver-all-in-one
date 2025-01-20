using OneDriver.Probe.Abstract.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.Probe.General.Channels
{
    public class Channel : CommonChannel<ChannelParams, ChannelProcessData>
    {
        protected Channel(ChannelParams parameters, ChannelProcessData processData) : base(parameters, processData)
        {
        }
    }
}
