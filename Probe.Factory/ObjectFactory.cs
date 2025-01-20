using Device.Interface;
using Device.Interface.Probe;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module.Parameter;
using OneDriver.Probe.Abstract;
using OneDriver.Probe.Abstract.Channels;
using OneDriver.Probe.General.Products;
using System.Collections.ObjectModel;

namespace Probe.Factory
{
    public class BasicDummyDevice
    {
        public CommonDeviceParams? Parameters { get; set; }
        public IProbe? Methods { get; set; }
        public ObservableCollection<BaseChannelWithProcessData<CommonChannelParams, CommonChannelProcessData>>? Elements { get; set; }
    }
    public class ObjectFactory
    {
        public static BasicDummyDevice CreateDevice(Defines.Devices deviceType)
        {
            BasicDummyDevice device = new BasicDummyDevice();
            switch (deviceType)
            {
                case Defines.Devices.ProbeVirtual:
                    var obj = new OneDriver.Probe.General.Device("ProbeVirtual", new ComportValidator(), new VirtualDevice());
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
