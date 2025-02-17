using OneDriver.Framework.Module.Parameter;

namespace OneDriver.Daq.Abstract.Channels
{
    public class CommonChannel<TChannelParams>
        : BaseChannel<TChannelParams>
        where TChannelParams : CommonChannelParams
    {
        public CommonChannel(TChannelParams parameters) : base(parameters)
        {
        }
    }
}
