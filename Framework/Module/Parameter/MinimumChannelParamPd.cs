using Framework.Base;
using Framework.ModuleBuilder;

namespace Framework.Module.Parameter
{
    public class MinimumChannelParamPd<TChannelProcessData> : MinimumChannelParam, IProcessData<TChannelProcessData>
        where TChannelProcessData : IParameter
    {
        public MinimumChannelParamPd(string name, TChannelProcessData processData) : base(name)
        {
            ProcessData = processData;
        }

        public TChannelProcessData ProcessData { get; set; }

    }
    
}
