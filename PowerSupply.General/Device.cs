using Framework.Libs.Validator;
using Framework.Module.Parameter;
using Framework.ModuleBuilder;
using PowerSupply.Abstract;
using PowerSupply.Abstract.Channels;
using PowerSupply.General.Channels;
using PowerSupply.General.Products;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Channels;
using System.Xml.Linq;

namespace PowerSupply.General
{
    public class Device : CommonDevice<DeviceParams, ChannelParams, ChannelProcessData>
    {
        public Device(string name, IValidator validator, IPowerSupplyHAL powerSupplyHAL) : 
            base(new DeviceParams(name), validator, new ObservableCollection<BaseChannelWithProcessData<ChannelParams, ChannelProcessData>>())
        {
            _powerSupplyHAL = powerSupplyHAL;
            Init(name);
        }

        private void Init(string name)
        {
            Parameters = new DeviceParams(name);
            Parameters.PropertyChanging += Parameters_PropertyChanging;
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            _powerSupplyHAL.AttachToProcessDataEvent(ProcessDataChanged);


            for (var i = 0; i < _powerSupplyHAL.NumberOfChannels; i++)
            {
                var item = new BaseChannelWithProcessData<ChannelParams, ChannelProcessData>(new ChannelParams("Ch" + i.ToString()), new ChannelProcessData());
                Elements.Add(item);
                Elements[i].Parameters.PropertyChanged += Parameters_PropertyChanged;
                Elements[i].Parameters.PropertyChanging += Parameters_PropertyChanging;
            }
            
        }

        private void ProcessDataChanged(object sender, InternalDataHAL e)
        {
            Elements[e.ChannelNumber].ProcessData.Current = e.CurrentCurrent;
            Elements[e.ChannelNumber].ProcessData.Voltage = e.CurrentVoltage;
            Elements[e.ChannelNumber].ProcessData.TimeStamp = e.TimeStamp;
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Parameters_PropertyChanging(object sender, Framework.Base.PropertyValidationEventArgs e)
        {
            throw new NotImplementedException();
        }

        IPowerSupplyHAL _powerSupplyHAL;
        protected override int CloseConnection() => (int)_powerSupplyHAL.Close();
        
        protected override int OpenConnection(string initString) => (int)_powerSupplyHAL.Open(initString, validator);
        
    }
}
