using OneDriver.Framework.Module.Parameter;
using OneDriver.Master.Abstract.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OneDriver.Device.Interface.Defines;

namespace OneDriver.Master.Uptp.Channels
{
    public class SensorParameter : CommonSensorParameter
    {
        public SensorParameter(string name, int index, AccessType access, DataType dataType, int arrayCount, int lengthInBits,
        int offset,
        string? value, string? @default, string? minimum, string? maximum, string? valid) :
        base(name, index, access, dataType, arrayCount, lengthInBits, offset, value, @default, minimum, maximum, valid)
        {
        }

        public SensorParameter(CommonSensorParameter commonSensorParameter) :
            base(commonSensorParameter.Name, commonSensorParameter.Index, commonSensorParameter.Access, commonSensorParameter.DataType,
                commonSensorParameter.ArrayCount, commonSensorParameter.LengthInBits, commonSensorParameter.Offset,
                commonSensorParameter.Value, commonSensorParameter.Default, commonSensorParameter.Minimum,
                commonSensorParameter.Maximum, commonSensorParameter.Valid)
        {
        }

        public SensorParameter(string name) : base (name)
        { 
        }

        public SensorParameter() : base("")
        {
            
        }
    }
}
