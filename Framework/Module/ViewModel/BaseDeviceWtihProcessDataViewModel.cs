using Framework.Base;
using Framework.Module.Parameter;

namespace Framework.Module.ViewModel
{
    public abstract class BaseDeviceWithProcessDataViewModel<TParams, TProcessData> : BaseDeviceViewModel<TParams>
        where TParams : BaseDeviceParam
        where TProcessData : BaseProcessData
    {
        public readonly BaseDeviceWithProcessData<TParams, TProcessData> Device;

        protected BaseDeviceWithProcessDataViewModel(BaseDeviceWithProcessData<TParams, TProcessData> device) : base(device)
        {
            Device = device;
        }
    }
}
