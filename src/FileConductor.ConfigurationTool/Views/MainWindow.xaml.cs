using System.Windows;
using ConfigurationTool.ViewModels;

namespace ConfigurationTool.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainPageViewModel _viewModel = new MainPageViewModel();

        public MainWindow()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }

       
    }
}