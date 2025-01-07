using DummyDevice.Abstract.Channels;
using Framework.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyDevice.General.Channels
{
    public class ChannelParams : CommonChannelParams
    {
        public ChannelParams(string name) : base(name)
        {
        }

        public int MyProperty { get; set; }
    }
}
