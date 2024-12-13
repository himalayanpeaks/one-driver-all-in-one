using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Module
{
    public class Definition
    {
        public enum DeviceError
        { 
            NoError = 0,
            InvalidInitString,
            ConnectionError,
            DisconnectionError,
            InvalidParameter,
            InvalidCommand,
            InvalidResponse,
            Timeout,
            UnknownError,
            AlreadyConnected
        }
        public enum DeviceState
        {
            Disconnected,
            Connecting,
            Connected,
            Error
        }

    }
}

