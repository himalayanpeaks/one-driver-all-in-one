using Framework.ModuleBuilder;

namespace Framework.Module.Parameter
{
    public abstract class BaseChannel<TChannelParam> : IConfigurable<TChannelParam>
            where TChannelParam : BaseChannelParam
    {
        public TChannelParam Parameters { get; set; }

        public BaseChannel(TChannelParam parameters)
        {
            Parameters = parameters;
        }
    }
}
