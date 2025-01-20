using OneDriver.Framework.Module.Parameter;

namespace OneDriver.Master.Abstract.Channels
{
    public class CommonChannelParams<TSensorParameter> : BaseChannelParam
            where TSensorParameter : CommonSensorParameter
    {
        private int _deviceId; //Add
        private string _hashId = string.Empty;
        private List<TSensorParameter> _specificParameters = new List<TSensorParameter>();
        private List<TSensorParameter> _standardParameters = new List<TSensorParameter>();
        private List<TSensorParameter> _systemParameters = new List<TSensorParameter>();
        private List<TSensorParameter> _commands = new List<TSensorParameter>();
        public CommonChannelParams(string name) : base(name)
        {
            SpecificParameters = new List<TSensorParameter>();
            StandardParameters = new List<TSensorParameter>();
            SystemParameters = new List<TSensorParameter>();
            Commands = new List<TSensorParameter>();
        }
        public int DeviceId
        {
            get => _deviceId;
            set => SetProperty(ref _deviceId, value);
        }

        public string HashId
        {
            get => _hashId;
            set { SetProperty(ref _hashId, value); }
        }

        public List<TSensorParameter> StandardParameters
        {
            get => _standardParameters;
            set => SetProperty(ref _standardParameters, value);
        }

        public List<TSensorParameter> SpecificParameters
        {
            get => _specificParameters;
            set => SetProperty(ref _specificParameters, value);
        }

        public List<TSensorParameter> SystemParameters
        {
            get => _systemParameters;
            set => SetProperty(ref _systemParameters, value);
        }
        public List<TSensorParameter> Commands
        {
            get => _commands;
            set => SetProperty(ref _commands, value);
        }
    }
}
