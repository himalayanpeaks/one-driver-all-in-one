using OneDriver.Framework.Libs.Announcer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.Motor.General.Products
{
    public class InternalDataHAL : BaseDataForAnnouncement
    {
        public InternalDataHAL(double position, double distance)
        {
            Position = position;
            TimeStamp = DateTime.Now;
            Distance = distance;
        }

        public InternalDataHAL()
        {
            TimeStamp = DateTime.Now;
        }

        public double Position { get; }
        public double Distance { get; }
    }
}
