using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using OneDriver.Framework.Libs.DeviceDescriptor;
using ParameterTool.NSwagClass.Generator;
using ParameterTool.NSwagClass.Generator.Interface;

namespace OneDriver.Toolbox.ParameterDatabank
{
    public class ParamDb : IDeviceDescriptor
    {
        public List<ParameterDetailsResponse> ReadData(string server, string hashId, int protocolId)
        {
            HttpClient httpClient = new HttpClient();
            ParameterToolClient parameterToolClient = new ParameterToolClient(server, httpClient);
            var device = parameterToolClient.Device_GetDeviceByHashAsync(hashId).Result;
            return ReadData(server, device.DeviceId, protocolId);
        }

        public List<ParameterDetailsResponse> ReadData(string server, int deviceId, int protocolId)
        {
            HttpClient httpClient = new HttpClient();
            ParameterToolClient parameterToolClient = new ParameterToolClient(server, httpClient);
            return parameterToolClient.Device_GetParametersByDeviceAsync
                (deviceId, null, protocolId, null).Result.ToList();
        }
    }
}
