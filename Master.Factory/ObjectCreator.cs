using System.Collections.ObjectModel;
using OneDriver.Device.Interface;
using OneDriver.Device.Interface.Master;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Master.Abstract;
using OneDriver.Master.Abstract.Channels;
using OneDriver.Master.Uptp.Channels;
using OneDriver.Master.Uptp.Products;
using OneDriver.Toolbox.ParameterDatabank;
using OneDriver.Framework.Module.Parameter;

namespace Master.Factory
{
    public class BasicMaster
    {
        public CommonDeviceParams Parameters { get; set; }
        public ObservableCollection<BaseSensor> Sensors { get; set; }
        public IMaster Methods { get; set; }
    }
    public class BaseSensor
    {
        public IEnumerable<CommonSensorParameter> SpecificParamCollection { get; set; }
        public IEnumerable<CommonSensorParameter> StandardParamCollection { get; set; }
        public IEnumerable<CommonSensorParameter> SystemParamCollection { get; set; }
        public IEnumerable<CommonSensorParameter> CommandCollection { get; set; }
        public CommonSensorParameter ProcessData { get; set; }
    }

    public class ObjectCreator
    {
        public static BasicMaster CreateDevice(Defines.Devices deviceType)
        {
            BasicMaster device = new BasicMaster();
            
            switch (deviceType)
            {
                case Defines.Devices.MasterUpt_1_3:
                    var obj = new OneDriver.Master.Uptp.Device("UptMaster", new ComportValidator(), new UptMaster_1_3(),
                        new ParamDb());
                    device.Methods = obj;
                    device.Parameters = obj.Parameters;
                    device.Sensors = new ObservableCollection<BaseSensor>();
                    foreach (var ch in obj.Elements)
                    {
                        var item = new BaseSensor();
                        item.SpecificParamCollection = ch.Parameters.SpecificParameterCollection;
                        item.StandardParamCollection = ch.Parameters.StandardParameterCollection;
                        item.SystemParamCollection = ch.Parameters.SystemParameterCollection;
                        item.CommandCollection = ch.Parameters.CommandCollection;
                        device.Sensors.Add(item);
                    }
                    break;
                case Defines.Devices.MasterUptVirtual:
                    obj = new OneDriver.Master.Uptp.Device("VirtualUptMaster", new ComportValidator(), new UptMaster_1_3(),
                        new ParamDb());
                    device.Methods = obj;
                    device.Parameters = obj.Parameters;
                    device.Sensors = new ObservableCollection<BaseSensor>();
                    foreach (var ch in obj.Elements)
                    {
                        var item = new BaseSensor();
                        item.SpecificParamCollection = ch.Parameters.SpecificParameterCollection;
                        item.StandardParamCollection = ch.Parameters.StandardParameterCollection;
                        item.SystemParamCollection = ch.Parameters.SystemParameterCollection;
                        item.CommandCollection = ch.Parameters.CommandCollection;
                        device.Sensors.Add(item);
                    }
                    break;
            }

            return device;
        }
    }

}
