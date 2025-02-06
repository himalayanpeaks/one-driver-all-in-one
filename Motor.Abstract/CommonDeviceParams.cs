using OneDriver.Device.Interface.Motor;
using OneDriver.Framework.Module.Parameter;

namespace OneDriver.Motor.Abstract
{
    public class CommonDeviceParams : BaseDeviceParam
    {
        public CommonDeviceParams(string name) : base(name)
        {
        }
        private double _desiredPosition;
        private double _desiredSpeed;
        private Definition.DirectionOfRotation _directionOfRotation;
        private bool _isMotorReady;
        private double _maximumReferenceSpeed;
        private double _maximumSpeed;
        private double _originShift;
        private Definition.Status _status;
        private int _stepFactor;
        private Definition.TravelMode _travelMode;
        private Definition.Unit _unit;
        private double _axisLength;
        private double _maximumPosition;
        private double _minimumPosition;

        public Definition.TravelMode TravelMode
        {
            get => _travelMode;
            set => SetProperty(ref _travelMode, value);
        }
        public bool IsReferenced => GetProperty<bool>();
        public Definition.DirectionOfRotation DirectionOfRotation
        {
            get => _directionOfRotation;
            set => SetProperty(ref _directionOfRotation, value);
        }
        public int StepFactor
        {
            get => _stepFactor;
            set => SetProperty(ref _stepFactor, value);
        }
        public double AxisLength
        {
            get => _axisLength;
            set => SetProperty(ref _axisLength, value);
        }
        public double DesiredPosition
        {
            get => _desiredPosition;
            set => SetProperty(ref _desiredPosition, value);
        }
        public double DesiredSpeed
        {
            get => _desiredSpeed;
            set => SetProperty(ref _desiredSpeed, value);
        }
        public bool IsMotorReady //read only, read from dll on read request
        {
            get => _isMotorReady;
            set => SetProperty(ref _isMotorReady, value);
        }
        public double MaximumPosition
        {
            get => _maximumPosition;
            set => SetProperty(ref _maximumPosition, value);
        }
        public double MinimumPosition
        {
            get => _minimumPosition;
            set => SetProperty(ref _minimumPosition, value);
        }
        public double OriginShift
        {
            get => _originShift;
            set => SetProperty(ref _originShift, value);
        }
        public double MaximumSpeed
        {
            get => _maximumSpeed;
            set => SetProperty(ref _maximumSpeed, value);
        }
        public double MaximumReferenceSpeed
        {
            get => _maximumReferenceSpeed;
            set => SetProperty(ref _maximumReferenceSpeed, value);
        }
        public Definition.Status Status
        {
            get => _status;
            protected set => SetProperty(ref _status, value);
        }
        public Definition.Unit Unit
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }
    }
}