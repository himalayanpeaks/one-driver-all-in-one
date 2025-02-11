using System.Text.RegularExpressions;
using OneDriver.Framework.Libs.Validator;

namespace OneDriver.Motor.General.Products;

public class NanotecValidator : IValidator
{
    private static readonly Regex _validationRegex = new Regex(@"(?<com>COM\d+)(;{1}\s*)(?<Address>\d+)(;{1}\s*)(?<StepFactor>\d+)", RegexOptions.Compiled);
    public Regex ValidationRegex => _validationRegex;

    public bool Validate(string inputString)
    {
        if (string.IsNullOrEmpty(inputString))
        {
            return false;
        }

        return _validationRegex.IsMatch(inputString);
    }
    public string GetExample()
    {
        return "COM23;2;10";
    }
}