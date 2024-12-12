using Framework.Base;

namespace Framework.ModuleBuilder
{
    public interface IProcessData<T> where T : IParameter
    {
        public T ProcessData { get; set; }
    }
}
