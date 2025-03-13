using OneDriver.ClimateChamber.Abstract;
using OneDriver.Device.Interface;
using OneDriver.Device.Interface.ClimateChamber;
using OneDriver.ClimateChamber.Basic.Products;
using OneDriver.ClimateChamber.General.Products;
using OneDriver.Framework.Libs.Validator;

namespace OneDriver.ClimateChamber.Factory
{
    public class BasicClimateChamber
    {
        public CommonDeviceParams Parameters { get; set; }
        public IClimateChamber? Methods { get; set; }
        public CommonProcessData ProcessData { get; set; }
    }
    public class ObjectFactory
    {
        public static BasicClimateChamber CreateDevice(Defines.Devices deviceType)
        {
            BasicClimateChamber device = new BasicClimateChamber();
            switch (deviceType)
            {
                case Defines.Devices.Espec:
                    var obj = new General.Device("Espec", new ComportValidator(), new Espec());
                    device.Methods = obj;
                    device.Parameters = obj.Parameters;
                    device.ProcessData = obj.ProcessData;
                    break;
                case Defines.Devices.Voetsch:
                    var minobj = new Basic.Device("VoetschEthernet", new IpAddressValidator(), new VoetschEthernet());
                    device.Methods = minobj;
                    device.Parameters = minobj.Parameters;
                    device.ProcessData = minobj.ProcessData;
                    break;
                case Defines.Devices.VoetschAm:
                    minobj = new Basic.Device("VoetschSerialAmberg", new ComportValidator(), new VoetschSerial());
                    device.Methods = minobj;
                    device.Parameters = minobj.Parameters;
                    device.ProcessData = minobj.ProcessData;
                    break;
                case Defines.Devices.ClimateChamberVirtual:
                    obj = new General.Device("VirtualClimateChamber", new ComportValidator(), new VirtualClimateChamber());
                    device.Methods = obj;
                    device.Parameters = obj.Parameters;
                    device.ProcessData = obj.ProcessData;
                    break;
            }
            return device;
        }
    }
}
