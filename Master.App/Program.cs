// See https://aka.ms/new-console-template for more information

using OneDriver.Device.Interface;
using OneDriver.Master.Factory;
internal class Program
{
    private static void Main(string[] args)
    {
        var myMaster = ObjectCreator.CreateDevice(Defines.Devices.MasterUpt_1_3);
        var err = myMaster.Methods.Connect("COM11");
        var errn = myMaster.Methods.SelectSensorAtPort(0);
        var nerr = myMaster.Methods.ConnectSensor();
        var message = myMaster.Methods.GetErrorMessage(nerr);
        errn = myMaster.Methods.LoadDataFromPdb("https://pfde-vm-param.eu.p-f.biz:5054/", 96, myMaster.Parameters.ProtocolId);
        nerr = myMaster.Methods.ReadParameterFromSensor<uint>("IDX_MEAS_CYCLE", out var myVal);
        message = myMaster.Methods.GetErrorMessage(nerr);
        nerr = myMaster.Methods.WriteParameterToSensor<uint>("IDX_MEAS_CYCLE", 200);
        message = myMaster.Methods.GetErrorMessage(nerr);
        nerr = myMaster.Methods.ReadParameterFromSensor<uint>("IDX_MEAS_CYCLE", out myVal);
        message = myMaster.Methods.GetErrorMessage(nerr);
        nerr = myMaster.Methods.WriteParameterToSensor<uint>("IDX_MEAS_CYCLE", 50000);
        message = myMaster.Methods.GetErrorMessage(nerr);
        var allParams = myMaster.Methods.GetAllParamsFromSensor();
        errn = myMaster.Methods.LoadDataFromPdb("https://pfde-vm-param.eu.p-f.biz:5054/", myMaster.Parameters.ProtocolId, out var hash);
        allParams = myMaster.Methods.GetAllParamsFromSensor();

        nerr = myMaster.Methods.DisconnectSensor();
        err = myMaster.Methods.Disconnect();
    }
}