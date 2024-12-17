using Framework.Base;
using System.ComponentModel;

namespace Framework.Libs.Validator
{
    public class ValidatorViewModel : PropertyHandlers, IParameter
    {
        private string _userInput;
        private bool _isValid;
        private IValidator _validator;


        public ValidatorViewModel(IValidator validator)
        {
            Validator = validator;  
            IsValid = false;
            UserInput = Validator.GetExample();
            this.PropertyChanged += ValidatorViewModel_PropertyChanged;
            this.PropertyChanging += ValidatorViewModel_PropertyChanging;   
            
        }

        private void ValidatorViewModel_PropertyChanging(object? sender, PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(UserInput):
                    if (string.IsNullOrEmpty((string)sender) || !Validator.Validate((string)sender))
                        throw new ArgumentException("Invalid input");
                    break;
            }
        }

        private void ValidatorViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(Validator):
                    OnPropertyChanged(nameof(ExampleText));
                    break;             
            }
        }

        

        // The validator instance
        public IValidator Validator
        {
            get => GetProperty(ref _validator);
            set
            {
                _validator = value;
                SetProperty(ref _validator, value); 
                OnPropertyChanged(nameof(ExampleText));
            }
        }

        // User input bound to the TextBox
        public string UserInput
        {
            get => GetProperty(ref _userInput);
            set
            {
                SetProperty(ref _userInput, value);
                ValidateInput();
            }
        }

        // Validation result
        public bool IsValid
        {
            get => GetProperty(ref _isValid);
            private set => SetProperty(ref _isValid, value);
        }

        // Example text provided by the validator
        public string ExampleText => Validator?.GetExample() ?? string.Empty;



        // Validate the current input
        private void ValidateInput()
        {
            if (Validator == null || string.IsNullOrEmpty(UserInput))
            {
                IsValid = false;
            }
            else
            {
                IsValid = Validator.Validate(UserInput);
            }
        }

    }
}
