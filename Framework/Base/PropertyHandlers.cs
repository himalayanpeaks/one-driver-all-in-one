using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Framework.Base
{
    public class PropertyHandlers
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanging([CallerMemberName] string? propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            try
            {
                OnPropertyChanging(propertyName);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Validation failed for property '{propertyName}': {ex.Message}");
                return false; // Prevent field update
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected T GetProperty<T>(ref T field)
        {
            return field;
        }
    }
}
