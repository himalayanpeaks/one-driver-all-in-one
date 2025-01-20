﻿using OneDriver.Framework.ModuleBuilder;

namespace OneDriver.Framework.Module.Parameter
{
    public class BaseChannel<TChannelParam> : IConfigurable<TChannelParam>
            where TChannelParam : BaseChannelParam
    {
        public TChannelParam Parameters { get; set; }

        public BaseChannel(TChannelParam parameters)
        {
            Parameters = parameters;
        }
    }
}
