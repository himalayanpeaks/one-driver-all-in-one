﻿using OneDriver.Device.Interface.HardwareLayer;

namespace OneDriver.Master.Uptp.Products
{
    public interface IUptHAL : IDeviceHAL<InternalDataHAL>
    {
        Definition.e_com ConnectSensorWithMaster();
        Definition.e_com DisconnectSensorFromMaster();
        Definition.e_error_codes ReadParam(ushort index, out byte[] data);
        Definition.e_error_codes WriteParam(ushort index, byte[] data);
        Definition.e_error_codes SetProcessData(ushort index, out int lengthInBytes);
        Definition.e_error_codes WriteCommand(Definition.e_sspp_cmds command,
            ref byte[] aRequestData);
        Definition.e_slave_com_port SensorPortNumber { get; set; }
        Definition.e_error_codes PowerOff();
        Definition.e_error_codes PowerOn();
    }
}
