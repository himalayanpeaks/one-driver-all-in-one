using Framework.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework.Module.DeviceValidator
{
    public class Comport : IValidator
    {
        public Regex ValidationRegex => throw new NotImplementedException();

        public bool Validate(string inputString)
        {
            throw new NotImplementedException();
        }
    }
}
