using Framework.Base;
using Framework.Module.Parameter;
using Framework.ModuleBuilder;

namespace Framework.Module
{
    public abstract class MinimumDeviceWithProcessData<TParams, TProcessData> : MinimumDevice<TParams>, IProcessData<TProcessData>
        where TParams : MinimumDeviceParam
        where TProcessData : IParameter
    {
        public TProcessData ProcessData { get; set; }

        public MinimumDeviceWithProcessData(TParams deviceParams, TProcessData processData) : base(deviceParams)
        {
            ProcessData = processData;
        }
    }
}
