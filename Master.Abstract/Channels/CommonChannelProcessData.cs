using OneDriver.Framework.Module.Parameter;
using OneDriver.Framework.ModuleBuilder;

namespace OneDriver.Master.Abstract.Channels
{
    public class CommonChannelProcessData<TSensorParameter> : BaseProcessData
                where TSensorParameter : CommonSensorParameter

    {
        private List<TSensorParameter> _processData;

        public List<TSensorParameter> ProcessData
        {
            get => _processData;
            set => SetProperty(ref _processData, value);
        }

        public CommonChannelProcessData()
        {
            ProcessData = new List<TSensorParameter>();
        }
    }
}
