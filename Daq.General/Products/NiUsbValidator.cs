using System.Text.RegularExpressions;
using OneDriver.Framework.Libs.Validator;

namespace OneDriver.Daq.General.Products
{
    public class NiUsbValidator : IValidator
    {
        private static readonly Regex _validationRegex = new Regex(@"(?<devicename>\w+)", RegexOptions.Compiled);
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
            return "Dev5";
        }
    }
}
