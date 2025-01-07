using static Device.Interface.DummyDevice.Definition;

namespace Device.Interface.DummyDevice
{
    public interface IDummyChannel
    {
        void DummyChannel(DummyEnum dummyEnum);
    }
}
