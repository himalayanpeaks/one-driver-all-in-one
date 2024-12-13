﻿using Framework.Base;
using Framework.Module.Parameter;
using Framework.ModuleBuilder;

namespace Framework.Module
{
    public abstract class BaseDeviceWithProcessData<TParams, TProcessData> : BaseDevice<TParams>, IProcessData<TProcessData>
        where TParams : MinimumDeviceParamBase
        where TProcessData : IParameter
    {
        public TProcessData ProcessData { get; set; }

        public BaseDeviceWithProcessData(TParams deviceParams, IValidator validator, TProcessData processData) : base(deviceParams, validator)
        {
            ProcessData = processData;
        }
    }
}
