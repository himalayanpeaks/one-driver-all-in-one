using OneDriver.Framework.Module.Parameter;
using OneDriver.PowerSupply.Abstract.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.PowerSupply.General.Channels
{
    /// <summary>
    /// Unused class
    /// </summary>
    public class Channel : CommonChannel<ChannelParams, ChannelProcessData>
    {
        public Channel(ChannelParams parameters, ChannelProcessData processData) : base(parameters, processData)
        {
        }
    }
}
