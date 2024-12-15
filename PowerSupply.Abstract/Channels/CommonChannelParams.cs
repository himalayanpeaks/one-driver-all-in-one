using Framework.Base;
using Framework.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSupply.Abstract.Channels
{
    public abstract class CommonChannelParams : MinimumChannelParamBase
    {
        protected CommonChannelParams(string name) : base(name)
        {
        }
    }
}
