using Device.Interface;
using Framework.Libs;
using Framework.Module.ViewModel;
using PowerSupply.Abstract.Channels;
using System.Windows.Input;

namespace PowerSupply.Abstract
{
    public class CommonDeviceViewModel<TParams, TChannel, TChannelParams, TChannelProcessData> :
        BaseDeviceWithChannelsPdViewModel<TParams, TChannel, TChannelParams, TChannelProcessData>, IPowerSupplyViewModel
        where TParams : CommonDeviceParams
        where TChannel : CommonChannel<TChannelParams, TChannelProcessData>
        where TChannelParams : CommonChannelParams
        where TChannelProcessData : CommonProcessData
    {
        public CommonDevice<TParams, TChannel, TChannelParams, TChannelProcessData> PowerSupply;
        public CommonDeviceViewModel(CommonDevice<TParams, TChannel, TChannelParams, TChannelProcessData> device) : base(device)
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
