using Framework.Base;

namespace Framework.ModuleBuilder
{
    public interface IConfigurable<T> where T : IParameter
    {
        public T Parameters { get; set; }
    }
}
