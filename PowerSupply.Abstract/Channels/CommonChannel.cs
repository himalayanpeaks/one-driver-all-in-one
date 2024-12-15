using DeviceInterface;
using Framework.Base;
using Framework.Module;
using Framework.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSupply.Abstract.Channels
{
    public abstract class CommonChannel<TChannelParams, TChannelProcessData>
        : BaseChannelWithProcessData<TChannelParams, TChannelProcessData>
        where TChannelParams : CommonChannelParams
        where TChannelProcessData : CommonProcessData
    {
        protected CommonChannel(TChannelParams parameters, TChannelProcessData processData) : base(parameters, processData)
        {
        }
    }
}
