using Framework.Libs;
using System.Windows.Input;

namespace Framework.Module.ViewModel
{
    public abstract class BaseDeviceViewModel : IDeviceViewModel
    {
        public string InitString { get; set; } = string.Empty;
        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }

        protected BaseDeviceViewModel()
        {
            ConnectCommand = new RelayCommand(
                execute: _ => Connect(),
                canExecute: _ => CanConnect()
            );

            DisconnectCommand = new RelayCommand(
                execute: _ => Disconnect(),
                canExecute: _ => CanDisconnect()
            );
        }

        protected abstract void Connect();
        protected abstract void Disconnect();
        protected virtual bool CanConnect() => true;
        protected virtual bool CanDisconnect() => true;
    }
}
