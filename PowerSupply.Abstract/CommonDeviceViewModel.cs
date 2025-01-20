using Device.Interface.PowerSupply;
using OneDriver.Framework.Libs;
using OneDriver.Framework.Module.ViewModel;
using OneDriver.PowerSupply.Abstract.Channels;
using System.Windows.Input;

namespace OneDriver.PowerSupply.Abstract
{
    public class CommonDeviceViewModel<TParams, TChannelParams, TChannelProcessData> :
        BaseDeviceWithChannelsPdViewModel<TParams, TChannelParams, TChannelProcessData>, IPowerSupplyViewModel
        where TParams : CommonDeviceParams
        where TChannelParams : CommonChannelParams
        where TChannelProcessData : CommonProcessData
    {
        public CommonDevice<TParams, TChannelParams, TChannelProcessData> PowerSupply;
        public CommonDeviceViewModel(CommonDevice<TParams, TChannelParams, TChannelProcessData> device) : base(device)
        {
            PowerSupply = device;
            CommandAllChannelsOn = new RelayCommand(
    execute: _ => AllChannelsOn(),
    canExecute: _ => CanAllChannelsOn()
);

            CommandAllChannelsOff = new RelayCommand(
                execute: _ => AllChannelsOff(),
                canExecute: _ => CanAllChannelsOff()
            );
        }

        private bool CanAllChannelsOff()
        {
            throw new NotImplementedException();
        }

        private void AllChannelsOff()
        {
            throw new NotImplementedException();
        }

        private bool CanAllChannelsOn()
        {
            throw new NotImplementedException();
        }

        private void AllChannelsOn()
        {
            throw new NotImplementedException();
        }

        public ICommand CommandAllChannelsOn { get; }

        public ICommand CommandAllChannelsOff { get; }
    }
}
