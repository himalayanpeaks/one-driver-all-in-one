using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.Device.Interface.Daq
{

    public class Definition
    {
        public enum ChannelType
        {
            AnalogIn,
            AnalogOut,
            DigitalIn,
            DigitalOut,
            Frequency,
            CounterIn,
            CounterOut,
        }
    }
}
