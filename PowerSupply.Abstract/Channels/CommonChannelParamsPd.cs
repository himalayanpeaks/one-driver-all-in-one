using Framework.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSupply.Abstract.Channels
{
    public abstract class CommonChannelParamsPd<TProcessData> : MinimumChannelParamPdBase<TProcessData>
        where TProcessData : CommonProcessData

    {
        protected CommonChannelParamsPd(string name, TProcessData channelParams) : base(name, channelParams)
        {
        }
    }
}
