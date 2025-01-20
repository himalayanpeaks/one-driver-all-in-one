using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.Device.Interface.Master
{
    public interface IMaster
    {
        public int SelectSensorAtPort(int portNumber);
        public int ConnectSensor();
        public int DisconnectSensor();
        public int UpdateDataFromSensor();
        public int UpdateDataFromAllSensors();
        public int ReadParameterFromSensor(string name, out string? value);
        public int ReadParameterFromSensor<T>(string name, out T? value);
        public int WriteParameterToSensor(string name, string value);
        public int WriteParameterToSensor<T>(string name, T value);
        public int WriteCommandToSensor(string name, string value);
        public int WriteCommandToSensor<T>(string name, T value);
        public string GetErrorMessage(int errorCode);
        public string?[] GetAllParamsFromSensor();
        public void LoadDataFromPdb(string server, int deviceId, int protocolId);
    }
}
