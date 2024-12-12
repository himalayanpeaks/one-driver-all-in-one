using Framework.Base;
using Framework.Module.Parameter;
using Framework.ModuleBuilder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Module
{
    public class MinimumDeviceWithChannelPd<TParams, TChannel, TChannelParams, TChannelProcessData> : MinimumDevice<TParams>
        where TParams : MinimumDeviceParam
        where TChannel : ChannelWithProcessData<TChannelParams, TChannelProcessData>
        where TChannelParams : MinimumChannelParam
        where TChannelProcessData : IParameter

    {
        public MinimumDeviceWithChannelPd(TParams parameters, ObservableCollection<TChannel> elements) : base(parameters)
        {
            Elements = elements;
        }

        public ObservableCollection<TChannel> Elements { get; set; }       
    }    
}
