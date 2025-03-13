using OneDriver.Framework.Libs.Announcer;

namespace OneDriver.ClimateChamber.General.Products
{
    public class ClimateChamberDataHAL : BaseDataForAnnouncement
    {
        public ClimateChamberDataHAL(double currentTemperature, double currentHumidity, bool isTemperatureReached, bool isHumidityReached)
        {
            CurrentTemperature = currentTemperature;
            TimeStamp = DateTime.Now;
            CurrentHumidity = currentHumidity;
            IsTemperatureReached = isTemperatureReached;
            IsHumidityReached = isHumidityReached;
        }

        public ClimateChamberDataHAL()
        {
            TimeStamp = DateTime.Now;
        }

        public double CurrentTemperature { get; }
        public double CurrentHumidity { get; }
        public bool IsTemperatureReached { get; }
        public bool IsHumidityReached { get; }
    }
}
