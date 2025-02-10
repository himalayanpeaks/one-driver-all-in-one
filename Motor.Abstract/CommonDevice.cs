using OneDriver.Framework.Base;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using Serilog;
using System.ComponentModel;
using OneDriver.Device.Interface.Motor;
using Definition = OneDriver.Device.Interface.Motor.Definition;

namespace OneDriver.Motor.Abstract
{
    public abstract class CommonDevice<TDeviceParams, TChannelProcessData> :
        BaseDeviceWithProcessData<TDeviceParams, TChannelProcessData>, IMotor
        where TDeviceParams : CommonDeviceParams
        where TChannelProcessData : CommonProcessData
    {
        protected CommonDevice(TDeviceParams parameters, IValidator validator,
            TChannelProcessData processData) : base(parameters, validator, processData)
        {
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            Parameters.PropertyChanging += Parameters_PropertyChanging;
            Parameters.PropertyReadRequested += Parameters_PropertyReadRequested;
        }

        private void Parameters_PropertyReadRequested(object sender, PropertyReadRequestedEventArgs e)
        {
            
        }

        /// <summary>
        /// Write here the validation of a param before its new value of a param is accepted 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Parameters_PropertyChanging(object sender, PropertyValidationEventArgs e)
        {
            switch (e.PropertyName)
            {

            }
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Parameters.DesiredSpeed):
                    if ((double)(Parameters.DesiredSpeed) > Parameters.MaximumSpeed &&
                        (Parameters.TravelMode == Definition.TravelMode.Absolute
                         || Parameters.TravelMode == Definition.TravelMode.Relative))
                    {
                        Parameters.DesiredSpeed = Parameters.MaximumSpeed;
                        Log.Information("Desired speed reduced to maximum possible " + Parameters.MaximumSpeed);
                    }
                    if ((double)Parameters.DesiredSpeed > Parameters.MaximumReferenceSpeed &&
                        (Parameters.TravelMode == Definition.TravelMode.ExternalReference
                         || Parameters.TravelMode == Definition.TravelMode.InternalReference))
                    {
                        Parameters.DesiredSpeed = Parameters.MaximumReferenceSpeed;
                        Log.Information("Desired speed reduced to maximum possible " + Parameters.MaximumReferenceSpeed);
                    }
                    break;
                case nameof(Parameters.DesiredPosition):
                    if ((double)Parameters.DesiredPosition > Parameters.MaximumPosition)
                    {
                        Parameters.DesiredPosition = Parameters.MaximumPosition;
                        Log.Information("Desired position reduced to maximum possible " +
                                        Parameters.MaximumPosition);
                    }
                    if ((double)Parameters.DesiredPosition < Parameters.MinimumPosition)
                    {
                        Parameters.DesiredPosition = Parameters.MinimumPosition;
                        Log.Information("Desired position reduced to minimum possible " +
                                        Parameters.MinimumPosition);
                    }
                    break;
            }
        }

        public int Run(bool waitTillTravelEnds)
        {
            if (Parameters.TravelMode == Definition.TravelMode.Absolute && !Parameters.IsReferenced)
            {
                Log.Error("Motor not referenced");
                return (int)Definition.Errors.MotorNotReferenced;
            }

            StartTravelMode(Parameters.TravelMode);
            Log.Information($"Starting run {Parameters.TravelMode} {Parameters.DesiredPosition} {Parameters.Unit} @ {Parameters.DesiredSpeed} {Parameters.Unit}/sec");
            Task waitTillComplete = Task.Run(() =>
            {
                WaitTillPositionReached();
                if (RequiresErrorReset(Parameters.TravelMode))
                {
                    ResetError();
                }
            });

            waitTillComplete.ContinueWith(t => Log.Information("Position reached"), TaskScheduler.Default);

            if (waitTillTravelEnds)
            {
                waitTillComplete.Wait();
            }

            return GetLastError();
        }

        // Helper method to start travel based on mode
        private void StartTravelMode(Definition.TravelMode mode)
        {
            switch (mode)
            {
                case Definition.TravelMode.Absolute:
                    StartAbsoluteRun();
                    break;
                case Definition.TravelMode.Relative:
                    StartRelativeRun();
                    break;
                case Definition.TravelMode.ExternalReference:
                    StartReferenceRun();
                    break;
                case Definition.TravelMode.InternalReference:
                    StartInternalReferenceRun();
                    break;
            }
        }

        // Determines if error reset is required for certain travel modes
        private bool RequiresErrorReset(Definition.TravelMode mode)
        {
            return mode == Definition.TravelMode.ExternalReference || mode == Definition.TravelMode.InternalReference;
        }


        protected abstract void WaitTillPositionReached();
        protected abstract void ResetError();
        public abstract int GetLastError();
        public abstract string GetErrorMessage(int errorCode);
        protected abstract void StartReferenceRun();
        protected abstract void StartInternalReferenceRun();
        protected abstract void StartAbsoluteRun();
        protected abstract void StartRelativeRun();

        public abstract void Stop();
    }
}
