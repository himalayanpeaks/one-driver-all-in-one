using DummyDevice.Abstract.Channels;

namespace DummyDevice.General.Channels
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
