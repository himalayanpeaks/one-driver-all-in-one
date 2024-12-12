using Framework.Base;

namespace Framework.Module.Parameter
{
    public abstract class MinimumDeviceParam : PropertyHandlers, IParameter
    {
        private string name;

        public MinimumDeviceParam(string name)
        {
            this.name = name;
        }

        private bool isConnected;

        public string Name { get => GetProperty(ref name); set => SetProperty(ref name, value); }
        public bool IsConnected { get => GetProperty(ref isConnected); set => SetProperty(ref isConnected, value); }
    }
}
