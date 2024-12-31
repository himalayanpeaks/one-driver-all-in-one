using Framework.Base;

namespace Framework.Module.Parameter
{
    public class BaseChannelParam : PropertyHandlers, IParameter
    {
        private string name;

        public BaseChannelParam(string name)
        {
            this.name = name;
        }


        public string Name { get => GetProperty(ref name); set => SetProperty(ref name, value); }
    }
}
