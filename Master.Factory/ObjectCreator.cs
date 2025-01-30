using System.Collections.ObjectModel;
using OneDriver.Device.Interface;
using OneDriver.Device.Interface.Master;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Master.Abstract;
using OneDriver.Master.Abstract.Channels;
using OneDriver.Master.Uptp.Channels;
using OneDriver.Master.Uptp.Products;
using OneDriver.Toolbox.ParameterDatabank;

namespace OneDriver.Master.Factory
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
        public IEnumerable<SensorParameter> ProcessData { get; set; }

        public BaseSensor(IEnumerable<CommonSensorParameter> specificParamCollection, IEnumerable<CommonSensorParameter> standardParamCollection, IEnumerable<CommonSensorParameter> systemParamCollection, IEnumerable<CommonSensorParameter> commandCollection, IEnumerable<SensorParameter> processData)
        {
            SpecificParamCollection = specificParamCollection;
            StandardParamCollection = standardParamCollection;
            SystemParamCollection = systemParamCollection;
            CommandCollection = commandCollection;
            ProcessData = processData;
        }
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
                        var item = new BaseSensor(ch.Parameters.SpecificParameterCollection, ch.Parameters.SpecificParameterCollection,
                            ch.Parameters.SystemParameterCollection, ch.Parameters.CommandCollection, ch.ProcessData.PdInCollection);
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
                        var item = new BaseSensor(ch.Parameters.SpecificParameterCollection, ch.Parameters.SpecificParameterCollection,
                            ch.Parameters.SystemParameterCollection, ch.Parameters.CommandCollection, ch.ProcessData.PdInCollection);
                        device.Sensors.Add(item);
                    }
                    break;
            }

            return device;
        }
    }

}
