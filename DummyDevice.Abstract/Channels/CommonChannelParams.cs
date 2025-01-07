using Framework.Module.Parameter;

namespace DummyDevice.Abstract.Channels
{
    public class CommonChannelParams : BaseChannelParam
    {
        private int commonParamExample;

        public int CommonParamExample 
        { 
            get => GetProperty(ref commonParamExample); 
            set => SetProperty(ref commonParamExample, value); 
        }
        public CommonChannelParams(string name) : base(name)
        {            
        }
    }
}
