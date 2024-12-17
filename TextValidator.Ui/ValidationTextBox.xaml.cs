using System.Windows.Controls;
using Framework.Libs.Validator;

namespace TextValidator.Ui
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
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
