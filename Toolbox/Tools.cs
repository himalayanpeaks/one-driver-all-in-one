using System;

namespace OneDriver.Toolbox
{
    public static class Tools
    {
        public static void Wait(uint aWaitTimeInMs)
        {

            uint elapsedTimeInMs = 0;
            DateTime initialTime = DateTime.Now;
            for (ushort i = 0; ; i++)
            {
                elapsedTimeInMs = (uint)(DateTime.Now - initialTime).TotalMilliseconds;
                if (elapsedTimeInMs > aWaitTimeInMs)
                    break;
                if (i >= ushort.MaxValue)
                    i = 0;
            }
        }
    }
}
