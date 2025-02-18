using OneDriver.Daq.Abstract;
using OneDriver.Daq.Abstract.Channels;
using OneDriver.Daq.General.Products;
using OneDriver.Device.Interface;
using OneDriver.Device.Interface.DummyDevice;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module.Parameter;
using System.Collections.ObjectModel;
using OneDriver.Device.Interface.Daq;

namespace OneDriver.Daq.Factory
{
    public class BasicDummyDevice
    {
        public CommonDeviceParams? Parameters { get; set; }
        public IDaq? Methods { get; set; }
        public ObservableCollection<BaseChannel<CommonChannelParams>>? Elements { get; set; }
    }
    public class ObjectFactory
    {
        public static BasicDummyDevice CreateDevice(Defines.Devices deviceType)
        {
            BasicDummyDevice device = new BasicDummyDevice();
            switch (deviceType)
            {
                case Defines.Devices.DaqNiUsb:
                    var obj = new General.Device("NiUsb", new NiUsbValidator(), new NiUsb());
                    device.Methods = obj;
                    device.Parameters = obj.Parameters;
                    device.Elements = new ObservableCollection<BaseChannel<CommonChannelParams>>();
                    foreach (var ch in obj.Elements)
                    {
                        var item = new BaseChannel<CommonChannelParams>(new CommonChannelParams(ch.Parameters.Name, ch.Parameters.PhysicalAddress, ch.Parameters.Type))
                            {
                                Parameters = ch.Parameters
                            };
                        device.Elements.Add(item);
                    }
                    break;
            }
            return device;
        }
    }
}
