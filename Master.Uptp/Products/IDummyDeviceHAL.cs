﻿using Device.Interface.HardwareLayer;
using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;

namespace OneDriver.Master.Uptp.Products
{
    public interface IDummyDeviceHAL : IDeviceHAL<InternalDummyDeviceDataHAL>
    {
        void HALFunction();
    }
}
