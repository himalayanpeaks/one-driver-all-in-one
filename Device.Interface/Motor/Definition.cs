using System.ComponentModel;

namespace OneDriver.Device.Interface.Motor
{
    public class Definition
    {
        /// <summary>
        ///     Travel direction
        /// </summary>
        public enum DirectionOfRotation
        {
            Left,
            Right
        }


        public enum Errors
        {
            [Description("No motor found. Check port or physical address")]
            MotorNotFound,

            [Description("Exceeds maximum speed allowed by the drive")]
            AboveMaxSpeed,

            [Description("Below minimum speed allowed by the drive")]
            BelowMinSpeed,

            [Description(
                "Motor not ready. Last travel profile may still be active or error due to proximity switch position reached")]
            MotorNotReady,

            [Description("Timeout occured while communication")]
            Timeout,

            [Description("Motor not referenced")]
            MotorNotReferenced
        }


        /// <summary>
        ///     Motor status
        /// </summary>
        public enum Status
        {
            Ready,
            Running,
            Error
        }

        /// <summary>
        ///     Only FEED_CONSTANT is active
        /// </summary>
        public enum StepMode
        {
            FullStep,
            HalfStep,
            QuarterStep,
            FifthStep,
            EighthStep,
            TenthStep,
            SixteenthStep,
            ThirtySecondStep,
            SixtyForthStep,
            FeedConstant,
            AdaptiveStep
        }

        /// <summary>
        ///     Position modes
        /// </summary>
        public enum TravelMode
        {
            Relative = 1,
            Absolute = 2,
            InternalReference = 3,
            ExternalReference = 4
        }

        public enum Unit
        {
            [Description("Linear movement. In feed constant mode, one step means a moavement of 1 mm")]
            Millimeters,

            [Description("Radial movement. In feed constant mode, one step means a movement of 1°")]
            Degrees
        }
    }
}
