﻿using Framework.Base;
using Framework.Module.Parameter;
using System.Collections.ObjectModel;

namespace Framework.Module
{
    public abstract class BaseDeviceWithChannelsPd<TParams, TChannel, TChannelParams, TChannelProcessData> : BaseDevice<TParams>
        where TParams : BaseDeviceParam
        where TChannel : BaseChannelWithProcessData<TChannelParams, TChannelProcessData>
        where TChannelParams : BaseChannelParam
        where TChannelProcessData : IParameter

    {
        public BaseDeviceWithChannelsPd(TParams parameters, IValidator validator, ObservableCollection<TChannel> elements, TChannelProcessData channelProcessData) : base(parameters, validator)
        {
            Elements = elements;
        }

        public ObservableCollection<TChannel> Elements { get; set; }
    }
}
