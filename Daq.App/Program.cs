// See https://aka.ms/new-console-template for more information

using OneDriver.Device.Interface;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var myDaq = OneDriver.Daq.Factory.ObjectFactory.CreateDevice(Defines.Devices.DaqNiUsb);
        myDaq.Methods.Connect("Dev1");
        myDaq.Methods.SetDoChannels("Out=1");
        myDaq.Methods.GetDiChannels(new List<string>() { "In" }, out var str);
        myDaq.Methods.SetDoChannels("Out=0");
        myDaq.Methods.GetDiChannels(new List<string>(){ "In" }, out str );


        myDaq.Methods.Disconnect();
    }
}
