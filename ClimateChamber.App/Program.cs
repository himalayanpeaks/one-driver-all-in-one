using OneDriver.ClimateChamber.Factory;
using OneDriver.Device.Interface;

internal class Program
{
    private static void Main(string[] args)
    {
        var myProbe = ObjectFactory.CreateDevice(Defines.Devices.Espec);
        myProbe.Methods.Connect("COM3");
        
        myProbe.Parameters.MinimumTemperature = 0;

        myProbe.Parameters.MaximumWarningTemperatureLimit = 80;
        myProbe.Parameters.MaximumAlarmTemperatureLimit = 110;
        myProbe.Parameters.MaximumTemperature = 100;
        ((OneDriver.ClimateChamber.General.DeviceParams)myProbe.Parameters).DesiredHumidity = 60;

        myProbe.Methods.Start(25);
        myProbe.Methods.Stop();

        myProbe.Methods.Disconnect();
    }
}