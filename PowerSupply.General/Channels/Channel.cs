using Framework.Module.Parameter;
using PowerSupply.Abstract.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSupply.General.Channels
{
    public class Channel : CommonChannel<ChannelParams, ChannelProcessData>
    {
        public Channel(ChannelParams parameters, ChannelProcessData processData) : base(parameters, processData)
        {
        }
    }
}
