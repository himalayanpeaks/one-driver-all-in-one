using Framework.Base;
using Framework.ModuleBuilder;

namespace Framework.Module.Parameter
{
    public abstract class MinimumChannelParamPdBase<TChannelProcessData> : MinimumChannelParamBase, IProcessData<TChannelProcessData>
        where TChannelProcessData : IParameter
    {
        public MinimumChannelParamPdBase(string name, TChannelProcessData processData) : base(name)
        {
            ProcessData = processData;
        }

        public TChannelProcessData ProcessData { get; set; }

    }

}
