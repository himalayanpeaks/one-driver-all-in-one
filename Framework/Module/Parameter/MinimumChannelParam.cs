using Framework.Base;
using Framework.ModuleBuilder;

namespace Framework.Module.Parameter
{
    public abstract class MinimumChannelParam : PropertyHandlers, IParameter
    {
        private string name;

        public MinimumChannelParam(string name)
        {
            this.name = name;
        }


        public string Name { get => GetProperty(ref name); set => SetProperty(ref name, value); }
    }
}
