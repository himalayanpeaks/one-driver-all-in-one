using Framework.Base;

namespace Framework.Module.Parameter
{
    public abstract class MinimumChannelParamBase : PropertyHandlers, IParameter
    {
        private string name;

        public MinimumChannelParamBase(string name)
        {
            this.name = name;
        }


        public string Name { get => GetProperty(ref name); set => SetProperty(ref name, value); }
    }
}
