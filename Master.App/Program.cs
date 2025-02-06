// See https://aka.ms/new-console-template for more information

using OneDriver.Device.Interface;
using OneDriver.Device.Interface.Master;
using OneDriver.Master.Factory;
using OneDriver.Master.Uptp;

internal class Program
{
    private static void Main(string[] args)
    {
        var myMaster = ObjectCreator.CreateDevice(Defines.Devices.MasterUpt_1_3);
        var err = myMaster.Methods.Connect("COM11");
        var errn = myMaster.Methods.SelectSensorAtPort(0);
        var nerr = myMaster.Methods.ConnectSensor();
        var message = myMaster.Methods.GetErrorMessage(nerr);
        errn = myMaster.Methods.LoadDataFromPdb("https://pfde-vm-param.eu.p-f.biz:5054/", 94, myMaster.Parameters.ProtocolId);
        nerr = myMaster.Methods.ReadParameterFromSensor<uint>("IDX_MEAS_CYCLE", out var myVal);
        message = myMaster.Methods.GetErrorMessage(nerr);
        nerr = myMaster.Methods.WriteParameterToSensor<uint>("IDX_MEAS_CYCLE", 222);
        message = myMaster.Methods.GetErrorMessage(nerr);
        nerr = myMaster.Methods.ReadParameterFromSensor<uint>("IDX_MEAS_CYCLE", out myVal);
        message = myMaster.Methods.GetErrorMessage(nerr);
        nerr = myMaster.Methods.WriteParameterToSensor<uint>("IDX_MEAS_CYCLE", 333);
        message = myMaster.Methods.GetErrorMessage(nerr);
        var allParams = myMaster.Methods.GetAllParamsFromSensor();
        ((Device)myMaster.Methods).AddProcessDataIndex(288);
        myMaster.Parameters.Mode = Definition.Mode.ProcessData;
        
        int i = 0;
        while ( i++ < 10)
        {
            var pr = myMaster.Sensors[0].ProcessData.ToArray()[0].Value;
        }
        nerr = myMaster.Methods.DisconnectSensor();
        err = myMaster.Methods.Disconnect();
    }
}