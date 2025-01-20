using Device.Interface.DummyDevice;
using OneDriver.DummyDevice.General.Products;
using OneDriver.Framework.Libs.Validator;
using OneDriver.DummyDevice.Abstract;
using OneDriver.DummyDevice.Abstract.Channels;
using OneDriver.Framework.Module.Parameter;
using System.Collections.ObjectModel;
using OneDriver.Device.Interface;

namespace OneDriver.DummyDevice.Factory
{
    public class BasicDummyDevice
    {
        public CommonDeviceParams? Parameters { get; set; }
        public IDummyDevice? Methods { get; set; }
        public ObservableCollection<BaseChannelWithProcessData<CommonChannelParams, CommonChannelProcessData>>? Elements { get; set; }
    }
    public class ObjectFactory
    {
        public static BasicDummyDevice CreateDevice(Defines.Devices deviceType)
        {
            BasicDummyDevice device = new BasicDummyDevice();
            switch (deviceType)
            {
                case Defines.Devices.DummyDeviceVirtual:
                    var obj = new DummyDevice.General.Device("DummyDeviceVirtual", new ComportValidator(), new VirtualDevice());
                    device.Methods = obj;
                    device.Parameters = obj.Parameters;
                    device.Elements = new ObservableCollection<BaseChannelWithProcessData<CommonChannelParams, CommonChannelProcessData>>();
                    foreach (var ch in obj.Elements)
                    {
                        var item = new BaseChannelWithProcessData<CommonChannelParams, CommonChannelProcessData>(new CommonChannelParams(ch.Parameters.Name), new CommonChannelProcessData());
                        item.Parameters = ch.Parameters;
                        item.ProcessData = ch.ProcessData;
                        device.Elements.Add(item);
                    }

                    break;
            }
            return device;
        }

    }
}
