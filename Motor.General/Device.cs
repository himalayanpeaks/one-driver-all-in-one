using OneDriver.Framework.Libs.Validator;
using OneDriver.Motor.Abstract;
using OneDriver.Motor.General.Products;
using Serilog;
using System.ComponentModel;
using OneDriver.Framework.Base;
using Definition = OneDriver.Device.Interface.Motor.Definition;

namespace OneDriver.Motor.General
{
    public class Device : CommonDevice<DeviceParams, ProcessData>
    {
        IMotorHAL _deviceHAL { get; set; }
        public Device(string name, IValidator validator, IMotorHAL deviceHAL) :
            base(new DeviceParams(name), validator, new ProcessData())
        {
            _deviceHAL = deviceHAL;
            _deviceHAL.AttachToProcessDataEvent(ProcessDataChanged);
            Init();

            #region TestToDelete
            Parameters.MaximumSpeed = 20;
            Parameters.MaximumReferenceSpeed = 5;
            Parameters.StepFactor = 5;
            Parameters.AxisLength = 1000;
            Parameters.DesiredPosition = 10;
            Parameters.TravelMode = Definition.TravelMode.ExternalReference;
            Parameters.DesiredSpeed = 40;
            Parameters.DirectionOfRotation = Definition.DirectionOfRotation.Right;
            Parameters.Name = "Motor";
            Parameters.Unit = Definition.Unit.Millimeters;
            #endregion
        }

        private void Init()
        {
            Parameters.PropertyChanging += Parameters_PropertyChanging;
            Parameters.PropertyChanged += Parameters_PropertyChanged;
            Parameters.PropertyReadRequested += Parameters_PropertyReadRequested;
        }

        private void Parameters_PropertyReadRequested(object sender, PropertyReadRequestedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Parameters.IsReferenced):
                    e.Value = _deviceHAL.IsReferenceTravelDone;
                    break;
            }
        }

        private void ProcessDataChanged(object sender, InternalDataHAL e)
        {

           ProcessData.Position = e.Position;
        }

        private void Parameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {

            }
        }

        protected override void WaitTillPositionReached()
        {
            while (_deviceHAL.IsMotorReady == false) 
            {
            }
        }

        protected override void ResetError()
        {
            _deviceHAL.ResetError();
        }

        public override void Stop()
        {
            _deviceHAL.StopImmediately();
            Log.Information("Motor stopped");
        }

        public override int GetLastError() => _deviceHAL.GetLastError();


        public override string GetErrorMessage(int errorCode) => _deviceHAL.GetErrorMessage(errorCode);

        protected override void StartReferenceRun() =>
            _deviceHAL.Run(Definition.TravelMode.ExternalReference, Parameters.DirectionOfRotation, speed: Parameters.DesiredSpeed);
        

        protected override void StartInternalReferenceRun() => 
            _deviceHAL.Run(Definition.TravelMode.InternalReference, Parameters.DirectionOfRotation, speed: Parameters.DesiredSpeed);

        protected override void StartAbsoluteRun() => 
            _deviceHAL.Run(Definition.TravelMode.Absolute, position: Parameters.DesiredPosition, speed: Parameters.DesiredSpeed);

        protected override void StartRelativeRun() => 
            _deviceHAL.Run(Definition.TravelMode.Relative, Parameters.DirectionOfRotation, Parameters.DesiredPosition, Parameters.DesiredSpeed);
        private void Parameters_PropertyChanging(object sender, Framework.Base.PropertyValidationEventArgs e)
        {
            //Write validity before property is changed here
            switch (e.PropertyName)
            {

            }
        }

        protected override int CloseConnection() => (int)_deviceHAL.Close();
        protected override int OpenConnection(string initString) => (int)_deviceHAL.Open(initString, validator);

    }
}
