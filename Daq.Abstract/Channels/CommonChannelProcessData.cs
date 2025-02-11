using OneDriver.Framework.Module.Parameter;

namespace OneDriver.Daq.Abstract.Channels
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
