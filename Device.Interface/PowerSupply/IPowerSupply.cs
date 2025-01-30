﻿using OneDriver.Framework.Module;

namespace OneDriver.Device.Interface.PowerSupply
{
    public interface IPowerSupply : IDevice
    {
        int SetVolts(int channelNumber, double volts);
        int SetAmps(int channelNumber, double amps);
        int AllChannelsOn();
        int AllChannelsOff();
    }
}
