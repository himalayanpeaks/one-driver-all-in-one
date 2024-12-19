using Framework.Module;
using Framework.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceParam.Ui
{
    public class DeviceParametersViewModel<TParams> : BaseDeviceViewModel<TParams>
        where TParams : BaseDeviceParam
    {
        public DynamicPropertyWrapper ParametersWrapper { get; }

        public DeviceParametersViewModel(BaseDevice<TParams> device) : base(device)
        {
            ParametersWrapper = new DynamicPropertyWrapper(device.Parameters);
        }
    }

}
