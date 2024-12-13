using Framework.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInterface
{
    public interface IPowerSupply : IDevice
    {
        int AllChannelsOn();
        int AllChannelsOff();
    }
}
