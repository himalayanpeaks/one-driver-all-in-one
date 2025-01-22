using OneDriver.Master.Abstract.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.Master.Uptp.Channels
{
    public class Channel : CommonChannel<SensorParameter>
    {
        protected Channel(CommonChannelParams<SensorParameter> parameters, CommonChannelProcessData<SensorParameter> processData)
            : base(parameters, processData)
        {
        }
    }
}
