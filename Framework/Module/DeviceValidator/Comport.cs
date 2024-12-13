using Framework.Base;
using System;
using System.Text.RegularExpressions;

namespace Framework.Module.DeviceValidator
{
    public class Comport : IValidator
    {
        private static readonly Regex _validationRegex = new Regex(@"^COM\d{1,3}(;[1-9]\d{3,5})?$", RegexOptions.Compiled);
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
            return "COM23;19200 or COM23";
        }
    }
}
