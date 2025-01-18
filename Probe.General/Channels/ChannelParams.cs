using Framework.Module.Parameter;
using Probe.Abstract.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probe.General.Channels
{
    public class ChannelParams : CommonChannelParams
    {
        public ChannelParams(string name) : base(name)
        {
        }

        public int MyProperty { get; set; }
    }
}
