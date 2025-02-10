using OneDriver.Framework.Libs.Announcer;
using System.Text.RegularExpressions;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using CommandsPD4I;
using Serilog;

namespace OneDriver.Motor.General.Products
{
    public class Nanotec : DataTunnel<InternalDataHAL>, IMotorHAL
    {
        private int _stepfactor = 1;
        private int _stepDirection = -1;
        private IComMotorCommands MotorCommands { get; }

        Regex initRegex = new Regex(@"(?<com>COM\d+)(;{1}\s*)(?<Address>\d+)(;{1}\s*)(?<StepFactor>\d+)");

        public Nanotec(IComMotorCommands motorCommands)
        {
            MotorCommands = motorCommands;
        }

        protected override void FetchDataForTunnel(out InternalDataHAL data)
        {
            data = new InternalDataHAL((double)MotorCommands.GetMaxFrequency(1) / _stepfactor,
                (double)_stepDirection * MotorCommands.GetPosition() / _stepfactor);
        }

        public ConnectionError Open(string initString, IValidator validator)
        {
            MotorCommands.SelectedPort = validator.ValidationRegex.Match(initString).Result("${com}");
            if (MotorCommands.ErrorFlag == true)
                throw new Exception(MotorCommands.ErrorNumber + MotorCommands.ErrorMessageString);
            MotorCommands.MotorAddresse = Convert.ToInt32(validator.ValidationRegex.Match(initString).Result("${Address}"));
            if (MotorCommands.ErrorFlag == true)
                throw new Exception(MotorCommands.ErrorNumber + MotorCommands.ErrorMessageString);
            _stepfactor = Convert.ToInt32(validator.ValidationRegex.Match(initString).Result("${StepFactor}"));
            try
            {
                if (MotorCommands.GetDirection(1) == (int)OneDriver.Device.Interface.Motor.Definition.DirectionOfRotation.Right) _stepDirection = -1;
                else _stepDirection = 1;
                MotorCommands.SetRecord(1);
                if (MotorCommands.GetStepMode() != 254)
                    MotorCommands.SetStepMode(254);
                StartAnnouncingData();
            }
            catch (Exception)
            {
                throw new Exception(MotorCommands.ErrorNumber + "  " + MotorCommands.ErrorMessageString);
            }
            return ConnectionError.NoError;
        }
        
        

        public ConnectionError Close()
        {
            StopAnnouncingData();
            OneDriver.Toolbox.Tools.Wait(100);
            return ConnectionError.NoError;
        }

        public void StartProcessDataAnnouncer() => StartAnnouncingData();

        public void StopProcessDataAnnouncer() => StopAnnouncingData();

        public void AttachToProcessDataEvent(DataEventHandler processDataEventHandler) => DataEvent += processDataEventHandler;
        public int NumberOfChannels { get; } = 0;

        public bool IsMotorReady => MotorCommands.IsMotorReady();
        public bool IsReferenceTravelDone => Convert.ToBoolean(MotorCommands.GetSoftwareFilter());
        
        public void Run(OneDriver.Device.Interface.Motor.Definition.TravelMode mode, 
            OneDriver.Device.Interface.Motor.Definition.DirectionOfRotation direction = OneDriver.Device.Interface.Motor.Definition.DirectionOfRotation.Right, double position = 0,
            double speed = 0)
        {
            MotorCommands.SetPositionType((int)mode);
            MotorCommands.SetMaxFrequency((int)(speed * _stepfactor));
            switch (mode)
            {
                case OneDriver.Device.Interface.Motor.Definition.TravelMode.ExternalReference:
                    MotorCommands.SetDirection((int)direction);
                    if (direction == OneDriver.Device.Interface.Motor.Definition.DirectionOfRotation.Right) _stepDirection = -1;
                    else _stepDirection = 1;
                    ResetError();
                    break;
                case OneDriver.Device.Interface.Motor.Definition.TravelMode.InternalReference:
                    MotorCommands.SetDirection((int)direction);
                    ResetError();
                    break;
                case OneDriver.Device.Interface.Motor.Definition.TravelMode.Absolute:
                    if (MotorCommands.ErrorFlag == true)
                    {
                        Log.Information(MotorCommands.ErrorNumber + "_" + MotorCommands.ErrorMessageString);
                        return;
                    }
                    MotorCommands.SetSteps((int)Math.Round(_stepDirection * position * _stepfactor));
                    break;
                case OneDriver.Device.Interface.Motor.Definition.TravelMode.Relative:
                    if (MotorCommands.ErrorFlag == true)
                    {
                        Log.Information(MotorCommands.ErrorNumber + "_" + MotorCommands.ErrorMessageString);
                        return;
                    }
                    MotorCommands.SetDirection((int)direction);
                    MotorCommands.SetSteps((int)Math.Round(position * _stepfactor));
                    break;
            }
            MotorCommands.StartTravelProfile();
        }

        public void StopImmediately() => MotorCommands.StopTravelProfile();

        public int GetLastError() => MotorCommands.ErrorNumber;

        public string GetErrorMessage(int errorCode) => MotorCommands.ErrorMessageString;

        public bool ResetError()
        {
            if (MotorCommands.HasPositionError())
                return MotorCommands.ResetPositionError(true, 0);
            return false;
        }
    }
}
