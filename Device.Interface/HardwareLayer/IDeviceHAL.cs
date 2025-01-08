﻿using Framework.Libs.Announcer;
using Framework.Libs.Validator;
using Framework.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Interface.HardwareLayer
{
    
    public interface IDeviceHAL<TInternalDataHAL> where TInternalDataHAL : BaseDataForAnnouncement, new()
    {
        public delegate void ProcessDataAnnouncer(TInternalDataHAL dataHAL);
        ConnectionError Open(string initString, IValidator validator);
        ConnectionError Close();
        void StartProcessDataAnnouncer();
        void StopProcessDataAnnouncer();
        void AttachToProcessDataEvent(DataTunnel<TInternalDataHAL>.DataEventHandler processDataEventHandler);
        int NumberOfChannels { get; }
    }
}
