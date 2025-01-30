using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OneDriver.Framework.Module.Definition;

namespace OneDriver.Framework.Module
{
    public interface IDevice
    {
        DeviceError Connect(string initString);
        DeviceError Disconnect();

    }
}
