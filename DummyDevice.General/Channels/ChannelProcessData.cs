using OneDriver.DummyDevice.Abstract.Channels;

namespace OneDriver.DummyDevice.General.Channels
{
    public class ChannelProcessData : CommonChannelProcessData
    {
        private string generalProcessSampleData;

        public string GeneralProcessSampleData
        {
            get => GetProperty(ref generalProcessSampleData);
            set => SetProperty(ref generalProcessSampleData, value);
        }
    }
}
