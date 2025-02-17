using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OneDriver.Daq.General.Products
{
    public class NiUsbValidator
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
