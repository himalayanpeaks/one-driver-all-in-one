using Framework.Libs.Validator;
using Framework.Module;
using System.Windows;

namespace Device.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            var validator = ValidatorFactory.CreateValidator("Comport");
            var viewmodel = new ValidatorViewModel(validator);
            

            InitializeComponent();
            ValidationTextBoxControl.validatorViewModel = viewmodel;
            ValidationTextBoxControl.SetDataContext();

            var exampleParams = new ExampleParams("ExampleDevice")
            {
                Count = 10,
                Version = "1.0.0"
            };

            // Create the concrete implementation of the device
            var exampleDevice = new ExampleDevice(exampleParams);

            // Create the ViewModel
            var viewModel = new ExampleClass(exampleDevice);

            // Bind the ViewModel to the DataContext
            this.DataContext = viewModel;

        }
    }
}