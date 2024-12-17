using Framework.Libs.Validator;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace GraphicalInterface
{
    public partial class ValidatorTextBox : UserControl
    {
        public ValidatorTextBox()
        {
            InitializeComponent();
        }

        public ValidatorViewModel ViewModel
        {
            get => (ValidatorViewModel)DataContext;
            set => DataContext = value;
        }
    }
}
