using Framework.ModuleBuilder;

namespace Framework.Module.Parameter
{
    public abstract class ChannelBase<TChannelParam> : IConfigurable<TChannelParam>
            where TChannelParam : MinimumChannelParamBase
    {
        public TChannelParam Parameters { get; set; }

        public ChannelBase(TChannelParam parameters)
        {
            Parameters = parameters;
        }
    }
}
