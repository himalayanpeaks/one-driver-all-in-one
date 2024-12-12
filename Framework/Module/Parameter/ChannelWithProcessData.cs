using Framework.Base;
using Framework.ModuleBuilder;

namespace Framework.Module.Parameter
{
    public abstract class ChannelWithProcessData<TChannelParams, TChannelProcessData> : Channel<TChannelParams>, IProcessData<TChannelProcessData>
        where TChannelParams : MinimumChannelParam
        where TChannelProcessData : IParameter
    {
        public ChannelWithProcessData(TChannelParams parameters, TChannelProcessData processData) : base(parameters)
        {
            ProcessData = processData;
        }

        public TChannelProcessData ProcessData { get; set; }
    }

}

