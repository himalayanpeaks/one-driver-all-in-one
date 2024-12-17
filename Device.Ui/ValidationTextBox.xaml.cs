using Framework.Libs.Validator;
using System.Windows.Controls;

namespace Device.Ui
{
    /// <summary>
    /// Interaction logic for ValidationTextBox.xaml
    /// </summary>
    public partial class ValidationTextBox : UserControl
    {
        public required ValidatorViewModel validatorViewModel { get; set; }

        public ValidationTextBox()
        {
            InitializeComponent();
 
        }
        public void SetDataContext()
        {   
            TextBlockErrorMessage.DataContext = validatorViewModel;
            TextBlockExampleText.DataContext = validatorViewModel;
            TextBoxUserInput.DataContext = validatorViewModel;  
        }   
    }
}
