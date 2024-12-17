using System.Text.RegularExpressions;

namespace Framework.Libs.Validator
{
    public interface IValidator
    {
        Regex ValidationRegex { get; }
        bool Validate(string inputString);
        string GetExample();
    }
}
