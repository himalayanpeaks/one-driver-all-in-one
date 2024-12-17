using Framework.Module.Parameter;

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
