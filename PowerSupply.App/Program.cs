// See https://aka.ms/new-console-template for more information
using Device.Interface;

Console.WriteLine("Hello, World!");

var myPowerSupply = PowerSupply.Factory.Factory.CreateDevice(Defines.Devices.PowerSupplyVirtual);
myPowerSupply.Methods.Connect("COM3");


var curr = myPowerSupply.Elements[0].ProcessData.Current;
Thread.Sleep(1000);
curr = myPowerSupply.Elements[0].ProcessData.Current;
Thread.Sleep(1000);
curr = myPowerSupply.Elements[0].ProcessData.Current;
Thread.Sleep(1000);
curr = myPowerSupply.Elements[0].ProcessData.Current;
Thread.Sleep(1000);
curr = myPowerSupply.Elements[0].ProcessData.Current;
Thread.Sleep(1000);
curr = myPowerSupply.Elements[0].ProcessData.Current;
Thread.Sleep(1000);
curr = myPowerSupply.Elements[0].ProcessData.Current;
Thread.Sleep(1000);
curr = myPowerSupply.Elements[0].ProcessData.Current;
myPowerSupply.Methods.Disconnect();

