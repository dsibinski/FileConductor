using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceProcess;
using System.Windows;
using FileConductor.ConfigurationTool.ViewModels;
using FileConductorUI;

namespace FileConductorUI.UI
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