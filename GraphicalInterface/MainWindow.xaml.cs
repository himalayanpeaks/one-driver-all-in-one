using Framework.Libs.Validator;
using Framework.Module.DeviceValidator;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicalInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
             
            if (DataContext is ValidatorViewModel viewModel)
            {
                viewModel.Validator = new ComportValidator(); // Assign ComportValidator
                viewModel.UserInput = "COM1"; // Initialize with empty input
            }

        }
    }
}