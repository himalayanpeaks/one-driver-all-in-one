﻿using OneDriver.Framework.Module.Parameter;
using OneDriver.Device.Interface.Master;

namespace OneDriver.Master.Abstract
{
    public class CommonDeviceParams : BaseDeviceParam
    {
        private int protocolId;
        private Definition.Mode _mode;

        public int ProtocolId
        {
            get => GetProperty(ref protocolId);
            set => SetProperty(ref protocolId, value);
        }
        public Definition.Mode Mode
        {
            get => _mode;
            set => SetProperty(ref _mode, value);
        }
        public CommonDeviceParams(string name) : base(name)
        {
        }
    }
}
