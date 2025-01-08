// See https://aka.ms/new-console-template for more information
using Device.Interface;

Console.WriteLine("Hello, World!");

var myPowerSupply = PowerSupply.Factory.ObjectFactory.CreateDevice(Defines.Devices.PowerSupplyVirtual);
myPowerSupply.Methods.Connect("COM3");

while(true)
{
    var curr1 = myPowerSupply.Elements[0].ProcessData.Current;
    var curr2 = myPowerSupply.Elements[1].ProcessData.Current;
    var volt1 = myPowerSupply.Elements[0].ProcessData.Voltage;
    var volt2 = myPowerSupply.Elements[1].ProcessData.Voltage;
    Thread.Sleep(1000);
}


myPowerSupply.Methods.Disconnect();

