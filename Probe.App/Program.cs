// See https://aka.ms/new-console-template for more information
using Device.Interface;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var myProbe = Probe.Factory.ObjectFactory.CreateDevice(Defines.Devices.ProbeVirtual);
        myProbe.Methods.Connect("COM3");

        while (true)
        {
            var temp1 = myProbe.Elements[0].ProcessData.Temperature;
            var humid1 = ((Probe.General.Channels.ChannelProcessData)(myProbe.Elements[0].ProcessData)).Humidity;
            var temp2 = myProbe.Elements[1].ProcessData.Temperature;
            var humid2 = ((Probe.General.Channels.ChannelProcessData)(myProbe.Elements[1].ProcessData)).Humidity;
            Thread.Sleep(1000);
        }


        myProbe.Methods.Disconnect();
    }
}