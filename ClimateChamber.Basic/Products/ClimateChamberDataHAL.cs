using OneDriver.Framework.Libs.Announcer;

namespace OneDriver.ClimateChamber.Basic.Products
{
    public class ClimateChamberDataHAL : BaseDataForAnnouncement
    {
        public ClimateChamberDataHAL(double currentTemperature, bool isTemperatureReached)
        {
            CurrentTemperature = currentTemperature;
            TimeStamp = DateTime.Now;
            IsTemperatureReached = isTemperatureReached;
        }

        public ClimateChamberDataHAL()
        {
            TimeStamp = DateTime.Now;
        }

        public double CurrentTemperature { get; }

        public bool IsTemperatureReached { get; }

    }
}
