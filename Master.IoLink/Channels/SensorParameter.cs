using OneDriver.Master.Abstract.Channels;
using static OneDriver.Device.Interface.Defines;

namespace OneDriver.Master.IoLink.Channels
{
    public class SensorParameter : CommonSensorParameter
    {
        private int _subindex;

        public int Subindex
        {
            get => _subindex;
            set => SetProperty(ref _subindex, value);
        }

        public SensorParameter(string name, int index, int subindex, AccessType access, DataType dataType,
            int arrayCount, int lengthInBits, int offset, string? value, string? @default, string? minimum, string? maximum, string? valid) :
            base(name, index, access, dataType, arrayCount, lengthInBits, offset, value, @default, minimum, maximum, valid)
        {
            _subindex = subindex;
        }

        public SensorParameter(CommonSensorParameter commonSensorParameter) :
            base(commonSensorParameter.Name, commonSensorParameter.Index, commonSensorParameter.Access, commonSensorParameter.DataType,
                commonSensorParameter.ArrayCount, commonSensorParameter.LengthInBits, commonSensorParameter.Offset,
                commonSensorParameter.Value, commonSensorParameter.Default, commonSensorParameter.Minimum,
                commonSensorParameter.Maximum, commonSensorParameter.Valid)
        {
            _subindex = 0;
        }

        public SensorParameter(string name) : base(name)
        {
        }

        public SensorParameter() : base("")
        {

        }
    }
}