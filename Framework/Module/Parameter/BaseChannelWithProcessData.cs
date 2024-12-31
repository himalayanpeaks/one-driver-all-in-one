﻿using Framework.Base;
using Framework.ModuleBuilder;

namespace Framework.Module.Parameter
{
    public class BaseChannelWithProcessData<TChannelParams, TChannelProcessData> : BaseChannel<TChannelParams>, IProcessData<TChannelProcessData>
        where TChannelParams : BaseChannelParam
        where TChannelProcessData : BaseProcessData
    {
        public BaseChannelWithProcessData(TChannelParams parameters, TChannelProcessData processData) : base(parameters)
        {
            ProcessData = processData;
        }

        public TChannelProcessData ProcessData { get; set; }
    }

}

