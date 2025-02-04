// See https://aka.ms/new-console-template for more information
using OneDriver.Device.Interface;
using OneDriver.Device.Interface.PowerSupply;
using OneDriver.PowerSupply.Factory;

Console.WriteLine("Hello, World!");

var myPowerSupply = ObjectFactory.CreateDevice(Defines.Devices.PowerSupplyKd3005p);
myPowerSupply.Methods.Connect("COM12");
var max = myPowerSupply.Parameters.MaxAmps;
//myPowerSupply.Elements[0].Parameters.ControlMode = Definition.ControlMode.Voltage;
myPowerSupply.Methods.SetVolts(0, 4);
myPowerSupply.Elements[0].Parameters.DesiredVolts = 5;

myPowerSupply.Elements[0].Parameters.ControlMode = Definition.ControlMode.Current;
myPowerSupply.Methods.SetAmps(0, 0.4);
myPowerSupply.Elements[0].Parameters.DesiredAmps = 0.2;
while (true)
{
    var curr1 = myPowerSupply.Elements[0].ProcessData.Current;
    var volt1 = myPowerSupply.Elements[0].ProcessData.Voltage;
    Thread.Sleep(1000);
}


myPowerSupply.Methods.Disconnect();

