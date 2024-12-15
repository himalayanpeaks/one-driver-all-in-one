using Framework.Base;
using Framework.ModuleBuilder;

namespace Framework.Module.Parameter
{
    public abstract class BaseChannelParamPd<TChannelProcessData> : BaseChannelParam, IProcessData<TChannelProcessData>
        where TChannelProcessData : IParameter
    {
        public BaseChannelParamPd(string name, TChannelProcessData processData) : base(name)
        {
            ProcessData = processData;
        }

        public TChannelProcessData ProcessData { get; set; }

    }

}
