using Device.Interface;
using Device.Interface.DummyDevice;
using DummyDevice.Abstract;
using DummyDevice.Abstract.Channels;
using DummyDevice.General.Products;
using Framework.Libs.Validator;
using Framework.Module.Parameter;
using System.Collections.ObjectModel;

namespace DummyDevice.Factory
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
                case Defines.Devices.PowerSupplyVirtual:
                    var obj = new General.Device("DummySupplyVirtual", new ComportValidator(), new VirtualDevice());
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
