using Framework.Base;
using Framework.ModuleBuilder;

namespace Framework.Module.Parameter
{
    public abstract class ChannelWithProcessDataBase<TChannelParams, TChannelProcessData> : ChannelBase<TChannelParams>, IProcessData<TChannelProcessData>
        where TChannelParams : MinimumChannelParamBase
        where TChannelProcessData : IParameter
    {
        public ChannelWithProcessDataBase(TChannelParams parameters, TChannelProcessData processData) : base(parameters)
        {
            ProcessData = processData;
        }

        public TChannelProcessData ProcessData { get; set; }
    }

}

