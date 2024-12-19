using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSupply.General.Products
{
    public class Definition
    {
        public enum ControlMode
        {
            Voltage,
            Current
        }
        public enum ErrorState
        {
            NoError,
            OverVoltage,
            OverCurrent,
            CommunicationError,
            Unknown
        }
    }
}
