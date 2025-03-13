using OneDriver.Device.Interface.HardwareLayer;

namespace OneDriver.ClimateChamber.General.Products
{
    public interface IClimateChamberHAL : IDeviceHAL<ClimateChamberDataHAL>
    {
        double MAX_TEMPERATURE { get; }
        double MIN_TEMPERATURE { get; }
        double ReadDesiredTemperature();
        double ReadDesiredHumidity();
        double ReadCurrentTemperature();
        double ReadCurrentHumidity();
        void Start(double desiredTemperature, double desiredHumidity);
        int Start(double desiredTemperature);
        int StartWithDelay(double desiredTemperature, double desiredHumidity);
        int StartWithDelay(double desiredTemperature);
        bool HasReachedDesiredTemperature();
        bool HasReachedDesiredHumidity();
        int Stop();
    }
}
