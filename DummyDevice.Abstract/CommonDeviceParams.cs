using Framework.Module.Parameter;

namespace DummyDevice.Abstract
{
    public class CommonDeviceParams : BaseDeviceParam
    {
        private int commonDeviceParamDataExample;

        public int CommonDeviceParamDataExample
        {
            get => GetProperty(ref commonDeviceParamDataExample);
            set => SetProperty(ref commonDeviceParamDataExample, value);
        }
        public CommonDeviceParams(string name) : base(name)
        {
        }
    }
}
