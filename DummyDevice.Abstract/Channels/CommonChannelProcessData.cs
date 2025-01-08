using Framework.Module.Parameter;

namespace DummyDevice.Abstract.Channels
{
    public class CommonChannelProcessData : BaseProcessData
    {
        private int commonProcessDataExample;

        public int CommonProcessSampleData
        {
            get => GetProperty(ref commonProcessDataExample);
            set => SetProperty(ref commonProcessDataExample, value);
        }
    }
}
