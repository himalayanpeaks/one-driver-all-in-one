using Framework.Libs.Validator;
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

        }
    }
}