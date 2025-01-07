using DummyDevice.Abstract;
using DummyDevice.General.Channels;
using Framework.Libs.Validator;
using Framework.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyDevice.General
{
    public class Device : CommonDevice<DeviceParams, ChannelParams, ChannelProcessData>
    {
        public Device(DeviceParams parameters, IValidator validator, ObservableCollection<BaseChannelWithProcessData<ChannelParams, ChannelProcessData>> elements) : base(parameters, validator, elements)
        {
        }

        public override void DummyDeviceFunction()
        {
            throw new NotImplementedException();
        }

        protected override int CloseConnection()
        {
            throw new NotImplementedException();
        }

        protected override int OpenConnection(string initString)
        {
            throw new NotImplementedException();
        }
    }
}
