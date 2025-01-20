using OneDriver.Framework.Module.Parameter;

namespace OneDriver.Probe.Abstract.Channels
{
    public class CommonChannel<TChannelParams, TChannelProcessData>
        : BaseChannelWithProcessData<TChannelParams, TChannelProcessData>
        where TChannelParams : CommonChannelParams
        where TChannelProcessData : CommonChannelProcessData
    {
        public CommonChannel(TChannelParams parameters, TChannelProcessData processData) : base(parameters, processData)
        {
        }
    }
}
