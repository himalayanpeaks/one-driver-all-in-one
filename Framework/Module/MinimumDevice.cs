using Framework.Module.Parameter;
using Framework.ModuleBuilder;

namespace Framework.Module
{
    public abstract class MinimumDevice<TParams> : IConfigurable<TParams>
        where TParams : MinimumDeviceParam
    {
        public TParams Parameters { get; set; }

        public MinimumDevice(TParams parameters)
        {
            Parameters = parameters;
        }
    }
}
