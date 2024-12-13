using Framework.Base;
using Framework.Libs;
using Framework.Module.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Framework.Module
{
    public abstract class BaseDeviceWtihProcessDataViewModel<TParams, TProcessData> : IDeviceViewModel
                where TParams : MinimumDeviceParamBase
        where TProcessData : IParameter
    {
        public readonly BaseDeviceWithProcessData<TParams, TProcessData> Device;
        public string InitString { get; set; } = string.Empty;
        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }

        protected BaseDeviceWtihProcessDataViewModel(BaseDeviceWithProcessData<TParams, TProcessData> device)
        {
            Device = device;
            ConnectCommand = new RelayCommand(
    execute: _ => Connect(),
    canExecute: _ => CanConnect()
);

            DisconnectCommand = new RelayCommand(
                execute: _ => Disconnect(),
                canExecute: _ => CanDisconnect()
            );
        }
        private void Connect()
        {
            var result = Device.Connect(InitString);
            if (result != Definition.DeviceError.NoError)
            {
                // Handle connection error (e.g., log or notify the user)
            }
        }

        private void Disconnect()
        {
            var result = Device.Disconnect();
            if (result != Definition.DeviceError.NoError)
            {
                // Handle disconnection error (e.g., log or notify the user)
            }
        }

        private bool CanConnect()
        {
            // Add logic to determine if the device can connect (e.g., not already connected)
            return true;
        }

        private bool CanDisconnect()
        {
            // Add logic to determine if the device can disconnect (e.g., already connected)
            return true;
        }

    }
}
