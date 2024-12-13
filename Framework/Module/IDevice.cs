using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Framework.Module.Definition;

namespace Framework.Module
{
    public interface IDevice
    {
        DeviceError Connect(string initString);
        DeviceError Disconnect();
    }
}
