using System.Collections.ObjectModel;
using OneDriver.Device.Interface;
using OneDriver.Device.Interface.PowerSupply;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module.Parameter;
using OneDriver.PowerSupply.Abstract;
using OneDriver.PowerSupply.Abstract.Channels;
using OneDriver.PowerSupply.General.Products;

namespace OneDriver.PowerSupply.Factory
{
    public class BasicPowerSupply
    {
        public CommonDeviceParams Parameters { get; set; }
        public IPowerSupply Methods { get; set; }
        public ObservableCollection<BaseChannelWithProcessData<CommonChannelParams, CommonProcessData>> Elements { get; set; }
    }

    public class ObjectFactory
    {
        public static BasicPowerSupply CreateDevice(Defines.Devices deviceType)
        {
            BasicPowerSupply device = new BasicPowerSupply();   
            switch (deviceType)
            {
                case Defines.Devices.PowerSupplyVirtual:
                    var obj = new OneDriver.PowerSupply.General.Device("PowerSupplyVirtual", new ComportValidator(), new VirtualPowerSupply());                    
                    device.Methods = obj;                        
                    device.Parameters = obj.Parameters;
                    device.Elements = new ObservableCollection<BaseChannelWithProcessData<CommonChannelParams, CommonProcessData>>();
                    foreach (var ch in obj.Elements)
                    {
                        var item = new BaseChannelWithProcessData<CommonChannelParams, CommonProcessData>(new CommonChannelParams(ch.Parameters.Name), new CommonProcessData());
                        item.Parameters = ch.Parameters;
                        item.ProcessData = ch.ProcessData;
                        device.Elements.Add(item);
                    }   
                    
                    break;
                case Defines.Devices.PowerSupplyKd3005p:
                    obj = new General.Device("PowerSupplyVirtual", new ComportValidator(), new Kd3005p());
                    device.Methods = obj;
                    device.Parameters = obj.Parameters;
                    device.Elements = new ObservableCollection<BaseChannelWithProcessData<CommonChannelParams, CommonProcessData>>();
                    foreach (var ch in obj.Elements)
                    {
                        var item = new BaseChannelWithProcessData<CommonChannelParams, CommonProcessData>(new CommonChannelParams(ch.Parameters.Name), new CommonProcessData())
                            {
                                Parameters = ch.Parameters,
                                ProcessData = ch.ProcessData
                            };
                        device.Elements.Add(item);
                    }

                    break;
            }
            return device;
        }
    }
}
