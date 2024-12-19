using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeviceParam.Ui
{
    public class DynamicPropertyWrapper : INotifyPropertyChanged
    {
        public ObservableCollection<KeyValuePair<string, object>> Properties { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly object _source;

        public DynamicPropertyWrapper(object source)
        {
            _source = source;
            Properties = new ObservableCollection<KeyValuePair<string, object>>();

            foreach (var prop in source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = prop.GetValue(source);
                Properties.Add(new KeyValuePair<string, object>(prop.Name, value));
            }
        }

        // Add indexer for direct access
        public object this[string propertyName]
        {
            get
            {
                var prop = _source.GetType().GetProperty(propertyName);
                return prop?.GetValue(_source);
            }
            set
            {
                var prop = _source.GetType().GetProperty(propertyName);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(_source, value);
                    UpdateProperty(propertyName, value);
                }
            }
        }

        public void UpdateProperty(string propertyName, object value)
        {
            for (int i = 0; i < Properties.Count; i++)
            {
                if (Properties[i].Key == propertyName)
                {
                    Properties[i] = new KeyValuePair<string, object>(propertyName, value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Properties)));
                    break;
                }
            }
        }
    }


}
