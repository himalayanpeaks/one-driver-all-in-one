using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OneDriver.Framework.Base;

namespace OneDriver.Toolbox.Email
{
    public class EmailParams : IParameter
    {
        public string Sender { get; set; }
        public List<string> RecipientsList { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyValidationEventHandler PropertyChanging;
    }
}