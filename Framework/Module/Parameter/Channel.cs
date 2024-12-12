using Framework.ModuleBuilder;

namespace Framework.Module.Parameter
{
    public abstract class Channel<TChannelParam> : IConfigurable<TChannelParam>
            where TChannelParam : MinimumChannelParam
    {
        public TChannelParam Parameters { get; set; }

        public Channel(TChannelParam parameters)
        {
            Parameters = parameters;
        }
    }
}
