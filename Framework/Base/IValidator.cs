using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework.Base
{
    public interface IValidator
    {
        Regex ValidationRegex { get; }
        bool Validate(string inputString);
        string GetExample();
    }
}
