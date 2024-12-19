using DeviceParam.Ui;
using Framework.Libs.Validator;
using Framework.Module;
using Framework.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Device.Ui.ExampleDevice;

namespace Device.Ui
{
    public class ExampleClass : BaseDeviceViewModel<ExampleParams>
    {
        public ObservableCollection<KeyValuePair<string, object>> ParametersCollection { get; }

        public ExampleClass(ExampleDevice device) : base(device)
        {
            var wrapper = new DynamicPropertyWrapper(device.Parameters);
            ParametersCollection = wrapper.Properties;
        }
    }

    public class ExampleDevice : BaseDevice<ExampleParams>
    {
        public ExampleDevice(ExampleParams parameters) : base(parameters, new ComportValidator()) { }

        protected override int CloseConnection()
        {
            throw new NotImplementedException();
        }

        protected override int OpenConnection(string initString)
        {
            throw new NotImplementedException();
        }
    }
        public class ExampleParams : BaseDeviceParam
    {
        private int count;
        private string version;

        public ExampleParams(string name) : base(name) { }

        public int Count
        {
            get => GetProperty(ref count);
            set => SetProperty(ref count, value);
        }

        public string Version
        {
            get => GetProperty(ref version);
            set => SetProperty(ref version, value);
        }
    }
}
