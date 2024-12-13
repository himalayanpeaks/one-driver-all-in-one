using System.Windows.Input;
using Framework.Module;
using Framework.Libs;
using Framework.Module.Parameter;

public abstract class DeviceViewModel<TParams> : IDeviceViewModel where TParams : MinimumDeviceParamBase
{
    public readonly BaseDevice<TParams> Device;

    public string InitString { get; set; } = string.Empty;
    public ICommand ConnectCommand { get; }
    public ICommand DisconnectCommand { get; }

    public DeviceViewModel(BaseDevice<TParams> device)
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
